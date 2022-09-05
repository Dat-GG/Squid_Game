using System;
using System.Collections.Generic;
using Challenger7.Base;
using Challenger7.Gameplay;
using Challenger7.Item;
using Core.Pool;
using Spine;
using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenger7.Enemy
{
	public class EnemyController : MoveController
	{
		[SerializeField] private SkeletonAnimation skeletonAnimation;
		[SerializeField] private float minSpeed, maxSpeed;
		private float _speed;
		private bool _isAlive = true;
		
		private void OnEnable()
		{
			this.RegisterListener(EventID.PlayerLose, delegate { OnFinishGame(); });
			this.RegisterListener(EventID.TimesUp, delegate { OnFinishGame(); });
		}

		protected override void Start()
		{
			PlayerSpine.PlayRun();
			SetRandomSkins();
			_speed = Random.Range(minSpeed, maxSpeed);
			
			base.Start();
		}

		protected override void Update()
		{
			if (transform.position.x >= HorizontalBound - BorderGap)
				VelX = -1;
			else if (transform.position.x <= HorizontalBound * -1 + BorderGap)
				VelX = 1;
			
			base.Update();
		}

		private void FixedUpdate() => Move(VelX, _speed);

		private void SetRandomSkins()
		{
			var listSkin = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Skins.Items;
			var skins = new List<Skin>(listSkin);

			for (var i = skins.Count - 1; i >= 0; i--)
			{
				var skin = skins[i];
				var isUnusedSkin = PlayerSpine.IsUnusedSkin(skin.Name);
				if (isUnusedSkin)
					skins.RemoveAt(i);
			}
			
			var rdIndex = Random.Range(0, skins.Count);
			var skinName = skins[rdIndex].Name;
            
			skeletonAnimation.Skeleton.SetSkin(skinName);
			skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);
		}

		private void OnFinishGame()
		{
			if (!_isAlive) return;
			VelX = 0;
			PlayerSpine.PlayWin();
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
					break;
				case ItemType.Knife:
					if (GameManagerChallenger7.Instance.Player.WinGame) return;
					GameManagerChallenger7.Instance.Enemy.Remove(this);
					SmartPool.Instance.Despawn(gameObject);
					break;
				case ItemType.Rock:
					if (GameManagerChallenger7.Instance.Player.WinGame) return;
					GameManagerChallenger7.Instance.Enemy.Remove(this);
					SmartPool.Instance.Despawn(gameObject);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			_isAlive = false;
			SmartPool.Instance.Despawn(item.gameObject);
		}
	}
}