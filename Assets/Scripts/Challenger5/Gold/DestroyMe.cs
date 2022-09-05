using System.Collections;
using Core.Pool;
using UnityEngine;

namespace Challenger5.Gold
{
    public class DestroyMe : MonoBehaviour
    {
        public float timeToDestroy = 1;
        
        private void OnEnable()
        {
            StartCoroutine(CountTimeToDestroy());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CountTimeToDestroy()
        {
            yield return new WaitForSeconds(timeToDestroy);
            SmartPool.Instance.Despawn(gameObject);
        }
    }
}
