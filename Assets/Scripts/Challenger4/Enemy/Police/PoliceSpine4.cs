using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger4.Enemy.Police
{
    public class PoliceSpine4 : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;
        public SpineAnimationHold spineAnimationHold4;

        private const int TrackIndex = 0;
        private float _timeScaleDefault = 1f;
        private float _currentTimeScale;
        private bool _checkSkillAnim;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _currentTimeScale = _timeScaleDefault;
        }

        #region Idle, Shoot
        public void PlayIdle(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.idleAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.idleAnim, true);
        }
        
        public void PlayShoot(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold4.shootAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold4.shootAnim, true);
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
