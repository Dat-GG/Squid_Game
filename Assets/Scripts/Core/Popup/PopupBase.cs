using UnityEngine;
using System;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;

namespace Core.Common.Popup
{
    public abstract class PopupBase : MonoBehaviour
    {
        public PopupType popupType;
        [NonSerialized] public PopupType ParentPopupType;
        private bool _isOpen;
        
        public virtual bool CanClose => true;

        public void Open()
        {
            if (_isOpen)
            {
                return;
            }

            _isOpen = true;
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
            OnShow();
        }

        public virtual void Close()
        {
            if (_isOpen is false)
            {
                return;
            }

            if (PopupController.Instance != null)
            {
                SoundManager.Instance.PlaySound(SoundType.PopupClose);
                PopupController.Instance.CloseCurrentPopupAndOpenParent();
            }
        }

        public void ClosePanelOnly()
        {
            _isOpen = false;
            gameObject.SetActive(false);
            OnHide();
        }

        protected abstract void OnShow();

        protected abstract void OnHide();
    }
}