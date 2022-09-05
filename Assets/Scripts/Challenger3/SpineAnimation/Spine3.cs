using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger3.SpineAnimation
{
    public class Spine3 : MonoBehaviour
    {
        [SerializeField] private GameObject bloodEffect;
        [SerializeField] private List<GameObject> soulEffects;
        
        public SpineAnimationHold spineAnimationHold;

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
        
        public void PlayIdle(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.idleAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.idleAnim, true);
        }

        public void PlayThrowStart(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.throwAnimStart))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.throwAnimStart, true);
        }
        
        public void PlayThrowEnd(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.throwAnimEnd))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.throwAnimEnd, false);
        }
        
        public void PlayShoot(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.shootAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.shootAnim, false);
        }
        
        public void PlayDie(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold.dieAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold.dieAnim, false);
            bloodEffect.SetActive(true);

            var index = Random.Range(0, soulEffects.Count);
            soulEffects[index].SetActive(true);
        }
        
        public void PlayWin(float timeSpeedPercent = 1f)
        {
            var randomIndex = Random.Range(0, spineAnimationHold.winAnims.Count);
            var animWin = spineAnimationHold.winAnims[randomIndex];
            PlayAnimation(TrackIndex, animWin, true);
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
        
        public bool IsCanNotUseSkin(string skinName)
        {
            return spineAnimationHold.notUseSkins.Contains(skinName);
        }
        #endregion
    }
}