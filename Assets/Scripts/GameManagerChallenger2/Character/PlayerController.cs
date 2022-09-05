using System.Collections;
using Core.Common.Popup;
using DataAccount;
using DG.Tweening;
using GameManagerChallenger2.UI;
using UnityEngine;

namespace GameManagerChallenger2.Character
{
	public class PlayerController : CharacterController
	{
		internal int JumpTurn;
		internal bool StartPlay { get; set; } = false;
		[SerializeField] private float timeToShowLosePopup;
		private ModuleUiPowerUpBtn2 _powerUp2;
		[SerializeField] private GameObject balloon;
		[SerializeField] private float balloonTravelTime;
		[SerializeField] private GameObject scan;
		internal bool ReadyToJump = false;

		private void Awake()
		{
			_powerUp2 = FindObjectOfType<ModuleUiPowerUpBtn2>();
		}

		private void Start()
		{
			CharacterSpine.PlayIdle();
			StartCoroutine(BalloonFly());
			StartCoroutine(PlayerReady());
		}
		
		private void Update()
		{
			JumpTurn = Gameplay.GameManagerChallenger2.Instance.PlayerTurn;
			UsingScan();
			if (!FallOff) return;
			if (Gameplay.GameManagerChallenger2.Instance.Lose) return;
			timeToShowLosePopup -= Time.deltaTime;
			if (timeToShowLosePopup > 0) return;
			DataAccountPlayer.PlayerChallenger2PowerUp.LoseCount++;
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LosePopup);
			gameObject.SetActive(false);
			FallOff = false;
		}

		private IEnumerator PlayerReady()
		{
			while (!ReadyToJump)
				yield return null;
			
			if (FinishRound)
				yield break;
			
			CharacterSpine.PlayRun();
			transform.DOLocalMove(Gameplay.GameManagerChallenger2.Instance.ReadyPos.position, 1f)
				.OnComplete(delegate
				{
					CharacterSpine.PlayIdle();
				});
		}
		
		private IEnumerator BalloonFly()
		{
			while (!_powerUp2.HasBalloon)
				yield return null;
			
			FinishRound = true;
			balloon.SetActive(true);
			HasBalloon = true;
			
			Gameplay.GameManagerChallenger2.Instance.LeftJumpBtn.interactable = false;
			Gameplay.GameManagerChallenger2.Instance.RightJumpBtn.interactable = false;

			transform.DOLocalMove(Gameplay.GameManagerChallenger2.Instance.GenerateGoalPos().position, balloonTravelTime)
				.SetEase(Ease.InOutSine);
			transform.DOScale(transform.localScale * 0.4f, balloonTravelTime).SetEase(Ease.InOutSine);

			yield return new WaitForSeconds(balloonTravelTime);
			balloon.SetActive(false);
		}

		private void UsingScan()
		{
			if (!_powerUp2.HasScan) return;
			scan.SetActive(true);
		}

		private void OnDisable() => StopAllCoroutines();
	}
}