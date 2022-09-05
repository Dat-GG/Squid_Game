using UnityEngine;

namespace Challenger7.Base
{
	public class MoveController : MonoBehaviour
	{
		[SerializeField] private PlayerSpine playerSpine;
		internal PlayerSpine PlayerSpine => playerSpine;
		
		[SerializeField] private Rigidbody2D rigidBody;
		internal Rigidbody2D RigidBody => rigidBody;
		protected float VelX;
		private bool _isFacingRight = true;
		
		[SerializeField] private float borderGap;
		protected float BorderGap => borderGap;
		private Camera _mainCamera;
		protected float HorizontalBound;
		private float _verticalBound;

		protected virtual void Awake() => _mainCamera = Camera.main;

		protected virtual void Start()
		{
			var screenBound =
				_mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
					_mainCamera.transform.position.z));
			HorizontalBound = screenBound.x;
			_verticalBound = screenBound.y;
		}
		
		protected virtual void Update() => FlipCheck();

		private void LateUpdate() // keep the object inside the screen
		{
			var viewPos = transform.position;
			viewPos.x = Mathf.Clamp(viewPos.x, HorizontalBound * -1 + borderGap, HorizontalBound - borderGap);
			viewPos.y = Mathf.Clamp(viewPos.y, _verticalBound * -1, -1);
			transform.position = viewPos;
		}
		
		protected void Move(float direction, float moveSpeed)
		{
			rigidBody.velocity = new Vector2(direction * moveSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
		}
		
		private void Flip()
		{
			_isFacingRight = !_isFacingRight;
			transform.Rotate(0f, 180f, 0f);
		}

		private void FlipCheck()
		{
			if (VelX > 0 && !_isFacingRight)
				Flip();
			else if (VelX < 0 && _isFacingRight)
				Flip();
		}
	}
}