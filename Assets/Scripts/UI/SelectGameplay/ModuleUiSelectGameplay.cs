using System;
using Core.Common;
using Core.Common.Popup;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;
using UI.LoadingScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SelectGameplay
{
    public class ModuleUiSelectGameplay : PopupBase
    {
        [SerializeField] private Button platformBtn;

        private void Start()
        {
            platformBtn.onClick.AddListener(OnClickPlatformBtn);
        }

        private void OnClickPlatformBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            //GameManager.Instance.LoadScene(SceneName.PlatformerCampaign);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.TestUi);
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }
    }
}
