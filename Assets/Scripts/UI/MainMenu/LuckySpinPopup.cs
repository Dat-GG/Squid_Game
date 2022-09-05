using System;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using EasyUI.PickerWheelUI;
using Plugins.Scripts.Core.Common.Sound;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
	public class LuckySpinPopup : PopupBase
	{
		[SerializeField] private Button spinBtn;
		[SerializeField] private Button spinAdsBtn;
		[SerializeField] private PickerWheel spinWheel;
		[SerializeField] private Text wheelStatusTxt;
		[SerializeField] private Image rewardPanel;
		internal Image RewardPanel => rewardPanel;
		[SerializeField] private Text rewardTxt;
		internal int Money { get; private set; }
		internal string LabelReward { get; private set; }
		internal Sprite RewardIcon { get; private set; }
		[SerializeField] private float timeLeft;
		// private bool _timerOn;

		private void Awake()
		{
			spinBtn.onClick.AddListener(OnClickSpin);
			spinAdsBtn.onClick.AddListener(OnClickAdsSpin);
		}

		private void Start()
		{
			rewardPanel.gameObject.SetActive(false);
			PopupController.Instance.GetHud().ShowBackButton(true);
		}

		protected override void OnShow()
		{
			if (!DataAccountPlayer.PlayerSettings.ResetTime)
			{
				wheelStatusTxt.text = "Ready";
				DataAccountPlayer.PlayerSettings.SetLuckySpinCoolDown(timeLeft);
				spinBtn.gameObject.SetActive(true);
				spinAdsBtn.gameObject.SetActive(false);
			}
			else
			{
				TimerCountDown();
				spinBtn.gameObject.SetActive(false);
				spinAdsBtn.gameObject.SetActive(true);
			}
			
			var timeQuitString = DataAccountPlayer.PlayerSettings.Time;
			if (!timeQuitString.IsNullOrWhitespace())
			{
				var timeQuit = DateTime.Parse(timeQuitString);
				var timeNow = DateTime.Now;

				if (timeNow > timeQuit)
				{
					var timeSpan = timeNow - timeQuit;
					DataAccountPlayer.PlayerSettings.LuckySpinCoolDown -= (float) timeSpan.TotalSeconds;
				}
				DataAccountPlayer.PlayerSettings.QuitLuckySpin("");
			}
		}

		private void Update() => TimerCountDown();

		private void OnClickSpin()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			wheelStatusTxt.text = "Spinning...";
			spinBtn.gameObject.SetActive(false);
			PopupController.Instance.GetHud().BlockInput(true);
			
			spinWheel.OnSpinEnd(a =>
			{
				spinAdsBtn.gameObject.SetActive(true);

				DataAccountPlayer.PlayerSettings.SetReward(a.Label);
				DataAccountPlayer.PlayerSettings.ResetTime = true;
				
				rewardTxt.text = "You get " + a.Label.ToString();
				Money = a.Amount;
				LabelReward = a.RewardType;
				RewardIcon = a.Icon;
				PopupController.Instance.GetHud().BlockInput(false);
				PopupController.Instance.OpenPopupAndKeepParent(PopupType.SpinRewardPopup);
			});

			spinWheel.Spin();
		}

		private void OnClickAdsSpin()
		{
			// TODO: play ads then spin
		}

		private void TimerCountDown()
		{
			if (DataAccountPlayer.PlayerSettings.ResetTime)
			{
				if (DataAccountPlayer.PlayerSettings.LuckySpinCoolDown > 0)
				{
					DataAccountPlayer.PlayerSettings.LuckySpinCoolDown -= Time.deltaTime;
					UpdateTimer(DataAccountPlayer.PlayerSettings.LuckySpinCoolDown);
				}
				else
				{
					DataAccountPlayer.PlayerSettings.LuckySpinCoolDown = 0;
					DataAccountPlayer.PlayerSettings.ResetTime = false;
				}
			}

			void UpdateTimer(float currentTime)
			{
				float hours = Mathf.FloorToInt(currentTime / 3600) % 24;
				float minutes = Mathf.FloorToInt(currentTime / 60) % 60;
				float seconds = Mathf.FloorToInt(currentTime % 60);
				wheelStatusTxt.text = $"{hours:00}:{minutes:00}:{seconds:00}";
			}
		}

		protected override void OnHide()
		{
			var timeQuit = DateTime.Now;
			var timeQuitString = timeQuit.ToString();
			DataAccountPlayer.PlayerSettings.QuitLuckySpin(timeQuitString);
		}

		private void OnApplicationQuit()
		{
			var timeQuitString = DataAccountPlayer.PlayerSettings.Time;
			if (!timeQuitString.IsNullOrWhitespace())
			{
				var timeQuit = DateTime.Parse(timeQuitString);
				var timeNow = DateTime.Now;

				if (timeNow > timeQuit)
				{
					DataAccountPlayer.PlayerSettings.QuitLuckySpin(timeNow.ToString());
				}
			}
		}
	}
}