using Core.Common;
using UnityEngine;

namespace GamePlay._1_GreenRedLight.Police
{
    public class GreenRedPoliceController : MonoBehaviour
    {
        private GreenRedPoliceSpine _spine;

        public bool IsShooting { get; private set; }

        private Coroutine _shootEndCoroutine;

        private void Start()
        {
            _spine = GetComponentInChildren<GreenRedPoliceSpine>();
        }

        private void PlayIdle()
        {
            IsShooting = false;
            _spine.PlayIdle();
        }

        public void PlayShoot()
        {
            var trackEntry = _spine.PlayShoot();
            if (trackEntry is null)
                return;

            IsShooting = true;

            if (_shootEndCoroutine != null)
            {
                StopCoroutine(_shootEndCoroutine);
            }

            _shootEndCoroutine = this.StartDelayMethod(1f, PlayIdle);
        }
    }
}