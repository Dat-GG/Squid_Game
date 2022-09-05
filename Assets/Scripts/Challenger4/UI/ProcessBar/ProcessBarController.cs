using System.Collections.Generic;
using Challenger4.Player;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Challenger4.UI.ProcessBar
{
    public class ProcessBarController : MonoBehaviour
    {
        public bool isRight;
        public bool isLeft;
        public bool isPower;
        public bool moveRight;
        public bool moveLeft;
        
        
        [SerializeField] private float widthOfTracker;
        [SerializeField] private float widthOfProcess;
        [SerializeField] private GameObject processBar;
        [SerializeField] private Image redPanel;
        [SerializeField] private Image timeProgress;
        [SerializeField] private Image indicator;
        [SerializeField] private Image tracker;
        [SerializeField] private RectTransform rightPos;
        [SerializeField] private RectTransform leftPos;
        //Random Time Fill
        [SerializeField] private float fixedTimeFill1;
        [SerializeField] private float fixedTimeFill2;
        [SerializeField] private float fixedTimeFill3;
        [SerializeField] private float fixedTimeFill4;
        [SerializeField] private float fixedTimeFill5;
        [SerializeField] private float fixedTimeFill6;
        [SerializeField] private float fixedTimeFill7;
        [SerializeField] private List<float> probabilityTime1;
        [SerializeField] private List<float> probabilityTime2;
        [SerializeField] private List<float> probabilityTime3;
        [SerializeField] private List<float> probabilityTime4;
        [SerializeField] private List<float> probabilityTime5;
        [SerializeField] private List<float> probabilityTime6;
        [SerializeField] private List<float> probabilityTime7;
        //RandomSizeTracker
        [SerializeField] private List<float> factorOfSizeTracker;
        [SerializeField] private List<float> probabilitySize1;
        [SerializeField] private List<float> probabilitySize2;
        [SerializeField] private List<float> probabilitySize3;
        [SerializeField] private List<float> probabilitySize4;
        [SerializeField] private List<float> probabilitySize5;
        [SerializeField] private List<float> probabilitySize6;
        [SerializeField] private List<float> probabilitySize7;

        private PlayerController4 _playerController4;
        private InputChecking4 _inputChecking4;
        private float _fillTime;
        private bool _check;
        private bool _checkRealTime;
        private float _realFillTimeInGame;
        private float _realFactorOfSize;

        private void Start()
        {
            _inputChecking4 = FindObjectOfType<InputChecking4>();
            _playerController4 = FindObjectOfType<PlayerController4>();
            timeProgress.fillAmount = 0;
            MoveTracker();
            _check = false;
            isLeft = false;
            isRight = false;
            isPower = false;
            _checkRealTime = false;
            moveRight = false;
            moveLeft = false;
        }

        private void MoveTracker()
        {
            var i = rightPos.position.x;
            var j = leftPos.position.x;
            var a = Random.Range(i, j);
            tracker.transform.DOMoveX(a, 0.25f);
            CheckRealSize();
            tracker.transform.DOScaleX(_realFactorOfSize, 0);
            DOVirtual.DelayedCall(0.3f, delegate
            {
                if (!_check && !isLeft && !isRight && !_checkRealTime) return;
                _check = false;
                isLeft = false;
                isRight = false;
                _checkRealTime = false;
            });
        }

        public void AutoFillProcess()
        {
            if (_checkRealTime == false)
            {
                CheckRealTimeFill();
                _checkRealTime = true;
            }
            processBar.SetActive(true);
            
            if (_fillTime < _realFillTimeInGame && _inputChecking4.IsTouch == false && _check == false)
            {
                _fillTime += Time.deltaTime;
                var fillAmount = _fillTime / _realFillTimeInGame;
                timeProgress.fillAmount = fillAmount;
                var indicatorX = widthOfProcess * fillAmount;
                indicator.rectTransform.anchoredPosition = new Vector2(indicatorX, 0);
            }
            
            ResetProcess();
        }

        private void ResetProcess()
        {
            if ((!(_fillTime >= _realFillTimeInGame) && !_inputChecking4.IsTouch) || _check) return;
            CheckInteractive();
            _fillTime = 0;
            MoveTracker();
        }

        private void CheckInteractive()
        {
            _check = true;
            var delta = _fillTime / _realFillTimeInGame * widthOfProcess;
            var pos = tracker.rectTransform.localPosition.x;
            var range = widthOfTracker / 2;
            if (pos >= 0)
            {
                pos += widthOfProcess / 2;
            }
            else
            {
                pos = widthOfProcess / 2 + pos;
            }
            var finalPos = pos;

            if (_inputChecking4.touchCount > 0 && delta >= finalPos && _inputChecking4.IsTouch == false)
            {
                isLeft = true;
                SoundManager.Instance.PlaySound(SoundType.False);
                moveLeft = true;
                ShowWarning();
            }
            if(_inputChecking4.touchCount > 0) return;
            if (delta <= finalPos + range * 2 * _realFactorOfSize && delta >= finalPos - range / 3f * _realFactorOfSize)
            {
                isRight = true;
                SoundManager.Instance.PlaySound(SoundType.True);
                _inputChecking4.touchCount++;
                if (_playerController4.isPowerPower)
                {
                    isPower = true;
                }

                if (_inputChecking4.checkTouch > 1)
                {
                    moveRight = true;
                }
            }
            else
            {
                isLeft = true;
                SoundManager.Instance.PlaySound(SoundType.False);
                ShowWarning();
                _inputChecking4.touchCount++;
                
                if (!_playerController4.isFreezePower && _inputChecking4.checkTouch > 1)
                {
                    moveLeft = true;
                }
            }
        }

        private void ShowWarning()
        {
            redPanel.DOFade(1, 0.1f);
            DOVirtual.DelayedCall(0.1f, delegate
            {
                redPanel.DOFade(0, 0.3f);
            });
        }

        #region RandomTimeFillProgress
        private void CheckRealTimeFill()
        {
            var data = DataAccountPlayer.PlayerChallenger4PowerUp;
            if (data.WinCount <= 0)
            {
                RandomTime1();
            }
            else if( data.WinCount > 0 && data.WinCount <= 3)
            {
                RandomTime2();
            }
            else if(data.WinCount > 3 && data.WinCount <= 6)
            {
                RandomTime3();
            }
            else if(data.WinCount > 6 && data.WinCount <= 9)
            {
                RandomTime4();
            }
            else if(data.WinCount > 9 && data.WinCount <= 12)
            {
                RandomTime5();
            }
            else if(data.WinCount > 12 && data.WinCount <= 15)
            {
                RandomTime6();
            }
            else if(data.WinCount > 15)
            {
                RandomTime7();
            }
        }

        private void RandomTime1()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime1[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime1[0])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
        }

        private void RandomTime2()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime2[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime2[0] && rd < probabilityTime2[1])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
            else if(rd >= probabilityTime2[1] && rd < probabilityTime2[2])
            {
                _realFillTimeInGame = fixedTimeFill3;
            }
            else if(rd >= probabilityTime2[2] && rd < probabilityTime2[3])
            {
                _realFillTimeInGame = fixedTimeFill4;
            }
        }

        private void RandomTime3()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime3[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime3[0] && rd < probabilityTime3[1])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
            else if(rd >= probabilityTime3[1] && rd < probabilityTime3[2])
            {
                _realFillTimeInGame = fixedTimeFill3;
            }
            else if(rd >= probabilityTime3[2] && rd < probabilityTime3[3])
            {
                _realFillTimeInGame = fixedTimeFill4;
            }
            else if(rd >= probabilityTime3[3] && rd < probabilityTime3[4])
            {
                _realFillTimeInGame = fixedTimeFill5;
            }
            else if(rd >= probabilityTime3[4] && rd < probabilityTime3[5])
            {
                _realFillTimeInGame = fixedTimeFill6;
            }
            else if(rd >= probabilityTime3[5] && rd < probabilityTime3[6])
            {
                _realFillTimeInGame = fixedTimeFill7;
            }
        }

        private void RandomTime4()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime4[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime4[0] && rd < probabilityTime4[1])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
            else if(rd >= probabilityTime4[1] && rd < probabilityTime4[2])
            {
                _realFillTimeInGame = fixedTimeFill3;
            }
            else if(rd >= probabilityTime4[2] && rd < probabilityTime4[3])
            {
                _realFillTimeInGame = fixedTimeFill4;
            }
            else if(rd >= probabilityTime4[3] && rd < probabilityTime4[4])
            {
                _realFillTimeInGame = fixedTimeFill5;
            }
            else if(rd >= probabilityTime4[4] && rd < probabilityTime4[5])
            {
                _realFillTimeInGame = fixedTimeFill6;
            }
            else if(rd >= probabilityTime4[5] && rd < probabilityTime4[6])
            {
                _realFillTimeInGame = fixedTimeFill7;
            }
        }

        private void RandomTime5()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime5[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime5[0] && rd < probabilityTime5[1])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
            else if(rd >= probabilityTime5[1] && rd < probabilityTime5[2])
            {
                _realFillTimeInGame = fixedTimeFill3;
            }
            else if(rd >= probabilityTime5[2] && rd < probabilityTime5[3])
            {
                _realFillTimeInGame = fixedTimeFill4;
            }
            else if(rd >= probabilityTime5[3] && rd < probabilityTime5[4])
            {
                _realFillTimeInGame = fixedTimeFill5;
            }
            else if(rd >= probabilityTime5[4] && rd < probabilityTime5[5])
            {
                _realFillTimeInGame = fixedTimeFill6;
            }
            else if(rd >= probabilityTime5[5] && rd < probabilityTime5[6])
            {
                _realFillTimeInGame = fixedTimeFill7;
            }
        }

        private void RandomTime6()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime6[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime6[0] && rd < probabilityTime6[1])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
            else if(rd >= probabilityTime6[1] && rd < probabilityTime6[2])
            {
                _realFillTimeInGame = fixedTimeFill3;
            }
            else if(rd >= probabilityTime6[2] && rd < probabilityTime6[3])
            {
                _realFillTimeInGame = fixedTimeFill4;
            }
            else if(rd >= probabilityTime6[3] && rd < probabilityTime6[4])
            {
                _realFillTimeInGame = fixedTimeFill5;
            }
            else if(rd >= probabilityTime6[4] && rd < probabilityTime6[5])
            {
                _realFillTimeInGame = fixedTimeFill6;
            }
            else if(rd >= probabilityTime6[5] && rd < probabilityTime6[6])
            {
                _realFillTimeInGame = fixedTimeFill7;
            }
        }
        
        private void RandomTime7()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilityTime7[0])
            {
                _realFillTimeInGame = fixedTimeFill1;
            }
            else if(rd >= probabilityTime7[0] && rd < probabilityTime7[1])
            {
                _realFillTimeInGame = fixedTimeFill2;
            }
            else if(rd >= probabilityTime7[1] && rd < probabilityTime7[2])
            {
                _realFillTimeInGame = fixedTimeFill3;
            }
            else if(rd >= probabilityTime7[2] && rd < probabilityTime7[3])
            {
                _realFillTimeInGame = fixedTimeFill4;
            }
            else if(rd >= probabilityTime7[3] && rd < probabilityTime7[4])
            {
                _realFillTimeInGame = fixedTimeFill5;
            }
            else if(rd >= probabilityTime7[4] && rd < probabilityTime7[5])
            {
                _realFillTimeInGame = fixedTimeFill6;
            }
            else if(rd >= probabilityTime7[5] && rd < probabilityTime7[6])
            {
                _realFillTimeInGame = fixedTimeFill7;
            }
        }
        #endregion

        #region RandomSizeOfTracker
        
        private void CheckRealSize()
        {
            var data = DataAccountPlayer.PlayerChallenger4PowerUp;
            if (data.WinCount <= 0)
            {
                RandomSize1();
            }
            else if( data.WinCount > 0 && data.WinCount <= 3)
            {
                RandomSize2();
            }
            else if(data.WinCount > 3 && data.WinCount <= 6)
            {
                RandomSize3();
            }
            else if(data.WinCount > 6 && data.WinCount <= 9)
            {
                RandomSize4();
            }
            else if(data.WinCount > 9 && data.WinCount <= 12)
            {
                RandomSize5();
            }
            else if(data.WinCount > 12 && data.WinCount <= 15)
            {
                RandomSize6();
            }
            else if(data.WinCount > 15)
            {
                RandomSize7();
            }
        }
        private void RandomSize1()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize1[0])
            {
                _realFactorOfSize = factorOfSizeTracker[0];
            }
            else if(rd >= probabilityTime1[0])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
        }
        
        private void RandomSize2()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize2[0])
            {
                _realFactorOfSize = factorOfSizeTracker[0];
            }
            else if (rd >= probabilityTime2[0] && rd < probabilityTime2[1])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
            else if (rd >= probabilityTime2[1] && rd < probabilityTime2[2])
            {
                _realFactorOfSize = factorOfSizeTracker[2];
            }
            else if (rd >= probabilityTime2[2] && rd < probabilityTime2[3])
            {
                _realFactorOfSize = factorOfSizeTracker[3];
            }
        }
        
        private void RandomSize3()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize3[0])
            {
                _realFactorOfSize = factorOfSizeTracker[0];
            }
            else if (rd >= probabilitySize3[0] && rd < probabilitySize3[1])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
            else if (rd >= probabilitySize3[1] && rd < probabilitySize3[2])
            {
                _realFactorOfSize = factorOfSizeTracker[2];
            }
            else if (rd >= probabilitySize3[2] && rd < probabilitySize3[3])
            {
                _realFactorOfSize = factorOfSizeTracker[3];
            }
        }
        
        private void RandomSize4()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize4[0])
            {
                _realFactorOfSize = factorOfSizeTracker[0];
            }
            else if (rd >= probabilitySize4[0] && rd < probabilitySize4[1])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
            else if (rd >= probabilitySize4[1] && rd < probabilitySize4[2])
            {
                _realFactorOfSize = factorOfSizeTracker[2];
            }
            else if (rd >= probabilitySize4[2] && rd < probabilitySize4[3])
            {
                _realFactorOfSize = factorOfSizeTracker[3];
            }
        }
        
        private void RandomSize5()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize5[1])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
            else if (rd >= probabilitySize5[1] && rd < probabilitySize5[2])
            {
                _realFactorOfSize = factorOfSizeTracker[2];
            }
            else if (rd >= probabilitySize5[2] && rd < probabilitySize5[3])
            {
                _realFactorOfSize = factorOfSizeTracker[3];
            }
        }
        
        private void RandomSize6()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize6[1])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
            else if (rd >= probabilitySize6[1] && rd < probabilitySize6[2])
            {
                _realFactorOfSize = factorOfSizeTracker[2];
            }
            else if (rd >= probabilitySize6[2] && rd < probabilitySize6[3])
            {
                _realFactorOfSize = factorOfSizeTracker[3];
            }
        }
        
        private void RandomSize7()
        {
            var rd = Random.Range(0, 101);
            if (rd < probabilitySize7[1])
            {
                _realFactorOfSize = factorOfSizeTracker[1];
            }
            else if (rd >= probabilitySize7[1] && rd < probabilitySize7[2])
            {
                _realFactorOfSize = factorOfSizeTracker[2];
            }
            else if (rd >= probabilitySize7[2] && rd < probabilitySize7[3])
            {
                _realFactorOfSize = factorOfSizeTracker[3];
            }
        }
        #endregion
    }
}
