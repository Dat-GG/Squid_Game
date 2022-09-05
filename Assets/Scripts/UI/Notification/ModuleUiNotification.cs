using System;
using Core.Common.Popup;
using Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Notification
{
    public class ModuleUiNotification : PopupBase
    {
        [SerializeField] private Text notification;
        [SerializeField] private Button backgroundBtn;

        private Action _closeAction;

        private void Start()
        {
            backgroundBtn.onClick.AddListener(OnClickClose);
        }

        private void OnClickClose()
        {
            Close();
            _closeAction?.Invoke();
            _closeAction = null;
        }

        public void InitData(string content, Action closeAction = null)
        {
            _closeAction = closeAction;
            notification.text = content;
        }

        protected override void OnShow() { }

        protected override void OnHide() { }
    }
}