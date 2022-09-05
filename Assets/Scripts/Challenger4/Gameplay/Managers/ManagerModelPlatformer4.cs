using System;
using System.Collections.Generic;
using Challenger4.Gameplay.Utils;
using Challenger4.Player;
using DataAccount;
using DG.Tweening;
using UnityEngine;

namespace Challenger4.Gameplay.Managers
{
    public class ManagerModelPlatformer4 : MonoBehaviour
    {
        public Transform botRightSpawnStartPoint;
        public Transform botRightSpawnEndPoint;
        public Transform botLeftSpawnStartPoint;
        public Transform botLeftSpawnEndPoint;
        public Transform bananaEndPos;
        public List<Transform> rightBotStart;
        public List<Transform> leftBotStart;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform policeSpawnPointRight;
        [SerializeField] private Transform policeSpawnPointLeft;
        [SerializeField] private int numberOfBotRight;
        [SerializeField] private int numberOfBotLeft;
        
        private GameObject _player;
        private GameObject _bot;
        private GameObject _police;
        private void Awake()
        {
            SpawnPlayer();
            SpawnPoliceRight();
            SpawnPoliceLeft();
            SpawnBotRight();
            SpawnBotLeft();
        }

        private void SpawnPlayer()
        {
            _player = GameplayHelper4.SpawnPlayer(playerSpawnPoint.position);
            var skinPlayer = _player.GetComponentInChildren<PlayerSpine4>();
            skinPlayer.ChangeSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString());
        }

        private void SpawnPoliceRight()
        {
            _police = GameplayHelper4.SpawnPolice(policeSpawnPointRight.position);
            _police.transform.DOLocalRotate(new Vector3(0, 180, 0), 0);
        }
        
        private void SpawnPoliceLeft()
        {
            _police = GameplayHelper4.SpawnPolice(policeSpawnPointLeft.position);
        }

        private void SpawnBotRight()
        {
            var a = Math.Abs(botRightSpawnStartPoint.position.x - botRightSpawnEndPoint.position.x) / numberOfBotRight;
            for (var i = 0; i <= numberOfBotRight; i++)
            {
                var pos = botRightSpawnStartPoint.position;
                pos.x += i * a;
                _bot = GameplayHelper4.SpawnBot(pos);
                _bot.transform.DOLocalRotate(new Vector3(0, 180, 0), 0);
            }
        }

        private void SpawnBotLeft()
        {
            var a = Math.Abs(botLeftSpawnStartPoint.position.x - botLeftSpawnEndPoint.position.x) / numberOfBotLeft;
            for (var i = 0; i <= numberOfBotLeft; i++)
            {
                var pos = botLeftSpawnStartPoint.position;
                pos.x -= i * a;
                _bot = GameplayHelper4.SpawnBot(pos);
            }
        }
        
        
    }
}
