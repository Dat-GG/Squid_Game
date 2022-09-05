using DG.Tweening;
using UnityEngine;

namespace GameManagerChallenger2.Character
{
	public class CharacterController : JumpController
	{
		[SerializeField] private Transform turnView;
			
		protected internal void JumpRight()
		{
			turnView.DORotate(new Vector3(0, 0, 0), 0f); // rotate when jumping
			
			var glassPiecesList = Gameplay.GameManagerChallenger2.Instance.ListGlassPieces.GlassPiecesList;

			Jump(JumpCount <= 5
				? glassPiecesList.glassPiecesList[JumpCount].glassPieces[1].transform
				: Gameplay.GameManagerChallenger2.Instance.GoalPosRight);
		}

		protected internal void JumpLeft()
		{
			turnView.DORotate(new Vector3(0, 180, 0), 0f);
	
			var glassPiecesList = Gameplay.GameManagerChallenger2.Instance.ListGlassPieces.GlassPiecesList;

			Jump(JumpCount <= 5
				? glassPiecesList.glassPiecesList[JumpCount].glassPieces[0].transform
				: Gameplay.GameManagerChallenger2.Instance.GoalPosLeft);
		}
	}
}