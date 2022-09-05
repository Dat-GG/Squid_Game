using Core.Common;
using Core.Common.Popup;
using DataAccount;
using GameManagerChallenger2.Character;
using UnityEngine;

namespace GameManagerChallenger2.Goal
{
	public class GoalController : MonoBehaviour
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other == null)
				return;
			if (other.gameObject.CompareTag($"Bot"))
			{
				var c = other.gameObject.GetComponent<JumpController>();
				c.CanJump = false;
				c.FinishRound = true;	
			}
			else if (other.gameObject.CompareTag($"Player"))
			{
				DataAccountPlayer.PlayerChallenger2PowerUp.WinCount++;
				DataAccountPlayer.PlayerChallenger2PowerUp.CountSpin++;
				this.StartDelayMethod(1f, delegate
				{
					PopupController.Instance.OpenPopupAndKeepParentWithHud(PopupType.WinPopup);
				});
			}
		}
	}
}