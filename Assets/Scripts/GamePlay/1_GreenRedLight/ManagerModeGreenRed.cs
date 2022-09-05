using System.Collections;
using System.Collections.Generic;
using Core.Load;
using DataAccount;
using DG.Tweening;
using GamePlay._1_GreenRedLight.Characters;
using GamePlay._1_GreenRedLight.Data;
using GamePlay._1_GreenRedLight.Police;
using GamePlay.Utils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GamePlay._1_GreenRedLight
{
    public class ManagerModeGreenRed : MonoBehaviour
    {
        [SerializeField] private Transform startPlayerTransform;
        [SerializeField] private List<Transform> startEnemyTransforms;
        [SerializeField] private float spawnDistanceFromStartPos;
        [SerializeField] private Transform targetTransform;

        [SerializeField] private Button holdToMoveBtn;
        [SerializeField] private Image timeProgress;
        [SerializeField] private SpriteRenderer warningArea;

        public bool IsStarted { get; private set; }

        private bool IsWinTriggered { get; set; }

        private GreenRedSingTimeRate _singTimeRate;
        private GreenRedSingTimeData _singTimeData;

        private float _gameTimer;
        private int _totalEnemyCanDie;
        private List<GreenRedEnemy> _enemiesCanDie = new List<GreenRedEnemy>();

        private List<GreenRedPoliceController> _allPolices = new List<GreenRedPoliceController>();

        private void Start()
        {
            IsWinTriggered = false;
            _gameTimer = 0f;
            _singTimeData = LoadResourceController.Instance.LoadGreenRedSingTimeData();

            var currentTimePlayed = DataAccountPlayer.Player1GreenRedLight.GetTotalTimePlayed();
            _singTimeRate = _singTimeData.GetRate(currentTimePlayed);

            RegisterEvents();
            holdToMoveBtn.onClick.AddListener(OnClickHoldToMove);
            IsStarted = false;
            InitUi();
            SpawnPlayer();
            SpawnEnemies();
            LoadPolices();
        }

        private void Update()
        {
            if (!IsStarted)
                return;

            _gameTimer += Time.deltaTime;
        }

        private void OnClickHoldToMove()
        {
            IsStarted = true;
            holdToMoveBtn.gameObject.SetActive(false);
            StartSing();
        }

        #region Init Data

        private void RegisterEvents()
        {
            this.RegisterListener(EventID.GreenRedPlayerReachedStartLine, (sender, param) =>
            {
                holdToMoveBtn.gameObject.SetActive(true);
            });

            this.RegisterListener(EventID.GreenRedDollyFinishRotate, (sender, param) =>
            {
                StartSing();
            });

            this.RegisterListener(EventID.GreenRedPlayerReachedFinishLine, (sender, param) =>
            {
                OnWin();
            });

            this.RegisterListener(EventID.GreenRedPoliceNeedShot, (sender, param) =>
            {
                PoliceNeedShot();
            });
        }

        private void InitUi()
        {
            holdToMoveBtn.gameObject.SetActive(false);
        }

        private void SpawnPlayer()
        {
            var spawnPos = startPlayerTransform.position;
            spawnPos.x += spawnDistanceFromStartPos;
            var player = GamePlayHelper.SpawnGreenRedCharacter(true, spawnPos);
            var playerScript = player.GetComponent<CharacterGreenRedController>();
            playerScript.InitData(startPlayerTransform.position, targetTransform.position.x,this);
        }

        private void SpawnEnemies()
        {
            var allEnemies = new List<GreenRedEnemy>();
            foreach (var enemyTransform in startEnemyTransforms)
            {
                var spawnPos = enemyTransform.position;
                spawnPos.x += spawnDistanceFromStartPos;
                var enemyClone = GamePlayHelper.SpawnGreenRedCharacter(false, spawnPos);
                var enemyScript = enemyClone.GetComponent<GreenRedEnemy>();
                enemyScript.InitData(enemyTransform.position, targetTransform.position.x,this);

                allEnemies.Add(enemyScript);
            }

            _totalEnemyCanDie = _singTimeData.GetRandomEnemyDieInGame();
            Debug.LogError($"Total die: {_totalEnemyCanDie}");
            _enemiesCanDie.Clear();

            for (int i = 0; i < _totalEnemyCanDie; i++)
            {
                var enemyIndexRandom = Random.Range(0, allEnemies.Count);
                _enemiesCanDie.Add(allEnemies[enemyIndexRandom]);

                allEnemies.RemoveAt(enemyIndexRandom);
            }
        }

        private void OnWin()
        {
            if (IsWinTriggered)
                return;

            IsWinTriggered = true;
            DataAccountPlayer.Player1GreenRedLight.OnWin();
        }

        #endregion

        #region Time Sing

        private float _warningBeforeEndTime = 1f;
        private float _currentDeadRate;

        private void StartSing()
        {
            SetWarning(false);
            this.PostEvent(EventID.GreenRedCanMove);

            int timeId = _singTimeRate.GetTimeSingId();
            float timeSing = _singTimeData.GetSingTime(timeId);
            LoadDeadRate(timeId);
            LoadFillImage(timeSing);
            StartCoroutine(CountToEndSing(timeSing));
        }

        private IEnumerator CountToEndSing(float timeCount)
        {
            float warningTime = timeCount - _warningBeforeEndTime;
            yield return new WaitForSeconds(warningTime);

            SetWarning(true);
            yield return new WaitForSeconds(_warningBeforeEndTime);

            StopSing();
        }

        private void StopSing()
        {
            this.PostEvent(EventID.GreenRedStopMove);
        }

        private void LoadDeadRate(int timeId)
        {
            var deadRateData = _singTimeData.GetDeadRate(_gameTimer);
            float deadRate = deadRateData.GetRate(timeId);

            for (int i = _enemiesCanDie.Count - 1; i >= 0; i--)
            {
                var enemy = _enemiesCanDie[i];
                if (deadRate <= 0)
                {
                    enemy.CanLive = true;
                    continue;
                }

                bool canLive = Random.Range(0, 1f) > deadRate;
                enemy.CanLive = canLive;

                if (!canLive)
                {
                    _enemiesCanDie.RemoveAt(i);
                }
            }
        }

        private void LoadFillImage(float timeSing)
        {
            timeProgress.fillAmount = 0f;
            timeProgress.DOFillAmount(1f, timeSing);
        }

        private void SetWarning(bool isWarning)
        {
            if (isWarning)
            {
                warningArea.DOColor(new Color(1, 0, 0, 0.2f), _warningBeforeEndTime);
            }
            else
            {
                warningArea.DOColor(new Color(0, 1, 0, 0.3f), 0);
                warningArea.DOFade(0, 1f);
            }
        }

        #endregion

        #region Polices

        private void LoadPolices()
        {
            _allPolices.AddRange(FindObjectsOfType<GreenRedPoliceController>());
        }

        private void PoliceNeedShot()
        {
            foreach (var police in _allPolices)
            {
                if (police.IsShooting)
                    continue;

                police.PlayShoot();
                return;
            }

            if (_allPolices.Count > 0)
            {
                var randomIndex = Random.Range(0, _allPolices.Count);
                _allPolices[randomIndex].PlayShoot();
            }
        }

        #endregion
    }
}