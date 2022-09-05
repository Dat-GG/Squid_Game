using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger6.Characters
{
	public class CharactersSpine : MonoBehaviour
	{
		public SpineAnimationHold spineAnimationHold6;
		
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
			if (IsPlayingAnimation(spineAnimationHold6.idleAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold6.idleAnim, true);
		}
		
		internal void PlayWin(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold6.winAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold6.winAnim, true);
		}
		
		internal void PlayDie(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold6.dieAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold6.dieAnim, false);
		}
		
		internal void PlayAttack(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold6.attackAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold6.attackAnim, false);
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
		#endregion
	}
}