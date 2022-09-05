using Spine;
using Spine.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay._1_GreenRedLight.Characters
{
    public class CharacterSpine : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;
        public CharacterSpineAnimationHold spineAnimationHold;

        private float _timeScaleDefault = 1f;
        private const int TrackIndex = 0;

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

        #region Idle, Run, Shoot,Die, Win

        public void PlayIdle()
        {
            if (IsPlayingAnimation(spineAnimationHold.idleAnim))
            {
                return;
            }

            PlayAnimation(TrackIndex, spineAnimationHold.idleAnim, true);
        }

        public void PlayRun()
        {
            var randomIndex = Random.Range(0, spineAnimationHold.runAnims.Count);
            var runAnim = spineAnimationHold.runAnims[randomIndex];

            PlayAnimation(TrackIndex, runAnim, true);
        }

        public void PlayShoot()
        {
            if (IsPlayingAnimation(spineAnimationHold.shootAnim))
            {
                return;
            }

            PlayAnimation(TrackIndex, spineAnimationHold.shootAnim, false);
        }

        public TrackEntry PlayDie()
        {
            if (IsPlayingAnimation(spineAnimationHold.dieAnim))
            {
                return null;
            }

            return PlayAnimation(TrackIndex, spineAnimationHold.dieAnim, false);
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