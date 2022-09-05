using Core.Common.GameResources;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using EasyUI.PickerWheelUI;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger2.UI
{
	public class ModuleUiPowerUpBtn2 : MonoBehaviour
	{
		[SerializeField] private GameObject blockInput;
		[SerializeField] private float bonusTime;
		internal bool HasBalloon { get; private set; } = false;
		internal bool HasScan { get; private set; } = false;
		internal bool HasDrone { get; private set; } = false;
		internal bool HasReinforce { get; private set; } = false;
		
		[SerializeField] private Button diamondSpinBtn, adsSpinBtn, bonusTimeBtn,
			balloonBtn, scanBtn, droneBtn, reinforceBtn, quitSpinBtn;
		[SerializeField] private Image bonusTimeImg, balloonImg, scanImg, droneImg, reinforceImg;

		[SerializeField] private GameObject luckySpin;
		[SerializeField] private PickerWheel spinWheel;
		[SerializeField] private Text costSpin;

		[SerializeField] private TimerCountdown timerCountdown;
		
		private void Awake()
		{
			diamondSpinBtn.onClick.AddListener(OnClickDiamondSpin);
			adsSpinBtn.onClick.AddListener(OnClickAdsSpin);
			bonusTimeBtn.onClick.AddListener(OnClickBonusTime);
			balloonBtn.onClick.AddListener(OnClickBalloon);
			scanBtn.onClick.AddListener(OnClickScan);
			droneBtn.onClick.AddListener(OnClickDrone);
			reinforceBtn.onClick.AddListener(OnClickReinforce);
			quitSpinBtn.onClick.AddListener(OnClickQuitSpin);
		}

		private void Start()
		{
			var bonusTime = DataAccountPlayer.PlayerChallenger2PowerUp.GetPowerUp(PowerUpType.BonusTime2) > 0;
			var balloon = DataAccountPlayer.PlayerChallenger2PowerUp.GetPowerUp(PowerUpType.Balloon) > 0;
			var scan = DataAccountPlayer.PlayerChallenger2PowerUp.GetPowerUp(PowerUpType.Scan) > 0;
			var drone = DataAccountPlayer.PlayerChallenger2PowerUp.GetPowerUp(PowerUpType.Drone) > 0;
			
			bonusTimeBtn.gameObject.SetActive(bonusTime);
			balloonBtn.gameObject.SetActive(balloon);
			scanBtn.gameObject.SetActive(scan);
			droneBtn.gameObject.SetActive(drone);

			if (bonusTime || balloon || scan || drone || DataAccountPlayer.PlayerChallenger2PowerUp.countSpin < 1)
				return;
			luckySpin.gameObject.SetActive(true);
			if (DataAccountPlayer.PlayerChallenger2PowerUp.countSpin > 1)
			{
				adsSpinBtn.gameObject.SetActive(true);
				diamondSpinBtn.transform.DOLocalMoveX(-200, 0);
			}

			DataAccountPlayer.PlayerChallenger2PowerUp.spinCost =
				DataAccountPlayer.PlayerChallenger2PowerUp.CountSpin <= 1 ? 0 : 1;
			costSpin.text = DataAccountPlayer.PlayerChallenger2PowerUp.spinCost.ToString();
		}

		private void Update()
		{
			CheckPowerUp();
		}

		private void CheckPowerUp()
		{
			spinWheel.OnSpinEnd(wheelPiece =>
			{
				switch (wheelPiece.Label)
				{
					case "BonusTime":
						ShowPowerUp(bonusTimeImg, bonusTimeBtn);
						DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp = wheelPiece.Amount;
						DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(true, PowerUpType.BonusTime2,
							DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
						break;
					case "Balloon":
						ShowPowerUp(balloonImg, balloonBtn);
						DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp = wheelPiece.Amount;
						DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(true, PowerUpType.Balloon,
							DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
						break;
					case "Scan":
						ShowPowerUp(scanImg, scanBtn);
						DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp = wheelPiece.Amount;
						DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(true, PowerUpType.Scan,
							DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
						break;
					case "Drone":
						ShowPowerUp(droneImg, droneBtn);
						DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp = wheelPiece.Amount;
						DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(true, PowerUpType.Drone,
							DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
						break;
					case "Reinforce":
						ShowPowerUp(reinforceImg, reinforceBtn);
						DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp = wheelPiece.Amount;
						DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(true, PowerUpType.Reinforce,
							DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
						break;						
				}

				DOVirtual.DelayedCall(0.5f, () =>
				{
					diamondSpinBtn.gameObject.SetActive(false);
					luckySpin.gameObject.SetActive(false);
				});
			});
		}

		private void ShowPowerUp(Component image, Component button)
		{
			DOVirtual.DelayedCall(0.5f, () =>
			{
				PowerUpImgAnimation(image);
				DOVirtual.DelayedCall(3f, () =>
				{
					button.gameObject.SetActive(true);
					blockInput.SetActive(false);
				});
			});
			
			void PowerUpImgAnimation(Component img)
			{
				blockInput.SetActive(true);
				img.gameObject.SetActive(true);
				img.transform.DOScale(0, 0);
				img.transform.DOScale(1.5f, 2f);
				DOVirtual.DelayedCall(2f, () =>
				{
					img.transform.DOScale(0, 1f);
				});
			}
		}
		
		private void OnClickDiamondSpin()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			quitSpinBtn.gameObject.SetActive(false);
			if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
				DataAccountPlayer.PlayerChallenger2PowerUp.SpinCost) == false) return;
            
			spinWheel.Spin();
			DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
				DataAccountPlayer.PlayerChallenger2PowerUp.SpinCost);
		}

		private void OnClickAdsSpin()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			quitSpinBtn.gameObject.SetActive(false);
			// TODO: Spin after watch ads
		}

		private void OnClickBonusTime()
		{
			timerCountdown.AddBonusTime(bonusTime);
			bonusTimeBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(false, PowerUpType.BonusTime2,
				DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
		}

		private void OnClickBalloon()
		{
			HasBalloon = true;
			balloonBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(false, PowerUpType.Balloon,
				DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
		}

		private void OnClickScan()
		{
			HasScan = true;
			scanBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(false, PowerUpType.Scan,
				DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
		}

		private void OnClickDrone()
		{
			HasDrone = true;
			droneBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(false, PowerUpType.Drone,
				DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
		}

		private void OnClickReinforce()
		{
			HasReinforce = true;
			reinforceBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger2PowerUp.SetPowerUp(false, PowerUpType.Reinforce,
				DataAccountPlayer.PlayerChallenger2PowerUp.TimeUsePowerUp);
		}

		private void OnClickQuitSpin()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			luckySpin.SetActive(false);
		}
	}
}