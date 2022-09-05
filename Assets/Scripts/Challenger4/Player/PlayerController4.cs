using System.Collections.Generic;
using Challenger4.Banana;
using Challenger4.Enemy.Bot;
using Challenger4.Gameplay.Managers;
using Challenger4.UI.ProcessBar;
using Challenger4.UI.Spin;
using Challenger4.UI.TimerCountdown;
using Core.Common.Sound;
using Core.Pool;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger4.Player
{
    public class PlayerController4 : MonoBehaviour
    {
        public enum States
        {
            Init, Idle, Run, Pull, Throw, Fall, Win, Die
        }
        public States state = States.Init;
        public bool isHaveBanana;
        public bool isGiantPower;
        public bool isPowerPower;
        public bool isFreezePower;
        public bool isBananaFail;
        public List<BotController4> rightBot = new List<BotController4>();
        public List<BotController4> leftBot = new List<BotController4>();

        [SerializeField] private Vector3 startPos;
        [SerializeField] private float speed;
        [SerializeField] private SpriteRenderer powerAura;
        [SerializeField] private TextMeshPro idTxt;
        [SerializeField] private ParticleSystem blood;
        [SerializeField] private ParticleSystem[] soul;
        [SerializeField] private float delayTimeRight;
        [SerializeField] private float delayTimeLeft;
        [SerializeField] private float prepareTime;
        [SerializeField] private Transform bananaPos;
        [SerializeField] private GameObject bananaPrefab;
        [SerializeField] private GameObject liquid;
        private Rigidbody2D _rb;
        private PlayerSpine4 _playerSpine4;
        private GameManagerChallenger4 _gameManager;
        private TimerCountdown _timerCountdown;
        private ProcessBarController _processBar;
        private BotController4[] _botControllers;
        private ModuleUiPowerUpBtn4 _moduleUi;
        private ManagerModelPlatformer4 _manager;
        private GameObject _banana;
        private GameObject _liquid;
        private bool _isFall;
        private float _delayRight;
        private float _delayLeft;
        private int _botIndexRight;
        private int _botIndexLeft;
        private void Awake()
        {
            _isFall = false;
            isHaveBanana = false;
            isGiantPower = false;
            isPowerPower = false;
            isFreezePower = false;
            isBananaFail = false;
            _rb = GetComponent<Rigidbody2D>();
            _playerSpine4 = GetComponentInChildren<PlayerSpine4>();
            _gameManager = FindObjectOfType<GameManagerChallenger4>();
            _processBar = FindObjectOfType<ProcessBarController>();
            _timerCountdown = FindObjectOfType<TimerCountdown>();
            _moduleUi = FindObjectOfType<ModuleUiPowerUpBtn4>();
            _manager = FindObjectOfType<ManagerModelPlatformer4>();
        }

        private void Start()
        {
            _botControllers = FindObjectsOfType<BotController4>();
            _delayRight = delayTimeRight;
            _delayLeft = delayTimeLeft;
            foreach (var bot in _botControllers)
            {
                if (bot.isRightBot)
                {
                    rightBot.Add(bot);
                }
                else if(bot.isLeftBot)
                {
                    leftBot.Add(bot);
                }
            }

            _botIndexRight = rightBot.Count;
            _botIndexLeft = leftBot.Count;
        }

        private void Update()
        {
            if (_moduleUi.isHaveBananaPower)
            {
                isHaveBanana = true;
                ChangeState(States.Throw);
            }

            if (_moduleUi.isHaveFreezePower)
            {
                isFreezePower = true;
            }

            if (_moduleUi.isHaveGiantPower)
            {
                isGiantPower = true;
            }

            if (_moduleUi.isHavePowerPower)
            {
                isPowerPower = true;
                powerAura.gameObject.SetActive(true);
            }
            
            if (_isFall)
            {
                ChangeState(States.Fall);
            }
            
            foreach (var bot in _botControllers)
            {
                if (bot.isFall && bot.isLeftBot)
                {
                    ChangeState(States.Win);
                }
            }

            if (_timerCountdown.gameDuration <= 0)
            {
                ChangeState(States.Die);
                RightBotDieOrder();
                LeftBotDieOrder();
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
                    if (_gameManager.state == GameManagerChallenger4.States.Play)
                    {
                        ChangeState(States.Pull);
                    }
                    break;
                case States.Pull:
                    if (_processBar.isLeft)
                    {
                        if (isFreezePower)
                        {
                            MoveLeft(0);
                        }
                        else
                        {
                            MoveLeft(speed);
                        }
                    }
                    else if(_processBar.isRight)
                    {
                        MoveRight(speed);
                    }
                    break;
                case States.Fall:
                    _rb.bodyType = RigidbodyType2D.Dynamic;
                    var i = Random.Range(60, 120);
                    if (_isFall)
                    {
                        transform.DOLocalRotate(new Vector3(0, 0, i), 0);
                        _isFall = false;
                    }
                    DOVirtual.DelayedCall(0.3f, delegate
                    {
                        _rb.velocity = new Vector2(0, -speed * 10);
                    });
                    break;
                case States.Die:
                    break;
                case States.Win:
                    break;
                case States.Throw:
                    DOVirtual.DelayedCall(1f, delegate
                    {
                        _banana.SetActive(false);
                        if (isBananaFail == false)
                        {
                            _liquid = SmartPool.Instance.Spawn(liquid, _manager.bananaEndPos.position,
                                Quaternion.identity, new Vector3(0.5f, 0.5f, 1));
                            _liquid.transform.SetParent(_manager.bananaEndPos);
                            isBananaFail = true;
                        }
                    });
                    break;
                case States.Run:
                    transform.DOMove(startPos, prepareTime);
                    DOVirtual.DelayedCall(prepareTime - 0.5f, delegate
                    {
                        ChangeState(States.Idle);
                    });
                    break;
                default:
                    break;
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

        private void RightBotDieOrder()
        {
            delayTimeRight -= Time.deltaTime;
            if (delayTimeRight > 0) return;
            if(_botIndexRight <= 0) return;
            rightBot[_botIndexRight - 1].isDead = true;
            delayTimeRight = _delayRight;
            _botIndexRight--;
        }
        
        private void LeftBotDieOrder()
        {
            delayTimeLeft -= Time.deltaTime;
            if (delayTimeLeft > 0) return;
            if(_botIndexLeft <= 0) return;
            leftBot[_botIndexLeft - 1].isDead = true;
            delayTimeLeft = _delayLeft;
            _botIndexLeft--;
        }
        
        private void RightBotRun()
        {
            rightBot[3].transform.DOMove(_manager.rightBotStart[0].position, prepareTime);
            rightBot[2].transform.DOMove(_manager.rightBotStart[1].position, prepareTime);
            rightBot[1].transform.DOMove(_manager.rightBotStart[2].position, prepareTime);
            rightBot[0].transform.DOMove(_manager.rightBotStart[3].position, prepareTime);
        }

        private void LeftBotRun()
        {
            leftBot[4].transform.DOMove(_manager.leftBotStart[0].position, prepareTime);
            leftBot[3].transform.DOMove(_manager.leftBotStart[1].position, prepareTime);
            leftBot[2].transform.DOMove(_manager.leftBotStart[2].position, prepareTime);
            leftBot[1].transform.DOMove(_manager.leftBotStart[3].position, prepareTime);
            leftBot[0].transform.DOMove(_manager.leftBotStart[4].position, prepareTime);
        }

        private void ChangeState(States newstate)
        {
            if (newstate == state) return;
            ExitCurrentState();
            state = newstate;
            EnterNewState();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DeadPoint"))
            {
                _isFall = true;
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
                    _playerSpine4.PlayIdle();
                    break;
                case States.Pull:
                    _playerSpine4.PlayPull();
                    break;
                case States.Fall:
                    _playerSpine4.PlayFall();
                    SoundManager.Instance.PlaySound(SoundType.Fall);
                    break;
                case States.Die:
                    SoundManager.Instance.PlaySound(SoundType.Die);
                    idTxt.gameObject.SetActive(false);
                    blood.gameObject.SetActive(true);
                    blood.Play();
                    _playerSpine4.PlayDie();
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        var i = Random.Range(0, 4);
                        soul[i].gameObject.SetActive(true);
                        soul[i].Play();
                    });
                    break;
                case States.Win:
                    SoundManager.Instance.PlaySound(SoundType.Victory);
                    _playerSpine4.PlayWin();
                    break;
                case States.Throw:
                    _moduleUi.isHaveBananaPower = false;
                    _banana = SmartPool.Instance.Spawn(bananaPrefab, bananaPos.position,
                        Quaternion.identity, new Vector3(0.4f, 0.4f, 1));
                    StartCoroutine(_banana.GetComponentInChildren<BananaMove>().Curve(bananaPos.transform.position,
                        _manager.bananaEndPos.position));
                    _playerSpine4.PlayThrow();
                    break;
                case States.Run:
                    _playerSpine4.PlayRun();
                    RightBotRun();
                    LeftBotRun();
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
                    powerAura.gameObject.SetActive(false);
                    break;
                case States.Win:
                    powerAura.gameObject.SetActive(false);
                    break;
                case States.Throw:
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
