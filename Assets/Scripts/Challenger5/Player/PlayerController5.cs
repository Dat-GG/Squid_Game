using Challenger5.Bot;
using Challenger5.Gameplay.Managers;
using Challenger5.Gameplay.Utils;
using Challenger5.UI.CountTotalAlive;
using Challenger5.UI.Spin;
using Core.Common.Sound;
using DG.Tweening;
using EasyJoystick;
using Plugins.Scripts.Core.Common.Sound;
using TMPro;
using UnityEngine;

namespace Challenger5.Player
{
    public class PlayerController5 : MonoBehaviour
    {
        public enum States
        {
            Init, Idle, Run, Attack, Win, Die, Tumble, Katana, Giant, Hammer
        }
        public States state = States.Init;
        public bool isUseKatana;
        public bool isUseGiant;
        public bool isUseHammer;
        public bool isUseTumble;
        public bool isUseShield;
        public bool isLostShield;
        public Transform attackPoint;
        public Transform katanaAttackPoint;
        public Transform giantAttackPoint;
        public int countKill;
        public float attackRange;
        public float katanaAttackRange;
        public float giantAttackRange;
        public LayerMask enemyLayer;
        
        [SerializeField] private float speed;
        [SerializeField] private float thunderDelayTimeSpawn;
        [SerializeField] private float attackTime;
        [SerializeField] private SpriteRenderer shield;
        [SerializeField] private Transform jumpPos;
        [SerializeField] private GameObject spine;
        [SerializeField] private TextMeshPro idText;
        [SerializeField] private float xRange;
        [SerializeField] private float yRange;
        [SerializeField] private ParticleSystem blood;
        [SerializeField] private ParticleSystem[] soul;
        
        private Joystick _joystick;
        private InputAttackBtn _inputAttackBtn;
        private PlayerSpine5 _playerSpine5;
        private MixAndMatchSkinPlayer5 _mixAndMatchSkinPlayer5;
        private GameManagerChallenger5 _gameManagerChallenger5;
        private ModuleUiPowerUpBtn5 _moduleUiPowerUpBtn5;
        private CountTotalAlive _countTotalAlive;
        private BotController5[] _botController5;
        private float _attackTime;
        private bool _isDead;
        private bool _canNotMove;
        private bool _isBloodFx;
        private bool _isSoulFx;

        private void Awake()
        {
            _moduleUiPowerUpBtn5 = FindObjectOfType<ModuleUiPowerUpBtn5>();
            _playerSpine5 = GetComponentInChildren<PlayerSpine5>();
            _inputAttackBtn = FindObjectOfType<InputAttackBtn>();
            _gameManagerChallenger5 = FindObjectOfType<GameManagerChallenger5>();
            _countTotalAlive = FindObjectOfType<CountTotalAlive>();
            _botController5 = FindObjectsOfType<BotController5>();
            _mixAndMatchSkinPlayer5 = GetComponentInChildren<MixAndMatchSkinPlayer5>();
            _isDead = false;
            isUseGiant = false;
            isUseHammer = false;
            isUseKatana = false;
            isUseShield = false;
            isUseTumble = false;
            isLostShield = false;
            _canNotMove = false;
            _isBloodFx = false;
            _isSoulFx = false;
        }

        private void Start()
        {
            _botController5 = FindObjectsOfType<BotController5>();
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x, -xRange, xRange), 
                Mathf.Clamp(position.y, -yRange - 1, yRange), position.z);
            transform.position = position;

            if (_gameManagerChallenger5.state == GameManagerChallenger5.States.Play && _canNotMove == false)
            {
                var xMovement = _joystick.Horizontal();
                var yMovement = _joystick.Vertical();
                transform.position += new Vector3(xMovement, yMovement, 0) * (speed * Time.deltaTime);
                if (xMovement > 0)
                {
                    spine.transform.localScale = new Vector3(1, 1, 1);
                    //idText.rectTransform.DOLocalRotate(new Vector3(0, 0, -74), 0);
                }
                else if (xMovement < 0)
                {
                    spine.transform.localScale = new Vector3(-1, 1, 1);
                    //idText.rectTransform.DOLocalRotate(new Vector3(0, 0, -74), 0);
                }
            }

            if (_gameManagerChallenger5.state == GameManagerChallenger5.States.Win)
            {
                ChangeState(States.Win);
            }

            if (_isDead)
            {
                ChangeState(States.Die);
            }

            if (_moduleUiPowerUpBtn5.isHaveKatana)
            {
                isUseKatana = true;
                _canNotMove = true;
                ChangeState(States.Katana);
            }

            if (_moduleUiPowerUpBtn5.isHaveGiant)
            {
                isUseGiant = true;
                _canNotMove = true;
                ChangeState(States.Giant);
            }

            if (_moduleUiPowerUpBtn5.isHaveHammer)
            {
                isUseHammer = true;
                _canNotMove = true;
                ChangeState(States.Hammer);
            }

            if (_moduleUiPowerUpBtn5.isHaveShield)
            {
                isUseShield = true;
                shield.gameObject.SetActive(true);
            }

            if (_moduleUiPowerUpBtn5.isHaveTumble)
            {
                isUseTumble = true;
                ChangeState(States.Tumble);
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
                    if (_gameManagerChallenger5.state == GameManagerChallenger5.States.Play)
                    {
                        if (_joystick.IsTouching && !_inputAttackBtn.IsAttacking)
                        {
                            ChangeState(States.Run);
                        }
                        if (_inputAttackBtn.IsAttacking && _attackTime <= 0)
                        {
                            ChangeState(States.Attack);
                        }
                    }
                    break;
                case States.Run:
                    if (!_joystick.IsTouching)
                    {
                        ChangeState(States.Idle);
                    }
                    if (_inputAttackBtn.IsAttacking && _attackTime <= 0)
                    {
                        ChangeState(States.Attack);
                    }
                    break;
                case States.Attack:
                    _attackTime -= Time.deltaTime;
                    if (_attackTime <= 0)
                    {
                        ChangeState(States.Idle);
                    }
                    break;
                case States.Tumble:
                    transform.position = Vector2.MoveTowards(transform.position, jumpPos.position, speed * 2 * Time.deltaTime);
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        ChangeState(States.Idle);
                    });
                    break;
                case States.Die:
                    break;
                case States.Win:
                    break;
                case States.Katana:
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        ChangeState(States.Idle);
                    });
                    break;
                case States.Giant:
                    DOVirtual.DelayedCall(1f, delegate
                    {
                        ChangeState(States.Idle);
                    });
                    break;
                case States.Hammer:
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BotHitBox") || other.CompareTag("KatanaHitBox") || other.CompareTag("GiantHitBox"))
            {
                if (isUseShield)
                {
                    _moduleUiPowerUpBtn5.isHaveShield = false;
                    shield.gameObject.SetActive(false);
                    isLostShield = true;
                }
                else
                {
                    _isDead = true;
                }
            }

            if (other.CompareTag("Thunder") && isUseHammer == false)
            {
                _isDead = true;
            }
        }

        #region Attack
        private void SpamThunderBolt()
        {
            foreach (var bot in _botController5)
            {
                if (bot.isVisible)
                {
                    var position = bot.transform.position;
                    GameplayHelper5.SpawnThunderTrack(position);
                    DOVirtual.DelayedCall(thunderDelayTimeSpawn, delegate
                    {
                        GameplayHelper5.SpawnPlayerThunder(position);
                        SoundManager.Instance.PlaySound(SoundType.Thunder);
                        DOVirtual.DelayedCall(0.2f, delegate
                        {
                            GameplayHelper5.SpawnThunderGround(position);
                        });
                    });
                }
            }
        }

        private void NormalAttack()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<BotController5>().TakeDamage();
            }
        }
        
        private void KatanaAttack()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(katanaAttackPoint.position, katanaAttackRange, enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<BotController5>().TakeDamage();
            }
        }
        
        private void GiantAttack()
        {
            var hitEnemies = Physics2D.OverlapCircleAll(giantAttackPoint.position, giantAttackRange, enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<BotController5>().TakeDamage();
            }

            DOVirtual.DelayedCall(0.8f, delegate
            { 
                GameplayHelper5.SpawnGroundBreak(transform.position);
            });
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
            Gizmos.DrawWireSphere(katanaAttackPoint.position, katanaAttackRange);
            Gizmos.DrawWireCube(giantAttackPoint.position, new Vector3(giantAttackRange * 2.5f, giantAttackRange));
        }
        #endregion

        #region FSM
        private void EnterNewState()
        {
            switch (state)
            {
                case States.Init:
                    break;
                case States.Idle:
                    _attackTime = 0;
                    _playerSpine5.PlayIdle();
                    break;
                case States.Run:
                    _playerSpine5.PlayRun();
                    break;
                case States.Attack:
                    _attackTime = attackTime;
                    _playerSpine5.PlayAttack();
                    SoundManager.Instance.PlaySound(SoundType.Attack);
                    NormalAttack();
                    break;
                case States.Tumble:
                    _canNotMove = false;
                    _playerSpine5.PlayTumble();
                    break;
                case States.Die:
                    idText.gameObject.SetActive(false);
                    if (_isBloodFx == false)
                    {
                        _isBloodFx = true;
                        blood.gameObject.SetActive(true);
                        blood.Play();
                    }
                    _playerSpine5.PlayDie();
                    DOVirtual.DelayedCall(0.5f, delegate
                    {
                        if (_isSoulFx == false)
                        {
                            _isSoulFx = true;
                            var i = Random.Range(0, 4);
                            soul[i].gameObject.SetActive(true);
                            soul[i].Play();
                        }
                    });
                    _countTotalAlive.alive--;
                    break;
                case States.Win:
                    _playerSpine5.PlayWin();
                    SoundManager.Instance.PlaySound(SoundType.Victory);
                    break;
                case States.Katana:
                    _mixAndMatchSkinPlayer5.UpdateBaseSkin();
                    _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
                    _playerSpine5.PlayKatana();
                    SoundManager.Instance.PlaySound(SoundType.Katana);
                    KatanaAttack();
                    break;
                case States.Giant:
                    _mixAndMatchSkinPlayer5.UpdateBaseSkin();
                    _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
                    _playerSpine5.PlayGiant();
                    SoundManager.Instance.PlaySound(SoundType.Hulk);
                    GiantAttack();
                    break;
                case States.Hammer:
                    _mixAndMatchSkinPlayer5.UpdateHammerSkin();
                    _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
                    _playerSpine5.PlayHammer();
                    SpamThunderBolt();
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
                case States.Attack:
                    break;
                case States.Tumble:
                    _moduleUiPowerUpBtn5.isHaveTumble = false;
                    break;
                case States.Die:
                    break;
                case States.Win:
                    break;
                case States.Katana:
                    _mixAndMatchSkinPlayer5.UpdateCharacterSkin();
                    _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
                    _moduleUiPowerUpBtn5.isHaveKatana = false;
                    _canNotMove = false;
                    break;
                case States.Giant:
                    _mixAndMatchSkinPlayer5.UpdateCharacterSkin();
                    _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
                    _moduleUiPowerUpBtn5.isHaveGiant = false;
                    _canNotMove = false;
                    break;
                case States.Hammer:
                    _mixAndMatchSkinPlayer5.UpdateCharacterSkin();
                    _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
                    _moduleUiPowerUpBtn5.isHaveHammer = false;
                    _canNotMove = false;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
