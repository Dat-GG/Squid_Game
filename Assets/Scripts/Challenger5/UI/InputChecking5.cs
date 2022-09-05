using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenger5.UI
{
    public class InputChecking5 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsFirstTimeTouch { get; private set; }

        private void Awake()
        {
            IsFirstTimeTouch = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsFirstTimeTouch = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}
