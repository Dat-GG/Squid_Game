using System;
using Core.Common;
using Core.Common.Popup;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;
using UI.LoadingScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class SettingUiInGame : SettingUiMainMenu
    {
        [SerializeField] private Button homeBtn;
        [SerializeField] private Button resumeBtn;

        private void Start()
        {
            homeBtn.onClick.AddListener(OnClickHomeBtn);
            resumeBtn.onClick.AddListener(OnClickResumeBtn);
        }

        private void OnClickHomeBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.CloseCurrentPopupAndOpenParent();
            GameManager.Instance.LoadScene(SceneName.PlatformerMainMenu);
        }

        private void OnClickResumeBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.HideHud();
            Close();
        }
    }
}
