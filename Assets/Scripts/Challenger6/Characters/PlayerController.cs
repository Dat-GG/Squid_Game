using System.Collections;
using System.Collections.Generic;
using Challenger6.Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger6.Characters
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private CharactersSpine charactersSpine;
		[SerializeField] private float timeSwitchAnim;
		[SerializeField] private GameObject bloodEffect;
		[SerializeField] private List<GameObject> soulMissile;
		[SerializeField] private Transform effectPos;

		[SerializeField] private SpriteRenderer candyOnHand;
		[SerializeField] private List<Sprite> winningCandySpr;
		private int _winningCandyIndex;
		
		[SerializeField] private SpriteRenderer fallenCandy;
		[SerializeField] private Sprite fallenCandySpr, brokenCandySpr;

		private void Start()
		{
			_winningCandyIndex = GameManagerChallenger6.Instance.WinningCandyIndex;
			charactersSpine.PlayIdle();
			StartCoroutine(PlayAnimation());
		}

		private IEnumerator PlayAnimation()
		{
			if (GameManagerChallenger6.Instance.LoseScratch)
			{
				candyOnHand.sprite = brokenCandySpr;
				fallenCandy.sprite = fallenCandySpr;
			}
			else if (GameManagerChallenger6.Instance.WinDetach)
				candyOnHand.sprite = winningCandySpr[_winningCandyIndex];
			
			yield return new WaitForSeconds(timeSwitchAnim);
			if (GameManagerChallenger6.Instance.LoseScratch)
			{
				charactersSpine.PlayDie();
				
				var bloodEff = Instantiate(bloodEffect, transform);
				bloodEff.transform.position = effectPos.position;

				yield return new WaitForSeconds(timeSwitchAnim * 0.5f);
				var soulIndex = Random.Range(0, soulMissile.Count);
				var soulEff = Instantiate(soulMissile[soulIndex], transform);
				soulEff.transform.position = effectPos.position;
			}
			else if (GameManagerChallenger6.Instance.WinDetach)
				charactersSpine.PlayWin();
		}

		private void OnDisable() => StopCoroutine(PlayAnimation());
	}
}