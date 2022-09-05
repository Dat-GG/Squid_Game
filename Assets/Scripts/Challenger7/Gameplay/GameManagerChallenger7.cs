using System.Collections;
using System.Collections.Generic;
using Challenger7.Enemy;
using Challenger7.Player;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Challenger7.Gameplay
{
	public class GameManagerChallenger7 : MonoBehaviour
	{
		public static GameManagerChallenger7 Instance;
		
		[SerializeField] private Transform playerSpawn;
		[SerializeField] private int botNum;
		[SerializeField] private List<Transform> botSpawns = new List<Transform>(2);
		internal PlayerController Player { get; private set; }
		internal List<EnemyController> Enemy { get; } = new List<EnemyController>();

		[SerializeField] private List<Transform> itemSpawns = new List<Transform>();

		[SerializeField] private Button moveLeftBtn, moveRightBtn, moveUpBtn, settingBtn;
		[SerializeField] private Text goldAmountTxt;
		private int _goldAmount;

		private void Awake()
		{
			if (Instance == null) Instance = this;
			settingBtn.onClick.AddListener(OnClickSettingBtn);
		}

		private void OnEnable()
		{
			this.RegisterListener(EventID.CollectGold, delegate { AddGold(); });
			this.RegisterListener(EventID.PlayerLose, delegate { OnPlayerLose(); });
		}

		private void Start()
		{
			StartCoroutine(InitCharacters());
			InitButtons();
			StartCoroutine(GenerateItem());
			StartCoroutine(CheckWinGame());
		}

		private void Update()
		{
			
#if UNITY_EDITOR
			if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
			
		}

		private IEnumerator InitCharacters()
		{
			var player = GameplayHelper.SpawnPlayer(playerSpawn.position);
			Player = player.GetComponent<PlayerController>();

			for (var i = 0; i < botNum; i++)
			{
				var index = Random.Range(0, 2);
				var bot = GameplayHelper.SpawnBot(botSpawns[index].position);
				Enemy.Add(bot.GetComponent<EnemyController>());
				yield return new WaitForSeconds(1f);
			}
		}

		private void InitButtons()
		{
			AddEventToBtn(moveLeftBtn, EventTriggerType.PointerDown, Player.CallMoveLeft);
			AddEventToBtn(moveRightBtn, EventTriggerType.PointerDown, Player.CallMoveRight);
			AddEventToBtn(moveLeftBtn, EventTriggerType.PointerUp, Player.CallRelease);
			AddEventToBtn(moveRightBtn, EventTriggerType.PointerUp, Player.CallRelease);
			AddEventToBtn(moveUpBtn, EventTriggerType.PointerDown, Player.CallMoveUp);
		}

		private void AddEventToBtn(Button button, EventTriggerType type, PlayerController.EventButton eventFunction)
		{
			var trigger = button.GetComponent<EventTrigger>();
			
			var entry = new EventTrigger.Entry();
			entry.eventID = type;
			entry.callback.AddListener(delegate { eventFunction(); });
			
			trigger.triggers.Add(entry);
		}

		private IEnumerator GenerateItem()
		{
			while (!Player.WinGame)
			{
				var index = Random.Range(0, itemSpawns.Count);
				var chance1 = Random.Range(0, 2);
				if (chance1 == 0)
					GameplayHelper.SpawnGold(itemSpawns[index].position);
				else
				{
					var chance2 = Random.Range(0, 2);
					if (chance2 == 0)
						GameplayHelper.SpawnKnife(itemSpawns[index].position);
					else
						GameplayHelper.SpawnRock(itemSpawns[index].position);
				}

				yield return new WaitForSeconds(2f);
			}
		}

		private IEnumerator CheckWinGame()
		{
			while (Enemy.Count > 0)
				yield return null;

			this.PostEvent(EventID.PlayerWin);
			DataAccountPlayer.PlayerChallenger7PowerUp.WinCount++;
			DataAccountPlayer.PlayerChallenger7PowerUp.CountSpin++;
		}
		
		private void AddGold()
		{
			_goldAmount++;
			if (_goldAmount < 10)
				goldAmountTxt.text = "x 0" + _goldAmount;
			else
				goldAmountTxt.text = "x " + _goldAmount;
		}

		private void OnPlayerLose() => PopupController.Instance.OpenPopupAndKeepParent(PopupType.LosePopup);

		private void OnClickSettingBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParent(PopupType.SettingPopupInGame);
			PopupController.Instance.HideHud();
		}
		
		private void OnDisable() => StopAllCoroutines();
	}
}