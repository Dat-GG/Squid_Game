using System;
using System.Collections;
using Core.Common;
using Core.Common.GameResources;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using EasyUI.PickerWheelUI;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger4.UI.Spin
{
    public class ModuleUiPowerUpBtn4 : MonoBehaviour
    {
        public bool isHaveGiantPower;
        public bool isHavePowerPower;
        public bool isHaveFreezePower;
        public bool isHaveBananaPower;
        
        [SerializeField] private Button bananaBtn;
        [SerializeField] private Button bonusTimeBtn;
        [SerializeField] private Button freezeBtn;
        [SerializeField] private Button giantBtn;
        [SerializeField] private Button powerBtn;
        [SerializeField] private Button diamondSpinBtn;
        [SerializeField] private Button adsSpinBtn;
        [SerializeField] private Button freeSpinBtn;
        [SerializeField] private Button closeSpinBtn;
        [SerializeField] private Image bananaImg;
        [SerializeField] private Image bonusTimeImg;
        [SerializeField] private Image freezeImg;
        [SerializeField] private Image giantImg;
        [SerializeField] private Image powerImg;
        [SerializeField] private PickerWheel pickerWheel;
        [SerializeField] private GameObject spinWheel;
        [SerializeField] private Text costSpin;
        [SerializeField] private Text cooldownTxt;
        [SerializeField] private Transform powerUpTarget;
        
        private TimerCountdown.TimerCountdown _timerCountdown;
        private UiController _uiController;
        private Coroutine _countDownCor;
        private const int NumberOfAdCanWatch = 5;

        private void Awake()
        {
            _uiController = FindObjectOfType<UiController>();
            diamondSpinBtn.onClick.AddListener(OnCliCkDiamondSpinBtn);
            adsSpinBtn.onClick.AddListener(OnClickAdsSpinBtn);
            freeSpinBtn.onClick.AddListener(OnClickFreeSpinBtn);
            giantBtn.onClick.AddListener(OnClickGiantBtn);
            powerBtn.onClick.AddListener(OnClickPowerBtn);
            bonusTimeBtn.onClick.AddListener(OnClickBonusTimeBtn);
            bananaBtn.onClick.AddListener(OnClickBananaBtn);
            freezeBtn.onClick.AddListener(OnClickFreezeBtn);
            closeSpinBtn.onClick.AddListener(OnClickCloseSpinBtn);
            isHaveBananaPower = false;
            isHaveFreezePower = false;
            isHaveGiantPower = false;
            isHavePowerPower = false;
        }

        private void Start()
        {
            _timerCountdown = FindObjectOfType<TimerCountdown.TimerCountdown>();
            var power = DataAccountPlayer.PlayerChallenger4PowerUp.GetPowerUp(PowerUpType.Power) > 0;
            var banana = DataAccountPlayer.PlayerChallenger4PowerUp.GetPowerUp(PowerUpType.Banana) > 0;
            var giant = DataAccountPlayer.PlayerChallenger4PowerUp.GetPowerUp(PowerUpType.Giant) > 0;
            var freeze = DataAccountPlayer.PlayerChallenger4PowerUp.GetPowerUp(PowerUpType.Freeze) > 0;
            var time = DataAccountPlayer.PlayerChallenger4PowerUp.GetPowerUp(PowerUpType.BonusTime4) > 0;
            powerBtn.gameObject.SetActive(power);
            bananaBtn.gameObject.SetActive(banana);
            giantBtn.gameObject.SetActive(giant);
            freezeBtn.gameObject.SetActive(freeze);
            bonusTimeBtn.gameObject.SetActive(time);
            if (power || banana || giant || freeze || time || DataAccountPlayer.PlayerChallenger4PowerUp.CountSpin < 1) return;
            _uiController.panelBlock.SetActive(true);
            _uiController.settingBtn.gameObject.SetActive(false);
            spinWheel.gameObject.SetActive(true);
            closeSpinBtn.gameObject.SetActive(true);
            LoadData();
            DataAccountPlayer.PlayerChallenger4PowerUp.SpinCost = DataAccountPlayer.PlayerChallenger4PowerUp.CountSpin <= 1 ? 0 : 1;
            costSpin.text = DataAccountPlayer.PlayerChallenger4PowerUp.SpinCost.ToString();
        }

        private void Update()
        {
            CheckPowerUp();
        }
        
        private IEnumerator CountDownWatchAd(long _remainTime)
        {
            cooldownTxt.text = $"Free spin in:\n{GetCurrentTimeString(_remainTime)}";

            while (_remainTime > 0)
            {
                yield return new WaitForSeconds(1);
                _remainTime--;
                cooldownTxt.text = $"Free spin in:\n{GetCurrentTimeString(_remainTime)}";
            }

            LoadData();
        }

        private string GetCurrentTimeString(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return timeSpan.ToString(@"hh\:mm\:ss");
        }
        
        private void LoadData()
        {
            var lastTimeSpin = DataAccountPlayer.Challenger4Spin.lastTimeSpin;
            if (UtilityGame.GetCurrentTime() - lastTimeSpin >= 86400)
            {
                freeSpinBtn.gameObject.SetActive(true);
                adsSpinBtn.gameObject.SetActive(false);
                diamondSpinBtn.gameObject.SetActive(false);
            }
            else
            {
                freeSpinBtn.gameObject.SetActive(false);
                var numberOfAdWatched = DataAccountPlayer.Challenger4Spin.adWatchInDay;
                bool isCanWatchAd = numberOfAdWatched < NumberOfAdCanWatch;
                adsSpinBtn.gameObject.SetActive(isCanWatchAd);
                diamondSpinBtn.gameObject.SetActive(isCanWatchAd);
                
                long remainTime = 86400 - (UtilityGame.GetCurrentTime() - lastTimeSpin);

                if (_countDownCor != null)
                {
                    StopCoroutine(_countDownCor);
                }
                _countDownCor = StartCoroutine(CountDownWatchAd(remainTime));
            }
        }
        
        private void PowerUpImgAnimation(Component image)
        {
            image.gameObject.SetActive(true);
            image.transform.DOScale(0, 0);
            image.transform.DOScale(1.5f, 2f);
            DOVirtual.DelayedCall(2f, delegate
            {
                image.transform.DOScale(0.3f, 1f);
                image.transform.DOMove(powerUpTarget.position, 1f)
                    .SetEase(Ease.InCubic)
                    .OnComplete(() =>
                    {
                        image.gameObject.SetActive(false);
                    });
            });
        }

        private void ShowPowerUpBtn(Component image, Component button)
        {
            DOVirtual.DelayedCall(0.5f, delegate
            {
                PowerUpImgAnimation(image);
                DOVirtual.DelayedCall(3f, delegate
                {
                    button.gameObject.SetActive(true);
                    _uiController.panelBlock.SetActive(false);
                });
            });
        }

        private void OnClickCloseSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            spinWheel.gameObject.SetActive(false);
            closeSpinBtn.gameObject.SetActive(false);
            _uiController.settingBtn.gameObject.SetActive(true);
        }

        private void OnCliCkDiamondSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger4PowerUp.SpinCost) == false) return;
            pickerWheel.Spin();
            closeSpinBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger4PowerUp.SpinCost);
        }
        
        private void OnClickAdsSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            closeSpinBtn.gameObject.SetActive(false);
        }
        
        private void OnClickFreeSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            pickerWheel.Spin();
            DataAccountPlayer.Challenger4Spin.OnNormalSpin();
            LoadData();
            closeSpinBtn.gameObject.SetActive(false);
        }

        private void OnClickBonusTimeBtn()
        {
            _timerCountdown.gameDuration += 20;
            bonusTimeBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger4PowerUp.SetPowerUp(
                false, PowerUpType.BonusTime4, DataAccountPlayer.PlayerChallenger4PowerUp.TimeUsePowerUp);
        }
        
        private void OnClickGiantBtn()
        {
            isHaveGiantPower = true;
            giantBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger4PowerUp.SetPowerUp(
                false, PowerUpType.Giant, DataAccountPlayer.PlayerChallenger4PowerUp.TimeUsePowerUp);
        }
        
        private void OnClickBananaBtn()
        {
            isHaveBananaPower = true;
            bananaBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger4PowerUp.SetPowerUp(
                false, PowerUpType.Banana, DataAccountPlayer.PlayerChallenger4PowerUp.TimeUsePowerUp);
        }
        
        private void OnClickPowerBtn()
        {
            isHavePowerPower = true;
            powerBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger4PowerUp.SetPowerUp(false, PowerUpType.Power,
                DataAccountPlayer.PlayerChallenger4PowerUp.TimeUsePowerUp);
        }
        
        private void OnClickFreezeBtn()
        {
            isHaveFreezePower = true;
            freezeBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger4PowerUp.SetPowerUp(
                false, PowerUpType.Freeze, DataAccountPlayer.PlayerChallenger4PowerUp.TimeUsePowerUp);
        }
        
        private void CheckPowerUp()
        {
            var data = DataAccountPlayer.PlayerChallenger4PowerUp;
            pickerWheel.OnSpinEnd(wheelPiece  =>
            {
                switch (wheelPiece.Label)
                {
                    case "Time":
                        ShowPowerUpBtn(bonusTimeImg, bonusTimeBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.BonusTime4, data.TimeUsePowerUp);
                        break;
                    case "Giant":
                        ShowPowerUpBtn(giantImg, giantBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.Giant, data.TimeUsePowerUp);
                        break;
                    case "Power":
                        ShowPowerUpBtn(powerImg, powerBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.Power, data.TimeUsePowerUp);
                        break;
                    case "Banana":
                        ShowPowerUpBtn(bananaImg, bananaBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.Banana, data.TimeUsePowerUp);
                        break;
                    case "Freeze":
                        ShowPowerUpBtn(freezeImg, freezeBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.Freeze, data.TimeUsePowerUp);
                        break;
                }

                DOVirtual.DelayedCall(0.5f, delegate
                {
                    diamondSpinBtn.gameObject.SetActive(false);
                    spinWheel.gameObject.SetActive(false);
                    _uiController.settingBtn.gameObject.SetActive(true);
                });
            });
        }
    }
}
