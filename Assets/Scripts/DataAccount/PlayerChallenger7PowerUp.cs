using System.Collections.Generic;
using Core.Common.GameResources;
using DataAccount;

namespace Plugins.Scripts.DataAccount
{
	public class PlayerChallenger7PowerUp
	{
		private Dictionary<PowerUpType, int> _allPowerUps;
		public int spinCost = 1;
		public int countSpin, winCount, loseCount, timeUsePowerUp;

		public PlayerChallenger7PowerUp()
		{
			_allPowerUps = new Dictionary<PowerUpType, int>()
			{
				{PowerUpType.Shield7, 0},
				{PowerUpType.Magnet, 0},
				{PowerUpType.Push, 0},
				{PowerUpType.AllGoldItem, 0},
				{PowerUpType.Freeze7, 0},
				{PowerUpType.PassThrough, 0},
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
			return _allPowerUps.ContainsKey(powerUpType) ? _allPowerUps[powerUpType] : 0;
		}

		public int CountSpin
		{
			get => countSpin;
			set
			{
				countSpin = value;
				DataAccountPlayer.SavePlayerChallenger7PowerUp();
			}
		}

		public int SpinCost
		{
			get => spinCost;
			set
			{
				spinCost = value;
				DataAccountPlayer.SavePlayerChallenger7PowerUp();
			}
		}

		public int TimeUsePowerUp
		{
			get => timeUsePowerUp;
			set
			{
				timeUsePowerUp = value;
				DataAccountPlayer.SavePlayerChallenger7PowerUp();
			}
		}

		public int WinCount
		{
			get => winCount;
			set
			{
				winCount = value;
				DataAccountPlayer.SavePlayerChallenger7PowerUp();
			}
		}

		public int LoseCount
		{
			get => loseCount;
			set
			{
				loseCount = value;
				DataAccountPlayer.SavePlayerChallenger7PowerUp();
			}
		}

		public int SetPowerUp(bool isAdd, PowerUpType powerUpType, int value)
		{
			if (isAdd)
			{
				if (_allPowerUps.ContainsKey(powerUpType))
					_allPowerUps[powerUpType] += value;
				else
					_allPowerUps[powerUpType] = value;

				EventDispatcher.Instance.PostEvent(EventID.CollectPowerUpChallenger7, powerUpType);
			}
			else
			{
				if (_allPowerUps.ContainsKey(powerUpType))
				{
					_allPowerUps[powerUpType] -= value;
					if (_allPowerUps[powerUpType] < 0)
						_allPowerUps[powerUpType] = 0;
				}
				
				EventDispatcher.Instance.PostEvent(EventID.UsePowerUpChallenger7, powerUpType);
			}

			DataAccountPlayer.SavePlayerChallenger7PowerUp();
			return value;
		}

		#endregion
	}
}