using Challenger4.Player;
using Challenger4.UI;
using Challenger4.UI.ProcessBar;
using Challenger4.UI.TimerCountdown;
using Core.Common.Popup;
using DataAccount;
using DG.Tweening;
using UnityEngine;

namespace Challenger4.Gameplay.Managers
{
    public class GameManagerChallenger4 : MonoBehaviour
    {
        public enum States
        {
            Init, Play, Win, Lose
        }
        public States state;
        public bool isWin;
        public bool isBotTugWin;
        [SerializeField] private GameObject inviBox;
        private InputChecking4 _inputChecking4;
        private PlayerController4 _playerController4;
        private ProcessBarController _processBarController;
        private TimerCountdown _timerCountdown;
        private UiController _uiController;
        private int _checkStart;
        
        private void Awake()
        {
            _inputChecking4 = FindObjectOfType<InputChecking4>();
            _timerCountdown = FindObjectOfType<TimerCountdown>();
            isWin = false;
            isBotTugWin = false;
        }
        
        private void Start()
        {
            _playerController4 = FindObjectOfType<PlayerController4>();
            _processBarController = FindObjectOfType<ProcessBarController>();
            _uiController = FindObjectOfType<UiController>();
        }

        private void Update()
        {
            if (_playerController4.state == PlayerController4.States.Win)
            {
                ChangeState(States.Win);
            }

            if (_playerController4.state == PlayerController4.States.Idle)
            {
                _uiController.panelBlock.gameObject.SetActive(false);
            }
            
            switch (state)
            {
                case States.Init:
                    if (_inputChecking4.IsFirstTimeTouch && _checkStart < 1 && _playerController4.state == PlayerController4.States.Idle)
                    {
                        ChangeState(States.Play);
                        _checkStart++;
                    }
                    break;
                case States.Play:
                    _processBarController.AutoFillProcess();
                    _timerCountdown.CountdownTime();
                    
                    if (_timerCountdown.gameDuration <= 0)
                    {
                        _timerCountdown.OverTime();
                        ChangeState(States.Lose);
                    }
                    break;
                case States.Win:
                    break;
                case States.Lose:
                    break;
                default:
                    break;
            }
        }

        public void ChangeState(States newstate)
        {
            if (newstate == state) return;
            ExitCurrentState();
            state = newstate;
            EnterNewState();
        }
        
        #region FSM
        private void EnterNewState()
        {
            var data = DataAccountPlayer.PlayerChallenger4PowerUp;
            switch (state)
            {
                case States.Init:
                    break;
                case States.Play:
                    _uiController.tapToPlay.gameObject.SetActive(false);
                    _uiController.AppearObject();
                    break;
                case States.Win:
                    isWin = true;
                    inviBox.SetActive(false);
                    data.WinCount++;
                    data.CountSpin++;
                    DOVirtual.DelayedCall(3f, delegate
                    {
                        PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
                    });
                    break;
                case States.Lose:
                    _uiController.settingBtn.interactable = false;
                    inviBox.SetActive(false);
                    data.LoseCount++;
                    data.CountSpin++;
                    DOVirtual.DelayedCall(3f, delegate
                    {
                        PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LosePopup);
                    });
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
                case States.Play:
                    break;
                case States.Win:
                    break;
                case States.Lose:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
