using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
    public class PlayerChallenger1PowerUp
    {
        public Dictionary<PowerUpType, int> allPowerUps;

        public int countSpin;

        public int spinCost = 1;

        public int winCount;

        public int loseCount;

        public int _timeUsePowerUp;

        public PlayerChallenger1PowerUp()
        {
            allPowerUps = new Dictionary<PowerUpType, int>()
            {
                {PowerUpType.UnderGround, 0},
                {PowerUpType.Shield, 0},
                {PowerUpType.Gun, 0},
                {PowerUpType.BonusTime, 0},
                {PowerUpType.BonusSpeed, 0}
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

        public int CountSpin
        {
            get => countSpin;

            set
            {
                countSpin = value;
                DataAccountPlayer.SavePlayerChallenger1PowerUp();
            }
        }
        
        public int SpinCost
        {
            get => spinCost;

            set
            {
                spinCost = value;
                DataAccountPlayer.SavePlayerChallenger1PowerUp();
            }
        }
        
        public int TimeUsePowerUp
        {
            get => _timeUsePowerUp;

            set
            {
                _timeUsePowerUp = value;
                DataAccountPlayer.SavePlayerChallenger1PowerUp();
            }
        }

        public int WinCount
        {
            get => winCount;

            set
            {
                winCount = value;
                DataAccountPlayer.SavePlayerChallenger1PowerUp();
            }
        }
        
        public int LoseCount
        {
            get => loseCount;

            set
            {
                loseCount = value;
                DataAccountPlayer.SavePlayerChallenger1PowerUp();
            }
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

                EventDispatcher.Instance.PostEvent(EventID.CollectPowerUpChallenger1, powerUpType);
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

                EventDispatcher.Instance.PostEvent(EventID.UsePowerUpChallenger1, powerUpType);
            }

            DataAccountPlayer.SavePlayerChallenger1PowerUp();
            return value;
        }
        #endregion
    }
}

