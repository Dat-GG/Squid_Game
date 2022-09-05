using Core.Load;
using Core.Pool;
using Plugins.Scripts.Core.Common.Load;
using UnityEngine;

namespace Challenger7.Gameplay
{
	public class GameplayHelper : MonoBehaviour
	{
		public static GameObject SpawnPlayer(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadPlayer7();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}

		public static GameObject SpawnBot(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadBot7();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}

		public static GameObject SpawnGold(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadGold7();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
		
		public static GameObject SpawnKnife(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadKnife7();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
		
		public static GameObject SpawnRock(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadRock7();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
	}
}