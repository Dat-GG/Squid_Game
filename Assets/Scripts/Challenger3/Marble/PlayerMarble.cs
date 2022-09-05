using Challenger3.Enemy;
using Challenger3.Gameplay.Managers;
using Challenger3.Player;
using Core.Pool;
using UnityEngine;

namespace Challenger3.Marble
{
    public class PlayerMarble : MonoBehaviour
    {
        // [SerializeField] private GameObject collisionEffect;

        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if(other.gameObject.CompareTag("MarblePlayer"))
        //     {
        //         var effect = SmartPool.Instance.Spawn(collisionEffect, transform.position, Quaternion.identity);
        //     }
        // }
        [SerializeField] private Rigidbody2D rigidBody2D;
        
        private GameManagerChallenger3 _gameManagerChallenger3;
        private PlayerController3 _playerController3;
        private EnemyController3 _enemyController3;

        private void Start()
        {
            _gameManagerChallenger3 = FindObjectOfType<GameManagerChallenger3>();
            _playerController3 = FindObjectOfType<PlayerController3>();
            _enemyController3 = FindObjectOfType<EnemyController3>();
        }

        private void Update()
        {
            if (!(Mathf.Abs(rigidBody2D.velocity.x) <= 0.1) || _gameManagerChallenger3.endTurn ||
                !_playerController3.EndThrow) return;
            SmartPool.Instance.Despawn(gameObject);
            SmartPool.Instance.Despawn(_enemyController3.spawnMarbleEnemy);
            _gameManagerChallenger3.endTurn = true;
            _gameManagerChallenger3.round += 1;
            _playerController3.EndThrow = false;
            if (_gameManagerChallenger3.endTurn)
                _gameManagerChallenger3.CheckEndTurnEnemyFirst();
        }
    }
}