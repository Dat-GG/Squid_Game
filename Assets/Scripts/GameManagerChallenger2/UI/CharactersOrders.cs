using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagerChallenger2.UI
{
	public class CharactersOrders : MonoBehaviour
	{
		[SerializeField] private Image progressBar;
		private Vector2 _sizeDelta;

		[SerializeField] private List<CharacterSlot> characterSlots;

		private void Awake()
		{
			_sizeDelta = progressBar.GetComponent<RectTransform>().sizeDelta;
		}

		private void Start()
		{
			DefineSlots();
			
			StartCoroutine(MoveProgressBar());
			StartCoroutine(OpenSlots());
		}
		
		private void DefineSlots()
		{
			var i = 0;
			var listBots = Gameplay.GameManagerChallenger2.Instance.Bots;
			
			foreach (var slot in characterSlots)
			{
				if (slot.TurnTxt.text == Gameplay.GameManagerChallenger2.Instance.PlayerTurn.ToString())
				{
					slot.TurnTxt.text = "You";
					slot.PlayerSlot = true;
				}
				else
				{
					slot.Bot = listBots[listBots.Length - 1 - i];
					i++;
				}
			}
		}
		
		private IEnumerator MoveProgressBar()
		{
			while (_sizeDelta.x < 450)
			{
				_sizeDelta += new Vector2(3, 0);
				progressBar.GetComponent<RectTransform>().sizeDelta = _sizeDelta;
				yield return null;
			}
		}

		private IEnumerator OpenSlots()
		{
			foreach (var slot in characterSlots)
			{
				slot.transform.DOScale(new Vector2(1f, 1f), 1f);
				yield return new WaitForSeconds(0.2f);
			}
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}
	}
}