using System.Collections;
using Challenger6.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger6.UI
{
	public class TimerCountdown6 : MonoBehaviour
	{
		[SerializeField] private float gameDuration;
		[SerializeField] private Text timerTxt;
		internal bool TimerOn = true;

		private Coroutine _countDownCoroutine;

		private void Start() => _countDownCoroutine = StartCoroutine(Countdown());

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
			
			TimerOn = false;
			GameManagerChallenger6.Instance.LoseScratch = true;
		}

		internal void SetTimer(float time)
		{
			gameDuration = time;
			if (_countDownCoroutine != null)
			{
				StopCoroutine(_countDownCoroutine);
			}

			_countDownCoroutine = StartCoroutine(Countdown());
		}

		private void OnDisable() => StopCoroutine(Countdown());
	}
}