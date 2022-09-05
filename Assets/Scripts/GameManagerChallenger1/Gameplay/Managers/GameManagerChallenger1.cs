using Core.Common.Popup;
using DataAccount;
using DG.Tweening;
using GameManagerChallenger1.Player;
using GameManagerChallenger1.UI;
using GameManagerChallenger1.UI.CountdownTime;
using UnityEngine;

namespace GameManagerChallenger1.Gameplay.Managers
{
    public class GameManagerChallenger1 : MonoBehaviour
    {
        public enum States
        {
            Init, Play, Win, Lose
        }
        public States state;
        
        private UiController1 _uiController1;
        private InputChecking _inputChecking;
        private PlayerController _playerController;
        private int _checkStart;
        private Camera _camera;

        private void Awake()
        {
            _inputChecking = FindObjectOfType<InputChecking>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            _playerController = FindObjectOfType<PlayerController>();
            _uiController1 = FindObjectOfType<UiController1>();
            _camera.transparencySortMode = TransparencySortMode.CustomAxis;
            _camera.transparencySortAxis = Vector3.up;
        }

        private void Update()
        {
            if (_playerController.state == PlayerController.States.Win)
            {
                ChangeState(States.Win);
            }

            if (_playerController.state == PlayerController.States.Idle)
            {
                _uiController1.panelBlock.gameObject.SetActive(false);
            }

            if (CountdownTime.Instance.gameDuration <= 0 && _playerController.state != PlayerController.States.Win)
            {
                CountdownTime.Instance.OverTime();
                _playerController.ChangeState(PlayerController.States.Die);
            }
            switch (state)
            {
                case States.Init:
                    if (_inputChecking.IsFirstTimeTouch && _checkStart < 1)
                    {
                        ChangeState(States.Play);
                        _checkStart++;
                        _uiController1.holdToMove.gameObject.SetActive(false);
                        _uiController1.MoveObjects();
                    }
                    break;
                case States.Play:
                    CountdownTime.Instance.CountdownTimer();
                    if (_playerController.state == PlayerController.States.Die)
                    {
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
        
        private void ChangeState(States newstate)
        {
            if (newstate == state) return;
            ExitCurrentState();
            state = newstate;
            EnterNewState();
        }

        #region FSM
        private void EnterNewState()
        {
            var data = DataAccountPlayer.PlayerChallenger1PowerUp;
            switch (state)
            {
                case States.Init:
                    break;
                case States.Play:
                    break;
                case States.Win:
                    data.WinCount++;
                    data.CountSpin++;
                    DOVirtual.DelayedCall(1.2f, delegate
                    {
                        PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
                    });
                    break;
                case States.Lose:
                    data.LoseCount++;
                    data.CountSpin++;
                    _uiController1.settingBtn.interactable = false;
                    DOVirtual.DelayedCall(0.6f, delegate
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
