using Challenger3.Enemy;
using Challenger3.Gameplay.Runtime;
using Challenger3.Player;
using Challenger3.Police;
using Challenger3.SpineAnimation;
using Core.Common;
using Core.Common.Popup;
using Core.Common.Sound;
using Core.Pool;
using DataAccount;
using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Challenger3.Gameplay.Managers
{
    public class GameManagerChallenger3 : MonoBehaviour
    {
        [SerializeField] private Transform playerPos, enemyPos, policePos;
        [SerializeField] private GameObject playerPrefab, enemyPrefab, policePrefab;

        [SerializeField] private Button tabToPlayBtn, settingBtn;
        [SerializeField] private Indicator indicator;
        [SerializeField] private Transform calculateWinner, calculateWinner1;
        [SerializeField] private Sprite winImg, loseImg;
        [SerializeField] private Image turn1B, turn1P, turn2B, turn2P, turn3B, turn3P;

        [SerializeField] private GameObject scoreBoard;
        [SerializeField] private Transform scoreBoardTargetMove;
        [SerializeField] private Text roundTxt, namePlayerScoreBoard, nameEnemyScoreBoard;
        
        [HideInInspector] public int round;
        [HideInInspector] public bool endTurn;

        [HideInInspector] public int nameEnemyNumber;
        
        private EnemyController3 _enemyController3;
        private PlayerController3 _playerController3;
        private PoliceController3 _policeController3;
        private int _turnEnemyWin, _turnPlayerWin;
        private GameObject _player;
    
        private void Awake() => Init();

        private void Start()
        {
            nameEnemyNumber = Random.Range(100, 500);
            tabToPlayBtn.onClick.AddListener(StartGame);
            settingBtn.onClick.AddListener(OnClickSettingBtn);
            _enemyController3 = FindObjectOfType<EnemyController3>();
            _playerController3 = FindObjectOfType<PlayerController3>();
            _policeController3 = FindObjectOfType<PoliceController3>();
            // _timeCountdown = FindObjectOfType<TimeCountdown>();
            namePlayerScoreBoard.text = "456";
            nameEnemyScoreBoard.text = nameEnemyNumber.ToString();
        }

        private void StartGame()
        {
            tabToPlayBtn.gameObject.SetActive(false);
            RoundTextOnScale(1);
            scoreBoard.transform.DOMoveY(scoreBoardTargetMove.position.y, 0.5f);
            
            var randomTurn = Random.Range(0, 2);
            this.StartDelayMethod(1, () =>
            {
                if (randomTurn == 0)
                    SetPlayerTurnFirst();
                else
                    SetEnemyTurnFirst();
            });
        }

        // Player starts first
        
        public void CheckEndTurnPlayerFirst()
        {
            switch (round)
            {
                case 1:
                    if (_enemyController3.spawnMarbleEnemy.transform.position.x - calculateWinner1.position.x <
                        _playerController3.spawnMarblePlayer.transform.position.x - calculateWinner.position.x)
                    {
                        _turnEnemyWin += 1;
                        turn1B.sprite = winImg;
                        turn1P.sprite = loseImg;
                        turn1B.gameObject.SetActive(true);
                        turn1P.gameObject.SetActive(true);
                    }
                    else
                    {
                        _turnPlayerWin += 1;
                        turn1B.sprite = loseImg;
                        turn1P.sprite = winImg;
                        turn1B.gameObject.SetActive(true);
                        turn1P.gameObject.SetActive(true);
                    }

                    this.StartDelayMethod(0.2f, () => RoundTextOnScale(2));
                    this.StartDelayMethod(1, SetPlayerTurnFirst);
                    break;
                case 2:
                    if (_enemyController3.spawnMarbleEnemy.transform.position.x - calculateWinner1.position.x <
                        _playerController3.spawnMarblePlayer.transform.position.x - calculateWinner.position.x)
                    {
                        turn2B.gameObject.SetActive(true);
                        turn2P.gameObject.SetActive(true);
                        turn2B.sprite = winImg;
                        turn2P.sprite = loseImg;
                        _turnEnemyWin += 1;
                    }
                    else
                    {
                        turn2B.gameObject.SetActive(true);
                        turn2P.gameObject.SetActive(true);
                        turn2B.sprite = loseImg;
                        turn2P.sprite = winImg;
                        _turnPlayerWin += 1;
                    }
                    this.StartDelayMethod(0.2f, () => RoundTextOnScale(3));
                    this.StartDelayMethod(1, SetPlayerTurnFirst);
                    break;
                case 3:
                    if (_enemyController3.spawnMarbleEnemy.transform.position.x - calculateWinner1.position.x <
                        _playerController3.spawnMarblePlayer.transform.position.x - calculateWinner.position.x)
                    {
                        turn3B.gameObject.SetActive(true);
                        turn3P.gameObject.SetActive(true);
                        turn3B.sprite = winImg;
                        turn3P.sprite = loseImg;
                        _turnEnemyWin += 1;
                    }
                    else
                    {
                        turn3B.gameObject.SetActive(true);
                        turn3P.gameObject.SetActive(true);
                        turn3B.sprite = loseImg;
                        turn3P.sprite = winImg;
                        _turnPlayerWin += 1;
                    }
                
                    if (_turnEnemyWin > _turnPlayerWin)
                    {
                        _enemyController3.EnemySpine.PlayWin();
                        _policeController3.PoliceSpine.PlayShoot();
                        _playerController3.PlayerSpine.PlayDie();
                        this.StartDelayMethod(1.5f, () =>
                        {
                            DataAccountPlayer.PlayerChallenger3.LoseCount++;
                            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LosePopup);
                        });
                    }
                    else
                    {
                        _enemyController3.EnemySpine.PlayDie();
                        _policeController3.PoliceSpine.PlayShoot();
                        _playerController3.PlayerSpine.PlayWin();
                        this.StartDelayMethod(1.5f, () =>
                        {
                            DataAccountPlayer.PlayerChallenger3.WinCount++;
                            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
                        });
                    }
                    break;
            }
        }

        private void SetPlayerTurnFirst()
        {
            endTurn = false;
            _enemyController3.EnemySpine.PlayIdle();
            _playerController3.PlayerSpine.PlayIdle();
            indicator.WhenPlayerTurn();
            indicator.fill.fillAmount = 0;
            DOVirtual.DelayedCall(0.5f, delegate
            {
                indicator.fillFirstBtn.gameObject.SetActive(true);
                indicator.canThrow = true;
            });
        }

        public void SetEnemyTurnSecond()
        {
            indicator.WhenEnemyTurn();
            indicator.throwFirstBtn.gameObject.SetActive(false);
            indicator.fillFirstBtn.gameObject.SetActive(false);
            _enemyController3.EnemyThrowSecond();
        }
        
        // Enemy starts first
        
        public void CheckEndTurnEnemyFirst()
        {
            switch (round)
            {
                case 1:
                    if (_enemyController3.spawnMarbleEnemy.transform.position.x - calculateWinner1.position.x <
                        _playerController3.spawnMarblePlayer.transform.position.x - calculateWinner.position.x)
                    {
                        _turnEnemyWin += 1;
                        turn1B.sprite = winImg;
                        turn1P.sprite = loseImg;
                        turn1B.gameObject.SetActive(true);
                        turn1P.gameObject.SetActive(true);
                    }
                    else
                    {
                        _turnPlayerWin += 1;
                        turn1B.sprite = loseImg;
                        turn1P.sprite = winImg;
                        turn1B.gameObject.SetActive(true);
                        turn1P.gameObject.SetActive(true);
                    }

                    this.StartDelayMethod(0.2f, () => RoundTextOnScale(2));
                    this.StartDelayMethod(1, SetEnemyTurnFirst);
                    break;
                case 2:
                    if (_enemyController3.spawnMarbleEnemy.transform.position.x - calculateWinner1.position.x <
                        _playerController3.spawnMarblePlayer.transform.position.x - calculateWinner.position.x)
                    {
                        turn2B.gameObject.SetActive(true);
                        turn2P.gameObject.SetActive(true);
                        turn2B.sprite = winImg;
                        turn2P.sprite = loseImg;
                        _turnEnemyWin += 1;
                    }
                    else
                    {
                        turn2B.gameObject.SetActive(true);
                        turn2P.gameObject.SetActive(true);
                        turn2B.sprite = loseImg;
                        turn2P.sprite = winImg;
                        _turnPlayerWin += 1;
                    }
                    this.StartDelayMethod(0.2f, () => RoundTextOnScale(3));
                    this.StartDelayMethod(1, SetEnemyTurnFirst);
                    break;
                case 3:
                    if (_enemyController3.spawnMarbleEnemy.transform.position.x - calculateWinner1.position.x <
                        _playerController3.spawnMarblePlayer.transform.position.x - calculateWinner.position.x)
                    {
                        turn3B.gameObject.SetActive(true);
                        turn3P.gameObject.SetActive(true);
                        turn3B.sprite = winImg;
                        turn3P.sprite = loseImg;
                        _turnEnemyWin += 1;
                    }
                    else
                    {
                        turn3B.gameObject.SetActive(true);
                        turn3P.gameObject.SetActive(true);
                        turn3B.sprite = loseImg;
                        turn3P.sprite = winImg;
                        _turnPlayerWin += 1;
                    }
                
                    if (_turnEnemyWin > _turnPlayerWin)
                    {
                        _enemyController3.EnemySpine.PlayWin();
                        _policeController3.PoliceSpine.PlayShoot();
                        _playerController3.PlayerSpine.PlayDie();
                        this.StartDelayMethod(1.5f, () =>
                        {
                            DataAccountPlayer.PlayerChallenger3.LoseCount++;
                            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LosePopup);
                        });
                    }
                    else
                    {
                        _enemyController3.EnemySpine.PlayDie();
                        _policeController3.PoliceSpine.PlayShoot();
                        _playerController3.PlayerSpine.PlayWin();
                        this.StartDelayMethod(1.5f, () =>
                        {
                            DataAccountPlayer.PlayerChallenger3.WinCount++;
                            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
                        });
                    }
                    break;
            }
        }

        private void SetEnemyTurnFirst()
        {
            indicator.WhenEnemyTurn();
            indicator.throwSecondBtn.gameObject.SetActive(false);
            indicator.fillSecondBtn.gameObject.SetActive(false);
            _enemyController3.EnemyThrowFirst();
        }
        
        internal void SetPlayerTurnSecond()
        {
            endTurn = false;
            _enemyController3.EnemySpine.PlayIdle();
            _playerController3.PlayerSpine.PlayIdle();
            indicator.WhenPlayerTurn();
            indicator.fill.fillAmount = 0;
            DOVirtual.DelayedCall(0.5f, delegate
            {
                indicator.fillSecondBtn.gameObject.SetActive(true);
                indicator.canThrow = true;
            });
        }

        private void Init()
        {
            _player = SmartPool.Instance.Spawn(playerPrefab, playerPos.position, Quaternion.identity);
            var skinPlayer = _player.GetComponentInChildren<Spine3>();
            skinPlayer.ChangeSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString());
            SmartPool.Instance.Spawn(enemyPrefab, enemyPos.position, Quaternion.identity);
            SmartPool.Instance.Spawn(policePrefab, policePos.position, Quaternion.identity);
        }
        
        private void RoundTextOnScale(int round)
        {
            roundTxt.gameObject.SetActive(true);
            roundTxt.text = "Round" + " " + round;
            var originalScale = roundTxt.transform.localScale;
            var scaleTo = originalScale * 2;
            roundTxt.transform.DOScale(scaleTo, 1f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    roundTxt.gameObject.SetActive(false);
                    roundTxt.transform.localScale = originalScale;
                });
        }

        private static void OnClickSettingBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupInGame);
            PopupController.Instance.HideHud();
        }
    }
}