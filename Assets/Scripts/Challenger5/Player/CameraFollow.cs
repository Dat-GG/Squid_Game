using UnityEngine;

namespace Challenger5.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float damping;
        [SerializeField] private Vector3 offset;
        private Vector3 _velocity = Vector3.zero;
        private PlayerController5 _playerController5;
        private Transform _target;
        private void Awake()
        {
            _playerController5 = FindObjectOfType<PlayerController5>();
            _target = _playerController5.transform;
        }

        private void FixedUpdate()
        {
            var movePosition = _target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref _velocity, damping);
        }
    }
}
