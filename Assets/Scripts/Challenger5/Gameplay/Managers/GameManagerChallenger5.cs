using Challenger5.Player;
using Challenger5.UI;
using Challenger5.UI.CountTotalAlive;
using Challenger5.UI.TimerCountdown;
using Core.Common.GameResources;
using Core.Common.Popup;
using DataAccount;
using DG.Tweening;
using UnityEngine;

namespace Challenger5.Gameplay.Managers
{
    public class GameManagerChallenger5 : MonoBehaviour
    {
        public enum States
        {
            Init, Play, Win, Lose
        }
        public States state = States.Init;
        public int goldDropPerBot;
        private TimerCountdown5 _timerCountdown5;
        private InputChecking5 _inputChecking5;
        private CountTotalAlive _countTotalAlive;
        private PlayerController5 _playerController5;
        private UiController5 _uiController5;
        private Camera _camera;

        private void Awake()
        {
            _timerCountdown5 = FindObjectOfType<TimerCountdown5>();
            _inputChecking5 = FindObjectOfType<InputChecking5>();
            _countTotalAlive = FindObjectOfType<CountTotalAlive>();
            _uiController5 = FindObjectOfType<UiController5>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            _playerController5 = FindObjectOfType<PlayerController5>();
            _camera.transparencySortMode = TransparencySortMode.CustomAxis;
            _camera.transparencySortAxis = Vector3.up;
        }

        private void Update()
        {
            if (_playerController5.state == PlayerController5.States.Die)
            {
                ChangeState(States.Lose);
            }
            
            switch (state)
            {
                case States.Init:
                    if (_inputChecking5.IsFirstTimeTouch)
                    {
                        ChangeState(States.Play);
                    }
                    break;
                case States.Play:
                    _uiController5.AppearObject();
                    _timerCountdown5.CountdownTime();
                    _countTotalAlive.CountAlive();
                    if (_timerCountdown5.gameDuration <= 0)
                    {
                        _timerCountdown5.OverTime();
                        ChangeState(States.Win);
                    }

                    if (_countTotalAlive.alive == 1)
                    {
                        ChangeState(States.Win);
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
            var data = DataAccountPlayer.BotChallenger5PowerUp;
            switch (state)
            {
                case States.Init:
                    break;
                case States.Play:
                    break;
                case States.Win:
                    DataAccountPlayer.PlayerChallenger5PowerUp.FirstGame++;
                    DataAccountPlayer.PlayerChallenger5PowerUp.WinCount++;
                    data.SetPowerUp(false, PowerUpBotType.Hammer, 1);
                    data.SetPowerUp(false, PowerUpBotType.Giant, 1);
                    data.SetPowerUp(false, PowerUpBotType.Katana, 1);
                    data.SetPowerUp(false, PowerUpBotType.Tumble, 1);
                    data.SetPowerUp(false, PowerUpBotType.Shield, 1);
                    DOVirtual.DelayedCall(2f, delegate
                    {
                        PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
                    });
                    break;
                case States.Lose:
                    _uiController5.settingBtn.interactable = false;
                    DataAccountPlayer.PlayerChallenger5PowerUp.FirstGame++;
                    DataAccountPlayer.PlayerChallenger5PowerUp.LoseCount++;
                    data.SetPowerUp(false, PowerUpBotType.Hammer, 1);
                    data.SetPowerUp(false, PowerUpBotType.Giant, 1);
                    data.SetPowerUp(false, PowerUpBotType.Katana, 1);
                    data.SetPowerUp(false, PowerUpBotType.Tumble, 1);
                    data.SetPowerUp(false, PowerUpBotType.Shield, 1);
                    DOVirtual.DelayedCall(1f, delegate
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
