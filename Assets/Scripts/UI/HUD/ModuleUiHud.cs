using Core.Common.GameResources;
using Core.Common.Popup;
using DataAccount;
using UnityEngine;
using UnityEngine.UI;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;
using Core.Common;
using UI.LoadingScene;

namespace UI.HUD
{
    public class ModuleUiHud : PopupBase
    {
        [SerializeField] private Button diamondAddBtn;
        [SerializeField] private Button goldAddBtn;
        [SerializeField] private Button backBtn;

        [SerializeField] private Text diamondNumberTxt;
        [SerializeField] private Text goldNumberTxt;

        [SerializeField] private Image blockInput;

        private void Awake()
        {
            this.RegisterListener(EventID.EarnMoney, (sender, param) =>
            {
                OnMoneyChange((MoneyType) param);
            });

            this.RegisterListener(EventID.SpendMoney, (sender, param) =>
            {
                OnMoneyChange((MoneyType) param);
            });

            diamondAddBtn.onClick.AddListener(OnClickAddDiamond);
            goldAddBtn.onClick.AddListener(OnClickAddGold);
            backBtn.onClick.AddListener(OnClickBackBtn);
        }

        private void OnEnable()
        {
            LoadDiamondNumber();
            LoadGoldNumber();
        }

        public void ShowBackButton(bool isShow)
        {
            backBtn.gameObject.SetActive(isShow);
        }

        private void OnClickAddDiamond()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParent(PopupType.DiamondTablePopup);
        }

        private void OnClickAddGold()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParent(PopupType.GoldTablePopup);
        }

        private void OnClickBackBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.CloseCurrentPopupAndOpenParent();
            GameManager.Instance.LoadScene(SceneName.PlatformerMainMenu);
        }

        private void OnMoneyChange(MoneyType moneyType)
        {
            if (moneyType == MoneyType.Gold)
            {
                LoadGoldNumber();
                return;
            }

            if (moneyType == MoneyType.Diamond)
                LoadDiamondNumber();
        }

        private void LoadDiamondNumber()
        {
            diamondNumberTxt.text = DataAccountPlayer.PlayerMoney.GetMoney(MoneyType.Diamond).ToString();
        }

        private void LoadGoldNumber()
        {
            goldNumberTxt.text = DataAccountPlayer.PlayerMoney.GetMoney(MoneyType.Gold).ToString();
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        public void BlockInput(bool toggle)
        {
            blockInput.gameObject.SetActive(toggle);
        }
    }
}