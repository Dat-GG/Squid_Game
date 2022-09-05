using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
	public class SettingUiMainMenu : PopupBase
	{
		[SerializeField] private Button soundBtn, musicBtn, vibrationBtn, closeBtn, backgroundBtn, restorePurchase;
		[SerializeField] private Sprite soundOff, musicOff, vibrationOff;
		private Sprite _sound, _music, _vibration;
		[SerializeField] private SwipeFlagSettingUiMainMenu swipeFlags;
		
		private void Awake()
		{
			soundBtn.onClick.AddListener(OnClickToggleSound);
			musicBtn.onClick.AddListener(OnClickToggleMusic);
			vibrationBtn.onClick.AddListener(OnClickToggleVibration);
			closeBtn.onClick.AddListener(OnClickClose);
			backgroundBtn.onClick.AddListener(OnClickClose);
			restorePurchase.onClick.AddListener(OnClickRestorePurchase);

			_sound = soundBtn.GetComponent<Image>().sprite;
			_music = musicBtn.GetComponent<Image>().sprite;
			_vibration = vibrationBtn.GetComponent<Image>().sprite;
		}

		protected override void OnShow()
		{
		}

		private void OnClickToggleSound()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			DataAccountPlayer.PlayerSettings.SetSound(!DataAccountPlayer.PlayerSettings.SoundOff);
			soundBtn.GetComponent<Image>().sprite = DataAccountPlayer.PlayerSettings.SoundOff ? soundOff : _sound;
		}

		private void OnClickToggleMusic()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			DataAccountPlayer.PlayerSettings.SetMusic(!DataAccountPlayer.PlayerSettings.MusicOff);
			musicBtn.GetComponent<Image>().sprite = DataAccountPlayer.PlayerSettings.MusicOff ? musicOff : _music;
		}

		private void OnClickToggleVibration()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			DataAccountPlayer.PlayerSettings.SetVibration(!DataAccountPlayer.PlayerSettings.VibrationOff);
			vibrationBtn.GetComponent<Image>().sprite = DataAccountPlayer.PlayerSettings.VibrationOff ? vibrationOff : _vibration;
		}

		private void OnClickClose()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			Close();
		}

		private void OnClickRestorePurchase()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			// TODO: restore purchase
		}
		
		protected override void OnHide()
		{
			DataAccountPlayer.PlayerSettings.SetFlag(swipeFlags.FlagSelected);
		}
	}
}