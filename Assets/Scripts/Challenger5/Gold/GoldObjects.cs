using Challenger5.Gameplay.Utils;
using Core.Common.GameResources;
using Core.Pool;
using DataAccount;
using DG.Tweening;
using UnityEngine;

namespace Challenger5.Gold
{
    public class GoldObjects : MonoBehaviour
    {
        [SerializeField] private int goldNumber;
        private float _maxRange = 2f;
        private Vector3 _targetPos = Vector3.zero;

        public void InitData(int goldValue)
        { 
            goldNumber = goldValue;
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
            else if(other.CompareTag("Bot"))
            {
                SmartPool.Instance.Despawn(gameObject);
            }
        }

        private void OnContactWithPlayer()
        {
            GameplayHelper5.ShowGoldTxtInGame(transform.position, goldNumber);
            DataAccountPlayer.PlayerMoney.SetMoney(true, MoneyType.Gold, goldNumber);
            SmartPool.Instance.Despawn(gameObject);
        }
    }
}
