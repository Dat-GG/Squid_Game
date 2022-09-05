using UnityEngine;
using UnityEngine.EventSystems;

namespace GameManagerChallenger1.UI
{
    public class InputChecking : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsHolding { get; private set; }

        public bool IsFirstTimeTouch { get; set; }

        private void Awake()
        {
            IsHolding = false;
            IsFirstTimeTouch = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsHolding = true;
            IsFirstTimeTouch = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHolding = false;
        }
    }
}