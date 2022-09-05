using Challenger5.Gameplay.Managers;
using Challenger5.Player;
using Core.Common.GameResources;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger5.UI.Spin
{
    public class ModuleUiPowerUpBtn5 : MonoBehaviour
    {
        public bool isHaveKatana;
        public bool isHaveGiant;
        public bool isHaveHammer;
        public bool isHaveTumble;
        public bool isHaveShield;

        [SerializeField] private Button katanaBtn;
        [SerializeField] private Button giantBtn;
        [SerializeField] private Button hammerBtn;
        [SerializeField] private Button tumbleBtn;
        [SerializeField] private Button shieldBtn;

        [SerializeField] private Button diamondBuyKatana;
        [SerializeField] private Button diamondBuyGiant;
        [SerializeField] private Button diamondBuyHammer;
        [SerializeField] private Button diamondBuyTumble;
        [SerializeField] private Button diamondBuyShield;

        [SerializeField] private Text costBuyKatana;
        [SerializeField] private Text costBuyGiant;
        [SerializeField] private Text costBuyHammer;
        [SerializeField] private Text costBuyTumble;
        [SerializeField] private Text costBuyShield;

        [SerializeField] private Button adsBuyKatana;
        [SerializeField] private Button adsBuyGiant;
        [SerializeField] private Button adsBuyHammer;
        [SerializeField] private Button adsBuyTumble;
        [SerializeField] private Button adsBuyShield;
        
        [SerializeField] private Image katanaImg;
        [SerializeField] private Image giantImg;
        [SerializeField] private Image hammerImg;
        [SerializeField] private Image tumbleImg;
        [SerializeField] private Image shieldImg;

        [SerializeField] private Image fillKatana;
        [SerializeField] private Image fillGiant;
        [SerializeField] private Image fillHammer;
        [SerializeField] private Image fillTumble;
        [SerializeField] private Image fillShield;
        
        [SerializeField] private Button closeBoosterBtn;
        [SerializeField] private float maxTimeCdKatana;
        [SerializeField] private float maxTimeCdGiant;
        [SerializeField] private float maxTimeCdHammer;
        [SerializeField] private float maxTimeCdTumble;
        [SerializeField] private float maxTimeCdShield;
        [SerializeField] private GameObject boosterStore;
        [SerializeField] private GameObject blackBg;
        [SerializeField] private Transform powerUpTarget;
        private float _timeLeft;
        private bool _isBuyPowerUp;
        private PlayerController5 _playerController5;
        private GameManagerChallenger5 _gameManagerChallenger5;
        private UiController5 _uiController5;
        

        private void Awake()
        {
            katanaBtn.onClick.AddListener(OnClickKatanaBtn);
            giantBtn.onClick.AddListener(OnClickGiantBtn);
            hammerBtn.onClick.AddListener(OnClickHammerBtn);
            tumbleBtn.onClick.AddListener(OnClickTumbleBtn);
            shieldBtn.onClick.AddListener(OnClickShieldBtn);
            
            diamondBuyKatana.onClick.AddListener(OnClickDiamondBuyKatana);
            diamondBuyGiant.onClick.AddListener(OnClickDiamondBuyGiant);
            diamondBuyHammer.onClick.AddListener(OnClickDiamondBuyHammer);
            diamondBuyTumble.onClick.AddListener(OnClickDiamondBuyTumble);
            diamondBuyShield.onClick.AddListener(OnClickDiamondBuyShield);
            
            adsBuyKatana.onClick.AddListener(OnClickAdsBuyKatana);
            adsBuyGiant.onClick.AddListener(OnClickAdsBuyGiant);
            adsBuyHammer.onClick.AddListener(OnClickAdsBuyHammer);
            adsBuyTumble.onClick.AddListener(OnClickAdsBuyTumble);
            adsBuyShield.onClick.AddListener(OnClickAdsBuyShield);
            
            closeBoosterBtn.onClick.AddListener(OnClickCloseBoosterBtn);
            _playerController5 = FindObjectOfType<PlayerController5>();
            _gameManagerChallenger5 = FindObjectOfType<GameManagerChallenger5>();
            _uiController5 = FindObjectOfType<UiController5>();
            _isBuyPowerUp = false;
        }

        private void Start()
        {
            if (DataAccountPlayer.PlayerChallenger5PowerUp.FirstGame < 1) return;
            _uiController5.panelBlock.SetActive(true);
            _uiController5.settingBtn.gameObject.SetActive(false);
            boosterStore.SetActive(true);
            closeBoosterBtn.gameObject.SetActive(true);
            blackBg.SetActive(true);
            costBuyGiant.text = DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost.ToString();
            costBuyHammer.text = DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost.ToString();
            costBuyKatana.text = DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost.ToString();
            costBuyShield.text = DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost.ToString();
            costBuyTumble.text = DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost.ToString();
        }

        private void Update()
        {
            if (_playerController5.isUseKatana)
            {
                CountdownPowerUp(fillKatana, katanaBtn, _playerController5.isUseKatana);
            }

            if (_playerController5.isUseGiant)
            {
                CountdownPowerUp(fillGiant, giantBtn, _playerController5.isUseGiant);
            }

            if (_playerController5.isUseHammer)
            {
                CountdownPowerUp(fillHammer, hammerBtn, _playerController5.isUseHammer);
            }

            if (_playerController5.isLostShield)
            {
                CountdownPowerUp(fillShield, shieldBtn, _playerController5.isUseShield);
            }

            if (_playerController5.isUseTumble)
            {
                CountdownPowerUp(fillTumble, tumbleBtn, _playerController5.isUseTumble);
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
            _isBuyPowerUp = true;
            DOVirtual.DelayedCall(0.5f, delegate
            {
                PowerUpImgAnimation(image);
                DOVirtual.DelayedCall(3f, delegate
                {
                    button.gameObject.SetActive(true);
                    _uiController5.panelBlock.gameObject.SetActive(false);
                });
            });
        }

        private void CheckBuyPowerUp()
        {
            diamondBuyGiant.interactable = false;
            diamondBuyHammer.interactable = false;
            diamondBuyKatana.interactable = false;
            diamondBuyShield.interactable = false;
            diamondBuyTumble.interactable = false;
            adsBuyGiant.interactable = false;
            adsBuyHammer.interactable = false;
            adsBuyKatana.interactable = false;
            adsBuyShield.interactable = false;
            adsBuyTumble.interactable = false;
        }

        private void CountdownPowerUp(Image image, Button button, bool usePowerUp)
        {
            _timeLeft -= Time.deltaTime;
            image.fillAmount = _timeLeft / maxTimeCdKatana;
            if (!(_timeLeft <= 0)) return;
            usePowerUp = false;
            button.interactable = true;
            if (_playerController5.isLostShield)
            {
                _playerController5.isLostShield = false;
            }
        }

        private static void CheckFirstBuy()
        {
            if (DataAccountPlayer.PlayerChallenger5PowerUp.FirstTimeBuy > 0) return;
            DataAccountPlayer.PlayerChallenger5PowerUp.FirstTimeBuy++;
            DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost += 5;
        }
        
        
        private void OnClickCloseBoosterBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            boosterStore.SetActive(false);
            closeBoosterBtn.gameObject.SetActive(false);
            blackBg.SetActive(false);
            _uiController5.tapToPlayTxt.gameObject.SetActive(true);
            _uiController5.settingBtn.gameObject.SetActive(true);
            if (_isBuyPowerUp == false)
            {
                _uiController5.panelBlock.gameObject.SetActive(false);
            }
        }

        #region UsePowerUpBtn
        private void OnClickKatanaBtn()
        {
            if(_gameManagerChallenger5.state != GameManagerChallenger5.States.Play) return;
            isHaveKatana = true;
            katanaBtn.interactable = false;
            _timeLeft = maxTimeCdKatana;
            fillKatana.fillAmount = 1;
        }
        
        private void OnClickGiantBtn()
        {
            if(_gameManagerChallenger5.state != GameManagerChallenger5.States.Play) return;
            isHaveGiant = true;
            giantBtn.interactable = false;
            _timeLeft = maxTimeCdGiant;
            fillGiant.fillAmount = 1;
        }
        
        private void OnClickHammerBtn()
        {
            if(_gameManagerChallenger5.state != GameManagerChallenger5.States.Play) return;
            isHaveHammer = true;
            hammerBtn.interactable = false;
            _timeLeft = maxTimeCdHammer;
            fillHammer.fillAmount = 1;
        }
        
        private void OnClickTumbleBtn()
        {
            if(_gameManagerChallenger5.state != GameManagerChallenger5.States.Play) return;
            isHaveTumble = true;
            tumbleBtn.interactable = false;
            _timeLeft = maxTimeCdTumble;
            fillTumble.fillAmount = 1;
        }
        
        private void OnClickShieldBtn()
        {
            if(_gameManagerChallenger5.state != GameManagerChallenger5.States.Play) return;
            isHaveShield = true;
            shieldBtn.interactable = false;
            _timeLeft = maxTimeCdShield;
            fillShield.fillAmount = 1;
        }
        #endregion

        #region DiamondBuyPowerUpBtn
        private void OnClickDiamondBuyKatana()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost) == false) return;
            
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost);
            ShowPowerUpBtn(katanaImg, katanaBtn);
            CheckBuyPowerUp();
            CheckFirstBuy();
        }
        
        private void OnClickDiamondBuyGiant()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost) == false) return;
            
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost);
            ShowPowerUpBtn(giantImg, giantBtn);
            CheckBuyPowerUp();
            CheckFirstBuy();
        }
        
        private void OnClickDiamondBuyHammer()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost) == false) return;
            
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost);
            ShowPowerUpBtn(hammerImg, hammerBtn);
            CheckBuyPowerUp();
            CheckFirstBuy();
        }
        
        private void OnClickDiamondBuyTumble()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost) == false) return;
            
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost);
            ShowPowerUpBtn(tumbleImg, tumbleBtn);
            CheckBuyPowerUp();
            CheckFirstBuy();
        }
        
        private void OnClickDiamondBuyShield()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (DataAccountPlayer.PlayerMoney.IsEnough(MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost) == false) return;
            
            DataAccountPlayer.PlayerMoney.SetMoney(false, MoneyType.Diamond, 
                DataAccountPlayer.PlayerChallenger5PowerUp.DiamondCost);
            ShowPowerUpBtn(shieldImg, shieldBtn);
            CheckBuyPowerUp();
            CheckFirstBuy();
        }
        #endregion

        #region AdsBuyPowerUpBtn
        private void OnClickAdsBuyKatana()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            ShowPowerUpBtn(katanaImg, katanaBtn);
            CheckBuyPowerUp();
        }
        
        private void OnClickAdsBuyGiant()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            ShowPowerUpBtn(giantImg, giantBtn);
            CheckBuyPowerUp();
        }
        
        private void OnClickAdsBuyHammer()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            ShowPowerUpBtn(hammerImg, hammerBtn);
            CheckBuyPowerUp();
        }
        
        private void OnClickAdsBuyTumble()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            ShowPowerUpBtn(tumbleImg, tumbleBtn);
            CheckBuyPowerUp();
        }
        
        private void OnClickAdsBuyShield()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            ShowPowerUpBtn(shieldImg, shieldBtn);
            CheckBuyPowerUp();
        }
        #endregion
    }
}
