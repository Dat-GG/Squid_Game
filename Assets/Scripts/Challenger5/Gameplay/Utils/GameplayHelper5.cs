using Challenger5.Gold;
using Challenger5.Heart;
using Core.Load;
using Core.Pool;
using Plugins.Scripts.Core.Common.Load;
using UnityEngine;

namespace Challenger5.Gameplay.Utils
{
    public class GameplayHelper5 : MonoBehaviour
    {
        public static GameObject SpawnPlayer(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadPlayer5();
            if (gamePrefab is null)
                return null;
            
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
            return gameClone;
        }

        public static GameObject SpawnBot(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadBot5();
            if (gamePrefab is null)
                return null;
            
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
            return gameClone;
        }
        
        public static void SpawnThunderTrack(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadThunderTrack5();
            SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity, new Vector3(0.2f, 0.2f, 0));
        }
        
        public static void SpawnThunder(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadThunder5();
            SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
        }
        
        public static void SpawnPlayerThunder(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadPlayerThunder5();
            SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
        }
        
        public static void SpawnThunderGround(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadThunderGround5();
            SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
        }
        
        public static void SpawnGroundBreak(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadGroundBreak5();
            SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
        }
        
        public static void SpawnGold(Vector3 position, int goldContain)
        {
            var gamePrefab = LoadResourceController.Instance.LoadGold5();
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity, new Vector3(0.1f, 0.1f, 0));
            gameClone.GetComponent<GoldObjects>().InitData(goldContain);
        }
        
        public static void SpawnHeart(Vector3 position)
        {
            var gamePrefab = LoadResourceController.Instance.LoadHeart5();
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity, new Vector3(0.3f, 0.3f, 0));
            gameClone.GetComponent<HeartObjects>().InitData();
        }
        
        public static void ShowGoldTxtInGame(Vector3 position, int number)
        {
            var gamePrefab = LoadResourceController.Instance.LoadTextGoldInGame5();
            var gameClone = SmartPool.Instance.Spawn(gamePrefab, position, Quaternion.identity);
            gameClone.GetComponent<TextGoldInGame5>().InitData(number);
        }
    }
}
