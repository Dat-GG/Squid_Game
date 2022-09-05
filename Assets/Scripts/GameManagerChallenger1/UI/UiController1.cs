using Core.Common.Popup;
using Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;

namespace GameManagerChallenger1.UI
{
    public class UiController1 : MonoBehaviour
    {
        public Text holdToMove;
        public Button settingBtn;
        public Image blackBg;
        public Image normalSkull;
        public Image redEyeSkull;
        public GameObject panelBlock;
        public SpriteRenderer warningArea;
        [SerializeField] private GameObject progressBar;
        [SerializeField] private GameObject clock;
        [SerializeField] private Transform progressPos;
        [SerializeField] private Transform clockPos;

        private void Start()
        {
            holdToMove.gameObject.SetActive(true);
            settingBtn.onClick.AddListener(OnClickSettingBtn);
        }

        public void ChangeDangerous()
        {
            warningArea.DOColor(new Color(1, 0, 0, 0.2f), 0);
        }

        public void ChangeSafety()
        {
            warningArea.DOColor(new Color(0, 1, 0, 0.3f), 0);
            warningArea.DOFade(0, 2f);
        }
        public void MoveObjects()
        {
            progressBar.transform.DOMove(progressPos.position, 1f);
            clock.transform.DOMove(clockPos.position, 1f);
        }

        private static void OnClickSettingBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupInGame);
            PopupController.Instance.HideHud();
        }
    }
}
