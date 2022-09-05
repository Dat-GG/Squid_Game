using UnityEngine;
using UnityEngine.UI;

namespace Challenger5.Bot
{
    public class BotPowerUpController : MonoBehaviour
    {
        public Image fillKatana;
        public Image fillGiant;
        public Image fillHammer;
        public Image fillTumble;
        public Image fillShield;
        public float maxTimeCdKatana;
        public float maxTimeCdGiant;
        public float maxTimeCdHammer;
        public float maxTimeCdTumble;
        public float maxTimeCdShield;
        public float timeCountdown;
        [SerializeField] private BotController5 botController5;
        [SerializeField] private Image katana;
        [SerializeField] private Image hammer;
        [SerializeField] private Image giant;
        [SerializeField] private Image tumble;
        [SerializeField] private Image shield;
        private void Start()
        {
            CheckActivePowerUp();
        }
        
        private void Update()
        {
            CountdownKatana();
            CountdownGiant();
            CountdownShield();
            CountdownTumble();
            CountdownHammer();
            CheckOffPowerUp();
        }
        
        private void CheckActivePowerUp()
        {
            if (botController5.isHaveGiant)
            {
                giant.gameObject.SetActive(true);
            }

            if (botController5.isHaveHammer)
            {
                hammer.gameObject.SetActive(true);
            }
            if (botController5.isHaveKatana)
            {
                katana.gameObject.SetActive(true);
            }
            if (botController5.isHaveShield)
            {
                shield.gameObject.SetActive(true);
            }
            if (botController5.isHaveTumble)
            {
                tumble.gameObject.SetActive(true);
            }
        }

        private void CheckOffPowerUp()
        {
            if (!botController5.isDead) return;
            giant.gameObject.SetActive(false);
            hammer.gameObject.SetActive(false);
            katana.gameObject.SetActive(false);
            shield.gameObject.SetActive(false);
            tumble.gameObject.SetActive(false);
        }

        private void CountdownKatana()
        {
            if (botController5.isUseKatana)
            {
                timeCountdown -= Time.deltaTime;
                fillKatana.fillAmount = timeCountdown / maxTimeCdKatana;
                if (timeCountdown <= 0)
                {
                    botController5.isHaveKatana = true;
                    botController5.isUseKatana = false;
                }
            }
        }
        
        private void CountdownGiant()
        {
            if (botController5.isUseGiant)
            {
                timeCountdown -= Time.deltaTime;
                fillGiant.fillAmount = timeCountdown / maxTimeCdGiant;
                if (timeCountdown <= 0)
                {
                    botController5.isHaveGiant = true;
                    botController5.isUseGiant = false;
                }
            }
        }

        private void CountdownShield()
        {
            if (botController5.isLostShield)
            {
                botController5.isHaveShield = false;
                botController5.shield.gameObject.SetActive(false);
                timeCountdown -= Time.deltaTime;
                fillShield.fillAmount = timeCountdown / maxTimeCdShield;
                if (timeCountdown <= 0)
                {
                    botController5.isHaveShield = true;
                    botController5.isLostShield = false;
                    botController5.isUseShield = false;
                }
            }
        }
        
        private void CountdownTumble()
        {
            if (botController5.isUseTumble)
            {
                timeCountdown -= Time.deltaTime;
                fillTumble.fillAmount = timeCountdown / maxTimeCdTumble;
                if (timeCountdown <= 0)
                {
                    botController5.isHaveTumble = true;
                    botController5.isUseTumble = false;
                }
            }
        }
        
        private void CountdownHammer()
        {
            if (botController5.isUseHammer)
            {
                timeCountdown -= Time.deltaTime;
                fillHammer.fillAmount = timeCountdown / maxTimeCdHammer;
                if (timeCountdown <= 0)
                {
                    botController5.isHaveHammer = true;
                    botController5.isUseHammer = false;
                }
            }
        }
    }
}
