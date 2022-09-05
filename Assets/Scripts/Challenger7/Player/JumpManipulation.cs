using UnityEngine;

namespace Challenger7.Player
{
	public class JumpManipulation : MonoBehaviour
	{
		[SerializeField] private float fallMultiplier, lowJumpMultiplier;
		[SerializeField] private Rigidbody2D rigidBody;

		private void FixedUpdate() => ManipulateJump();

		private void ManipulateJump()
		{
			if (rigidBody.velocity.y < 0)
				rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
			else if (rigidBody.velocity.y > 0)
				rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
		}
	}
}