using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger5.Player
{
    public class PlayerSpine5 : MonoBehaviour
    {
        public SpineAnimationHold spineAnimationHold5;

        private SkeletonAnimation _skeletonAnimation;
        private MixAndMatchSkinPlayer5 _mixAndMatchSkinPlayer5;
        private const int TrackIndex = 0;
        private float _timeScaleDefault = 1f;
        private float _currentTimeScale;
        private bool _checkSkillAnim;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _mixAndMatchSkinPlayer5 = GetComponent<MixAndMatchSkinPlayer5>();
            _currentTimeScale = _timeScaleDefault;
        }
        
        public void ChangeSkin(string skinName)
        {
            _skeletonAnimation.Skeleton.SetSkin(skinName);
            _skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            _skeletonAnimation.AnimationState.Apply(_skeletonAnimation.Skeleton);
        }

        #region Idle, Run, Attack, Die, Win
        public void PlayIdle(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.idleAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.idleAnim, true);
        }
        
        public void PlayRun(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.runAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.runAnim, true);
        }
        
        public void PlayAttack(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.attackAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.attackAnim, false);
        }

        public void PlayDie(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.dieAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.dieAnim, false);
        }
        
        public void PlayWin(float timeSpeedPercent = 1f)
        {
            var randomIndex = Random.Range(0, spineAnimationHold5.winAnims.Count);
            var animWin = spineAnimationHold5.winAnims[randomIndex];
            PlayAnimation(TrackIndex, animWin, true);
        }
        #endregion

        #region Katana, Giant, Hammer, Tumble

        public void PlayTumble(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.tumbleAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.tumbleAnim, false);
        }
        
        public void PlayKatana(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.katanaAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.katanaAnim, false);
        }
        
        public void PlayGiant(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.giantAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            // _mixAndMatchSkinPlayer5.UpdateGiantSkin();
            // _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
            // var trackEntry = PlayAnimation(TrackIndex, spineAnimationHold5.giantAnim, false);
            // trackEntry.End += OnPlayGiantEnd;
            PlayAnimation(TrackIndex, spineAnimationHold5.giantAnim, false);
        }

        private void OnPlayGiantEnd(TrackEntry trackentry)
        {
            _mixAndMatchSkinPlayer5.UpdateCharacterSkin();
            _mixAndMatchSkinPlayer5.UpdateCombinedSkin();
        }

        public void PlayHammer(float timeSpeedPercent = 1f)
        {
            if (IsPlayingAnimation(spineAnimationHold5.hammerAnim))
            {
                return;
            }

            SetTimeSpeed(timeSpeedPercent);
            PlayAnimation(TrackIndex, spineAnimationHold5.hammerAnim, false);
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
