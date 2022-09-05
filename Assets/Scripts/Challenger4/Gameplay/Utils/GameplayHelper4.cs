using Core.Load;
using Core.Pool;
using Plugins.Scripts.Core.Common.Load;
using UnityEngine;

namespace Challenger4.Gameplay.Utils
{
    public class GameplayHelper4 : MonoBehaviour
    {
        public static GameObject SpawnPlayer(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadPlayer4();
            if (gamePrefab is null)
                return null;
            
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
            return gameClone;
        }

        public static GameObject SpawnBot(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadBot4();
            if (gamePrefab is null)
                return null;
            
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
            return gameClone;
        }
        
        public static GameObject SpawnPolice(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadPolice4();
            if (gamePrefab is null)
                return null;
            
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
            return gameClone;
        }
    }
}
