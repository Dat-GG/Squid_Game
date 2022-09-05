using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenger5.Player
{
    public class InputAttackBtn : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public bool IsAttacking { get; private set; }

        private void Awake()
        {
            IsAttacking = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsAttacking = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsAttacking = true;
        }
    }
}
