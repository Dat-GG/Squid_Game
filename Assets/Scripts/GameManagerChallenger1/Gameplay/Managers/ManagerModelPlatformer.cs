using System;
using System.Collections.Generic;
using DataAccount;
using GameManagerChallenger1.Gameplay.Utils;
using GameManagerChallenger1.Player;
using UnityEngine;

namespace GameManagerChallenger1.Gameplay.Managers
{
    public class ManagerModelPlatformer : MonoBehaviour
    {
        public List<Transform> botStartPos;
        public Transform playerStart;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform dollySpawnPoint;
        [SerializeField] private Transform botUpSpawnPoint;
        [SerializeField] private Transform botDownSpawnPoint;
        [SerializeField] private Transform []policeSpawnPoint;
        [SerializeField] private int amountBot;

        private GameObject _player;
        private GameObject _dolly;
        private GameObject _bot;
        private GameObject _police;
        private void Awake()
        {
            SpawnHero();
            SpawnDolly();
            SpawnPolice();
            SpawnBot();
        }

        private void SpawnHero()
        {
            _player = GameplayHelper.SpawnPlayer(playerSpawnPoint.position);
            var skinPlayer = _player.GetComponentInChildren<PlayerSpine>();
            skinPlayer.ChangeSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString());
        }
        
        private void SpawnDolly()
        {
            _dolly = GameplayHelper.SpawnDolly(dollySpawnPoint.position);
        }
        
        private void SpawnBot()
        {
            var a = Math.Abs(botUpSpawnPoint.position.y - botDownSpawnPoint.position.y) / amountBot;
            for (var i = 0; i <= amountBot; i++)
            {
                var pos = botUpSpawnPoint.position;
                pos.y -= i * a;
                _bot = GameplayHelper.SpawnBot(pos);
            }
        }

        private void SpawnPolice()
        {
            foreach (var t in policeSpawnPoint)
            {
                _police = GameplayHelper.SpawnPolice(t.position);
            }
        }
    }
    
}
