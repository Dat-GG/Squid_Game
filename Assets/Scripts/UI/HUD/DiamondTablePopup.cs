using Core.Common.Popup;
using UnityEngine;
using Core.Common.Sound;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine.UI;

public class DiamondTablePopup : PopupBase
{
    [SerializeField] private Button closeBtn, bgBtn, buyBtn, adsBtn;

    [SerializeField] private Text adsDiamondValueTxt;
    [SerializeField] private Text buyDiamondValueTxt;
    private void Awake()
    {
        closeBtn.onClick.AddListener(OnClickCloseBtn);
        bgBtn.onClick.AddListener(OnClickCloseBtn);
        buyBtn.onClick.AddListener(OnClickBuyBtn);
        adsBtn.onClick.AddListener(OnClickAdsBtn);
    }

    private void OnClickCloseBtn()
    {
        SoundManager.Instance.PlaySound(SoundType.ClickButton);
        Close();
    }

    private void OnClickBuyBtn()
    {
        SoundManager.Instance.PlaySound(SoundType.ClickButton);
    }

    private void OnClickAdsBtn()
    {
        SoundManager.Instance.PlaySound(SoundType.ClickButton);
    }

    

    protected override void OnShow()
    {
      
    }

    protected override void OnHide()
    {
        
    }
}
