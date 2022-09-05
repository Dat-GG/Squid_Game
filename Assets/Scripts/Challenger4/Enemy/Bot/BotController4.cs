using System.Collections.Generic;
using Challenger4.Gameplay.Managers;
using Challenger4.Player;
using Challenger4.UI.ProcessBar;
using Core.Common.Sound;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using Spine;
using UnityEngine;
using Spine.Unity;
using TMPro;
using Random = UnityEngine.Random;

namespace Challenger4.Enemy.Bot
{
    public class BotController4 : MonoBehaviour
    {
        public enum States
        {
            Init, Run, Idle, Pull, Fall, Win, Die
        }
        public States state = States.Init;
        public bool isFall;
        public bool isDead;
        public bool isRightBot;
        public bool isLeftBot;
        
        [SerializeField] private float speed;
        [SerializeField] private SpriteRenderer freeze;
        [SerializeField] private TextMeshPro idTxt;
        [SerializeField] private ParticleSystem blood;
        [SerializeField] private ParticleSystem[] soul;
        [SerializeField] private float prepareTime;
        
        private BotSpine4 _botSpine4;
        private SkeletonAnimation _skeletonAnimation;
        private Rigidbody2D _rb;
        private GameManagerChallenger4 _gameManagerChallenger4;
        private ManagerModelPlatformer4 _mangagerModelPlatformer4;
        private ProcessBarController _processBarController;
        private PlayerController4 _playerController4;
        private void Awake()
        {
            isLeftBot = false;
            isRightBot = false;
            isFall = false;
            isDead = false;
            _botSpine4 = GetComponentInChildren<BotSpine4>();
            _rb = GetComponent<Rigidbody2D>();
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _gameManagerChallenger4 = FindObjectOfType<GameManagerChallenger4>();
            _mangagerModelPlatformer4 = FindObjectOfType<ManagerModelPlatformer4>();
            _processBarController = FindObjectOfType<ProcessBarController>();
            _playerController4 = FindObjectOfType<PlayerController4>();
            freeze.gameObject.SetActive(false);
            if (transform.position.x >= _mangagerModelPlatformer4.botRightSpawnStartPoint.position.x)
            {
                isRightBot = true;
            }
            else
            {
                isLeftBot = true;
            }
        }

        private void Start()
        {
            SetRandomSkinForBot();
            SetRandomIdNumber();
        }

        private void Update()
        {
            if (isFall)
            {
                ChangeState(States.Fall);
            }

            if (isDead)
            {
                ChangeState(States.Die);
            }
            
            if (_playerController4.state == PlayerController4.States.Win && isRightBot)
            {
                ChangeState(States.Win);
            }

            if (_playerController4.isFreezePower && isLeftBot)
            {
                freeze.gameObject.SetActive(true);
                _botSpine4.PauseAnim();
            }

            switch (state)
            {
                case States.Init:
                    DOVirtual.DelayedCall(0.01f, delegate
                    {
                        ChangeState(States.Run);
                    });
                    break;
                case States.Idle:
                    if (_playerController4.isHaveBanana && isLeftBot && _playerController4.isBananaFail)
                    {
                        MoveRight(speed * 5f);
                    }
                    
                    if (_gameManagerChallenger4.state == GameManagerChallenger4.States.Play)
                    {
                        ChangeState(States.Pull);
                    }
                    break;
                case States.Pull:
                    if (_playerController4.isHaveBanana && isLeftBot && _playerController4.isBananaFail)
                    {
                        MoveRight(speed * 5f);
                    }
                    
                    if (_processBarController.isLeft)
                    {
                        if (_playerController4.isFreezePower)
                        {
                            MoveLeft(0);
                        }
                        else
                        {
                            MoveLeft(speed);
                        }
                    }
                    else if(_processBarController.isRight)
                    {
                        if (_playerController4.isGiantPower && isLeftBot)
                        {
                            MoveRight(speed * 3);
                        }
                        else if (_processBarController.isPower && isLeftBot)
                        {
                            MoveRight(speed * 25);
                        }
                        else
                        {
                            MoveRight(speed);
                        }
                    }
                    
                    switch (_playerController4.state)
                    {
                        case PlayerController4.States.Fall when isRightBot:
                            _rb.velocity = new Vector2(-speed * 6, 0);
                            break;
                        case PlayerController4.States.Win when isLeftBot:
                            _rb.velocity = new Vector2(speed * 6, 0);
                            break;
                    }

                    if (_playerController4.state == PlayerController4.States.Fall && isLeftBot)
                    {
                        ChangeState(States.Win);
                        _gameManagerChallenger4.isBotTugWin = true;
                        _gameManagerChallenger4.ChangeState(GameManagerChallenger4.States.Lose);
                    }
                    break;
                case States.Fall:
                    _rb.bodyType = RigidbodyType2D.Dynamic;
                    var i = Random.Range(60, 120);
                    if (isFall)
                    {
                        transform.DOLocalRotate(isLeftBot ? new Vector3(0, 0, -i) : new Vector3(0, 0, i), 0);
                        isFall = false;
                    }
                    DOVirtual.DelayedCall(0.2f, delegate
                    {
                        _rb.velocity = new Vector2(0, -speed * 10);
                    });
                    break;
                case States.Die:
                    break;
                case States.Win:
                    break;
                case States.Run:
                    DOVirtual.DelayedCall(prepareTime, delegate
                    {
                        ChangeState(States.Idle);
                    });
                    break;
                default:
                    break;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DeadPoint"))
            {
                isFall = true;
            }
        }
        
        private void MoveLeft(float a)
        {
            _rb.velocity = new Vector2(-a, 0);
            DOVirtual.DelayedCall(0.2f, delegate
            {
                _rb.velocity = new Vector2(0, 0);
            });
        }

        private void MoveRight(float a)
        {
            _rb.velocity = new Vector2(a, 0);
            DOVirtual.DelayedCall(0.2f, delegate
            {
                _rb.velocity = new Vector2(0, 0);
            });
        }

        public void ChangeState(States newstate)
        {
            if (newstate == state) return;
            ExitCurrentState();
            state = newstate;
            EnterNewState();
        }
        
        private void SetRandomSkinForBot()
        {
            var listSkins = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Skins.Items;
            List<Skin> skins = new List<Skin>(listSkins);
           
            for (int i = skins.Count - 1; i >= 0; i--)
            {
                var skin = skins[i];
                var isCanNotUse = _botSpine4.IsCanNotUseSkin(skin.Name);
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
            
            if (isRightBot)
            {
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 180, 74), 0);
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
                    _botSpine4.PlayIdle();
                    break;
                case States.Pull:
                    _botSpine4.PlayPull();
                    break;
                case States.Fall:
                    _botSpine4.PlayFall();
                    SoundManager.Instance.PlaySound(SoundType.Fall);
                    break;
                case States.Die:
                    SoundManager.Instance.PlaySound(SoundType.Die);
                    idTxt.gameObject.SetActive(false);
                    blood.gameObject.SetActive(true);
                    blood.Play();
                    _botSpine4.PlayDie();
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        var i = Random.Range(0, 4);
                        soul[i].gameObject.SetActive(true);
                        soul[i].Play();
                    });
                    break;
                case States.Win:
                    SoundManager.Instance.PlaySound(SoundType.Victory);
                    _botSpine4.PlayWin();
                    break;
                case States.Run:
                    _botSpine4.PlayRun();
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
                case States.Pull:
                    break;
                case States.Fall:
                    break;
                case States.Die:
                    break;
                case States.Win:
                    break;
                case States.Run:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
