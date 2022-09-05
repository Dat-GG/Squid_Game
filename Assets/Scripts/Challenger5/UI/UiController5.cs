using Challenger5.Player;
using Core.Common.Popup;
using Core.Common.Sound;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger5.UI
{ 
    public class UiController5 : MonoBehaviour
    {
        public Text tapToPlayTxt;
        public Button settingBtn;
        public GameObject panelBlock;
        [SerializeField] private Text killTxt;
        [SerializeField] private GameObject countAlive;
        [SerializeField] private GameObject timer;
        [SerializeField] private Transform timerPos;
        [SerializeField] private Transform countAlivePos;
        private PlayerController5 _playerController5;

        private void Start()
        {
            _playerController5 = FindObjectOfType<PlayerController5>();
            settingBtn.onClick.AddListener(OnClickSettingBtn);
        }

        private void Update()
        {
            killTxt.text = _playerController5.countKill switch
            {
                1 => "FirstBlood!!!",
                2 => "DoubleKill!!!",
                3 => "TripleKill!!!",
                4 => "QuadraKill!!!",
                5 => "PentaKill!!!",
                6 => "GodLike!!!",
                _ => killTxt.text
            };

            if (_playerController5.countKill > 6)
            {
                killTxt.text = "Legendary!!!";
            }
        }

        public void AppearObject()
        {
            tapToPlayTxt.gameObject.SetActive(false);
            countAlive.transform.DOMove(countAlivePos.position, 0.5f);
            timer.transform.DOMove(timerPos.position, 0.5f);
        }

        private void OnClickSettingBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupInGame);
            PopupController.Instance.HideHud();
        }
    }
}
