using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Challenger6.Gameplay
{
	public class InputCheck : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public static event Action MouseDown;
		internal bool IsFirstTimeTouch { get; private set; }
		// public bool IsTouch { get; private set; }
		// public int touchCount;
		// public int checkTouch;
		private void Awake()
		{
			IsFirstTimeTouch = false;
			// IsTouch = false;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			IsFirstTimeTouch = true;
			MouseDown?.Invoke();
			// checkTouch++;
			// if (checkTouch > 1)
			// {
			// 	IsTouch = true;
			// }
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			// IsTouch = false;
			// touchCount = 0;
		}
	}
}