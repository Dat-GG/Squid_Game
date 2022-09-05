using Core.Common;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using Plugins.Scripts.UI.ShopSkin;
using UI.LoadingScene;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ModuleUiMainMenu : PopupBase
    {
        // [SerializeField] private Button playCampaignBtn;
        [SerializeField] private Button playChallenger1Btn, playChallenger2Btn;
        [SerializeField] private Button playChallenger3Btn, playChallenger4Btn;
        [SerializeField] private Button playChallenger5Btn, playChallenger6Btn, playChallenger7Btn;
        [SerializeField] private Button settingsBtn, noAdsBtn, spinBtn, skinBtn, houseBtn;

        [SerializeField] private Text winCountChallenger4Txt;
        [SerializeField] private Text loseCountChallenger4Txt;
        
        [SerializeField] private Text winCountChallenger1Txt;
        [SerializeField] private Text loseCountChallenger1Txt;
        
        [SerializeField] private Text winCountChallenger2Txt;
        [SerializeField] private Text loseCountChallenger2Txt;
        
        [SerializeField] private Text winCountChallenger3Txt;
        [SerializeField] private Text loseCountChallenger3Txt;
        
        [SerializeField] private Text winCountChallenger5Txt;
        [SerializeField] private Text loseCountChallenger5Txt;
        
        [SerializeField] private Text winCountChallenger6Txt;
        [SerializeField] private Text loseCountChallenger6Txt;

        [SerializeField] private Text winCountChallenger7Txt;
        [SerializeField] private Text loseCountChallenger7Txt;
        
        private void Start()
        {
            var data1 = DataAccountPlayer.PlayerChallenger1PowerUp;
            var data2 = DataAccountPlayer.PlayerChallenger2PowerUp;
            var data3 = DataAccountPlayer.PlayerChallenger3;
            var data4 = DataAccountPlayer.PlayerChallenger4PowerUp;
            var data5 = DataAccountPlayer.PlayerChallenger5PowerUp;
            var data6 = DataAccountPlayer.PlayerChallenger6PowerUp;
            var data7 = DataAccountPlayer.PlayerChallenger7PowerUp;
            
            // playCampaignBtn.onClick.AddListener(OnClickPlayCampaign);
            settingsBtn.onClick.AddListener(OnClickSettings);
            noAdsBtn.onClick.AddListener(OnClickNoAds);
            spinBtn.onClick.AddListener(OnClickSpin);
            skinBtn.onClick.AddListener(OnClickSkin);
            houseBtn.onClick.AddListener(OnClickHouse);
            
            PopupController.Instance.GetHud().ShowBackButton(false);
            SoundManager.Instance.PlayBackgroundMusic(SoundType.BackgroundTheme);
            
            playChallenger1Btn.onClick.AddListener(OnClickPlayChallenger1Btn);
            playChallenger2Btn.onClick.AddListener(OnClickPlayChallenger2Btn);
            playChallenger3Btn.onClick.AddListener(OnClickPlayChallenger3Btn);
            playChallenger4Btn.onClick.AddListener(OnClickPlayChallenger4Btn);
            playChallenger5Btn.onClick.AddListener(OnClickPlayChallenger5Btn);
            playChallenger6Btn.onClick.AddListener(OnClickPlayChallenger6Btn);
            playChallenger7Btn.onClick.AddListener(OnClickPlayChallenger7Btn);
            
            winCountChallenger1Txt.text = "WIN : " + data1.WinCount;
            loseCountChallenger1Txt.text = "LOSE : " + data1.LoseCount;
            winCountChallenger2Txt.text = "WIN : " + data2.WinCount;
            loseCountChallenger2Txt.text = "LOSE : " + data2.LoseCount;
            winCountChallenger3Txt.text = "WIN : " + data3.WinCount;
            loseCountChallenger3Txt.text = "LOSE : " + data3.LoseCount;
            winCountChallenger4Txt.text = "WIN : " + data4.WinCount;
            loseCountChallenger4Txt.text = "LOSE : " + data4.LoseCount;
            winCountChallenger5Txt.text = "WIN : " + data5.WinCount;
            loseCountChallenger5Txt.text = "LOSE : " + data5.LoseCount;
            winCountChallenger6Txt.text = "WIN : " + data6.WinCount;
            loseCountChallenger6Txt.text = "LOSE : " + data6.LoseCount;
            winCountChallenger7Txt.text = "WIN : " + data7.WinCount;
            loseCountChallenger7Txt.text = "LOSE : " + data7.LoseCount;
        }
        
        protected override void OnShow(){}

        private void OnClickPlayChallenger1Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_1);
        }
        
        private void OnClickPlayChallenger2Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_2);
        }

        private void OnClickPlayChallenger3Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_3);
        }
        
        private void OnClickPlayChallenger4Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_4);
        }
        
        private void OnClickPlayChallenger5Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_5);
        }
        
        private void OnClickPlayChallenger6Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_6);
        }
        
        private void OnClickPlayChallenger7Btn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            GameManager.Instance.LoadScene(SceneName.Challenger_7);
        }

        private void OnClickSettings()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupMainMenu);
        }

        private void OnClickNoAds()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.NoAdsPopup);
        }

        private void OnClickSpin()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LuckySpinPopup);
        }

        private void OnClickSkin()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            var skinPopup = (SkinPopup) PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SkinPopup);
            skinPopup.LoadSkin(DataAccountPlayer.PlayerSkins.skinUsing);
        }

        private void OnClickHouse()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.HousePopup);
        }

        protected override void OnHide(){}
    }
}
