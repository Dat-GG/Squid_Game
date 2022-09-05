using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
    public class PlayerChallenger4PowerUp
    {
        public Dictionary<PowerUpType, int> allPowerUps;

        public int _countSpin;

        public int _spinCost = 1;

        public int _winCount;

        public int _loseCount;

        public int _timeUsePowerUp;

        public PlayerChallenger4PowerUp()
        {
            allPowerUps = new Dictionary<PowerUpType, int>()
            {
                {PowerUpType.BonusTime4, 0},
                {PowerUpType.Power, 0},
                {PowerUpType.Banana, 0},
                {PowerUpType.Giant, 0},
                {PowerUpType.Freeze, 0}
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
            get => _countSpin;

            set
            {
                _countSpin = value;
                DataAccountPlayer.SavePlayerChallenger4PowerUp();
            }
        }
        
        public int SpinCost
        {
            get => _spinCost;

            set
            {
                _spinCost = value;
                DataAccountPlayer.SavePlayerChallenger4PowerUp();
            }
        }
        
        public int TimeUsePowerUp
        {
            get => _timeUsePowerUp;

            set
            {
                _timeUsePowerUp = value;
                DataAccountPlayer.SavePlayerChallenger4PowerUp();
            }
        }

        public int WinCount
        {
            get => _winCount;

            set
            {
                _winCount = value;
                DataAccountPlayer.SavePlayerChallenger4PowerUp();
            }
        }
        
        public int LoseCount
        {
            get => _loseCount;

            set
            {
                _loseCount = value;
                DataAccountPlayer.SavePlayerChallenger4PowerUp();
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

                EventDispatcher.Instance.PostEvent(EventID.CollectPowerUpChallenger4, powerUpType);
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

                EventDispatcher.Instance.PostEvent(EventID.UsePowerUpChallenger4, powerUpType);
            }

            DataAccountPlayer.SavePlayerChallenger4PowerUp();
            return value;
        }
        #endregion
    }
}
