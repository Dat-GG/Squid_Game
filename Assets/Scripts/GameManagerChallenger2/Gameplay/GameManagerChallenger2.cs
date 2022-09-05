using System.Collections;
using System.Collections.Generic;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using DG.Tweening;
using GameManagerChallenger2.Character;
using GameManagerChallenger2.GlassPiece;
using GameManagerChallenger2.UI;
using GameManagerChallenger2.Utils;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameManagerChallenger2.Gameplay
{
	public class GameManagerChallenger2 : MonoBehaviour
	{
		public static GameManagerChallenger2 Instance;

		internal bool Lose = false;
		private bool _gameStart = false;
		[SerializeField] private Text yourTurnTxt;
		[SerializeField] private float turnChangeDelay;
		internal int PlayerTurn { get; private set; } = 1;
		internal bool PlayerTurnJump;

		[SerializeField] private TimerCountdown timerCountdown;
		[SerializeField] private CharactersOrders charactersOrders;
		[SerializeField] private Button playBtn, settingBtn, leftJumpBtn, rightJumpBtn;
		internal Button LeftJumpBtn => leftJumpBtn;
		internal Button RightJumpBtn => rightJumpBtn;
		[SerializeField] private Image blockInput;
		internal Image BlockInput => blockInput;

		[SerializeField] private List<Transform> spawnBots = new List<Transform>(5);
		[SerializeField] private Transform spawnPolice, spawnPlayer, readyPos, goalPosLeft, goalPosRight;
		internal Transform ReadyPos => readyPos;
		internal Transform GoalPosLeft => goalPosLeft;
		internal Transform GoalPosRight => goalPosRight;
		
		private PlayerController _player;
		private PoliceController _police;
		private BotController[] _bots;
		internal BotController[] Bots => _bots;

		private bool _showPopup = false;

		[SerializeField] private float botWaitTime, delayJumpTime;
		private float _delayJumpTime, _delayTimeToShowButton;
		private int _jumpOrder = 0;

		[SerializeField] private ListGlassPiecesController listGlassPieces;
		internal ListGlassPiecesController ListGlassPieces => listGlassPieces;

		private void Awake()
		{
			if (Instance == null) Instance = this;

			playBtn.onClick.AddListener(OnClickPlay);
			settingBtn.onClick.AddListener(OnClickSetting);
			leftJumpBtn.onClick.AddListener(OnClickLeftJump);
			rightJumpBtn.onClick.AddListener(OnClickRightJump);
			InitCharacter();
		}

		private void Start()
		{
			StartCoroutine(RandomPlayerTurn());
			
			_delayJumpTime = delayJumpTime;
			_delayTimeToShowButton = delayJumpTime;
		}

		private void Update()
		{

#if UNITY_EDITOR
			if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif

			CharactersJump();

			if (_player.FallOff)
			{
				leftJumpBtn.gameObject.SetActive(false);
				rightJumpBtn.gameObject.SetActive(false);
			}
			
			if (timerCountdown.TimerOn || _player.FinishRound) return; // if run out of time, and player hasn't won
			LoseGame();
		}

		private void InitCharacter()
		{
			var police = GameplayHelper.SpawnPolice(spawnPolice.position);
			var player = GameplayHelper.SpawnPlayer(spawnPlayer.position);
			for (var i = 0; i < 5; i++)
			{
				var bot = GameplayHelper.SpawnBot(spawnBots[i].position);
				if (spawnBots[i].position.x > 0) // if bot stand to the right, rotate so it looks at the centre
					bot.transform.GetChild(0).DORotate(new Vector3(0, 180, 0), 0f);
			}

			_player = player.GetComponent<PlayerController>();
			var skinPlayer = _player.GetComponentInChildren<CharacterSpine>();
			skinPlayer.ChangeSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString());
			_police = police.GetComponent<PoliceController>();
			_bots = FindObjectsOfType<BotController>();
		}

		private IEnumerator RandomPlayerTurn()
		{
			while (!_gameStart)
			{
				PlayerTurn++;
				if (PlayerTurn >= 6) PlayerTurn = 1;
				yourTurnTxt.text = "Your turn: " + PlayerTurn;
				yield return new WaitForSeconds(turnChangeDelay);
			}
		}
		
		private void CharactersJump()
		{
			if (!_gameStart)
				return;
			
			if (_jumpOrder >= 5)
				return;
			
			if (_jumpOrder == PlayerTurn - 1) // when it's player's turn to jump
			{
				_delayTimeToShowButton -= Time.deltaTime;
				if (_delayTimeToShowButton > 0) return;
				PlayerTurnJump = true;
				_player.ReadyToJump = true;
				rightJumpBtn.gameObject.SetActive(true);
				leftJumpBtn.gameObject.SetActive(true);
			}

			if (_jumpOrder + 1 == _player.JumpTurn && !_player.StartPlay)
				if (!timerCountdown.TimerOn) // if run out of time, player hasn't played his turn yet
					LoseGame();
				else // if it's player's turn and player hasn't played, bot will jump after bot wait time
				{
					botWaitTime -= Time.deltaTime;
					if (botWaitTime > 0) return;
					BotsJump();
				}
			
			BotsJump();
		}

		private void BotsJump()
		{
			_bots[_jumpOrder].CanJump = true;
			delayJumpTime -= Time.deltaTime;
			if (delayJumpTime > 0)
				return;

			_jumpOrder++;
			delayJumpTime = _delayJumpTime;
		}

		private void LoseGame()
		{
			_police.Attack();
			_player.Die();
			foreach (var bot in _bots)
			{
				if (bot.FinishRound) break;
				bot.Die();
			}

			if (_showPopup) return;
			Lose = true;
			DataAccountPlayer.PlayerChallenger2PowerUp.LoseCount++;
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LosePopup);
			_showPopup = true;
		}
		
		private void OnClickPlay()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			_gameStart = true;
			timerCountdown.gameObject.SetActive(true);
			charactersOrders.gameObject.SetActive(true);
			playBtn.gameObject.SetActive(false);
			var yourTurnRect = yourTurnTxt.GetComponent<RectTransform>();
			yourTurnRect.DOLocalMoveY(580, 1);
		}

		private void OnClickRightJump()
		{
			_player.JumpRight();
			if (_player.StartPlay) return;
			_player.StartPlay = true;
		}

		private void OnClickLeftJump()
		{
			_player.JumpLeft();
			if (_player.StartPlay) return;
			_player.StartPlay = true;
		}

		private void OnClickSetting()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupInGame);
			PopupController.Instance.HideHud();
		}

		internal Transform GenerateGoalPos()
		{
			var i = Random.Range(0, 2);
			return i == 0 ? goalPosLeft : goalPosRight;
		}

		private void OnDisable()
		{
			StopCoroutine(RandomPlayerTurn());
		}
	}
}