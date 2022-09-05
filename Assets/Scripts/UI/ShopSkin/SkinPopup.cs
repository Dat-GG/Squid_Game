using System.Collections.Generic;
using Core.Common.Popup;
using Core.Common.Sound;
using Core.Load;
using Plugins.Scripts.Core.Common.GameResources;
using Plugins.Scripts.Core.Common.Load;
using Plugins.Scripts.Core.Common.Sound;
using Plugins.Scripts.UI.ShopSkin.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Scripts.UI.ShopSkin
{
    public class SkinPopup : PopupBase
    {
        [SerializeField] private SkinElement skinElement;
        [SerializeField] private RectTransform skinParent;
        [SerializeField] private Button closeBtn;

        private SkinDataCollection _skinDataCollection;
        private List<SkinElement> _allElements = new List<SkinElement>();
        
        private void Awake()
        {
            closeBtn.onClick.AddListener(OnClickCloseBtn);
            _skinDataCollection = LoadResourceController.Instance.LoadSkinDataCollection();
            this.RegisterListener(EventID.SkinUsingSelected, (sender, param) =>
            {
                ReloadData();
            });
            
            this.RegisterListener(EventID.SkinUnlockSuccess, (sender, param) =>
            {
                ReloadData();
            });
        }

        private void OnClickCloseBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            Close();
        }
        
        public void LoadSkin(SkinType skin)
        {
            var listSkins = _skinDataCollection.GetSkinCollection(skin, out var skinUnlockType);
            LoadElements(listSkins);

            bool isSelected = false;
            foreach (var element in _allElements)
            {
                if (element.IsSkinType(skin))
                {
                    element.OnClickSelect();
                    isSelected = true;
                    break;
                }
            }

            if (!isSelected)
            {
                _allElements[0].OnClickSelect();
            }
        }

        public void LoadCoinSkins()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            LoadElements(_skinDataCollection.coinSkins);
        }
        
        private void ReloadData()
        {
            foreach (var element in _allElements)
            {
                if (element.gameObject.activeSelf)
                    element.ReloadData();
            }
        }

        private void LoadElements(List<SkinData> data)
        {
            HideAllElement();

            foreach (var skinData in data)
            {
                var element = GetElement();
                element.InitData(skinData);
                element.gameObject.SetActive(true);
            }
        }

        private SkinElement GetElement()
        {
            foreach (var element in _allElements)
            {
                if (!element.gameObject.activeSelf)
                    return element;
            }

            var elementClone = Instantiate(skinElement, skinParent, false);
            _allElements.Add(elementClone);
            return elementClone;
        }

        private void HideAllElement()
        {
            foreach (var element in _allElements)
            {
                element.gameObject.SetActive(false);
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
