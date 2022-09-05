using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay._1_GreenRedLight
{
    public class GreenRedInputChecking : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsHolding { get; private set; }

        private void Awake()
        {
            IsHolding = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHolding = false;
        }
    }
}