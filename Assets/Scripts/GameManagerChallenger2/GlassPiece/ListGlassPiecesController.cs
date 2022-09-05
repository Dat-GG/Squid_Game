using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameManagerChallenger2.GlassPiece
{
	public class ListGlassPiecesController : MonoBehaviour
	{
		[SerializeField] private float reinforceStepTime;
		[SerializeField] private GlassPiecesList glassPiecesList = new GlassPiecesList();
		internal GlassPiecesList GlassPiecesList => glassPiecesList;

		private void Start()
		{
			foreach (var t in glassPiecesList.glassPiecesList)
			{
				DefineGlassPieces(t.glassPieces);
			}
		}

		private void DefineGlassPieces(List<GlassPieceController> glassPieces)
		{
			var index = Random.Range(0, 2);
			if (index == 0)
			{
				glassPieces[0].IsBroken = true;
				glassPieces[1].IsBroken = false;
			}
			else
			{
				glassPieces[0].IsBroken = false;
				glassPieces[1].IsBroken = true;
			}
		}

		internal IEnumerator ReinforceGlasses()
		{
			foreach (var glasses in glassPiecesList.glassPiecesList)
			{
				Reinforce(glasses.glassPieces);
				yield return new WaitForSeconds(reinforceStepTime);
			}
		}
		
		private void Reinforce(List<GlassPieceController> glassPieces)
		{
			foreach (var piece in glassPieces)
			{
				piece.ReinforceGlass();
				piece.IsBroken = false;
			}
		}
	}
}