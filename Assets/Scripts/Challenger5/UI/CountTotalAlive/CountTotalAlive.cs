using UnityEngine;
using UnityEngine.UI;

namespace Challenger5.UI.CountTotalAlive
{
    public class CountTotalAlive : MonoBehaviour
    {
        public int alive;
        [SerializeField] private Text aliveTxt;

        public void CountAlive()
        {
            aliveTxt.text = "ALIVE: " + alive;
        }
    }
}
