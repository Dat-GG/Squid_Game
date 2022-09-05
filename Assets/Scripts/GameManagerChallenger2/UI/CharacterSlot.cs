using System.Collections;
using DG.Tweening;
using GameManagerChallenger2.Character;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger2.UI
{
	public class CharacterSlot : MonoBehaviour
	{
		internal bool PlayerSlot { get; set; }
		internal BotController Bot { get; set; }

		[SerializeField] private Text turnTxt;
		internal Text TurnTxt => turnTxt;
		
		[SerializeField] private Image currentOutline, dieMark, winMark, iconImg, arrowImg;
		private Vector3 _arrowLocalPos, _arrowTargetPos;

		[SerializeField] private float timeToShowCurrentOutline;
		private float _botIdleTime = 3f;

		private void Awake()
		{
			_arrowLocalPos = arrowImg.GetComponent<RectTransform>().localPosition;
			_arrowTargetPos = _arrowLocalPos - new Vector3(0, 10, 0);
		}

		private void Start()
		{
			StartCoroutine(ArrowLoopMove());
		}

		private void Update()
		{
			ToggleOutline();
			ToggleWinMark();
			ToggleDieMark();
			
			if (!PlayerSlot) return;
			arrowImg.gameObject.SetActive(true);
		}

		private IEnumerator ArrowLoopMove()
		{
			arrowImg.GetComponent<RectTransform>().DOLocalMove(_arrowTargetPos, 0.5f)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo);
			yield return null;
		}

		private void ToggleOutline()
		{
			if (Bot != null && Bot.CanJump)
			{
				_botIdleTime -= Time.deltaTime;
				if (_botIdleTime > 0)
					return;
				
				currentOutline.gameObject.SetActive(true);
				timeToShowCurrentOutline -= Time.deltaTime;
				if (timeToShowCurrentOutline > 0) return;
				currentOutline.gameObject.SetActive(false);
			}
			else if (Bot == null && PlayerSlot)
			{
				if (Gameplay.GameManagerChallenger2.Instance.PlayerTurnJump)
				{
					currentOutline.gameObject.SetActive(true);
					timeToShowCurrentOutline -= Time.deltaTime;
					if (timeToShowCurrentOutline > 0) return;
					currentOutline.gameObject.SetActive(false);
				}
			}
		}

		private void ToggleDieMark()
		{
			if (Bot != null && Bot.FallOff)
			{
				dieMark.gameObject.SetActive(true);
				if (!currentOutline.gameObject.activeInHierarchy) return;
				currentOutline.gameObject.SetActive(false);
			}
		}

		private void ToggleWinMark()
		{
			if (Bot != null && Bot.FinishRound)
				winMark.gameObject.SetActive(true);
		}

		private void OnDisable()
		{
			StopCoroutine(ArrowLoopMove());
		}
	}
}