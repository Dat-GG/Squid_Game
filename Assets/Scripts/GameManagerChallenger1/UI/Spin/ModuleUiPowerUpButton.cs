using System;
using System.Collections;
using Core.Common;
using Core.Common.GameResources;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using EasyUI.PickerWheelUI;
using GameManagerChallenger1.Player;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger1.UI.Spin
{
    public class ModuleUiPowerUpButton : MonoBehaviour
    {
        public bool isHaveShield;
        public bool isHaveGun;
        public bool isUnderground;
        
        [SerializeField] private Button bonusSpeedBtn;
        [SerializeField] private Button bonusTimeBtn;
        [SerializeField] private Button shieldBtn;
        [SerializeField] private Button gunBtn;
        [SerializeField] private Button undergroundBtn;
        [SerializeField] private Button freeSpinBtn;
        [SerializeField] private Button diamondSpinBtn;
        [SerializeField] private Button adsSpinBtn;
        [SerializeField] private Button closeSpinBtn;
        
        [SerializeField] private Image bonusSpeedImg;
        [SerializeField] private Image bonusTimeImg;
        [SerializeField] private Image shieldImg;
        [SerializeField] private Image gunImg;
        [SerializeField] private Image undergroundImg;
        
        [SerializeField] private PickerWheel pickerWheel;
        [SerializeField] private GameObject spinWheel;
        [SerializeField] private Text costSpin;
        [SerializeField] private Text cooldownTxt;
        [SerializeField] private Transform powerUpTarget;
        
        private PlayerController _playerController;
        private UiController1 _uiController1;
        private Coroutine _countDownCor;
        private const int NumberOfAdCanWatch = 5;

        private void Awake()
        {
            _uiController1 = FindObjectOfType<UiController1>();
            bonusSpeedBtn.onClick.AddListener(OnClickBonusSpeedBtn);
            bonusTimeBtn.onClick.AddListener(OnClickBonusTimeBtn);
            shieldBtn.onClick.AddListener(OnClickShieldBtn);
            gunBtn.onClick.AddListener(OnClickGunBtn);
            undergroundBtn.onClick.AddListener(OnClickUndergroundBtn);
            diamondSpinBtn.onClick.AddListener(OnClickDiamondSpinBtn);
            adsSpinBtn.onClick.AddListener(OnClickAdsSpinBtn);
            freeSpinBtn.onClick.AddListener(OnClickFreeSpinBtn);
            closeSpinBtn.onClick.AddListener(OnClickCloseSpinBtn);
            isHaveShield = false;
            isHaveGun = false;
            isUnderground = false;
        }
        
        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            var isHaveGunPower = DataAccountPlayer.PlayerChallenger1PowerUp.GetPowerUp(PowerUpType.Gun) > 0;
            var isHaveShieldPower = DataAccountPlayer.PlayerChallenger1PowerUp.GetPowerUp(PowerUpType.Shield) > 0;
            var isUndergroundPower = DataAccountPlayer.PlayerChallenger1PowerUp.GetPowerUp(PowerUpType.UnderGround) > 0;
            var isHaveSpeedPower = DataAccountPlayer.PlayerChallenger1PowerUp.GetPowerUp(PowerUpType.BonusSpeed) > 0;
            var isHaveTimePower = DataAccountPlayer.PlayerChallenger1PowerUp.GetPowerUp(PowerUpType.BonusTime) > 0;
            gunBtn.gameObject.SetActive(isHaveGunPower);
            shieldBtn.gameObject.SetActive(isHaveShieldPower);
            undergroundBtn.gameObject.SetActive(isUndergroundPower);
            bonusSpeedBtn.gameObject.SetActive(isHaveSpeedPower);
            bonusTimeBtn.gameObject.SetActive(isHaveTimePower);
            if (isHaveGunPower || isUndergroundPower || isHaveShieldPower || isHaveSpeedPower || isHaveTimePower ||
                DataAccountPlayer.PlayerChallenger1PowerUp.CountSpin < 1) return;
            _uiController1.settingBtn.gameObject.SetActive(false);
            _uiController1.blackBg.gameObject.SetActive(true);
            _uiController1.panelBlock.SetActive(true);
            spinWheel.gameObject.SetActive(true);
            closeSpinBtn.gameObject.SetActive(true);
            LoadData();
            DataAccountPlayer.PlayerChallenger1PowerUp.SpinCost = DataAccountPlayer.PlayerChallenger1PowerUp.CountSpin <= 1 ? 0 : 1;
            costSpin.text = DataAccountPlayer.PlayerChallenger1PowerUp.SpinCost.ToString();
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
            var lastTimeSpin = DataAccountPlayer.Challenger1Spin.lastTimeSpin;
            if (UtilityGame.GetCurrentTime() - lastTimeSpin >= 86400)
            {
                freeSpinBtn.gameObject.SetActive(true);
                adsSpinBtn.gameObject.SetActive(false);
                diamondSpinBtn.gameObject.SetActive(false);
            }
            else
            {
                freeSpinBtn.gameObject.SetActive(false);
                var numberOfAdWatched = DataAccountPlayer.Challenger1Spin.adWatchInDay;
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

        private void OnClickAdsSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            closeSpinBtn.gameObject.SetActive(false);
        }

        private void OnClickCloseSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            spinWheel.gameObject.SetActive(false);
            closeSpinBtn.gameObject.SetActive(false);
            _uiController1.blackBg.gameObject.SetActive(false);
            _uiController1.settingBtn.gameObject.SetActive(true);
        }

        private void OnClickDiamondSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, DataAccountPlayer.PlayerChallenger1PowerUp.SpinCost) == false) return;
            pickerWheel.Spin();
            closeSpinBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond,
                DataAccountPlayer.PlayerChallenger1PowerUp.SpinCost);
        }

        private void OnClickFreeSpinBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            pickerWheel.Spin();
            DataAccountPlayer.Challenger1Spin.OnNormalSpin();
            LoadData();
            closeSpinBtn.gameObject.SetActive(false);
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
                    _uiController1.panelBlock.SetActive(false);
                });
            });
        }

        private void OnClickBonusSpeedBtn()
        {
            _playerController.speed *= 2;
            _playerController.isHaveSpeed = true;
            bonusSpeedBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger1PowerUp.SetPowerUp(
                false, PowerUpType.BonusSpeed, DataAccountPlayer.PlayerChallenger1PowerUp.TimeUsePowerUp);
        }

        private void OnClickBonusTimeBtn()
        {
            CountdownTime.CountdownTime.Instance.gameDuration += 30;
            bonusTimeBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger1PowerUp.SetPowerUp(
                false, PowerUpType.BonusTime, DataAccountPlayer.PlayerChallenger1PowerUp.TimeUsePowerUp);
        }

        private void OnClickShieldBtn()
        {
            isHaveShield = true;
            shieldBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger1PowerUp.SetPowerUp(
                false, PowerUpType.Shield, DataAccountPlayer.PlayerChallenger1PowerUp.TimeUsePowerUp);
        }

        private void OnClickGunBtn()
        {
            isHaveGun = true;
            gunBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger1PowerUp.SetPowerUp(
                false, PowerUpType.Gun, DataAccountPlayer.PlayerChallenger1PowerUp.TimeUsePowerUp);
        }

        private void OnClickUndergroundBtn()
        {
            isUnderground = true;
            undergroundBtn.gameObject.SetActive(false);
            DataAccountPlayer.PlayerChallenger1PowerUp.SetPowerUp(
                false, PowerUpType.UnderGround, DataAccountPlayer.PlayerChallenger1PowerUp.TimeUsePowerUp);
        }

        private void CheckPowerUp()
        {
            var data = DataAccountPlayer.PlayerChallenger1PowerUp;
            pickerWheel.OnSpinEnd(wheelPiece  =>
            {
                switch (wheelPiece.Label)
                {
                    case "Gun":
                        ShowPowerUpBtn(gunImg, gunBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.Gun, data.TimeUsePowerUp);
                        break;
                    case "Shield":
                        ShowPowerUpBtn(shieldImg, shieldBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.Shield, data.TimeUsePowerUp);
                        break;
                    case "Underground":
                        ShowPowerUpBtn(undergroundImg, undergroundBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.UnderGround, data.TimeUsePowerUp);
                        break;
                    case "Time":
                        ShowPowerUpBtn(bonusTimeImg, bonusTimeBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.BonusTime, data.TimeUsePowerUp);
                        break;
                    case "Speed":
                        ShowPowerUpBtn(bonusSpeedImg, bonusSpeedBtn);
                        data.TimeUsePowerUp = wheelPiece.Amount;
                        data.SetPowerUp(true, PowerUpType.BonusSpeed, data.TimeUsePowerUp);
                        break;
                }

                DOVirtual.DelayedCall(0.5f, delegate
                {
                    spinWheel.gameObject.SetActive(false);
                    _uiController1.blackBg.gameObject.SetActive(false);
                    _uiController1.settingBtn.gameObject.SetActive(true);
                });
            });
        }
    }
}
