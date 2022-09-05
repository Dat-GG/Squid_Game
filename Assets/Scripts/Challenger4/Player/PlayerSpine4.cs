using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger4.Player
{
    public class PlayerSpine4 : MonoBehaviour
    {
        public SpineAnimationHold spineAnimationHold4;

        private SkeletonAnimation _skeletonAnimation;
        private const int TrackIndex = 0;
        private float _timeScaleDefault = 1f;
        private float _currentTimeScale;
        private bool _checkSkillAnim;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _currentTimeScale = _timeScaleDefault;
        }
        
        public void ChangeSkin(string skinName)
        {
            _skeletonAnimation.Skeleton.SetSkin(skinName);
            _skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            _skeletonAnimation.AnimationState.Apply(_skeletonAnimation.Skeleton);
        }

        #region Idle, Run, Pull, Throw, Fall, Win, Die
        
        public void PlayIdle(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.idleAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.idleAnim, true);
        }
        
        public void PlayRun(float timeSpeedPercent = 1f)
        {
            var randomIndex = Random.Range(0, spineAnimationHold4.runAnims.Count);
            var runAnim = spineAnimationHold4.runAnims[randomIndex];
            PlayAnimation(TrackIndex, runAnim, true);
        }
        
        public void PlayPull(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.pullAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.pullAnim, true);
        }
        
        public void PlayThrow(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.throwAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.throwAnim, false);
        }
        
        public void PlayFall(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.fallAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.fallAnim, false);
        }
        
        public void PlayDie(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.dieAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.dieAnim, false);
        }
        
        public void PlayWin(float timeSpeedPercent = 1f)
        {
            var randomIndex = Random.Range(0, spineAnimationHold4.winAnims.Count);
            var animWin = spineAnimationHold4.winAnims[randomIndex];
            PlayAnimation(TrackIndex, animWin, true);
        }
        #endregion
        
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
