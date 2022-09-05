using Core.Load;
using Core.Pool;
using Plugins.Scripts.Core.Common.Load;
using UnityEngine;

namespace GameManagerChallenger2.Utils
{
	public class GameplayHelper : MonoBehaviour
	{
		public static GameObject SpawnPlayer(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadPlayer2();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}

		public static GameObject SpawnBot(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadBot2();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
        
		public static GameObject SpawnPolice(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadPolice2();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
	}
}