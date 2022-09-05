using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenger4.UI
{
    public class InputChecking4 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsFirstTimeTouch { get; private set; }
        public bool IsTouch { get; private set; }
        public int touchCount;
        public int checkTouch;
        private void Awake()
        {
            IsFirstTimeTouch = false;
            IsTouch = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsFirstTimeTouch = true;
            checkTouch++;
            if (checkTouch > 1)
            {
                IsTouch = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsTouch = false;
            touchCount = 0;
        }
    }
}
