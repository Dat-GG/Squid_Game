using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger1.UI.CountdownTime
{
    public class CountdownTime : MonoBehaviour
    {
        public static CountdownTime Instance;
        public float gameDuration;
        [SerializeField] private Text timerTxt;

        private void Awake()
        {
            Instance = this;
        }

        public void CountdownTimer()
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
