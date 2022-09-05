using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
    public class PlayerChallenger5PowerUp
    {
        public Dictionary<PowerUpType, int> allPowerUps;
        
        public int _firstGame = 0;

        public int _diamondCost = 0;
        
        public int _winCount;

        public int _loseCount;

        public int _firstTimeBuy;

        public bool _firstBuy;
        public PlayerChallenger5PowerUp()
        {
            allPowerUps = new Dictionary<PowerUpType, int>()
            {
                {PowerUpType.Giant5, 0},
                {PowerUpType.Katana, 0},
                {PowerUpType.Tumble, 0},
                {PowerUpType.Hammer, 0},
                {PowerUpType.Shield5, 0}
            };
        }

        #region PowerUpAction
        public bool IsEnough(PowerUpType powerUpType, int value)
        {
            return GetPowerUp(powerUpType) >= value;
        }

        public bool IsEnough(PowerUpItem cost)
        {
            return IsEnough(cost.powerUpType, cost.value);
        }

        public int GetPowerUp(PowerUpType powerUpType)
        {
            return allPowerUps.ContainsKey(powerUpType) ? allPowerUps[powerUpType] : 0;
        }

        public int SetPowerUp(bool isAdd, PowerUpType powerUpType, int value)
        {
            if (isAdd)
            {
                if (allPowerUps.ContainsKey(powerUpType))
                {
                    allPowerUps[powerUpType] += value;
                }
                else
                {
                    allPowerUps[powerUpType] = value;
                }

                EventDispatcher.Instance.PostEvent(EventID.CollectPowerUpChallenger5, powerUpType);
            }
            else
            {
                if (allPowerUps.ContainsKey(powerUpType))
                {
                    allPowerUps[powerUpType] -= value;
                    if (allPowerUps[powerUpType] < 0)
                    {
                        allPowerUps[powerUpType] = 0;
                    }
                }

                EventDispatcher.Instance.PostEvent(EventID.UsePowerUpChallenger5, powerUpType);
            }

            DataAccountPlayer.SavePlayerChallenger5PowerUp();
            return value;
        }
        #endregion
        
        public int FirstGame
        {
            get => _firstGame;

            set
            {
                _firstGame = value;
                DataAccountPlayer.SavePlayerChallenger5PowerUp();
            }
        }

        public int DiamondCost
        {
            get => _diamondCost;

            set
            {
                _diamondCost = value;
                DataAccountPlayer.SavePlayerChallenger5PowerUp();
            }
        }
        
        public int FirstTimeBuy
        {
            get => _firstTimeBuy;

            set
            {
                _firstTimeBuy = value;
                DataAccountPlayer.SavePlayerChallenger5PowerUp();
            }
        }
        
        public int WinCount
        {
            get => _winCount;

            set
            {
                _winCount = value;
                DataAccountPlayer.SavePlayerChallenger5PowerUp();
            }
        }
        
        public int LoseCount
        {
            get => _loseCount;

            set
            {
                _loseCount = value;
                DataAccountPlayer.SavePlayerChallenger5PowerUp();
            }
        }
    }
}
