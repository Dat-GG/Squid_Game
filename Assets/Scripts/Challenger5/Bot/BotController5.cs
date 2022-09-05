using System.Collections;
using System.Collections.Generic;
using Challenger5.Gameplay.Managers;
using Challenger5.Gameplay.Utils;
using Challenger5.Player;
using Challenger5.UI.CountTotalAlive;
using Challenger5.UI.TimerCountdown;
using Core.Common.GameResources;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger5.Bot
{
    public class BotController5 : MonoBehaviour
    {
        public enum States
        {
            Init, Idle, Run, Turn, Attack, Win, Die, Tumble, Katana, Hammer, Giant
        }
        public States state = States.Init;
        public SpriteRenderer shield;
        public Skin finalSkin;
        public bool isHaveShield;
        public bool isHaveKatana;
        public bool isHaveGiant;
        public bool isHaveTumble;
        public bool isHaveHammer;
        public bool isUseShield;
        public bool isUseKatana;
        public bool isUseGiant;
        public bool isUseTumble;
        public bool isUseHammer;
        public bool isLostShield;
        public bool isDead;
        public bool isVisible;
        
        [SerializeField] private float speed;
        [SerializeField] private float delayTimeSpawnThunder;
        [SerializeField] private float attackTime;
        [SerializeField] private GameObject botHitBox;
        [SerializeField] private GameObject katanaHitBox;
        [SerializeField] private GameObject giantHitBox;
        [SerializeField] private BotPowerUpController botPowerUpController;
        [SerializeField] private TextMeshPro idTxt;
        [SerializeField] private Transform movePos;
        [SerializeField] private Transform jumpPos;
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float minY;
        [SerializeField] private float maxY;
        [SerializeField] private ParticleSystem blood;
        [SerializeField] private ParticleSystem[] soul;
        
        private BoxCollider2D _collider2D;
        private GameManagerChallenger5 _gameManagerChallenger5;
        private TimerCountdown5 _timerCountdown5;
        private CountTotalAlive _countTotalAlive;
        private PlayerController5 _playerController5;
        private BotController5[] _botController5;
        private BotSpine5 _botSpine5;
        private MixAndMatchSkinBot5 _mixAndMatchSkinBot5;
        private SkeletonAnimation _skeletonAnimation;
        private Camera _cam;
        private bool _isAttack;
        private bool _isSpawnGold;
        private bool _isCountAlive;
        private bool _isHavePowerUp;
        private bool _isOwnHammer;
        private bool _isLeft;
        private bool _isRight;
        private bool _isHaveTarget;
        private bool _isBloodFx;
        private bool _isSoulFx;
        private Transform _target;
        private float _attackTime;

        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _botSpine5 = GetComponentInChildren<BotSpine5>();
            _mixAndMatchSkinBot5 = GetComponentInChildren<MixAndMatchSkinBot5>();
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _gameManagerChallenger5 = FindObjectOfType<GameManagerChallenger5>();
            _timerCountdown5 = FindObjectOfType<TimerCountdown5>();
            _playerController5 = FindObjectOfType<PlayerController5>();
            _botController5 = FindObjectsOfType<BotController5>();
            _countTotalAlive = FindObjectOfType<CountTotalAlive>();
            _cam = FindObjectOfType<Camera>();
            _isAttack = false;
            isDead = false;
            _isSpawnGold = false;
            _isCountAlive = false;
            _isHavePowerUp = false;
            _isOwnHammer = false;
            isUseShield = false;
            isUseKatana = false;
            isUseGiant = false;
            isUseTumble = false;
            isUseHammer = false;
            isVisible = false;
            isLostShield = false;
            _isHaveTarget = false;
            _isBloodFx = false;
            _isSoulFx = false;
        }

        private void Start()
        {
            SetRandomSkinForBot();
            SetRandomIdNumber();
            _botController5 = FindObjectsOfType<BotController5>();
            CheckPowerUp();
        }

        private void Update()
        {
            CheckVisible();
            
            if (isDead)
            {
                _collider2D.enabled = false;
                isVisible = false;
                ChangeState(States.Die);
            }

            if (isHaveShield && _gameManagerChallenger5.state == GameManagerChallenger5.States.Play && !isDead)
            {
                isUseShield = true;
                shield.gameObject.SetActive(true);
            }

            if (isHaveTumble && _gameManagerChallenger5.state == GameManagerChallenger5.States.Play && !isDead)
            {
                ChangeState(States.Tumble);
                isUseTumble = true;
            }

            if (isHaveHammer && _gameManagerChallenger5.state == GameManagerChallenger5.States.Play && !isDead)
            {
                ChangeState(States.Hammer);
                isUseHammer = true;
            }

            if (_timerCountdown5.gameDuration <= 0 && !isDead)
            {
                ChangeState(States.Win);
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
                        movePos.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                        DOVirtual.DelayedCall(0.3f, delegate
                        {
                            ChangeState(States.Turn);
                        });
                    }

                    CheckAttack();
                    break;
                case States.Turn:
                    Turn();
                    ChangeState(States.Run);
                    break;
                case States.Run:
                    transform.position =
                        Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, movePos.position) < 0.01f)
                    {
                        ChangeState(States.Idle);
                    }

                    CheckAttack();
                    break;
                case States.Attack:
                    _attackTime -= Time.deltaTime;
                    if (_attackTime <= 0)
                    {
                        ChangeState(States.Idle);
                    }
                    break;
                case States.Tumble:
                    transform.position = Vector2.MoveTowards(transform.position, jumpPos.position, speed * 4 * Time.deltaTime);
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
                case States.Hammer:
                    DOVirtual.DelayedCall(1f, delegate
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
                    _botSpine5.PlayIdle();
                    break;
                case States.Turn:
                    break;
                case States.Run:
                    _botSpine5.PlayRun();
                    break;
                case States.Attack:
                    _attackTime = attackTime;
                    _botSpine5.PlayAttack();
                    SoundManager.Instance.PlaySound(SoundType.Attack);
                    botHitBox.gameObject.SetActive(true);
                    break;
                case States.Die:
                    idTxt.gameObject.SetActive(false);
                    if (_isBloodFx == false)
                    {
                        _isBloodFx = true;
                        blood.gameObject.SetActive(true);
                        blood.Play();
                    }
                    _botSpine5.PlayDie();
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
                    
                    if (_isSpawnGold == false)
                    {
                        OnBotDie(transform.position);
                        _isSpawnGold = true;
                    }

                    if (_isCountAlive == false)
                    {
                        _countTotalAlive.alive--;
                        _isCountAlive = true;
                    }
                    break;
                case States.Tumble:
                    _botSpine5.PlayTumble();
                    botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdTumble;
                    botPowerUpController.fillTumble.fillAmount = 1;
                    break;
                case States.Win:
                    _botSpine5.PlayWin();
                    SoundManager.Instance.PlaySound(SoundType.Victory);
                    break;
                case States.Katana:
                    _mixAndMatchSkinBot5.UpdateBaseSkin();
                    _mixAndMatchSkinBot5.UpdateCombinedSkin();
                    _botSpine5.PlayKatana();
                    SoundManager.Instance.PlaySound(SoundType.Katana);
                    katanaHitBox.gameObject.SetActive(true);
                    botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdKatana;
                    botPowerUpController.fillKatana.fillAmount = 1;
                    break;
                case States.Hammer:
                    _mixAndMatchSkinBot5.UpdateHammerSkin();
                    _mixAndMatchSkinBot5.UpdateCombinedSkin();
                    _botSpine5.PlayHammer();
                    SpamThunderBolt();
                    botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdHammer;
                    botPowerUpController.fillHammer.fillAmount = 1;
                    break;
                case States.Giant:
                    _mixAndMatchSkinBot5.UpdateBaseSkin();
                    _mixAndMatchSkinBot5.UpdateCombinedSkin();
                    _botSpine5.PlayGiant();
                    SoundManager.Instance.PlaySound(SoundType.Hulk);
                    giantHitBox.gameObject.SetActive(true);
                    botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdGiant;
                    botPowerUpController.fillGiant.fillAmount = 1;
                    DOVirtual.DelayedCall(0.8f, delegate
                    {
                        GameplayHelper5.SpawnGroundBreak(transform.position);
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
                case States.Turn:
                    break;
                case States.Run:
                    break;
                case States.Attack:
                    _isAttack = false;
                    _isHaveTarget = false;
                    botHitBox.gameObject.SetActive(false);
                    break;
                case States.Tumble:
                    isHaveTumble = false;
                    break;
                case States.Die:
                    break;
                case States.Win:
                    break;
                case States.Katana:
                    _mixAndMatchSkinBot5.UpdateCharacterSkin();
                    _mixAndMatchSkinBot5.UpdateCombinedSkin();
                    katanaHitBox.gameObject.SetActive(false);
                    isHaveKatana = false;
                    break;
                case States.Hammer:
                    _mixAndMatchSkinBot5.UpdateCharacterSkin();
                    _mixAndMatchSkinBot5.UpdateCombinedSkin();
                    isHaveHammer = false;
                    break;
                case States.Giant:
                    _mixAndMatchSkinBot5.UpdateCharacterSkin();
                    _mixAndMatchSkinBot5.UpdateCombinedSkin();
                    giantHitBox.gameObject.SetActive(false);
                    isHaveGiant = false;
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BotHitBox") || other.CompareTag("KatanaHitBox") ||
                other.CompareTag("GiantHitBox") || other.CompareTag("Thunder"))
            {
                if (isHaveShield)
                {
                    shield.gameObject.SetActive(false);
                    isLostShield = true;
                    botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdShield;
                    botPowerUpController.fillShield.fillAmount = 1;
                }
                else
                {
                    isDead = true;
                }
            }

            if (other.CompareTag("PlayerThunder"))
            {
                if (isHaveShield)
                {
                    shield.gameObject.SetActive(false);
                    isLostShield = true;
                    botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdShield;
                    botPowerUpController.fillShield.fillAmount = 1;
                }
                else
                {
                    isDead = true;
                    _playerController5.countKill++;
                }
            }

            if ((other.CompareTag("Bot") || other.CompareTag("Player")) 
                && _gameManagerChallenger5.state == GameManagerChallenger5.States.Play)
            {
                if (isHaveKatana)
                {
                    ChangeState(States.Katana);
                    isUseKatana = true;
                }
                else if(isHaveGiant)
                {
                    ChangeState(States.Giant);
                    isUseGiant = true;
                }
                else
                {
                    if (_isHaveTarget == false)
                    {
                        _target = other.transform;
                        _isAttack = true;
                        _isHaveTarget = true;
                    }
                }
            }
        }
        
        private void CheckAttack()
        {
            if (_isAttack && _gameManagerChallenger5.state == GameManagerChallenger5.States.Play)
            {
                var a = Random.Range(0, 1f);
                DOVirtual.DelayedCall(a, delegate
                {
                    if (!isDead && _attackTime <= 0)
                    {
                        LookAt(_target);
                        ChangeState(States.Attack);
                    }
                    else if (Vector2.Distance(transform.position, _target.transform.position) > 3.85 )
                    {
                        ChangeState(States.Turn);
                    }
                });
            }
        }

        private void LookAt(Component target)
        {
            if (target.transform.position.x <  transform.position.x && _isLeft)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 180, 74), 0);
            }
            if (target.transform.position.x >  transform.position.x && _isLeft)
            {
                transform.localScale = new Vector3(1, 1, 1);
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 0, -74), 0);
            }
            if (target.transform.position.x <  transform.position.x && _isRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 180, 74), 0);
            }
            if (target.transform.position.x >  transform.position.x && _isRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 0, -74), 0);
            }
        }

        private void SpamThunderBolt()
        {
            foreach (var bot in _botController5)
            {
                if (bot._isOwnHammer != false || bot.isDead != false) continue;
                if (!(Vector2.Distance(transform.position, bot.transform.position) <= 5)) continue;
                var pos = bot.transform.position;
                GameplayHelper5.SpawnThunderTrack(pos);
                DOVirtual.DelayedCall(delayTimeSpawnThunder, delegate
                {
                    GameplayHelper5.SpawnThunder(pos);
                    SoundManager.Instance.PlaySound(SoundType.Thunder);
                    DOVirtual.DelayedCall(0.2f, delegate
                    {
                        GameplayHelper5.SpawnThunderGround(pos);
                    });
                });
            }

            if (!(Vector2.Distance(transform.position, _playerController5.transform.position) <= 5)) return;
            var playerPos = _playerController5.transform.position;
            GameplayHelper5.SpawnThunderTrack(playerPos);
            DOVirtual.DelayedCall(delayTimeSpawnThunder, delegate
            {
                GameplayHelper5.SpawnThunder(playerPos);
                DOVirtual.DelayedCall(0.2f, delegate
                {
                    GameplayHelper5.SpawnThunderGround(playerPos);
                });
            });
        }
        
        public void TakeDamage()
        {
            if (isHaveShield)
            {
                shield.gameObject.SetActive(false);
                isLostShield = true;
                botPowerUpController.timeCountdown = botPowerUpController.maxTimeCdShield;
                botPowerUpController.fillShield.fillAmount = 1;
            }
            else
            {
                isDead = true;
                _playerController5.countKill++;
            }
        }
        private void Turn()
        {
            if (movePos.position.x < transform.position.x)
            {
                _isLeft = true;
                _isRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 180, 74), 0);
            }
            else if(movePos.position.x >= transform.position.x)
            {
                _isLeft = false;
                _isRight = true;
                transform.localScale = new Vector3(1, 1, 1);
                idTxt.rectTransform.DOLocalRotate(new Vector3(0, 0, -74), 0);
            }
        }
        private void SetRandomSkinForBot()
        {
            var listSkins = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Skins.Items;
            List<Skin> skins = new List<Skin>(listSkins);
           
            for (int i = skins.Count - 1; i >= 0; i--)
            {
                var skin = skins[i];
                var isCanNotUse = _botSpine5.IsCanNotUseSkin(skin.Name);
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
            finalSkin = new Skin(skinName);
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

        private void OnBotDie(Vector3 diePosition)
        {
            var totalGold = _gameManagerChallenger5.goldDropPerBot;
            if (totalGold < 4)
            {
                GameplayHelper5.SpawnGold(diePosition, totalGold);
            }
            else
            {
                var firstFourObjectValue = Mathf.FloorToInt((float) totalGold / 4);
                var lastObjectValue = totalGold - (firstFourObjectValue * 4);
                StartCoroutine(SpawnGoldsInGame(diePosition, firstFourObjectValue, lastObjectValue));
            }
            GameplayHelper5.SpawnHeart(diePosition);
        }
        
        private static IEnumerator SpawnGoldsInGame(Vector3 position, int firstFourValue, int lastValue)
        {
            var count = 0;

            while (count < 5)
            {
                count++;
                if (count <= 4)
                {
                    GameplayHelper5.SpawnGold(position, firstFourValue);
                }
                else
                {
                    if (lastValue > 0)
                    {
                        GameplayHelper5.SpawnGold(position, lastValue);
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void CheckVisible()
        {
            var viewPos = _cam.WorldToViewportPoint(transform.position);
            if (viewPos.x > 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
                isVisible = true;
            else
                isVisible = false;
        }
        
        private void CheckPowerUp()
        {
            var data = DataAccountPlayer.BotChallenger5PowerUp;
            if(_isHavePowerUp || DataAccountPlayer.PlayerChallenger5PowerUp.FirstGame <= 0) return;
            if (data.GetPowerUp(PowerUpBotType.Hammer) <= 0 && _isHavePowerUp == false)
            {
                data.SetPowerUp(true, PowerUpBotType.Hammer, 1);
                _isHavePowerUp = true;
                isHaveHammer = true;
                _isOwnHammer = true;
            }
            if (data.GetPowerUp(PowerUpBotType.Tumble) <= 0 && _isHavePowerUp == false)
            {
                data.SetPowerUp(true, PowerUpBotType.Tumble, 1);
                _isHavePowerUp = true;
                isHaveTumble = true;
            }
            if (data.GetPowerUp(PowerUpBotType.Katana) <= 0 && _isHavePowerUp == false)
            {
                data.SetPowerUp(true, PowerUpBotType.Katana, 1);
                _isHavePowerUp = true;
                isHaveKatana = true;
            }
            if (data.GetPowerUp(PowerUpBotType.Shield) <= 0 && _isHavePowerUp == false)
            {
                data.SetPowerUp(true, PowerUpBotType.Shield, 1);
                _isHavePowerUp = true;
                isHaveShield = true;
            }
            if (data.GetPowerUp(PowerUpBotType.Giant) <= 0 && _isHavePowerUp == false)
            {
                data.SetPowerUp(true, PowerUpBotType.Giant, 1);
                _isHavePowerUp = true;
                isHaveGiant = true;
            }
        }
    }
}
