using Challenger6.UI;
using UnityEngine;

namespace Challenger6.Gameplay
{
	public class NeedleController : MonoBehaviour
	{
		[SerializeField] private ParticleSystem scratchEffect;
		private ParticleSystem.EmissionModule _scratchEmission;
		
		private bool _isStartTouch;
		[SerializeField] private float speed;
		private Vector3 _mouseLastPos;
		private Camera _mainCamera;
		private ScratchProgressController _progressStatus;

		[SerializeField] private Animator needleBodyShake;
		[SerializeField] private Transform needleTip;
		[SerializeField] private LayerMask candyOutlineLayer;

		private void Awake()
		{
			_scratchEmission = scratchEffect.emission;
			_mainCamera = Camera.main;
			_progressStatus = FindObjectOfType<ScratchProgressController>();
		}

		private void Update()
		{
			if (!Input.GetMouseButton(0))
			{
				_isStartTouch = false;
				needleBodyShake.enabled = false;
				_scratchEmission.rateOverTime = 0;
				if (_progressStatus.WarningImg.gameObject.activeInHierarchy) _progressStatus.ToggleWarning(false);
				return;
			}

			if (!_isStartTouch)
			{
				_mouseLastPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
				_isStartTouch = true;
				return;
			}

			var currentPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
			var direction = currentPos - _mouseLastPos;
			_mouseLastPos = currentPos;
			// transform.position += direction.normalized * speed * Time.smoothDeltaTime;
			_scratchEmission.rateOverTime = 25;
			transform.Translate(direction.normalized * speed * Time.smoothDeltaTime);
			
			needleBodyShake.enabled = true;
			
			OnMoving();
		}

		private void OnMoving()
		{
			var hit = Physics2D.Raycast(needleTip.position, Vector2.zero, Mathf.Infinity,
				candyOutlineLayer);
			if (hit.collider == null)
				_progressStatus.ToggleWarning(false);
			else if (hit.collider.CompareTag("Candy"))
			{
				_progressStatus.ProgressGain();
				_progressStatus.ToggleWarning(true);
			}
		}
	}
}