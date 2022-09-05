using System.Collections;
using Challenger6.Gameplay;
using UnityEngine;

namespace Challenger6.Characters
{
	public class PoliceController : MonoBehaviour
	{
		[SerializeField] private CharactersSpine charactersSpine;
		[SerializeField] private float timeSwitchAnim;

		private void Start()
		{
			charactersSpine.PlayIdle();
			
			StartCoroutine(PlayAnimation());
		}

		private IEnumerator PlayAnimation()
		{
			yield return new WaitForSeconds(timeSwitchAnim);
			if (!GameManagerChallenger6.Instance.LoseScratch)
				yield break;
			charactersSpine.PlayAttack();
		}

		private void OnDisable()
		{
			StopCoroutine(PlayAnimation());
		}
	}
}