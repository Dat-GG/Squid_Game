using Core.Common.Sound;
using DG.Tweening;
using GameManagerChallenger1.Enemy.Bot;
using GameManagerChallenger1.Enemy.Dolly;
using GameManagerChallenger1.Enemy.Police;
using GameManagerChallenger1.Gameplay.Managers;
using GameManagerChallenger1.UI;
using GameManagerChallenger1.UI.Spin;
using Plugins.Scripts.Core.Common.Sound;
using TMPro;
using UnityEngine;

namespace GameManagerChallenger1.Player
{
    public class PlayerController : MonoBehaviour
    {
        public enum States
        {
            Init, Prepare, Idle, Run, Shoot, Win, Die
        }
        public States state = States.Init;
        public float speed;
        public bool isHaveSpeed;

        [SerializeField] private float timeToStopAfterWin;
        [SerializeField] private float prepareTime;
        [SerializeField] private GameObject shield;
        [SerializeField] private TextMeshPro idTxt;
        [SerializeField] private ParticleSystem blood;
        [SerializeField] private ParticleSystem[] soul;

        private PlayerSpine _playerSpine;
        private DollyController _dollyController;
        private PoliceController[] _policeControllers;
        private BotController[] _botControllers;
        private Rigidbody2D _rb;
        private InputChecking _inputChecking;
        private Gameplay.Managers.GameManagerChallenger1 _gameManagerChallenger1;
        private ModuleUiPowerUpButton _moduleUiPowerUpButton;
        private MixAndMatchSkinPlayer _mixAndMatchSkinPlayer;
        private ManagerModelPlatformer _manager;
        private bool _isWin;
        private bool _isHaveShield;
        private bool _isUnderground;
        private bool _isSmaller;

        private void Awake()
        {
            _isWin = false;
            _isHaveShield = false;
            _isUnderground = false;
            _isSmaller = false;
            isHaveSpeed = false;
            _rb = GetComponent<Rigidbody2D>();
            _playerSpine = GetComponentInChildren<PlayerSpine>();
            _inputChecking = FindObjectOfType<InputChecking>();
            _gameManagerChallenger1 = FindObjectOfType<Gameplay.Managers.GameManagerChallenger1>();
            _moduleUiPowerUpButton = FindObjectOfType<ModuleUiPowerUpButton>();
            _mixAndMatchSkinPlayer = GetComponentInChildren<MixAndMatchSkinPlayer>();
            _manager = FindObjectOfType<ManagerModelPlatformer>();
        }

        private void Start()
        {
            _dollyController = FindObjectOfType<DollyController>();
            _policeControllers = FindObjectsOfType<PoliceController>();
            _botControllers = FindObjectsOfType<BotController>();
        }

        private void Update()
        {
            if (_moduleUiPowerUpButton.isHaveShield)
            {
                shield.gameObject.SetActive(true);
                _isHaveShield = true;
            }

            if (_moduleUiPowerUpButton.isUnderground)
            {
                _isUnderground = true;
                if (_isSmaller == false)
                {
                    transform.DOScale(0.4f, 1f);
                    _isSmaller = true;
                }
            }

            if (_moduleUiPowerUpButton.isHaveGun)
            {
                ChangeState(States.Shoot);
            }
            
            if (_isWin && state != States.Die)
            {
                ChangeState(States.Win);
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
                    if (_inputChecking.IsHolding && 
                        _gameManagerChallenger1.state == Gameplay.Managers.GameManagerChallenger1.States.Play)
                    {
                        _rb.velocity = new Vector2(-speed, 0);
                        _playerSpine.ResumeAnim();
                        ChangeState(States.Run);
                    }
                    break;
                case States.Run:
                    if (_inputChecking.IsHolding == false)
                    {
                        _rb.velocity = new Vector2(0, 0);
                        _playerSpine.PauseAnim();
                        ChangeState(States.Idle);
                    }

                    foreach (var police in _policeControllers)
                    {
                        if (police.state == PoliceController.States.Die) continue;
                        if (_dollyController.state == DollyController.States.TurnBack && _isHaveShield == false && _isUnderground == false)
                        {
                            DOVirtual.DelayedCall(0.5f, delegate
                            {
                                _rb.velocity = new Vector2(0, 0);
                                ChangeState(States.Die);
                            });
                        }
                    }
                    
                    break;
                case States.Shoot:
                    DOVirtual.DelayedCall(1f, delegate
                    {
                        _moduleUiPowerUpButton.isHaveGun = false;
                        ChangeState(States.Idle);
                    });
                    break;
                case States.Die:
                    break;
                case States.Win:
                    _rb.velocity = new Vector2(-speed * 2.2f, 0);
                    DOVirtual.DelayedCall(timeToStopAfterWin, delegate
                    {
                        _rb.velocity = new Vector2(0, 0);
                    });
                    break;
                case States.Prepare:
                    transform.DOMove(_manager.playerStart.position, prepareTime);
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
                if (isHaveSpeed)
                {
                    speed /= 2;
                }

                if (_isUnderground)
                {
                    transform.DOScale(1, 1f);
                }
            }

            if (other.CompareTag("Start"))
            {
                ChangeState(States.Idle);
            }
        }
        
        private void BotMove()
        {
            var transform = _manager.botStartPos;
            _botControllers[0].transform.DOMove(new Vector3(
                Random.Range(transform[15].position.x, transform[15].position.x - 0.5f), transform[15].position.y, 0), prepareTime);
            _botControllers[1].transform.DOMove(new Vector3(
                Random.Range(transform[14].position.x + 0.6f, transform[14].position.x + 0.1f), transform[14].position.y, 0), prepareTime);
            _botControllers[2].transform.DOMove(new Vector3(
                Random.Range(transform[13].position.x, transform[13].position.x - 0.5f), transform[13].position.y, 0), prepareTime);
            _botControllers[3].transform.DOMove(new Vector3(
                Random.Range(transform[12].position.x + 0.6f, transform[12].position.x + 0.1f), transform[12].position.y, 0), prepareTime);
            _botControllers[4].transform.DOMove(new Vector3(
                Random.Range(transform[11].position.x + 0.6f, transform[11].position.x + 0.1f), transform[11].position.y, 0), prepareTime);
            _botControllers[5].transform.DOMove(new Vector3(
                Random.Range(transform[10].position.x, transform[10].position.x -0.5f), transform[10].position.y, 0), prepareTime);
            _botControllers[6].transform.DOMove(new Vector3(
                Random.Range(transform[9].position.x + 0.6f, transform[9].position.x + 0.1f), transform[9].position.y, 0), prepareTime);
            _botControllers[7].transform.DOMove(new Vector3(
                Random.Range(transform[8].position.x, transform[8].position.x - 0.5f), transform[8].position.y, 0), prepareTime);
            _botControllers[8].transform.DOMove(new Vector3(transform[7].position.x + 0.2f, transform[7].position.y, 0), prepareTime);
            _botControllers[9].transform.DOMove(new Vector3(transform[6].position.x + 0.4f, transform[6].position.y, 0), prepareTime);
            _botControllers[10].transform.DOMove(new Vector3(transform[5].position.x + 0.6f, transform[5].position.y, 0), prepareTime);
            _botControllers[11].transform.DOMove(new Vector3(
                Random.Range(transform[4].position.x + 0.6f, transform[4].position.x + 0.1f), transform[4].position.y, 0), prepareTime);
            _botControllers[12].transform.DOMove(new Vector3(
                Random.Range(transform[3].position.x, transform[3].position.x - 0.5f), transform[3].position.y, 0), prepareTime);
            _botControllers[13].transform.DOMove(new Vector3(
                Random.Range(transform[2].position.x + 0.6f, transform[2].position.x + 0.1f), transform[2].position.y, 0), prepareTime);
            _botControllers[14].transform.DOMove(new Vector3(
                Random.Range(transform[1].position.x, transform[1].position.x - 0.5f), transform[1].position.y, 0), prepareTime);
            _botControllers[15].transform.DOMove(new Vector3(
                Random.Range(transform[0].position.x + 0.6f, transform[0].position.x + 0.1f), transform[0].position.y, 0), prepareTime);
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
                    _playerSpine.PlayIdle();
                    break;
                case States.Run:
                    _playerSpine.PlayRun();
                    break;
                case States.Shoot:
                    SoundManager.Instance.PlaySound(SoundType.GunFire);
                    _mixAndMatchSkinPlayer.UpdateCharacterSkin();
                    _mixAndMatchSkinPlayer.UpdateCombinedSkin();
                    _playerSpine.PlayShoot();
                    break;
                case States.Die:
                    SoundManager.Instance.PlaySound(SoundType.Die);
                    _playerSpine.ResumeAnim();
                    idTxt.gameObject.SetActive(false);
                    blood.gameObject.SetActive(true);
                    blood.Play();
                    _playerSpine.PlayDie();
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        var i = Random.Range(0, 4);
                        soul[i].gameObject.SetActive(true);
                        soul[i].Play();
                    });
                    break;
                case States.Win:
                    _playerSpine.PlayRun();
                    DOVirtual.DelayedCall(timeToStopAfterWin, delegate
                    {
                        _playerSpine.PlayWin();
                        speed = 0;
                        SoundManager.Instance.PlaySound(SoundType.Victory);
                    });
                    break;
                case States.Prepare:
                    BotMove();
                    _playerSpine.PlayRun();
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
                case States.Shoot:
                    _mixAndMatchSkinPlayer.ReturnNormalCharacterSkin();
                    _mixAndMatchSkinPlayer.UpdateCombinedSkin();
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
