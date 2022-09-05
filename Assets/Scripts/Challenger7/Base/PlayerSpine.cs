using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger7.Base
{
	public class PlayerSpine : MonoBehaviour
	{
		[SerializeField] private SpineAnimationHold spineAnimationHold;
		[SerializeField] private SkeletonAnimation skeletonAnimation;
		private const int TrackIndex = 0;
		private float _timeScaleDefault = 1f;
		private float _currentTimeScale;
		private bool _checkSkillAnim;

		internal void PlayIdle(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold.idleAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold.idleAnim, true);
		}
		
		internal void PlayRun(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold.runAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold.runAnim, true);
		}
		
		internal void PlayWin(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold.winAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold.winAnim, true);
		}
		
		internal void PlayDie(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold.dieAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold.dieAnim, false);
		}
		
		internal void PlayAttack(float timeSpeedPercent = 1f)
		{
			if (IsPlayingAnimation(spineAnimationHold.attackAnim))
			{
				return;
			}

			SetTimeSpeed(timeSpeedPercent);
			PlayAnimation(TrackIndex, spineAnimationHold.attackAnim, false);
		}

		#region Helpers
		
		private TrackEntry PlayAnimation(int trackIndex, string animationName, bool isLoop)
		{
			return skeletonAnimation.AnimationState.SetAnimation(trackIndex, animationName, isLoop);
		}
        
		private bool IsPlayingAnimation(string animationName)
		{
			var trackEntry = skeletonAnimation.state.GetCurrent(TrackIndex);
			if (trackEntry is null)
			{
				return false;
			}
            
			var runningAnimation = trackEntry.Animation;
			if (runningAnimation is null)
			{
				return false;
			}
            
			if (runningAnimation == skeletonAnimation.skeleton.Data.FindAnimation(animationName))
			{
				return true;
			}

			return false;
		}

		private void SetTimeSpeed(float percentWithDefault)
		{
			_currentTimeScale = _timeScaleDefault * percentWithDefault;
			skeletonAnimation.timeScale = _currentTimeScale;
		}

		internal bool IsUnusedSkin(string skinName)
		{
			return spineAnimationHold.noUseSkin.Contains(skinName);
		}
		
		#endregion
	}
}