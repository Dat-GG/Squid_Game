using Core.Common.Popup;
using Core.Common.Sound;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger4.UI
{
    public class UiController : MonoBehaviour
    {
        public Text tapToPlay;
        public GameObject processBar;
        public GameObject clock;
        public Button settingBtn;
        public GameObject panelBlock;
        
        [SerializeField] private Transform processPos;
        [SerializeField] private Transform clockPos;
        
        private void Start()
        {
            tapToPlay.gameObject.SetActive(true);
            settingBtn.onClick.AddListener(OnClickSettingBtn);
        }

        public void AppearObject()
        {
            processBar.transform.DOMove(processPos.position, 2f);
            clock.transform.DOMove(clockPos.position, 2f);
        }

        private static void OnClickSettingBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupInGame);
            PopupController.Instance.HideHud();
        }
    }
}
