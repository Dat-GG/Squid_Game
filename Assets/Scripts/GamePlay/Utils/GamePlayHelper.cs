using Core.Load;
using Core.Pool;
using Plugins.Scripts.Core.Common.Load;
using UnityEngine;

namespace GamePlay.Utils
{
    public class GamePlayHelper
    {
        public static GameObject SpawnGreenRedCharacter(bool isPlayer, Vector3 position)
        {
            GameObject gamePrefab;
            if (isPlayer)
            {
                gamePrefab = LoadResourceController.Instance.LoadGreenRedPlayer();
            }
            else
            {
                gamePrefab = LoadResourceController.Instance.LoadGreenRedEnemy();
            }

            if (gamePrefab is null)
                return null;

            var spawnPosition = position;
            spawnPosition.z = gamePrefab.transform.position.z;

            var gameClone = SmartPool.Instance.Spawn(gamePrefab, spawnPosition, Quaternion.identity);
            return gameClone;
        }
    }
}