using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger2.UI
{
	public class TimerCountdown : MonoBehaviour
	{
		[SerializeField] private float gameDuration;
		[SerializeField] private float timeScale;
		[SerializeField] private Text timerTxt;
		internal bool TimerOn = true;

		private Coroutine _countDownCoroutine;

		private void Start() => _countDownCoroutine = StartCoroutine(Countdown());
		
		private IEnumerator Countdown()
		{
			while (gameDuration > 0)
			{
				yield return new WaitForSeconds(timeScale);
				gameDuration--;
				float minutes = Mathf.FloorToInt(gameDuration / 60);
				float seconds = Mathf.FloorToInt(gameDuration % 60);
				timerTxt.text = $"{minutes:00}:{seconds:00}";
			}
			
			TimerOn = false;
		}

		internal void AddBonusTime(float bonusTime)
		{
			if(_countDownCoroutine != null)
				StopCoroutine(_countDownCoroutine);
			
			gameDuration += bonusTime;
			_countDownCoroutine = StartCoroutine(Countdown());
		}

		private void OnDisable() => StopCoroutine(Countdown());
	}
}