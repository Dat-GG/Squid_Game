using System.Collections.Generic;
using Core.Common.GameResources;
using Core.Common.Sound;
using Core.Load;
using DataAccount;
using Plugins.Scripts.Core.Common.Load;
using Plugins.Scripts.Core.Common.Sound;
using Plugins.Scripts.UI.ShopSkin.Data;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Scripts.UI.ShopSkin
{
    public class SkinDemo : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic spine;
        [SerializeField] private GameObject selectedBtn;
        [SerializeField] private Button selectBtn;
        [SerializeField] private Button buyBtn;
        [SerializeField] private Button adsBtn;
        [SerializeField] private Text unlockGoldValueTxt;
        [SerializeField] private Text watchedAdsNumberTxt;
        
        private SkinData _skinData;
        
        private void Awake()
        {
            buyBtn.onClick.AddListener(OnClickBuyBtn);
            adsBtn.onClick.AddListener(OnClickAdsBtn);
            selectBtn.onClick.AddListener(OnClickSelectBtn);
            this.RegisterListener(EventID.SkinElementSelect, (sender, param) =>
            {
                OnSkinElementSelect((SkinData) param);
            });
        }
        
        private void OnSkinElementSelect(SkinData skinData)
        {
            _skinData = skinData;
            spine.initialSkinName = _skinData.skinName.ToString();
            spine.Initialize(true);
            
            LoadButtons();
        }
        
        private void OnClickAdsBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            DataAccountPlayer.PlayerSkins.OnWatchAds(_skinData.skinName);
            if (DataAccountPlayer.PlayerSkins.GetAdsWatchedNumber(_skinData.skinName) >= _skinData.numberOfAds)
            {
                DataAccountPlayer.PlayerSkins.AddOwnedSkin(_skinData.skinName);
                this.PostEvent(EventID.SkinUnlockSuccess);
            }

            LoadButtons();
        }
        
        private void OnClickBuyBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            var skinData = LoadResourceController.Instance.LoadSkinDataCollection();
            var coinSkins = skinData.coinSkins;
            var skins = new List<SkinData>(coinSkins);
            int totalGoldNeed = _skinData.baseCoinSkin;
            if (!DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Gold, totalGoldNeed))
            {
                return;
            }

            if (skins.Count <= 0)
            {
                return;
            }
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Gold, totalGoldNeed);
            DataAccountPlayer.PlayerSkins.AddOwnedSkin(_skinData.skinName);
            this.PostEvent(EventID.SkinUnlockSuccess);
            
            LoadButtons();
        }
        
        private void OnClickSelectBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            var changeSuccess = DataAccountPlayer.PlayerSkins.OnChangeSkin(_skinData.skinName);
            if (changeSuccess)
            {
                this.PostEvent(EventID.SkinUsingSelected);
                LoadButtons();
            }
        }
        
        private void LoadButtons()
        {
            var playerSkins = DataAccountPlayer.PlayerSkins;
            var isOwned = playerSkins.IsHaveOwnedSkin(_skinData.skinName);
        
            if (isOwned)
            {
                var isUsing = playerSkins.skinUsing == _skinData.skinName;
                selectedBtn.SetActive(isUsing);
                selectBtn.gameObject.SetActive(!isUsing);
                adsBtn.gameObject.SetActive(false);
                buyBtn.gameObject.SetActive(false);
            }
            else
            {
                selectBtn.gameObject.SetActive(false);
                selectedBtn.SetActive(false);

                if (_skinData.inGoldReward)
                {
                    adsBtn.gameObject.SetActive(true);
                    buyBtn.gameObject.SetActive(true);
                    var skinData = LoadResourceController.Instance.LoadSkinDataCollection();
                    int totalGoldNeed = _skinData.baseCoinSkin;
                    unlockGoldValueTxt.text = totalGoldNeed.ToString();
                    watchedAdsNumberTxt.text =
                        $"{playerSkins.GetAdsWatchedNumber(_skinData.skinName)}/{_skinData.numberOfAds}";
                }
                else if(_skinData.onlyAdsReward)
                {
                    buyBtn.gameObject.SetActive(false);
                    adsBtn.gameObject.SetActive(true);
                    watchedAdsNumberTxt.text =
                        $"{playerSkins.GetAdsWatchedNumber(_skinData.skinName)}/{_skinData.numberOfAds}";
                }
            }
        }
    }
}
