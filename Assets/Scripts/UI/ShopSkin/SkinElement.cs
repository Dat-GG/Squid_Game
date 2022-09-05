using Core.Common.Sound;
using Core.Load;
using DataAccount;
using Plugins.Scripts.Core.Common.GameResources;
using Plugins.Scripts.Core.Common.Load;
using Plugins.Scripts.Core.Common.Sound;
using Plugins.Scripts.UI.ShopSkin.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Scripts.UI.ShopSkin
{
    public class SkinElement : MonoBehaviour
    {
        [SerializeField] private GameObject ownedBg;
        [SerializeField] private GameObject blackBgAva;
        [SerializeField] private GameObject tickObj;
        [SerializeField] private GameObject lockObj;
        [SerializeField] private Image avatarIcon;
        [SerializeField] private Button selectBtn;

        private SkinData _skinData;

        private void Awake()
        {
            selectBtn.onClick.AddListener(OnClickSelect);
            this.RegisterListener(EventID.SkinElementSelect, (sender, param) =>
            {
                var skinData = (SkinData) param;
                bool isTheSame = skinData.skinName == _skinData.skinName;
                blackBgAva.SetActive(!isTheSame);
            });
        }
        
        public bool IsSkinType(SkinType skin)
        {
            if (_skinData == null)
                return false;

            return _skinData.skinName == skin;
        }

        public void ReloadData()
        {
            InitData(_skinData);
        }

        public void InitData(SkinData skinData)
        {
            _skinData = skinData;
            avatarIcon.sprite = LoadResourceController.Instance.LoadSkinIcon(_skinData.skinName);

            var playerSkins = DataAccountPlayer.PlayerSkins;
            var isOwned = playerSkins.IsHaveOwnedSkin(_skinData.skinName);

            lockObj.SetActive(!isOwned);
            blackBgAva.SetActive(!isOwned);

            if (isOwned)
            {
                bool isUsing = playerSkins.skinUsing == _skinData.skinName;

                tickObj.SetActive(isUsing);
                ownedBg.SetActive(isUsing);
            }
            else
            {
                ownedBg.SetActive(false);
                tickObj.SetActive(false);
            
                // if (_skinData.rescueMapId > 0)
                // {
                //     if (!DataAccountPlayer.PlayerPlatformerCampaign.IsMapOpened(_skinData.rescueMapId))
                //     {
                //         unlockTxt.gameObject.SetActive(true);
                //         unlockTxt.text = $"Unlocked at Level {_skinData.rescueMapId}";
                //     }
                //     else
                //     {
                //         unlockTxt.gameObject.SetActive(false);
                //     }
                // }
                // else
                // {
                //     unlockTxt.gameObject.SetActive(false);
                // }
            }
        }
        public void OnClickSelect()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            this.PostEvent(EventID.SkinElementSelect, _skinData);
        }
    }
}
