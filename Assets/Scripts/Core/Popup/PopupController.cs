using System;
using System.Collections.Generic;
using Core.Load;
using Plugins.Scripts.Core.Common.Load;
using UI.HUD;
using UI.Notification;
using UnityEngine;

namespace Core.Common.Popup
{
    public class PopupController : SingletonMono<PopupController>
    {
        private ModuleUiHud _moduleUiHud;
        private List<PopupBase> _currrentPanelOpen = new List<PopupBase>();
        private Dictionary<PopupType, List<PopupBase>> _container = new Dictionary<PopupType, List<PopupBase>>();
        private RectTransform _canvasRect;

        #region CallMethod

        public ModuleUiHud GetHud()
        {
            if (_moduleUiHud is null)
            {
                _moduleUiHud = (ModuleUiHud) GetPanelInCache(PopupType.Hud);
            }

            return _moduleUiHud;
        }

        public void ShowHud()
        {
            if (_moduleUiHud is null)
            {
                _moduleUiHud = (ModuleUiHud) GetPanelInCache(PopupType.Hud);
            }
            
            _moduleUiHud.gameObject.SetActive(true);
            _moduleUiHud.transform.SetAsLastSibling();
        }

        public void HideHud()
        {
            if (_moduleUiHud != null)
            {
                _moduleUiHud.gameObject.SetActive(false);
            }
        }

        public PopupBase OpenPopupAndKeepParent(PopupType panelType)
        {
            var panel = GetPanelInCache(panelType);
            OpenPopUp(panel);
            return panel;
        }
        
        public PopupBase OpenPopupAndKeepParentWithHud(PopupType panelType)
        {
            var panel = GetPanelInCache(panelType);
            OpenPopUp(panel);
            ShowHud();
            return panel;
        } 

        public PopupBase CloseCurrentPopupAndOpenParent()
        {
            var panel = _currrentPanelOpen[_currrentPanelOpen.Count - 1];
            ClosePopUp(panel);
            return panel;
        }

        public void OpenNotification(string content, Action closeAction = null)
        {
            this.StartDelayMethod(0.2f, () =>
            {
                var moduleNotification =
                    (ModuleUiNotification) OpenPopupAndKeepParent(PopupType.NotificationPopup);
                moduleNotification.InitData(content, closeAction);
            });
        }

        #endregion

        #region PrivateProcessMethod

        private void OpenPopUp(PopupBase panel)
        {
            _currrentPanelOpen.Add(panel);
            panel.Open();
        }

        private void ClosePopUp(PopupBase panel)
        {
            this.StartDelayMethod(0.01f, () =>
            {
                _currrentPanelOpen.RemoveAt(_currrentPanelOpen.Count - 1);
            });
            panel.ClosePanelOnly();
        }

        private PopupBase GetPanelInCache(PopupType panelType)
        {
            if (panelType is PopupType.None)
            {
                CloseAllPanel();
                return null;
            }

            if (_container.ContainsKey(panelType))
            {
                var allBase = _container[panelType];
                foreach (var panel in allBase)
                {
                    if (panel.gameObject.activeInHierarchy is false)
                    {
                        return panel;
                    }
                }

                var panelClone = LoadFromResources(panelType);
                allBase.Add(panelClone);
                return panelClone;
            }
            else
            {
                var panelClone = LoadFromResources(panelType);
                _container[panelType] = new List<PopupBase> {panelClone};
                return panelClone;
            }
        }

        private PopupBase LoadFromResources(PopupType type)
        {
            var obj = LoadResourceController.Instance.LoadPanel(type);
            if (obj == null) return null;

            var panelClone = Instantiate(obj, transform, false);
            return panelClone;

        }

        public void CloseAllPanel()
        {
            foreach (var panel in _currrentPanelOpen)
            {
                panel.ClosePanelOnly();
            }

            _currrentPanelOpen.Clear();
        }

        public RectTransform GetRectCanvas()
        {
            if (_canvasRect == null)
            {
                _canvasRect = GetComponent<RectTransform>();
            }

            return _canvasRect;
        }

        #endregion

        #region Check Method

        public bool IsHavePopUpOpen(PopupType panelType)
        {
            if (_container.ContainsKey(panelType) is false)
            {
                return false;
            }
            else
            {
                var listPanels = _container[panelType];
                foreach (var panel in listPanels)
                {
                    if (panel.gameObject.activeInHierarchy)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsHaveAnyPopUpOpening()
        {
            return _currrentPanelOpen.Count > 0;
        }

        #endregion
    }
}