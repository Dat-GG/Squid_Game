using DG.Tweening;
using UnityEngine;

namespace GameManagerChallenger2.Character
{
	public class JumpController : MonoBehaviour
	{
		[SerializeField] private bool isPlayer;
		[SerializeField] private BoxCollider2D col;
		[SerializeField] private CharacterSpine characterSpine;
		internal CharacterSpine CharacterSpine => characterSpine;
		protected internal bool HasBalloon = false;
		protected internal int JumpCount = 0;
		internal bool FinishRound { get; set; } = false;
		internal bool FallOff { get; set; } = false;
		internal bool CanJump { get; set; } = false;

		protected void Jump(Transform destination)
		{
			col.enabled = false; // so the character will not detect any collisions before landing
			if (isPlayer) Gameplay.GameManagerChallenger2.Instance.BlockInput.gameObject.SetActive(true); // so player can't spam jumping button
			characterSpine.PlayJump();
			transform.DOLocalJump(destination.position, 0.25f, 1, 0.5f).SetEase(Ease.InSine).
				OnComplete(() =>
				{
					if (destination.gameObject.activeInHierarchy)
					{
						col.enabled = true;
						if (isPlayer) Gameplay.GameManagerChallenger2.Instance.BlockInput.gameObject.SetActive(false);
						characterSpine.PlayIdle();
						transform.DOScale(transform.localScale * 0.85f, 0.25f);	
					}
					else // if the glass piece is already broken
						Fall();
				});
		}

		protected internal void Fall()
		{
			GetComponentInChildren<MeshRenderer>().sortingOrder = -10;
			FallOff = true;
			CanJump = false;
			col.enabled = false;
			transform.DOLocalMove(new Vector3(transform.position.x, -10f, -1f), 0.5f).OnComplete(() =>
			{
				if (isPlayer) return;
				gameObject.SetActive(false);
			});
		}

		protected internal void Die()
		{
			if (isPlayer)
			{
				if (JumpCount <= 0)
					characterSpine.PlayDie();
				else
					Fall();
			}
			else
			{
				if (JumpCount <= 0)
				{
					CanJump = false;
					characterSpine.PlayDie();
				}
				else
					Fall();					
				
			}
		}
	}
}