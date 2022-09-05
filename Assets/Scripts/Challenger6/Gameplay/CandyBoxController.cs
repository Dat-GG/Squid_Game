using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger6.Gameplay
{
	public class CandyBoxController : MonoBehaviour
	{
		public static event Action BoxFullyOpen;
		
		[SerializeField] private GameObject boxCover;
		[SerializeField] private int boxId;
		[SerializeField] private CandyController[] candyPref;
		[SerializeField] private Transform candyPos;
		internal int BoxId => boxId;
		internal int CandyIndex { get; private set; }
		internal bool Chosen { get; private set; } = false;
		private Camera _cam;

		private void Awake()
		{
			_cam = Camera.main;
		}

		private void OnMouseOver()
		{
			if (!Input.GetMouseButtonDown(0))
				return;
			if (!Physics2D.Raycast(_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
				return;
			Chosen = true;
		}

		internal void ChosenBox(Transform centerPos)
		{
			transform.DOMove(centerPos.position, 0.5f)
				.SetEase(Ease.InBack)
				.OnComplete(() =>
				{
					transform.DOScale(transform.localScale * 2.5f, 0.5f)
						.OnComplete(() =>
						{
							var index = Random.Range(0, candyPref.Length);
							CandyIndex = index;
							var candy = Instantiate(candyPref[index], transform);
							candy.transform.localPosition = candyPos.localPosition;
							
							boxCover.transform.DOMove(new Vector2(0, 10f), 0.5f)
								.OnComplete(() =>
								{
									boxCover.SetActive(false);
									BoxFullyOpen?.Invoke();
								});							
						});
				});
		}

		internal void RemovedBox(Transform outsidePos)
		{
			transform.DOMove(outsidePos.position, 0.5f)
				.SetEase(Ease.InBack)
				.OnComplete(() =>
				{
					gameObject.SetActive(false);
				});
		}

		internal void FinishScratch()
		{
			var candy = GetComponentInChildren<CandyController>();
			candy.CandyOutlineScratch.SetActive(false);
		}
	}
}