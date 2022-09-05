using Core.Common.Sound;
using DG.Tweening;
using GameManagerChallenger1.UI;
using GameManagerChallenger1.UI.SingProgress;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;

namespace GameManagerChallenger1.Enemy.Dolly
{
    public class DollyController : MonoBehaviour
    {
        public enum States
        {
            Init, Idle, Sing, TurnBack
        }
        public States state = States.Init;
        [SerializeField] private float turnBackTime;
        private DollySpine _dollySpine;
        private UiController1 _uiController1;
        private Gameplay.Managers.GameManagerChallenger1 _gameManagerChallenger1;
        private SingProgressController _singProgressController;
        private float _turnBackTime;
        private bool _rdTime;
        private bool _isWarningTime;
        private bool _isSinging;
        private void Awake()
        {
            _dollySpine = FindObjectOfType<DollySpine>();
            _uiController1 = FindObjectOfType<UiController1>();
            _gameManagerChallenger1 = FindObjectOfType<Gameplay.Managers.GameManagerChallenger1>();
            _singProgressController = FindObjectOfType<SingProgressController>();
            _turnBackTime = turnBackTime;
            _rdTime = false;
            _isWarningTime = false;
            _isSinging = false;
        }

        private void Update()
        {
            switch (state)
            {
                case States.Init:
                    DOVirtual.DelayedCall(0.01f, delegate
                    {
                        ChangeState(States.Idle);
                    });
                    break;
                case States.Idle:
                    if (_gameManagerChallenger1.state == Gameplay.Managers.GameManagerChallenger1.States.Play)
                    {
                        ChangeState(States.Sing);
                    }
                    break;
                case States.Sing:
                    Singing();
                    break;
                case States.TurnBack:
                    TurnBack();
                    break;
                default:
                    break;
            }
        }
        
        private void ChangeState(States newstate)
        {
            if (newstate == state) return;
            ExitCurrentState();
            state = newstate;
            EnterNewState();
        }

        private void Singing()
        {
            if (_rdTime == false)
            {
                _singProgressController.CheckRealTimeSing();
                Debug.Log(_singProgressController.realTimeSingInGame);
                _rdTime = true;
            }

            if (_isSinging == false && _gameManagerChallenger1.state == Gameplay.Managers.GameManagerChallenger1.States.Play)
            {
                _isSinging = true;
                SoundManager.Instance.PlayDollySong(SoundType.DollySing, _singProgressController.realTimeSingInGame);
            }
            _singProgressController.currentTimeSing += Time.deltaTime;
            _singProgressController.AutoFillSingProgress();
            
            if (_singProgressController.currentTimeSing >= _singProgressController.realTimeSingInGame * 0.75f && state != States.TurnBack)
            {
                if (_isWarningTime == false)
                {
                    _isWarningTime = true;
                    _uiController1.warningArea.DOColor(new Color(1, 0, 0, 0), 0);
                    _uiController1.warningArea.DOFade(0.2f, _singProgressController.realTimeSingInGame * 0.25f);
                }
                
            }

            if (_singProgressController.currentTimeSing >= _singProgressController.realTimeSingInGame)
            {
                ChangeState(States.TurnBack);
                _singProgressController.currentTimeSing = 0;
            }
        }

        private void TurnBack()
        {
            _turnBackTime -= Time.deltaTime;
            if (_turnBackTime <= 0)
            {
                ChangeState(States.Sing);
            }
        }

        #region FSM
        private void EnterNewState()
        {
            switch (state)
            {
                case States.Init:
                    break;
                case States.Idle:
                    _dollySpine.PlayIdle();
                    break;
                case States.Sing:
                    _dollySpine.PlaySing();
                    _turnBackTime = turnBackTime;
                    _isWarningTime = false;
                    break;
                case States.TurnBack:
                    SoundManager.Instance.PauseDollySong(SoundType.DollySing);
                    _isSinging = false;
                    _uiController1.normalSkull.gameObject.SetActive(false);
                    _uiController1.redEyeSkull.gameObject.SetActive(true);
                    _dollySpine.PlayTurnBack();
                    _uiController1.ChangeDangerous();
                    break;
                default:
                    break;
            }
        }
    
        private void ExitCurrentState()
        {
            switch (state)
            {
                case States.Init:
                    break;
                case States.Idle:
                    break;
                case States.Sing:
                    _rdTime = false;
                    break;
                case States.TurnBack:
                    _uiController1.normalSkull.gameObject.SetActive(true);
                    _uiController1.redEyeSkull.gameObject.SetActive(false);
                    _uiController1.ChangeSafety();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
