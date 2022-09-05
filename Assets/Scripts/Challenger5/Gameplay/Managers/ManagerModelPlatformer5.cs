using Challenger5.Gameplay.Utils;
using Challenger5.Player;
using Challenger5.UI.CountTotalAlive;
using DataAccount;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger5.Gameplay.Managers
{
    public class ManagerModelPlatformer5 : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform botSpawnPoint;
        [SerializeField] private int numberOfBots;
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        private GameObject _player;
        private GameObject _bot;
        private CountTotalAlive _countTotalAlive;

        private void Awake()
        {
            SpawnPlayer();
            SpawnBot();
            _countTotalAlive = FindObjectOfType<CountTotalAlive>();
        }

        private void Start()
        {
            _countTotalAlive.alive += numberOfBots + 2;
        }

        private void SpawnPlayer()
        {
            _player = GameplayHelper5.SpawnPlayer(playerSpawnPoint.position);
            var skinPlayer = _player.GetComponentInChildren<PlayerSpine5>();
            skinPlayer.ChangeSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString());
        }

        private void SpawnBot()
        {
            for (var i = 0; i <= numberOfBots; i++)
            {
                var playerPos = playerSpawnPoint.position;
                botSpawnPoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                var pos = botSpawnPoint.position;
                if (pos.x <= playerPos.x + 2 && pos.x >= 0)
                {
                    pos.x = playerPos.x + Random.Range(2f, 4f);
                }

                if (pos.x >= playerPos.x - 2 && pos.x < 0)
                {
                    pos.x = playerPos.x - Random.Range(2f, 4f);
                }

                if (pos.y <= playerPos.y + 2 && pos.y >= 0)
                {
                    pos.y = playerPos.y + Random.Range(2f, 4f);
                }

                if (pos.y >= playerPos.y - 2 && pos.y < 0)
                {
                    pos.y = playerPos.y - Random.Range(2f, 4f);
                }
                _bot = GameplayHelper5.SpawnBot(pos);
            }
        }
    }
}
