using UnityEngine;

namespace Challenger3.UI
{
    public class TimeCountdown : MonoBehaviour
    {
        // [SerializeField] private float gameDuration;
        // [SerializeField] private Text timerTxt;

        // private void Start()
        // {
            // StartCoroutine(CountdownTime());
        // }

        // private IEnumerator CountdownTime()
        // {
        //     var counter = gameDuration;
        //     while (counter > 0)
        //     {
        //         yield return new WaitForSeconds(1f);
        //         counter--;
        //         float minutes = Mathf.FloorToInt(counter / 60);
        //         float seconds = Mathf.FloorToInt(counter % 60);
        //         timerTxt.text = $"{minutes:00}:{seconds:00}";
        //     }
        // }

        // private void OnDisable()
        // {
        //     StopCoroutine(CountdownTime());
        // }
    }
}