using Core.Common.Sound;
using DG.Tweening;
using GameManagerChallenger1.Enemy.Bot;
using GameManagerChallenger1.Enemy.Dolly;
using GameManagerChallenger1.Player;
using GameManagerChallenger1.UI.CountdownTime;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;

namespace GameManagerChallenger1.Enemy.Police
{
    public class PoliceController : MonoBehaviour
    {
       public enum States
       {
           Init, Idle, Shoot, Die
       }
       public States state = States.Init;
       [SerializeField] private ParticleSystem blood;
       [SerializeField] private ParticleSystem[] soul;

       private PoliceSpine _policeSpine;
       private DollyController _dollyController;
       private PlayerController _playerController;
       private BotController[] _botControllers;
       private MixAndMatchSkin _mixAndMatchSkin;
       private void Start()
       {
           _policeSpine = GetComponentInChildren<PoliceSpine>();
           _dollyController = FindObjectOfType<DollyController>();
           _playerController = FindObjectOfType<PlayerController>();
           _botControllers = FindObjectsOfType<BotController>();
           _mixAndMatchSkin = GetComponentInChildren<MixAndMatchSkin>();
       }

       private void Update()
       {
           if (_playerController.state == PlayerController.States.Shoot)
           {
               ChangeState(States.Die);
           }

           if (CountdownTime.Instance.gameDuration <= 0)
           {
               ChangeState(States.Shoot);
           }
           
           switch (state)
           {
               case States.Init:
                   DOVirtual.DelayedCall(0.01f, delegate
                   {
                       ChangeState(States.Idle);
                   });
                   break;
               case States.Idle:
                   if (_dollyController.state == DollyController.States.TurnBack && 
                       _playerController.state == PlayerController.States.Run)
                   {
                       ChangeState(States.Shoot);
                   }

                   foreach (var bot in _botControllers)
                   {
                       if (bot.isNotStop)
                       {
                           DOVirtual.DelayedCall(0.5f, delegate
                           {
                               ChangeState(States.Shoot);
                           });
                       }
                   }
                   break;
               case States.Shoot:
                   DOVirtual.DelayedCall(1f, delegate
                   {
                       ChangeState(States.Idle);
                   });
                   break;
               case States.Die:
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
                   _policeSpine.PlayIdle();
                   break;
               case States.Shoot:
                   _policeSpine.PlayShoot();
                   //SoundManager.Instance.PlaySound(SoundType.GunFire);
                   break;
               case States.Die:
                   blood.gameObject.SetActive(true);
                   blood.Play();
                   _mixAndMatchSkin.UpdateBaseSkin();
                   _mixAndMatchSkin.UpdateCombinedSkin();
                   _policeSpine.PlayDie();
                   DOVirtual.DelayedCall(0.5f, delegate
                   {
                       var i = Random.Range(0, 4);
                       soul[i].gameObject.SetActive(true);
                       soul[i].Play();
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
               case States.Idle:
                   break;
               case States.Shoot:
                   break;
               case States.Die:
                   break;
               default:
                   break;
           }
       }
       #endregion
    }
}
