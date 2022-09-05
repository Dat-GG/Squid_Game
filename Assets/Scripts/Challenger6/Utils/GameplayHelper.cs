using Core.Load;
using Core.Pool;
using Plugins.Scripts.Core.Common.Load;
using UnityEngine;

namespace Challenger6.Utils
{
	public class GameplayHelper : MonoBehaviour
	{
		public static GameObject SpawnPlayer(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadPlayer6();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
		
		public static GameObject SpawnPolice(Vector3 position)
		{
			var gamePrefab = LoadResourceController.Instance.LoadPolice6();
			if (gamePrefab is null)
				return null;
            
			var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
			return gameClone;
		}
	}
}