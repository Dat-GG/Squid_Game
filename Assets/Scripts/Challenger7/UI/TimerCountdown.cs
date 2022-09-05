using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger7.UI
{
	public class TimerCountdown : MonoBehaviour
	{
		[SerializeField] private float gameDuration;
		[SerializeField] private Text timerTxt;
		// internal bool TimerOn = true;

		private void Start()
		{
			this.RegisterListener(EventID.PlayerLose, delegate { OnFinishGame(); });
			this.RegisterListener(EventID.PlayerWin, delegate { OnFinishGame(); });
			StartCoroutine(Countdown());
		}

		private IEnumerator Countdown()
		{
			var counter = gameDuration;
			while (counter > 0)
			{
				yield return new WaitForSeconds(1f);
				counter--;
				float minutes = Mathf.FloorToInt(counter / 60);
				float seconds = Mathf.FloorToInt(counter % 60);
				timerTxt.text = $"{minutes:00}:{seconds:00}";
			}
			
			// TimerOn = false;
			this.PostEvent(EventID.TimesUp);
		}

		private void OnFinishGame() => gameObject.SetActive(false);

		private void OnDisable() => StopCoroutine(Countdown());
	}
}