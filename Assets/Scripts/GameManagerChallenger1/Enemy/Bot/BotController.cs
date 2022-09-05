using System.Collections.Generic;
using Core.Common.Sound;
using DG.Tweening;
using GameManagerChallenger1.Enemy.Dolly;
using Plugins.Scripts.Core.Common.Sound;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using PoliceController = GameManagerChallenger1.Enemy.Police.PoliceController;
using Random = UnityEngine.Random;

namespace GameManagerChallenger1.Enemy.Bot
{
    public class BotController : MonoBehaviour
    {
       public enum States
       {
          Init, Prepare, Idle, Run, Die, Win
       }
       public States state = States.Init;
       public bool isNotStop;
        
       [SerializeField] private float speed = 1f;
       [SerializeField] private float timeToStopAfterWin;
       [SerializeField] private float percentStop = 0.5f;
       [SerializeField] private float prepareTime;
       [SerializeField] private TextMeshPro idTxt;
       [SerializeField] private ParticleSystem blood;
       [SerializeField] private ParticleSystem[] soul;
       private BotSpine _botSpine;
       private DollyController _dollyController;
       private PoliceController[] _policeControllers;
       private SkeletonAnimation _skeletonAnimation;
       private Rigidbody2D _rb;
       private BoxCollider2D _boxCollider2D;
       private bool _checkRandom;
       private bool _isWin;
       private void Awake()
       {
           _isWin = false;
           isNotStop = false;
           _checkRandom = false;
           _rb = GetComponent<Rigidbody2D>();
           _boxCollider2D = GetComponent<BoxCollider2D>();
           _botSpine = GetComponentInChildren<BotSpine>();
           _dollyController = FindObjectOfType<DollyController>();
           _policeControllers = FindObjectsOfType<PoliceController>();
           _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
       }

       private void Start()
       {
           SetRandomSkinForBot();
           SetRandomIdNumber();
       }

       private void Update()
       {
           if (_isWin && state != States.Die)
           {
               ChangeState(States.Win);
           }
           if (_dollyController.state == DollyController.States.Sing)
           {
               _checkRandom = false;
               isNotStop = false;
           }
           switch (state)
           {
               case States.Init:
                   DOVirtual.DelayedCall(0.01f, delegate
                   {
                       ChangeState(States.Prepare);
                   });
                   break;
               case States.Idle:
                   if (_dollyController.state == DollyController.States.Sing && isNotStop == false)
                   {
                       var second = Random.Range(0, 1.2f);
                       DOVirtual.DelayedCall(second, delegate
                       {
                           _rb.velocity = new Vector2(-speed, _rb.velocity.y);
                           _botSpine.ResumeAnim();
                           ChangeState(States.Run);
                       });
                   }
                   break;
               case States.Run:
                   foreach (var police in _policeControllers)
                   {
                       if (police.state == PoliceController.States.Die) continue;
                       if (_dollyController.state == DollyController.States.TurnBack &&_checkRandom == false)
                       { 
                           PercentStop();
                           _checkRandom = true;
                           if (isNotStop == false)
                           {
                               _rb.velocity = new Vector2(0, 0);
                               _botSpine.PauseAnim();
                               ChangeState(States.Idle);
                           }
                           else if (isNotStop)
                           {
                               DOVirtual.DelayedCall(0.25f, delegate
                               {
                                   ChangeState(States.Idle);
                                   _rb.velocity = new Vector2(0, 0);
                                   DOVirtual.DelayedCall(0.25f, delegate
                                   {
                                       ChangeState(States.Die);
                                   });
                               });
                               
                           }
                       }
                   }
                   break;
               case States.Die:
                   break;
               case States.Win:
                   _rb.velocity = new Vector2(-speed * 4.5f, _rb.velocity.y);
                   DOVirtual.DelayedCall(timeToStopAfterWin, delegate
                   {
                       _rb.velocity = new Vector2(0, 0);
                   });
                   break;
               case States.Prepare:
                   break;
               default:
                   break;
           }
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.CompareTag("Goal"))
           {
               _isWin = true;
               _boxCollider2D.enabled = false;
           }
           if (other.CompareTag("Start"))
           {
               ChangeState(States.Idle);
           }
       }

       private void PercentStop()
       {
           var percent = Random.Range(0, 1f);
           if (percent < percentStop)
           {
               isNotStop = true;
           }
       }
       
       private void SetRandomSkinForBot()
       {
           var listSkins = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Skins.Items;
           List<Skin> skins = new List<Skin>(listSkins);
           
           for (int i = skins.Count - 1; i >= 0; i--)
           {
               var skin = skins[i];
               var isCanNotUse = _botSpine.IsCanNotUseSkin(skin.Name);
               if (isCanNotUse)
               {
                   skins.RemoveAt(i);
               }
           }

           var rdIndex = Random.Range(0, skins.Count);
           var skinName = skins[rdIndex].Name;
            
           _skeletonAnimation.Skeleton.SetSkin(skinName);
           _skeletonAnimation.Skeleton.SetSlotsToSetupPose();
           _skeletonAnimation.AnimationState.Apply(_skeletonAnimation.Skeleton);
       }
       
       private void SetRandomIdNumber()
       {
           var i = Random.Range(1, 999);
           if (i < 10)
           {
               idTxt.text = "00" + i;
           }
           else if(i > 10 && i < 100)
           {
               idTxt.text = "0" + i;
           }
           else
           {
               idTxt.text = i.ToString();
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
           switch (state)
           {
               case States.Init:
                   break;
               case States.Idle:
                   _botSpine.PlayIdle();
                   break;
               case States.Run:
                   _botSpine.PlayRun();
                   break;
               case States.Die:
                   SoundManager.Instance.PlaySound(SoundType.Die);
                   idTxt.gameObject.SetActive(false);
                   blood.gameObject.SetActive(true);
                   _botSpine.PlayDie();
                   DOVirtual.DelayedCall(0.25f, delegate
                   {
                       blood.Play();
                       DOVirtual.DelayedCall(0.5f, delegate
                       {
                           var i = Random.Range(0, 4);
                           soul[i].gameObject.SetActive(true);
                           soul[i].Play();
                       });
                   });
                   break;
               case States.Win:
                   _botSpine.PlayRun();
                   DOVirtual.DelayedCall(timeToStopAfterWin, delegate
                   {
                       _botSpine.PlayWin();
                       speed = 0;
                       SoundManager.Instance.PlaySound(SoundType.Victory);
                   });
                   break;
               case States.Prepare:
                   _botSpine.PlayRun();
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
               case States.Run:
                   break;
               case States.Die:
                   break;
               case States.Win:
                   break;
               case States.Prepare:
                   break;
               default:
                   break;
           }
       }
       #endregion
       
    }
}
