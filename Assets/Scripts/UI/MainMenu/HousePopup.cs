using System.Collections.Generic;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
	public class HousePopup : PopupBase
	{
		[SerializeField] private Button closeBtn;
		[SerializeField] private Button decorPiggyBtn, decorFerrisWheelBtn, decorGuardBtn,
			decorSwingBtn, decorDollyChairBtn, decorSlideBtn, decorSeeSawBtn, decorSpaceShipBtn;
		[SerializeField] private GameObject dollyChair;
		[SerializeField] private GameObject ferrisWheel;
		[SerializeField] private GameObject guard;
		[SerializeField] private GameObject piggy;
		[SerializeField] private GameObject slide;
		[SerializeField] private GameObject swing;
		[SerializeField] private GameObject seeSaw;
		[SerializeField] private GameObject spaceShip;

		#region GetObject
		internal GameObject FerrisWheel => ferrisWheel;
		internal GameObject DollyChair => dollyChair;
		internal GameObject Guard => guard;
		internal GameObject Piggy => piggy;
		internal GameObject Slide => slide;
		internal GameObject Swing => swing;
		internal GameObject SeeSaw => seeSaw;
		internal GameObject SpaceShip => spaceShip;

		internal Button DecorPiggyBtn => decorPiggyBtn;
		internal Button DecorFerrisWheelBtn => decorFerrisWheelBtn;
		internal Button DecorGuardBtn => decorGuardBtn;
		internal Button DecorSwingBtn => decorSwingBtn;
		internal Button DecorDollyChairBtn => decorDollyChairBtn;
		internal Button DecorSlideBtn => decorSlideBtn;
		internal Button DecorSeeSawBtn => decorSeeSawBtn;
		internal Button DecorSpaceShipBtn => decorSpaceShipBtn;
		#endregion

		private void Awake()
		{
			decorPiggyBtn.onClick.AddListener(OnClickDecorPiggyBtn);
			decorFerrisWheelBtn.onClick.AddListener(OnClickDecorFerrisWheelBtn);
			decorGuardBtn.onClick.AddListener(OnClickDecorGuardBtn);
			decorSwingBtn.onClick.AddListener(OnClickDecorSwingBtn);
			decorDollyChairBtn.onClick.AddListener(OnClickDollyChairBtn);
			decorSlideBtn.onClick.AddListener(OnClickDecorSlideBtn);
			decorSeeSawBtn.onClick.AddListener(OnClickDecorSeeSawBtn);
			decorSpaceShipBtn.onClick.AddListener(OnClickDecorSpaceShipBtn);
			closeBtn.onClick.AddListener(OnClickClose);
		}
		
		protected override void OnShow()
		{
			if (DataAccountPlayer.PlayerSettings.Decor.IsNullOrEmpty()) return;
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(dollyChair.name))
			{
				dollyChair.SetActive(true);
				decorDollyChairBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(ferrisWheel.name))
			{
				ferrisWheel.SetActive(true);
				decorFerrisWheelBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(guard.name))
			{
				guard.SetActive(true);
				decorGuardBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(piggy.name))
			{
				piggy.SetActive(true);
				decorPiggyBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(slide.name))
			{
				slide.SetActive(true);
				decorSlideBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(swing.name))
			{
				swing.SetActive(true);
				decorSwingBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(seeSaw.name))
			{
				seeSaw.SetActive(true);
				decorSeeSawBtn.gameObject.SetActive(false);
			}
			if (DataAccountPlayer.PlayerSettings.Decor.Contains(spaceShip.name))
			{
				spaceShip.SetActive(true);
				decorSpaceShipBtn.gameObject.SetActive(false);
			}
			
		}
		
		#region MethodOnClickBtn
		private void OnClickClose()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			Close();
		}

		private void OnClickDecorPiggyBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupPiggy);
		}
		private void OnClickDecorFerrisWheelBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupFerrisWheel);
		}
		private void OnClickDecorGuardBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupGuard);
		}
		private void OnClickDecorSwingBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupSwing);
		}
		private void OnClickDollyChairBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupDollyChair);
		}
		private void OnClickDecorSlideBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupSlide);
		}
		private void OnClickDecorSeeSawBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupSeeSaw);
		}
		private void OnClickDecorSpaceShipBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.PopupSpaceShip);
		}
		#endregion
		
		protected override void OnHide()
		{
		}
	}
}