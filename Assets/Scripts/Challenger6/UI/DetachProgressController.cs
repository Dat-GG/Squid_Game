using Challenger6.Gameplay;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger6.UI
{
	public class DetachProgressController : MonoBehaviour
	{
		[SerializeField] private RectTransform green, indicator, leftPos, rightPos;
		[SerializeField] private float resumeProgress;
		private float _greenWidth, _resumeProgress, _durationProgress;
		private bool _restartProgress = false;
		private Tweener _transitionTween;
		
		private CandyController _candy;
		private NeedleController _needle;

		private void Awake()
		{
			_candy = FindObjectOfType<CandyController>();
			_needle = FindObjectOfType<NeedleController>();
		}

		private void OnEnable()
		{
			InputCheck.MouseDown += OnMouseClick;
		}
		
		private void Start()
		{
			_durationProgress = 2f;
			_greenWidth = green.rect.width;
			_resumeProgress = resumeProgress;
			_needle.gameObject.SetActive(false);
			MoveGreenField();
			_transitionTween = MoveIndicator(_durationProgress);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				OnMouseClick();
				// green.DOShakeScale(0.1f, 0.5f);
				green.DOScaleX(green.localScale.x * 2f, 0.08f)
					.OnComplete(delegate
					{
						green.DOScaleX(green.localScale.x * 0.5f, 0.08f);
					});

				var greenLeftBorder = green.localPosition.x - _greenWidth * 0.5f;
				var greenRightBorder = green.localPosition.x + _greenWidth * 0.5f;
				if (indicator.localPosition.x > greenLeftBorder && indicator.localPosition.x < greenRightBorder)
					_candy.BreakPiece();
				else
				{
					GameManagerChallenger6.Instance.LoseScratch = true;
					gameObject.SetActive(false);					
				}
				
				_restartProgress = true;
			}
			else
			{
				if (!_restartProgress) return;
				resumeProgress -= Time.deltaTime;
				if (resumeProgress > 0) return;
				indicator.DOLocalMoveX(leftPos.localPosition.x, 0f);
				_transitionTween = MoveIndicator(_durationProgress);
				MoveGreenField();
				resumeProgress = _resumeProgress;
				_restartProgress = false;
			}
		}

		private void MoveGreenField()
		{
			var left = leftPos.localPosition.x + _greenWidth * 0.5f;
			var right = rightPos.localPosition.x - _greenWidth * 0.5f;
			var x = Random.Range(left, right);
			green.DOLocalMoveX(x, 0.1f);
		}

		private Tweener MoveIndicator(float duration)
		{
			var tween = indicator.DOLocalMoveX(rightPos.localPosition.x, duration)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo)
				.SetAutoKill(false);

			return tween;
		}
		
		private void OnMouseClick()
		{
			_transitionTween.Kill();
			_durationProgress = Random.Range(2, 7) * 0.5f;
		}

		private void OnDisable()
		{
			InputCheck.MouseDown -= OnMouseClick;
		}
	}
}