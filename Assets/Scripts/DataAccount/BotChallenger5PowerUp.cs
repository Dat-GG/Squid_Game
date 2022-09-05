using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
    public class BotChallenger5PowerUp
    {
        public Dictionary<PowerUpBotType, int> allPowerUps;
        
        public BotChallenger5PowerUp()
        {
            allPowerUps = new Dictionary<PowerUpBotType, int>()
            {
                {PowerUpBotType.Katana, 0},
                {PowerUpBotType.Hammer, 0},
                {PowerUpBotType.Shield, 0},
                {PowerUpBotType.Giant, 0},
                {PowerUpBotType.Tumble, 0}
            };
        }
        
        public bool IsEnough(PowerUpBotType powerUpBotType, int value)
        {
            return GetPowerUp(powerUpBotType) >= value;
        }

        public bool IsEnough(PowerUpBotItem cost)
        {
            return IsEnough(cost.powerUpBotType, cost.value);
        }

        public int GetPowerUp(PowerUpBotType powerUpBotType)
        {
            return allPowerUps.ContainsKey(powerUpBotType) ? allPowerUps[powerUpBotType] : 0;
        }
        
        public int SetPowerUp(bool isAdd, PowerUpBotType powerUpBotType, int value)
        {
            if (isAdd)
            {
                if (allPowerUps.ContainsKey(powerUpBotType))
                {
                    allPowerUps[powerUpBotType] += value;
                }
                else
                {
                    allPowerUps[powerUpBotType] = value;
                }

                EventDispatcher.Instance.PostEvent(EventID.BotHavePowerUpChallenger5, powerUpBotType);
            }
            else
            {
                if (allPowerUps.ContainsKey(powerUpBotType))
                {
                    allPowerUps[powerUpBotType] -= value;
                    if (allPowerUps[powerUpBotType] < 0)
                    {
                        allPowerUps[powerUpBotType] = 0;
                    }
                }

                EventDispatcher.Instance.PostEvent(EventID.BotUsePowerUpChallenger5, powerUpBotType);
            }

            DataAccountPlayer.SaveBotChallenger5PowerUp();
            return value;
        }
    }
}
