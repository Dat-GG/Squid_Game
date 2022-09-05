using Challenger5.Player;
using Core.Pool;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger5.Heart
{
    public class HeartObjects : MonoBehaviour
    {
        [SerializeField] private float scale;
        private float _maxRange = 2f;
        private Vector3 _targetPos = Vector3.zero;
        private PlayerController5 _playerController5;

        private void Awake()
        {
            _playerController5 = FindObjectOfType<PlayerController5>();
        }

        public void InitData()
        {
            var randomX = Random.Range(-_maxRange, _maxRange);
            var position = transform.position;
            _targetPos = new Vector3(position.x + randomX, position.y, position.z);
            transform.DOJump(_targetPos, 2f, 1, 0.3f);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnContactWithPlayer();
            } 
            
            if (other.CompareTag("Bot"))
            {
                other.transform.DOScale(other.transform.localScale * scale, 0.1f);
                SmartPool.Instance.Despawn(gameObject);
            }
        }

        private void OnContactWithPlayer()
        {
            var t = _playerController5.transform;
            _playerController5.transform.DOScale(t.localScale * scale, 0.1f);
            SmartPool.Instance.Despawn(gameObject);
        }
    }
}
