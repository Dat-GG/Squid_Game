using Challenger4.UI.TimerCountdown;
using Core.Common.Sound;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;

namespace Challenger4.Enemy.Police
{
    public class PoliceController4 : MonoBehaviour
    {
        public enum States
        {
            Init, Idle, Shoot
        }
        public States state = States.Init;
        
        private PoliceSpine4 _policeSpine4;
        private TimerCountdown _timerCountdown;
        private bool _isPlaySound;

        private void Awake()
        {
            _policeSpine4 = GetComponentInChildren<PoliceSpine4>();
            _timerCountdown = FindObjectOfType<TimerCountdown>();
            _isPlaySound = false;
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
                    if (_timerCountdown.gameDuration <= 0)
                    {
                        ChangeState(States.Shoot);
                    }
                    break;
                case States.Shoot:
                    DOVirtual.DelayedCall(1f, delegate
                    {
                        ChangeState(States.Idle);
                    });
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
            switch (state)
            {
                case States.Init:
                    break;
                case States.Idle:
                    _policeSpine4.PlayIdle();
                    break;
                case States.Shoot:
                    _policeSpine4.PlayShoot();
                    if (_isPlaySound == false)
                    {
                        _isPlaySound = true;
                        SoundManager.Instance.PlaySound(SoundType.GunFire);
                    }
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
                case States.Shoot:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
