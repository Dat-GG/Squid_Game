using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace GameManagerChallenger2.Character
{
	public class CharacterSpine : MonoBehaviour
	{
		[SerializeField] private GameObject bloodEffect;
		[SerializeField] private List<GameObject> soulEffects;
		public SpineAnimationHold spineAnimationHold2;
		
		private SkeletonAnimation _skeletonAnimation;
		private const int TrackIndex = 0;
		private float _timeScaleDefault = 1f;
		private float _currentTimeScale;
		private bool _checkSkillAnim;
		
		private void Awake()
		{
			_skeletonAnimation = GetComponent<SkeletonAnimation>();
		}
		
		public void ChangeSkin(string skinName)
		{
			_skeletonAnimation.Skeleton.SetSkin(skinName);
			_skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			_skeletonAnimation.AnimationState.Apply(_skeletonAnimation.Skeleton);
		}

		internal void PlayIdle(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold2.idleAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold2.idleAnim, true);
		}
		
		internal void PlayRun(float timeSpeedPercent = 1f)
		{
			var index = Random.Range(0, spineAnimationHold2.runAnim.Count);
			var runAnim = spineAnimationHold2.runAnim[index];
			
			if (IsPlayingAnimation(runAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, runAnim, true);
		}

		internal void PlayJump(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold2.jumpAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold2.jumpAnim, false);
		}

		internal void PlayDie(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold2.dieAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold2.dieAnim, false);
			bloodEffect.SetActive(true);
			
			var index = Random.Range(0, soulEffects.Count);
			soulEffects[index].SetActive(true);
		}

		internal void PlayAttack(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold2.attackAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold2.attackAnim, false);
		}

		#region Helpers
		private TrackEntry PlayAnimation(int trackIndex, string animationName, bool isLoop)
		{
			return _skeletonAnimation.AnimationState.SetAnimation(trackIndex, animationName, isLoop);
		}
        
		private bool IsPlayingAnimation(string animationName)
		{
			var trackEntry = _skeletonAnimation.state.GetCurrent(TrackIndex);
			if (trackEntry is null)
			{
				return false;
			}
            
			var runningAnimation = trackEntry.Animation;
			if (runningAnimation is null)
			{
				return false;
			}
            
			if (runningAnimation == _skeletonAnimation.skeleton.Data.FindAnimation(animationName))
			{
				return true;
			}

			return false;
		}

		private void SetTimeSpeed(float percentWithDefault)
		{
			_currentTimeScale = _timeScaleDefault * percentWithDefault;
			_skeletonAnimation.timeScale = _currentTimeScale;
		}
		
		internal bool IsUnusedSkin(string skinName)
		{
			return spineAnimationHold2.noUseSkin.Contains(skinName);
		}
		
		#endregion
	}
}