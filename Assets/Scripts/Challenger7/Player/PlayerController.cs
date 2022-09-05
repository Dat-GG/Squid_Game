using System;
using Challenger7.Base;
using Challenger7.Item;
using Core.Common.Popup;
using Core.Pool;
using DataAccount;
using UnityEngine;

namespace Challenger7.Player
{
	public class PlayerController : MoveController
	{
		[SerializeField] private float moveSpeed;
		private bool _isAlive = true;
		internal bool WinGame;
		internal delegate void EventButton();
		internal EventButton CallMoveLeft, CallMoveRight, CallMoveUp, CallRelease;

		protected override void Awake()
		{
			CallMoveLeft = OnClickMoveLeftBtn;
			CallMoveRight = OnClickMoveRightBtn;
			CallMoveUp = OnClickMoveUpBtn;
			CallRelease = OnReleaseMoveBtn;
			
			base.Awake();
		}

		private void OnEnable()
		{
			this.RegisterListener(EventID.TimesUp, delegate { OnFinishGame(); });
			this.RegisterListener(EventID.PlayerWin, delegate { OnFinishGame(); });
		} 

		protected override void Update()
		{
			
#if UNITY_EDITOR
			MovingTool();
#endif
			
			PlayAnimation();
			base.Update();
		}
		
		private void FixedUpdate() => Move(VelX, moveSpeed);

		// Attach in buttons' Event Trigger
		private void OnClickMoveLeftBtn() => VelX = -1;

		private void OnClickMoveRightBtn() => VelX = 1;

		private void OnReleaseMoveBtn() => VelX = 0;
		// private void OnClickMoveUpBtn() => RigidBody.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Force);
		private void OnClickMoveUpBtn() => RigidBody.velocity = Vector2.up * 8f;
		
		private void MovingTool()
		{
			if (Input.GetKeyDown(KeyCode.A))
				OnClickMoveLeftBtn();
			if (Input.GetKeyDown(KeyCode.D))
				OnClickMoveRightBtn();
			if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
				OnReleaseMoveBtn();
			if (Input.GetKeyDown(KeyCode.W))
				OnClickMoveUpBtn();
		}
		
		private void PlayAnimation()
		{
			if (WinGame) return;
			if (_isAlive)
				if (VelX == 0)
					PlayerSpine.PlayIdle();
				else
					PlayerSpine.PlayRun();
			else
				PlayerSpine.PlayDie();
		}

		private void OnFinishGame()
		{
			if (WinGame) return; // so it can't be called twice
			if (!_isAlive) return;
			WinGame = true;
			PlayerSpine.PlayWin();
			PopupController.Instance.OpenPopupAndKeepParent(PopupType.WinPopup);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var item = other.GetComponent<ItemController>();
			if (!item) return;
			switch (item.ItemType)
			{
				case ItemType.None:
					break;
				case ItemType.Gold:
					this.PostEvent(EventID.CollectGold);
					break;
				case ItemType.Knife:
					if (WinGame) return;
					if (!_isAlive) return;
					_isAlive = false;
					this.PostEvent(EventID.PlayerLose);
					DataAccountPlayer.PlayerChallenger7PowerUp.LoseCount++;
					break;
				case ItemType.Rock:
					if (WinGame) return;
					if (!_isAlive) return;
					_isAlive = false;
					this.PostEvent(EventID.PlayerLose);
					DataAccountPlayer.PlayerChallenger7PowerUp.LoseCount++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			SmartPool.Instance.Despawn(item.gameObject);
		}
	}
}