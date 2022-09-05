using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger6.Gameplay
{
	public class CandyController : MonoBehaviour
	{
		[SerializeField] private string shape;
		[SerializeField] private Transform candyCore;
		internal string Shape => shape;
		[SerializeField] private List<Transform> candyPieces = new List<Transform>();
		private readonly List<Transform> _candyPiecesRemoved = new List<Transform>();
		[SerializeField] private GameObject candyOutlineBg, candyOutlineScratch;
		internal GameObject CandyOutlineScratch => candyOutlineScratch;
		[SerializeField] private float breakForce;
		[SerializeField] private float breakTime;

		private void Start()
		{
			StartCoroutine(CheckFinishDetach());
		}
		
		private IEnumerator CheckFinishDetach()
		{
			while (candyPieces.Count > 0)
			{
				yield return null;
			}
			candyOutlineBg.SetActive(false);
			candyCore.DOScale(candyCore.localScale * 1.5f, 1f);
			GameManagerChallenger6.Instance.WinDetach = true;
			foreach (var piece in _candyPiecesRemoved)
				piece.gameObject.SetActive(false);
		}
		
		internal void BreakPiece()
		{
			var index = Random.Range(0, candyPieces.Count);
			var breakDir = (candyPieces[index].position - candyCore.position).normalized;
			candyPieces[index].DOLocalMove(breakDir * breakForce, breakTime);
			_candyPiecesRemoved.Add(candyPieces[index]);
			candyPieces.Remove(candyPieces[index]);
		}

		private void OnDisable()
		{
			StopCoroutine(CheckFinishDetach());
		}
	}
}