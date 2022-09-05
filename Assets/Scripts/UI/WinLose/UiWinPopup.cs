using System.Collections;
using Core.Common;
using Core.Common.GameResources;
using Core.Common.Popup;
using UnityEngine;
using UnityEngine.UI;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UI.LoadingScene;

namespace UI.WinLose
{
    public class UiWinPopup : PopupBase
    {
        [SerializeField] private Button adsBtn, closeBtn;
        [SerializeField] private Text rewardBonus;
        [SerializeField] private Text diamondWinTxt;
        [SerializeField] private Text goldWinTxt;
        [SerializeField] private Text moneyBonusTxt;
         
        [SerializeField] private float timeToShowThanksBtn;
        [SerializeField] private float resetTimeShowThanks;

        [SerializeField] private Transform posArrow;
        [SerializeField] private ArrowMove _arrowMove;

        [SerializeField] private Transform goldReward;
        [SerializeField] private Transform goldTarget;
        [SerializeField] private GameObject goldPrefabs;
        
        [SerializeField] private Transform diamondReward;
        [SerializeField] private Transform diamondTarget;
        [SerializeField] private GameObject diamondPrefabs;

        #region posX123

         [SerializeField] private Transform pos1;
         [SerializeField] private Transform pos2;
         [SerializeField] private Transform pos3;
         [SerializeField] private Transform pos4;
         [SerializeField] private Transform pos5;
         [SerializeField] private Transform pos6;
         

         #endregion

        private int _diamondWin = 1;
        private int _goldWin = 100;
        private int _moneyBonus;

        private void OnEnable()
        {
            timeToShowThanksBtn = resetTimeShowThanks;
            closeBtn.gameObject.SetActive(false);
        }

        private void Awake()
        {
            closeBtn.gameObject.SetActive(false);
            adsBtn.onClick.AddListener(OnClickAdsBtn);
            closeBtn.onClick.AddListener(OnClickCloseBtn);
            
            diamondWinTxt.text = "+ " + _diamondWin;
        }

        private void Update()
        {
            CheckReward();
            ShowThanksBtn();
        }

        private void OnClickAdsBtn()
        {
            _arrowMove.isMove = false;
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Gold, int.Parse((moneyBonusTxt.text)));
            DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Diamond, _diamondWin);
            StartCoroutine(RewardGold(int.Parse(moneyBonusTxt.text)));
            StartCoroutine(RewardDiamond(_diamondWin));
            DOVirtual.DelayedCall(2f, delegate
            {
                PopupController.Instance.CloseCurrentPopupAndOpenParent();
                GameManager.Instance.LoadScene(SceneName.PlatformerMainMenu);
            });
        }

        private void OnClickCloseBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Gold, _goldWin);
            DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Diamond, _diamondWin);
            StartCoroutine(RewardGold(_goldWin));
            StartCoroutine(RewardDiamond(_diamondWin));
            DOVirtual.DelayedCall(2f, delegate
            {
                PopupController.Instance.CloseCurrentPopupAndOpenParent();
                GameManager.Instance.LoadScene(SceneName.PlatformerMainMenu);
            });
            
        }

        protected override void OnShow(){}

        protected override void OnHide(){}
        
        private void ShowThanksBtn()
        {
            timeToShowThanksBtn -= Time.deltaTime;
            if (timeToShowThanksBtn >= 0) return;
            closeBtn.gameObject.SetActive(true);
        }

        private void CheckReward()
        {
            if(posArrow.position.x<pos2.position.x|| posArrow.position.x > pos5.position.x)
                moneyBonusTxt.text="+" + _goldWin * 2;
            if (posArrow.position.x >= pos2.position.x && posArrow.position.x < pos3.position.x ||
                posArrow.position.x >= pos4.position.x && posArrow.position.x < pos5.position.x)
                moneyBonusTxt.text = "+" + _goldWin * 3;
            if (posArrow.position.x > pos3.position.x && posArrow.position.x < pos4.position.x)
                moneyBonusTxt.text = "+" + _goldWin * 5;
        }
        
        private IEnumerator RewardGold(int amount, float timeScale = 1f)
        {
            goldReward.gameObject.SetActive(true);
            for (var i = 0; i < 5; i++)
            {
                var gold = Instantiate(goldPrefabs, goldReward).transform;
                //gold.localRotation = Random.rotation;
                gold.DOLocalMove(Random.insideUnitSphere, Random.Range(0.3f, 0.7f) / timeScale).SetEase(Ease.OutCubic);
                //gold.DOLocalRotate(Random.rotation.eulerAngles, 10).SetEase(Ease.Linear);
            }

            yield return new WaitForSeconds(0.7f / timeScale);

            var counter = 0;
            var goldCount = goldReward.childCount;
            var n = amount / goldCount;
            var a = amount % goldCount;
            for (var i = 0; i < goldReward.childCount; i++)
            {
                var gold = goldReward.GetChild(i);
                gold.DOMove(goldTarget.transform.position, timeScale)
                    .SetEase(Ease.InCubic)
                    .SetDelay(i * 0.05f)
                    .OnComplete(() =>
                    {
                        gold.gameObject.SetActive(false);
                        counter += n + (a > 0 ? 1 : 0);
                        a--;
                        if (counter <= amount)
                        {
                        }
                    });
            }
        }
        
        private IEnumerator RewardDiamond(int amount, float timeScale = 1f)
        {
            diamondReward.gameObject.SetActive(true);
            for (var i = 0; i < 1; i++)
            {
                var diamond = Instantiate(diamondPrefabs, diamondReward).transform;
                //diamond.localRotation = Random.rotation;
                diamond.DOLocalMove(Random.insideUnitSphere, Random.Range(0.3f, 0.7f) / timeScale).SetEase(Ease.OutCubic);
                //diamond.DOLocalRotate(Random.rotation.eulerAngles, 10).SetEase(Ease.Linear);
            }

            yield return new WaitForSeconds(0.7f / timeScale);

            var counter = 0;
            var diamondCount = diamondReward.childCount;
            var n = amount / diamondCount;
            var a = amount % diamondCount;
            for (var i = 0; i < diamondReward.childCount; i++)
            {
                var diamond = diamondReward.GetChild(i);
                diamond.DOMove(diamondTarget.transform.position, timeScale)
                    .SetEase(Ease.InCubic)
                    .SetDelay(i * 0.05f)
                    .OnComplete(() =>
                    {
                        diamond.gameObject.SetActive(false);
                        counter += n + (a > 0 ? 1 : 0);
                        a--;
                        if (counter <= amount)
                        {
                        }
                    });
            }
        }
    }
}