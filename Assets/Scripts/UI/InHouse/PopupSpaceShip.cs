using Core.Common.Popup;
using Core.Common.GameResources;
using Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using UI.MainMenu;

namespace UI.InHouse
{
    public class PopupSpaceShip : PopupBase
    {
        [SerializeField] private Button closeBtn, bgBtn, buyWithGoldBtn, buyWithDiamondBtn;
        [SerializeField] private Text diamondTxt;
        [SerializeField] private Text goldTxt;

        [SerializeField] private int priceItemWithGold;
        [SerializeField] private int priceItemWithDiamond;
        private HousePopup _housePopup;
        
        private void Awake()
        {
            closeBtn.onClick.AddListener(OnClickCloseBtn);
            bgBtn.onClick.AddListener(OnClickCloseBtn);
            buyWithDiamondBtn.onClick.AddListener(OnClickBuyWithDiamondBtn);
            buyWithGoldBtn.onClick.AddListener(OnClickBuyWithGoldBtn);

            diamondTxt.text = "" + priceItemWithDiamond;
            goldTxt.text = "" + priceItemWithGold;

            _housePopup = FindObjectOfType<HousePopup>();
        }

        private void OnClickCloseBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            Close();
        }
        private void OnClickBuyWithGoldBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.GetMoney(MoneyType.Gold) >= priceItemWithGold)
            {
                DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Gold, priceItemWithGold);
                _housePopup.SpaceShip.SetActive(true);
                _housePopup.DecorSpaceShipBtn.gameObject.SetActive(false);
                DataAccountPlayer.PlayerSettings.SetInHouse(_housePopup.SpaceShip.name);
                Close();
            }
            else
            {
                PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.GoldTablePopup);
            }
        }
        private void OnClickBuyWithDiamondBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.GetMoney(MoneyType.Diamond) >=  priceItemWithDiamond)
            {
                DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, priceItemWithDiamond);
                _housePopup.SpaceShip.SetActive(true);
                _housePopup.DecorSpaceShipBtn.gameObject.SetActive(false);
                DataAccountPlayer.PlayerSettings.SetInHouse(_housePopup.SpaceShip.name);
                Close();
            }
            else
            {
                PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.DiamondTablePopup);
            }
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }
    }
}
