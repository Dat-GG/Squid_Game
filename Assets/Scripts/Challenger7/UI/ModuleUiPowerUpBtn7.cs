using Core.Common.GameResources;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using EasyUI.PickerWheelUI;
using Plugins.Scripts.Core.Common.Sound;
using Plugins.Scripts.DataAccount;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger7.UI
{
	public class ModuleUiPowerUpBtn7 : MonoBehaviour
	{
		[SerializeField] private GameObject blockInput;

		[SerializeField] private Button diamondSpinBtn, adsSpinBtn, shield7Btn, magnetBtn,
			pushBtn, allGoldItemBtn, freeze7Btn, passThroughBtn, quitSpinBtn;
		[SerializeField] private Image shield7Img, magnetImg, pushImg, allGoldItemImg,
			freeze7Img, passThroughImg;

		[SerializeField] private GameObject luckySpin;
		[SerializeField] private PickerWheel spinWheel;
		[SerializeField] private Text costSpin;
		private PlayerChallenger7PowerUp _powerUp7;

		internal bool HasShield7, HasMagnet, HasPush, HasAllGoldItem, HasFreeze7, HasPassThrough;

		private void Awake()
		{
			_powerUp7 = DataAccountPlayer.PlayerChallenger7PowerUp;
			diamondSpinBtn.onClick.AddListener(OnClickDiamondSpinBtn);
			adsSpinBtn.onClick.AddListener(OnClickAdsSpinBtn);
			shield7Btn.onClick.AddListener(OnClickShield7Btn);
			magnetBtn.onClick.AddListener(OnClickMagnetBtn);
			pushBtn.onClick.AddListener(OnClickPushBtn);
			allGoldItemBtn.onClick.AddListener(OnClickAllGoldItemBtn);
			freeze7Btn.onClick.AddListener(OnClickFreeze7Btn);
			passThroughBtn.onClick.AddListener(OnClickPassThroughBtn);
			quitSpinBtn.onClick.AddListener(OnClickQuitSpinBtn);
		}

		private void Start()
		{
			TogglePowerUpAndSpin();
		}

		private void Update()
		{
			CheckPowerUp();
		}

		private void TogglePowerUpAndSpin()
		{	// toggle power-up buttons
			var shield7 = _powerUp7.GetPowerUp(PowerUpType.Shield7) > 0;
			var magnet = _powerUp7.GetPowerUp(PowerUpType.Magnet) > 0;
			var push = _powerUp7.GetPowerUp(PowerUpType.Push) > 0;
			var allGoldItem = _powerUp7.GetPowerUp(PowerUpType.AllGoldItem) > 0;
			var freeze = _powerUp7.GetPowerUp(PowerUpType.Freeze7) > 0;
			var passThrough = _powerUp7.GetPowerUp(PowerUpType.PassThrough) > 0;

			shield7Btn.gameObject.SetActive(shield7);
			magnetBtn.gameObject.SetActive(magnet);
			pushBtn.gameObject.SetActive(push);
			allGoldItemBtn.gameObject.SetActive(allGoldItem);
			freeze7Btn.gameObject.SetActive(freeze);
			passThroughBtn.gameObject.SetActive(passThrough);
			
			// toggle lucky spin
			if (shield7 || magnet || push || allGoldItem || freeze || passThrough || _powerUp7.countSpin < 1)
				return;
			luckySpin.gameObject.SetActive(true);

			if (_powerUp7.countSpin > 1)
			{
				adsSpinBtn.gameObject.SetActive(true);
				diamondSpinBtn.transform.DOLocalMoveX(-200, 0);
			}

			_powerUp7.spinCost = _powerUp7.CountSpin <= 1 ? 0 : 1;
			costSpin.text = _powerUp7.spinCost.ToString();
		}

		private void CheckPowerUp()
		{
			spinWheel.OnSpinEnd(wheelPiece =>
			{
				switch (wheelPiece.Label)
				{
					case "Shield":
						ShowPowerUp(shield7Img, shield7Btn);
						_powerUp7.TimeUsePowerUp = wheelPiece.Amount;
						_powerUp7.SetPowerUp(true, PowerUpType.Shield7, _powerUp7.TimeUsePowerUp);
						break;
					case "Magnet":
						ShowPowerUp(magnetImg, magnetBtn);
						_powerUp7.TimeUsePowerUp = wheelPiece.Amount;
						_powerUp7.SetPowerUp(true, PowerUpType.Magnet, _powerUp7.TimeUsePowerUp);
						break;
					case "Push":
						ShowPowerUp(pushImg, pushBtn);
						_powerUp7.TimeUsePowerUp = wheelPiece.Amount;
						_powerUp7.SetPowerUp(true, PowerUpType.Push, _powerUp7.TimeUsePowerUp);
						break;
					case "AllGoldItem":
						ShowPowerUp(allGoldItemImg, allGoldItemBtn);
						_powerUp7.TimeUsePowerUp = wheelPiece.Amount;
						_powerUp7.SetPowerUp(true, PowerUpType.AllGoldItem, _powerUp7.TimeUsePowerUp);
						break;
					case "Freeze":
						ShowPowerUp(freeze7Img, freeze7Btn);
						_powerUp7.TimeUsePowerUp = wheelPiece.Amount;
						_powerUp7.SetPowerUp(true, PowerUpType.Freeze7, _powerUp7.TimeUsePowerUp);
						break;
					case "PassThrough":
						ShowPowerUp(passThroughImg, passThroughBtn);
						_powerUp7.TimeUsePowerUp = wheelPiece.Amount;
						_powerUp7.SetPowerUp(true, PowerUpType.PassThrough, _powerUp7.TimeUsePowerUp);
						break;
				}

				DOVirtual.DelayedCall(0.5f, () =>
				{
					diamondSpinBtn.gameObject.SetActive(false);
					luckySpin.gameObject.SetActive(false);
				});
			});
		}

		private void ShowPowerUp(Image image, Button btn)
		{
			DOVirtual.DelayedCall(0.5f, () =>
			{
				PowerUpImgAnimation(image);
				DOVirtual.DelayedCall(3f, () =>
				{
					btn.gameObject.SetActive(true);
					blockInput.SetActive(false);
				});
			});

			void PowerUpImgAnimation(Image img)
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

		private void OnClickDiamondSpinBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			quitSpinBtn.gameObject.SetActive(false);
			if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
				DataAccountPlayer.PlayerChallenger7PowerUp.SpinCost) == false) return;
            
			spinWheel.Spin();
			DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
				DataAccountPlayer.PlayerChallenger7PowerUp.SpinCost);
		}

		private void OnClickAdsSpinBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			quitSpinBtn.gameObject.SetActive(false);
		}

		private void OnClickShield7Btn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			HasShield7 = true;
			shield7Btn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger7PowerUp.SetPowerUp(false, PowerUpType.Shield7,
				DataAccountPlayer.PlayerChallenger7PowerUp.TimeUsePowerUp);
		}

		private void OnClickMagnetBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			HasMagnet = true;
			magnetBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger7PowerUp.SetPowerUp(false, PowerUpType.Magnet,
				DataAccountPlayer.PlayerChallenger7PowerUp.TimeUsePowerUp);
		}

		private void OnClickPushBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			HasPush = true;
			pushBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger7PowerUp.SetPowerUp(false, PowerUpType.Push,
				DataAccountPlayer.PlayerChallenger7PowerUp.TimeUsePowerUp);
		}

		private void OnClickAllGoldItemBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			HasAllGoldItem = true;
			allGoldItemBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger7PowerUp.SetPowerUp(false, PowerUpType.AllGoldItem,
				DataAccountPlayer.PlayerChallenger7PowerUp.TimeUsePowerUp);
		}

		private void OnClickFreeze7Btn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			HasFreeze7 = true;
			freeze7Btn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger7PowerUp.SetPowerUp(false, PowerUpType.Freeze7,
				DataAccountPlayer.PlayerChallenger7PowerUp.TimeUsePowerUp);
		}

		private void OnClickPassThroughBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			HasPassThrough = true;
			passThroughBtn.gameObject.SetActive(false);
			DataAccountPlayer.PlayerChallenger7PowerUp.SetPowerUp(false, PowerUpType.PassThrough,
				DataAccountPlayer.PlayerChallenger7PowerUp.TimeUsePowerUp);
		}

		private void OnClickQuitSpinBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			luckySpin.SetActive(false);
		}
	}
}