using Core.Common;
using Core.Common.Popup;
using UnityEngine;
using UnityEngine.UI;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;
using UI.LoadingScene;
using UnityEngine.SceneManagement;

namespace UI.WinLose
{
    public class UiLosePopup : PopupBase
    {
        [SerializeField] private Button adsBtn, closeBtn;
        
        [SerializeField] private float timeToShowThanksBtn;
        [SerializeField] private float resetTimeShowThanks;
        
        private void OnEnable()
        {
            timeToShowThanksBtn = resetTimeShowThanks;
            closeBtn.gameObject.SetActive(false);
        }

        private void Awake()
        {
            adsBtn.onClick.AddListener(OnClickAdsBtn);
            closeBtn.onClick.AddListener(OnClickCloseBtn);
        }

        private void Update()
        {
            ShowThanksBtn();
        }

        private void OnClickAdsBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnClickCloseBtn()
        {
            PopupController.Instance.CloseCurrentPopupAndOpenParent();
            GameManager.Instance.LoadScene(SceneName.PlatformerMainMenu);
        }
        
        private void ShowThanksBtn()
        {
            timeToShowThanksBtn -= Time.deltaTime;
            if (timeToShowThanksBtn >= 0) return;
            closeBtn.gameObject.SetActive(true);
        }

        protected override void OnShow(){}

        protected override void OnHide(){}
    }
}