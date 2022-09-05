using System.Collections.Generic;
using GameManagerChallenger2.Character;
using UnityEngine;

namespace GameManagerChallenger2.GlassPiece
{
	public class GlassPieceController : MonoBehaviour
	{
		[SerializeField] private Sprite reinforceGlass;
		[SerializeField] private GameObject outline;
		internal bool IsBroken { get; set; }
		internal SpriteRenderer Sprite;

		private void Awake()
		{
			Sprite = GetComponent<SpriteRenderer>();
		}

		internal void ReinforceGlass()
		{
			Sprite.sprite = reinforceGlass;
			outline.SetActive(false);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other == null)
				return;
			if (!(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag($"Bot")))
				return;
			var c = other.gameObject.GetComponent<JumpController>();
			if (c.HasBalloon)
				return;
			c.JumpCount++;
			if (!IsBroken)
				return;
			gameObject.SetActive(false);
			c.Fall();
		}
	}
	
	[System.Serializable]
	public class GlassPieces
	{
		public List<GlassPieceController> glassPieces;
	}

	[System.Serializable]
	public class GlassPiecesList
	{
		public List<GlassPieces> glassPiecesList;
	}
}