using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
	public class PlayerChallenger2PowerUp
	{
		public Dictionary<PowerUpType, int> allPowerUps;

		public int countSpin;
		public int spinCost = 1;
		public int winCount;
		public int loseCount;
		public int timeUsePowerUp;

		public PlayerChallenger2PowerUp()
		{
			allPowerUps = new Dictionary<PowerUpType, int>()
			{
				{PowerUpType.BonusTime2, 0},
				{PowerUpType.Balloon, 0},
				{PowerUpType.Drone, 0},
				{PowerUpType.Scan, 0},
				{PowerUpType.Reinforce, 0},
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
				DataAccountPlayer.SavePlayerChallenger2PowerUp();
			}
		}
		
		public int SpinCost
		{
			get => spinCost;

			set
			{
				spinCost = value;
				DataAccountPlayer.SavePlayerChallenger2PowerUp();
			}
		}

		public int WinCount
		{
			get => winCount;
			
			set
			{
				winCount = value;
				DataAccountPlayer.SavePlayerChallenger2PowerUp();
			}
		}

		public int LoseCount
		{
			get => loseCount;

			set
			{
				loseCount = value;
				DataAccountPlayer.SavePlayerChallenger2PowerUp();
			}
		}

		public int TimeUsePowerUp
		{
			get => timeUsePowerUp;

			set
			{
				timeUsePowerUp = value;
				DataAccountPlayer.SavePlayerChallenger2PowerUp();
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

				EventDispatcher.Instance.PostEvent(EventID.CollectPowerUpChallenger2, powerUpType);
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

				EventDispatcher.Instance.PostEvent(EventID.UsePowerUpChallenger2, powerUpType);
			}

			DataAccountPlayer.SavePlayerChallenger2PowerUp();
			return value;
		}
		#endregion
	}
}