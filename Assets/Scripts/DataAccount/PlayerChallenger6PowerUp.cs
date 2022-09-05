// using System.Collections.Generic;
// using Core.Common.GameResources;

namespace DataAccount
{
	public class PlayerChallenger6PowerUp
	{
		// public Dictionary<PowerUpType, int> allPowerUps;
		
		// public int timeUsePowerUp;

		// public PlayerChallenger6PowerUp()
		// {
		// 	allPowerUps = new Dictionary<PowerUpType, int>()
		// 	{
		// 		{PowerUpType.Tongue, 0},
		// 	};
		// }
		//
		// public bool IsEnough(PowerUpType powerUpType, int value)
		// {
		// 	return GetPowerUp(powerUpType) >= value;
		// }
		//
		// public bool IsEnough(PowerUpItem cost)
		// {
		// 	return IsEnough(cost.powerUpType, cost.value);
		// }
		//
		// public int GetPowerUp(PowerUpType powerUpType)
		// {
		// 	return allPowerUps.ContainsKey(powerUpType) ? allPowerUps[powerUpType] : 0;
		// }
		
		public int winCount;
		public int loseCount;
		
		#region PowerUpAction

		public int WinCount
		{
			get => winCount;
			
			set
			{
				winCount = value;
				DataAccountPlayer.SavePlayerChallenger6PowerUp();
			}
		}

		public int LoseCount
		{
			get => loseCount;

			set
			{
				loseCount = value;
				DataAccountPlayer.SavePlayerChallenger6PowerUp();
			}
		}

		#endregion
	}
}