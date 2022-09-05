using Challenger3.Gameplay.Managers;
using Challenger3.Gameplay.Runtime;
using Challenger3.Marble;
using Challenger3.SpineAnimation;
using Core.Common;
using UnityEngine;

namespace Challenger3.Player
{
    public class PlayerController3 : MonoBehaviour
    {
        [SerializeField] private GameObject playerMarble;
        [SerializeField] private Transform spawnPos;
        [SerializeField] private float speedMarble;
        [SerializeField] private Spine3 playerSpine;
        internal Spine3 PlayerSpine => playerSpine;

        [HideInInspector] public GameObject spawnMarblePlayer;

        internal bool EndThrow;
        private Indicator _indicator;
        private GameManagerChallenger3 _gameManagerChallenger3;

        private void Start()
        {
            _gameManagerChallenger3 = FindObjectOfType<GameManagerChallenger3>();
            _indicator = FindObjectOfType<Indicator>();
            this.RegisterListener(EventID.ThrowMarbleFirst, delegate
            {
                PlayerThrowFirst();
            });
            this.RegisterListener(EventID.ThrowMarbleSecond, delegate
            {
                PlayerThrowSecond();
            });
        }
    
        private void PlayerThrowFirst()
        {
            playerSpine.PlayThrowEnd();
            
            Vector2 direction = _indicator.endPos.position - _indicator.startPos.position;
            var speedMarble2 = _indicator.fill.fillAmount * speedMarble;
            spawnMarblePlayer = Instantiate(playerMarble, spawnPos.transform.position, Quaternion.identity);
            spawnMarblePlayer.GetComponent<PlayerMarble>().enabled = false;
            var marbleRig = spawnMarblePlayer.GetComponent<Rigidbody2D>();
            marbleRig.AddForce(direction * speedMarble2);
            _indicator.Reset();

            this.StartDelayMethod(2f, delegate
            {
                _gameManagerChallenger3.SetEnemyTurnSecond();
            });
        }

        private void PlayerThrowSecond()
        {
            if (!EndThrow)
            {
                playerSpine.PlayThrowEnd();
            
                Vector2 direction = _indicator.endPos.position - _indicator.startPos.position;
                var speedMarble2 = _indicator.fill.fillAmount * speedMarble;
                spawnMarblePlayer = Instantiate(playerMarble, spawnPos.transform.position, Quaternion.identity);
                var marbleRig = spawnMarblePlayer.GetComponent<Rigidbody2D>();
                marbleRig.AddForce(direction * speedMarble2);
                _indicator.Reset();

                this.StartDelayMethod(1.5f, delegate { EndThrow = true; });
            }
        }
    }
}