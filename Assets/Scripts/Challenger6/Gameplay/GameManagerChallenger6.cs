using Challenger6.Characters;
using Challenger6.UI;
using Challenger6.Utils;
using Core.Common.Popup;
using Core.Common.Sound;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using ScratchCardAsset;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Challenger6.Gameplay
{
	public class GameManagerChallenger6 : MonoBehaviour
	{
		public static GameManagerChallenger6 Instance;

		[SerializeField] private Button tonguePowerUpBtn, settingBtn;
		[SerializeField] private GameObject scratchProgress, detachProgress;
		internal bool WinDetach = false;
		internal bool LoseScratch = false;
		private bool _updateResult = false;
		[SerializeField] private float timeToShowPopup;
		
		[SerializeField] private CandyBoxController[] candyBoxes;
		internal int WinningCandyIndex { get; private set; }
		private bool _boxChosen = false;
		[SerializeField] private Transform centerPos;
		[SerializeField] private Transform[] outsidePos;
		
		[SerializeField] private InputCheck inputCheck;
		private int _checkInputCount = 0;
		[SerializeField] private GameObject tutorialTxt, blurBg;
		[SerializeField] private TimerCountdown6 timerCountdown;
		
		[SerializeField] private GameObject brokenCircleCandyPref, brokenHeartCandyPref, brokenSquareCandyPref,
			brokenStarCandyPref, brokenTriangleCandyPref, brokenUmbrellaCandyPref;
		private bool _brokenSpawned = false;

		[SerializeField] private Transform policeSpawn, playerSpawn;
		[SerializeField] private float timeToSpawnCharacter;
		private bool _spawnedCharacter = false;
		private GameObject _player;
		
		private void Awake()
		{
			if (Instance == null) Instance = this;
			tonguePowerUpBtn.onClick.AddListener(OnClickTonguePowerUpBtn);
			settingBtn.onClick.AddListener(OnClickSettingBtn);
		}

		private void OnEnable()
		{
			EraseProgress.OnProgress += OnEraseProgress;
			CandyBoxController.BoxFullyOpen += OnBoxFullyOpen;
		}
		
		private void Start()
		{
			Application.targetFrameRate = 60;
		}

		private void Update()
		{
#if UNITY_EDITOR
			if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif

			ChooseCandyBox();
			
			if (LoseScratch)
			{
				if (_updateResult) return;
				SpawnBrokenCandy();
				SpawnCharacter();
				timeToShowPopup -= Time.deltaTime;
				if (timeToShowPopup > 0) return;
				PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.LosePopup);
				_updateResult = true;
			}
			else if (WinDetach)
			{
				if (_updateResult) return;
				detachProgress.SetActive(false);
				scratchProgress.SetActive(false);
				timerCountdown.TimerOn = false;
				SpawnCharacter();
				timeToShowPopup -= Time.deltaTime;
				if (timeToShowPopup > 0) return;
				PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
				_updateResult = true;
			}
		}
		
		private void ChooseCandyBox()
		{
			if (!(inputCheck.IsFirstTimeTouch && _checkInputCount < 1)) return;
			foreach (var box in candyBoxes)
			{
				if (box.Chosen)
					_boxChosen = true;
			}

			if (!_boxChosen) return;
			foreach (var candyBox in candyBoxes)
			{
				if (candyBox.Chosen)
					candyBox.ChosenBox(centerPos);
				else
				{
					switch (candyBox.BoxId)
					{
						case 0:
							candyBox.RemovedBox(outsidePos[0]);
							break;
						case 1:
							candyBox.RemovedBox(outsidePos[1]);
							break;
						case 2:
							candyBox.RemovedBox(outsidePos[2]);
							break;
						case 3:
							candyBox.RemovedBox(outsidePos[3]);
							break;
					}
				}
			}

			_checkInputCount++;
			tutorialTxt.SetActive(false);
			timerCountdown.gameObject.SetActive(true);
			scratchProgress.SetActive(true);
		}

		private void OnBoxFullyOpen() => tonguePowerUpBtn.gameObject.SetActive(true);

		private void OnEraseProgress(float progress)
		{
			var value = Mathf.Round(progress * 100f);
			if (value >= 96f && timerCountdown.TimerOn)
			{
				detachProgress.SetActive(true);
				timerCountdown.SetTimer(31f);
				scratchProgress.SetActive(false);
				OnFinishScratch();
				tonguePowerUpBtn.gameObject.SetActive(false);
			}
			else if (value < 100f && !timerCountdown.TimerOn)
				LoseScratch = true;
		}
		
		private void SpawnBrokenCandy()
		{
			timerCountdown.TimerOn = false;
			foreach (var box in candyBoxes)
			{
				if (!box.gameObject.activeInHierarchy) continue;
				var candy = box.gameObject.transform.GetChild(2).gameObject;
				candy.SetActive(false);
				if (_brokenSpawned) return;
				switch (candy.GetComponent<CandyController>().Shape)
				{
					case "Circle":
						var brokenCircle = Instantiate(brokenCircleCandyPref, box.transform);
						brokenCircle.transform.position = centerPos.position;
						break;
					case "Heart":
						var brokenHeart = Instantiate(brokenHeartCandyPref, box.transform);
						brokenHeart.transform.position = centerPos.position;
						break;
					case "Square":
						var brokenSquare = Instantiate(brokenSquareCandyPref, box.transform);
						brokenSquare.transform.position = centerPos.position;
						break;
					case "Star":
						var brokenStar = Instantiate(brokenStarCandyPref, box.transform);
						brokenStar.transform.position = centerPos.position;
						break;
					case "Triangle":
						var brokenTriangle = Instantiate(brokenTriangleCandyPref, box.transform);
						brokenTriangle.transform.position = centerPos.position;
						break;
					case "Umbrella":
						var brokenUmbrella = Instantiate(brokenUmbrellaCandyPref, box.transform);
						brokenUmbrella.transform.position = centerPos.position;
						break;
				}

				_brokenSpawned = true;
			}
		}

		private void SpawnCharacter()
		{
			timeToSpawnCharacter -= Time.deltaTime;
			if (timeToSpawnCharacter > 0) return;
			timeToSpawnCharacter = 0;
			blurBg.SetActive(false);
			foreach (var box in candyBoxes)
			{
				if (!box.gameObject.activeInHierarchy) continue;
				box.gameObject.SetActive(false);
			}
			if (_spawnedCharacter) return;
			GameplayHelper.SpawnPlayer(playerSpawn.position);
			_player = GameplayHelper.SpawnPolice(policeSpawn.position);
			var skinPlayer = _player.GetComponentInChildren<CharactersSpine>();
			skinPlayer.ChangeSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString());
			_spawnedCharacter = true;
		}

		private void OnClickTonguePowerUpBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			detachProgress.SetActive(true);
			timerCountdown.SetTimer(31f);
			scratchProgress.SetActive(false);
			tonguePowerUpBtn.gameObject.SetActive(false);
			OnFinishScratch();
		}

		private void OnFinishScratch()
		{
			foreach (var box in candyBoxes)
			{
				if (!box.gameObject.activeInHierarchy) continue;
				WinningCandyIndex = box.CandyIndex;
				box.FinishScratch();
			}
		}

		private void OnClickSettingBtn()
		{
			SoundManager.Instance.PlaySound(SoundType.ClickButton);
			PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.SettingPopupInGame);
			PopupController.Instance.HideHud();
		}
		
		private void OnDisable()
		{
			CandyBoxController.BoxFullyOpen -= OnBoxFullyOpen;
			EraseProgress.OnProgress -= OnEraseProgress;
			
			if (LoseScratch)
				DataAccountPlayer.PlayerChallenger6PowerUp.LoseCount++;
			else if (WinDetach)
				DataAccountPlayer.PlayerChallenger6PowerUp.WinCount++;
		}
	}
}