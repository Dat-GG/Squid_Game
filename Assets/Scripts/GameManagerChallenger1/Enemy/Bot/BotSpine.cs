using Spine;
using Spine.Unity;
using UnityEngine;

namespace GameManagerChallenger1.Enemy.Bot
{
    public class BotSpine : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;
        public SpineAnimationHold spineAnimationHold;

        private const int TrackIndex = 0;
        private float _timeScaleDefault = 1f;
        private float _currentTimeScale;
        private bool _checkSkillAnim;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _currentTimeScale = _timeScaleDefault;
        }

        #region Idle, Run, Die, Win

        public void PlayIdle(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.idleAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.idleAnim, true);
        }
        
        public void PlayRun()
        {
            var randomIndex = Random.Range(0, spineAnimationHold.runAnims.Count);
            var animRun = spineAnimationHold.runAnims[randomIndex];
            PlayAnimation(TrackIndex, animRun, true);
        }
        
        public void PlayDie(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.dieAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.dieAnim, false);
        }
        
        public void PlayWin()
        {
            var randomIndex = Random.Range(0, spineAnimationHold.winAnims.Count);
            var animWin = spineAnimationHold.winAnims[randomIndex];
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

        public bool IsCanNotUseSkin(string skinName)
        {
            return spineAnimationHold.notUseSkins.Contains(skinName);
        }

        public void PauseAnim()
        {
            _skeletonAnimation.AnimationState.TimeScale = 0f;
        }

        public void ResumeAnim()
        {
            _skeletonAnimation.AnimationState.TimeScale = _timeScaleDefault;
        }

        #endregion
    }
}
