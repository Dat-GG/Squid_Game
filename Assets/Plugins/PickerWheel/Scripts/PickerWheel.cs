using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace EasyUI.PickerWheelUI
{
	public class PickerWheel : MonoBehaviour
	{
		[Header ("References :")]
		[SerializeField] private GameObject linePrefab;
		[SerializeField] private Transform linesParent;

		[Space]
		[SerializeField] private Transform pickerWheelTransform;
		[SerializeField] private Transform wheelCircle;
		[SerializeField] private GameObject wheelPiecePrefab;
		[SerializeField] private Transform wheelPiecesParent;

		[Space]
		[Header ("Sounds :")]
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip tickAudioClip;
		[SerializeField] [Range (0f, 1f)] private float volume = 0.5f;
		[SerializeField] [Range (-3f, 3f)] private float pitch = 1f;

		[Space]
		[Header ("Picker wheel settings :")]
		[Range (1, 20)] public int spinDuration = 8;
		[SerializeField] [Range (0.2f, 2f)] private float wheelSize = 1f;

		[Space]
		[Header ("Picker wheel pieces :")]
		public WheelPiece[] wheelPieces;

		// Events
		private UnityAction onSpinStartEvent;
		private UnityAction<WheelPiece> onSpinEndEvent;
		
		private bool _isSpinning = false;

		internal bool IsSpinning => _isSpinning;

		private readonly Vector2 pieceMinSize = new Vector2 (81f, 146f);
		private readonly Vector2 pieceMaxSize = new Vector2 (144f, 213f);
		private const int PiecesMin = 2;
		private const int PiecesMax = 12;

		private float pieceAngle;
		private float halfPieceAngle;
		private float halfPieceAngleWithPaddings;
		
		private double accumulatedWeight;
		private readonly System.Random rand = new System.Random();

		private List<int> nonZeroChancesIndices = new List<int>();

		private void Start()
		{
			pieceAngle = 360 / wheelPieces.Length;
			halfPieceAngle = pieceAngle * 0.5f;
			halfPieceAngleWithPaddings = halfPieceAngle - halfPieceAngle * 0.25f;

			Generate();

			CalculateWeightsAndIndices();
			if (nonZeroChancesIndices.Count == 0)
				Debug.LogError ("You can't set all pieces chance to zero");
			
			SetupAudio();
		}

		private void SetupAudio()
		{
			audioSource.clip = tickAudioClip;
			audioSource.volume = volume;
			audioSource.pitch = pitch;
		}

		private void Generate()
		{
			wheelPiecePrefab = InstantiatePiece();

			var rt = wheelPiecePrefab.transform.GetChild(0).GetComponent<RectTransform>();
			var pieceWidth = Mathf.Lerp(pieceMinSize.x, pieceMaxSize.x, 
				1f - Mathf.InverseLerp(PiecesMin, PiecesMax, wheelPieces.Length));
			var pieceHeight = Mathf.Lerp(pieceMinSize.y, pieceMaxSize.y,
				1f - Mathf.InverseLerp(PiecesMin, PiecesMax, wheelPieces.Length));
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pieceWidth);
			rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pieceHeight);

			for (var i = 0; i < wheelPieces.Length; i++)
				DrawPiece(i);

			Destroy(wheelPiecePrefab);
		}

		private void DrawPiece(int index)
		{
			var piece = wheelPieces[index];
			var pieceTrns = InstantiatePiece().transform.GetChild(0);

			pieceTrns.GetChild(0).GetComponent<Image>().sprite = piece.Icon;
			pieceTrns.GetChild(1).GetComponent<Text>().text = piece.Label;
			pieceTrns.GetChild(2).GetComponent<Text>().text = piece.Amount.ToString();

			//Line
			var lineTrns = Instantiate(linePrefab, linesParent.position, Quaternion.identity, linesParent).transform;
			lineTrns.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index + halfPieceAngle);

			pieceTrns.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index);
		}

		private GameObject InstantiatePiece()
		{
			return Instantiate(wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent);
		}
		
		public void Spin()
		{
			if (_isSpinning) return;
			_isSpinning = true;
			onSpinStartEvent?.Invoke();

			var index = GetRandomPieceIndex();
			var piece = wheelPieces[index];

			if (piece.Chance == 0 && nonZeroChancesIndices.Count != 0)
			{
				index = nonZeroChancesIndices[Random.Range(0, nonZeroChancesIndices.Count)];
				piece = wheelPieces[index];
			}

			var angle = -(pieceAngle * index);

			var rightOffset = (angle - halfPieceAngleWithPaddings) % 360;
			var leftOffset = (angle + halfPieceAngleWithPaddings) % 360;

			var randomAngle = Random.Range(leftOffset, rightOffset);

			var targetRotation = Vector3.back * (randomAngle + 2 * 360 * spinDuration);

			//float prevAngle = wheelCircle.eulerAngles.z + halfPieceAngle;
			float prevAngle, currentAngle;
			prevAngle = currentAngle = wheelCircle.eulerAngles.z;

			var isIndicatorOnTheLine = false;

			wheelCircle
				.DORotate(targetRotation, spinDuration, RotateMode.Fast)
				.SetEase(Ease.InOutQuart)
				.OnUpdate(() => {
					var diff = Mathf.Abs (prevAngle - currentAngle);
					if (diff >= halfPieceAngle) {
						if (isIndicatorOnTheLine) {
							audioSource.PlayOneShot (audioSource.clip);
						}
						prevAngle = currentAngle;
						isIndicatorOnTheLine = !isIndicatorOnTheLine;
					}
					currentAngle = wheelCircle.eulerAngles.z;
				})
				.OnComplete(() => {
					_isSpinning = false;
					onSpinEndEvent?.Invoke(piece);

					onSpinStartEvent = null;
					onSpinEndEvent = null;
				});
		}

		public void OnSpinStart (UnityAction action)
		{
			onSpinStartEvent = action;
		}

		public void OnSpinEnd (UnityAction<WheelPiece> action)
		{
			onSpinEndEvent = action;
		}
		
		private int GetRandomPieceIndex()
		{
			var r = rand.NextDouble() * accumulatedWeight;

			for (var i = 0; i < wheelPieces.Length; i++)
				if (wheelPieces[i]._weight >= r)
					return i;

			return 0;
		}

		private void CalculateWeightsAndIndices()
		{
			for (var i = 0; i < wheelPieces.Length; i++)
			{
				var piece = wheelPieces[i];

				//add weights:
				accumulatedWeight += piece.Chance;
				piece._weight = accumulatedWeight;

				//add index :
				piece.Index = i;

				//save non zero chance indices:
				if (piece.Chance > 0)
					nonZeroChancesIndices.Add(i);
			}
		}
		
		private void OnValidate()
		{
			if (pickerWheelTransform != null)
				pickerWheelTransform.localScale = new Vector3 (wheelSize, wheelSize, 1f);

			if (wheelPieces.Length > PiecesMax || wheelPieces.Length < PiecesMin)
				Debug.LogError("[ PickerWheelwheel ]  pieces length must be between " + PiecesMin + " and " + PiecesMax);
		}
	}
}