using System.Collections;
using Challenger6.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Challenger6.UI
{
	public class ScratchProgressController : MonoBehaviour
	{
		[SerializeField] private Image progressStatusImg;
		[SerializeField] private float progressGainSpeed;

		[SerializeField] private Image skullImg;
		[SerializeField] private Sprite redEyesSkullSpr;

		[SerializeField] private Image warningImg;
		internal Image WarningImg => warningImg;
		
		private void Start() => StartCoroutine(CheckProgress());

		private IEnumerator CheckProgress()
		{
			while (progressStatusImg.fillAmount < 1)
				yield return null;

			GameManagerChallenger6.Instance.LoseScratch = true;
		}

		private void Update()
		{
			if (progressStatusImg.fillAmount < 0.7) return;
			skullImg.sprite = redEyesSkullSpr;
		}

		internal void ProgressGain() => progressStatusImg.fillAmount += Time.deltaTime * progressGainSpeed;
		
		internal void ToggleWarning(bool toggle) => warningImg.gameObject.SetActive(toggle);
		
		private void OnDisable() => StopCoroutine(CheckProgress());
	}
}