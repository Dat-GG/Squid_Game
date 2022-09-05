using System.Collections.Generic;
using DataAccount;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger1.UI.SingProgress
{
    public class SingProgressController : MonoBehaviour
    {
        [SerializeField] private Image singProgress;
        
        [SerializeField] private float fixedTimeSing1;
        [SerializeField] private float fixedTimeSing2;
        [SerializeField] private float fixedTimeSing3;
        [SerializeField] private float fixedTimeSing4;
        [SerializeField] private float fixedTimeSing5;
        [SerializeField] private float fixedTimeSing6;
        [SerializeField] private float fixedTimeSing7;

        [SerializeField] private List<float> probabilityTime1;
        [SerializeField] private List<float> probabilityTime2;
        [SerializeField] private List<float> probabilityTime3;
        [SerializeField] private List<float> probabilityTime4;
        [SerializeField] private List<float> probabilityTime5;
        [SerializeField] private List<float> probabilityTime6;
        [SerializeField] private List<float> probabilityTime7;

        public float realTimeSingInGame;
        public float currentTimeSing;

        public void AutoFillSingProgress()
        {
            singProgress.fillAmount = currentTimeSing / realTimeSingInGame;
        }

        public void CheckRealTimeSing()
        {
            var data = DataAccountPlayer.PlayerChallenger1PowerUp;
            if (data.WinCount <= 2)
            {
                RandomTime1();
            }
            else if( data.WinCount > 2 && data.WinCount <= 5)
            {
                RandomTime2();
            }
            else if(data.WinCount > 5 && data.WinCount <= 7)
            {
                RandomTime3();
            }
            else if(data.WinCount > 7 && data.WinCount <= 9)
            {
                RandomTime4();
            }
            else if(data.WinCount > 9)
            {
                RandomTime5();
            }
            // else if(data.WinCount > 12 && data.WinCount <= 15)
            // {
            //     RandomTime6();
            // }
            // else if(data.WinCount > 15)
            // {
            //     RandomTime7();
            // }
        }

        private void RandomTime1()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime1[0])
            {
                realTimeSingInGame = fixedTimeSing1;
            }
            else if(rd >= probabilityTime1[0] && rd < probabilityTime1[1])
            {
                realTimeSingInGame = fixedTimeSing2;
            }
            else if(rd >= probabilityTime1[1] && rd < probabilityTime1[2])
            {
                realTimeSingInGame = fixedTimeSing3;
            }
            else if(rd >= probabilityTime1[2] && rd < probabilityTime1[3])
            {
                realTimeSingInGame = fixedTimeSing4;
            }
        }

        private void RandomTime2()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime2[0])
            {
                realTimeSingInGame = fixedTimeSing1;
            }
            else if(rd >= probabilityTime2[0] && rd < probabilityTime2[1])
            {
                realTimeSingInGame = fixedTimeSing2;
            }
            else if (rd >= probabilityTime2[1] && rd < probabilityTime2[2])
            {
                realTimeSingInGame = fixedTimeSing3;
            }
            else if(rd >= probabilityTime2[2] && rd < probabilityTime2[3])
            {
                realTimeSingInGame = fixedTimeSing4;
            }
            else if(rd >= probabilityTime2[3] && rd < probabilityTime2[4])
            {
                realTimeSingInGame = fixedTimeSing5;
            }
            else if(rd >= probabilityTime2[4] && rd < probabilityTime2[5])
            {
                realTimeSingInGame = fixedTimeSing6;
            }
        }

        private void RandomTime3()
        {
            var rd = Random.Range(0, 101);
            if (rd <= probabilityTime3[0])
            {
                realTimeSingInGame = fixedTimeSing1;
            }
            else if(rd >= probabilityTime3[0] && rd < probabilityTime3[1])
            {
                realTimeSingInGame = fixedTimeSing2;
            }
            else if(rd >= probabilityTime3[1] && rd < probabilityTime3[2])
            {
                realTimeSingInGame = fixedTimeSing3;
            }
            else if(rd >= probabilityTime3[2] && rd < probabilityTime3[3])
            {
                realTimeSingInGame = fixedTimeSing4;
            }
            else if (rd >= probabilityTime3[3] && rd < probabilityTime3[4])
            {
                realTimeSingInGame = fixedTimeSing5;
            }
            else if(rd >= probabilityTime3[4] && rd < probabilityTime3[5])
            {
                realTimeSingInGame = fixedTimeSing6;
            }
            else if(rd >= probabilityTime3[5] && rd < probabilityTime3[6])
            {
                realTimeSingInGame = fixedTimeSing7;
            }
        }

        private void RandomTime4()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime4[0])
            {
                realTimeSingInGame = fixedTimeSing1;
            }
            else if(rd >= probabilityTime4[0] && rd < probabilityTime4[1])
            {
                realTimeSingInGame = fixedTimeSing2;
            }
            else if(rd >= probabilityTime4[1] && rd < probabilityTime4[2])
            {
                realTimeSingInGame = fixedTimeSing3;
            }
            else if(rd >= probabilityTime4[2] && rd < probabilityTime4[3])
            {
                realTimeSingInGame = fixedTimeSing4;
            }
            else if (rd >= probabilityTime4[3] && rd < probabilityTime4[4])
            {
                realTimeSingInGame = fixedTimeSing5;
            }
            else if(rd >= probabilityTime4[4] && rd < probabilityTime4[5])
            {
                realTimeSingInGame = fixedTimeSing6;
            }
            else if(rd >= probabilityTime4[5] && rd < probabilityTime4[6])
            {
                realTimeSingInGame = fixedTimeSing7;
            }
        }

        private void RandomTime5()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime5[0])
            {
                realTimeSingInGame = fixedTimeSing1;
            }
            else if(rd >= probabilityTime5[0] && rd < probabilityTime5[1])
            {
                realTimeSingInGame = fixedTimeSing2;
            }
            else if(rd >= probabilityTime5[1] && rd < probabilityTime5[2])
            {
                realTimeSingInGame = fixedTimeSing3;
            }
            else if(rd >= probabilityTime5[2] && rd < probabilityTime5[3])
            {
                realTimeSingInGame = fixedTimeSing4;
            }
            else if (rd >= probabilityTime5[3] && rd < probabilityTime5[4])
            {
                realTimeSingInGame = fixedTimeSing5;
            }
            else if(rd >= probabilityTime5[4] && rd < probabilityTime5[5])
            {
                realTimeSingInGame = fixedTimeSing6;
            }
            else if(rd >= probabilityTime5[5] && rd < probabilityTime5[6])
            {
                realTimeSingInGame = fixedTimeSing7;
            }
        }

        // private void RandomTime6()
        // {
        //     var rd = Random.Range(0, 101);
        //     if (rd < probabilityTime6[0])
        //     {
        //         realTimeSingInGame = fixedTimeSing1;
        //     }
        //     else if(rd >= probabilityTime6[0] && rd < probabilityTime6[1])
        //     {
        //         realTimeSingInGame = fixedTimeSing2;
        //     }
        //     else if(rd >= probabilityTime6[1] && rd < probabilityTime6[2])
        //     {
        //         realTimeSingInGame = fixedTimeSing3;
        //     }
        //     else if(rd >= probabilityTime6[2] && rd < probabilityTime6[3])
        //     {
        //         realTimeSingInGame = fixedTimeSing4;
        //     }
        //     else if (rd >= probabilityTime6[3] && rd < probabilityTime6[4])
        //     {
        //         realTimeSingInGame = fixedTimeSing5;
        //     }
        //     else if(rd >= probabilityTime6[4] && rd < probabilityTime6[5])
        //     {
        //         realTimeSingInGame = fixedTimeSing6;
        //     }
        //     else if(rd >= probabilityTime6[5] && rd < probabilityTime6[6])
        //     {
        //         realTimeSingInGame = fixedTimeSing7;
        //     }
        // }
        //
        // private void RandomTime7()
        // {
        //     var rd = Random.Range(0, 101);
        //     if (rd < probabilityTime7[0])
        //     {
        //         realTimeSingInGame = fixedTimeSing1;
        //     }
        //     else if(rd >= probabilityTime7[0] && rd < probabilityTime7[1])
        //     {
        //         realTimeSingInGame = fixedTimeSing2;
        //     }
        //     else if(rd >= probabilityTime7[1] && rd < probabilityTime7[2])
        //     {
        //         realTimeSingInGame = fixedTimeSing3;
        //     }
        //     else if(rd >= probabilityTime7[2] && rd < probabilityTime7[3])
        //     {
        //         realTimeSingInGame = fixedTimeSing4;
        //     }
        //     else if (rd >= probabilityTime7[3] && rd < probabilityTime7[4])
        //     {
        //         realTimeSingInGame = fixedTimeSing5;
        //     }
        //     else if(rd >= probabilityTime7[4] && rd < probabilityTime7[5])
        //     {
        //         realTimeSingInGame = fixedTimeSing6;
        //     }
        //     else if(rd >= probabilityTime7[5] && rd < probabilityTime7[6])
        //     {
        //         realTimeSingInGame = fixedTimeSing7;
        //     }
        // }
    }
}
