using System.Collections;
using Core.Common.GameResources;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
	public class SpinRewardPopup : PopupBase
	{
		[SerializeField] private Button thanksBtn;
		[SerializeField] private Button getMoreReward;
		[SerializeField] private float timeToShowThanksBtn;
		[SerializeField] private Text rewardTxt;
		[SerializeField] private Image rewardIconImg;
		
		[SerializeField] private Transform goldReward;
		[SerializeField] private Transform goldTarget;
		[SerializeField] private GameObject goldPrefabs;
		[SerializeField] private Transform diamondReward;
		[SerializeField] private Transform diamondTarget;
		[SerializeField] private GameObject diamondPrefabs;
		private LuckySpinPopup _popUp;
		private float _restartTime;
		
		private void Awake()
		{
			thanksBtn.onClick.AddListener(OnClickThanks);
			getMoreReward.onClick.AddListener(OnClickMoreReward);
			_popUp = FindObjectOfType<LuckySpinPopup>();
			
			thanksBtn.gameObject.SetActive(false);
		}
		
		protected override void OnShow()
		{
			thanksBtn.gameObject.SetActive(false);
			_restartTime = timeToShowThanksBtn;
		}

		private void Update()
		{
			ShowThanksBtn();
			rewardTxt.text = DataAccountPlayer.PlayerSettings.Reward;
			rewardIconImg.sprite = _popUp.RewardIcon;
		}

		private void OnClickThanks()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			switch (_popUp.LabelReward)
			{
				case "Gold":
					StartCoroutine(RewardGold(_popUp.Money));
					break;
				case "Diamond":
					StartCoroutine(RewardDiamond(_popUp.Money));
					break;
			}

			DOVirtual.DelayedCall(2.5f, Close);
		}

		private void OnClickMoreReward()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
		}

		private void ShowThanksBtn()
		{
			timeToShowThanksBtn -= Time.deltaTime;
			if (timeToShowThanksBtn >= 0) return;
			thanksBtn.gameObject.SetActive(true);
		}

		protected override void OnHide()
		{
			timeToShowThanksBtn = _restartTime;
			_popUp.RewardPanel.gameObject.SetActive(true);
			
			switch (_popUp.LabelReward)
			{
				case "Gold":
					DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Gold, _popUp.Money);
					break;
				case "Diamond":
					DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Diamond, _popUp.Money);
					break;
			}
		}
		
		private IEnumerator RewardGold(int amount, float timeScale = 1f)
        {
            goldReward.gameObject.SetActive(true);
            for (var i = 0; i < 5; i++)
            {
                var gold = Instantiate(goldPrefabs, goldReward).transform;
                gold.localRotation = Random.rotation;
                gold.DOLocalMove(Random.insideUnitSphere, Random.Range(0.3f, 0.7f) / timeScale).SetEase(Ease.OutCubic);
                gold.DOLocalRotate(Random.rotation.eulerAngles, 10).SetEase(Ease.Linear);
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
            for (var i = 0; i < 2; i++)
            {
                var diamond = Instantiate(diamondPrefabs, diamondReward).transform;
                diamond.localRotation = Random.rotation;
                diamond.DOLocalMove(Random.insideUnitSphere, Random.Range(0.3f, 0.7f) / timeScale).SetEase(Ease.OutCubic);
                diamond.DOLocalRotate(Random.rotation.eulerAngles, 10).SetEase(Ease.Linear);
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