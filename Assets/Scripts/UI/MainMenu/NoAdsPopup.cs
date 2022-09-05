using Core.Common.Popup;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
	public class NoAdsPopup : PopupBase
	{
		[SerializeField] private Button closeBtn, backgroundBtn;

		private void Awake()
		{
			closeBtn.onClick.AddListener(OnClickClose);
			backgroundBtn.onClick.AddListener(OnClickClose);
		}
		
		protected override void OnShow()
		{
		}
		
		private void OnClickClose()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			Close();
		}
		
		protected override void OnHide()
		{
		}
	}
}