using UnityEngine;
using UnityEngine.UI;

namespace Challenger4.UI.TimerCountdown
{
    public class TimerCountdown : MonoBehaviour
    {
        public float gameDuration;
        [SerializeField] private Text timerTxt;

        public void CountdownTime()
        {
            gameDuration -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(gameDuration / 60);
            float seconds = Mathf.FloorToInt(gameDuration % 60);
            timerTxt.text = $"{minutes:00}:{seconds:00}";
        }

        public void OverTime()
        {
            gameDuration = 0;
            float minutes = Mathf.FloorToInt(gameDuration / 60);
            float seconds = Mathf.FloorToInt(gameDuration % 60);
            timerTxt.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
