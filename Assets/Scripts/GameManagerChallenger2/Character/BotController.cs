using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameManagerChallenger2.Character
{
	public class BotController : CharacterController
	{
		[SerializeField] private SkeletonAnimation skeletonAnimation;
		[SerializeField] private TextMeshPro id;
		[SerializeField] private float idleTime;
		private bool _startJump = false;

		private void Start()
		{
			CharacterSpine.PlayIdle();
			SetRandomSkins();
			SetId();
			StartCoroutine(BotJump());
		}
		
		private void SetRandomSkins()
		{
			var listSkin = skeletonAnimation.skeletonDataAsset.GetSkeletonData(true).Skins.Items;
			var skins = new List<Skin>(listSkin);

			for (var i = skins.Count - 1; i >= 0; i--)
			{
				var skin = skins[i];
				var isUnusedSkin = CharacterSpine.IsUnusedSkin(skin.Name);
				if (isUnusedSkin)
					skins.RemoveAt(i);
			}
			
			var rdIndex = Random.Range(0, skins.Count);
			var skinName = skins[rdIndex].Name;
            
			skeletonAnimation.Skeleton.SetSkin(skinName);
			skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);
		}

		private void SetId()
		{
			var randomId = Random.Range(100, 1000);
			id.text = randomId.ToString();
		}

		private IEnumerator BotJump()
		{
			while (!CanJump)
				yield return null;

			yield return new WaitForSeconds(1f);
			CharacterSpine.PlayRun();
			transform.DOLocalMove(Gameplay.GameManagerChallenger2.Instance.ReadyPos.position, 1f)
				.OnComplete(delegate
				{
					CharacterSpine.PlayIdle();
					_startJump = true;
				});

			yield return new WaitUntil(() => _startJump);
			while (_startJump && CanJump)
			{
				yield return new WaitForSeconds(idleTime);
				if (JumpCount >= 6) // avoid index out of range exception
				{
					BotFinish();
					_startJump = false;
				}
				else
				{
					var glassPiecesList = Gameplay.GameManagerChallenger2.Instance.ListGlassPieces.GlassPiecesList;
					if (!glassPiecesList.glassPiecesList[JumpCount].glassPieces[1].gameObject.activeInHierarchy)
						JumpLeft();
					else if (!glassPiecesList.glassPiecesList[JumpCount].glassPieces[0].gameObject.activeInHierarchy)
						JumpRight();
					else
					{
						var index = Random.Range(0, 2);
						if (index == 0)
							JumpRight();
						else
							JumpLeft();
					}
				}
			}
		}
		
		private void BotFinish() => Jump(Gameplay.GameManagerChallenger2.Instance.GenerateGoalPos());

		private void OnDisable() => StopCoroutine(BotJump());
	}
}