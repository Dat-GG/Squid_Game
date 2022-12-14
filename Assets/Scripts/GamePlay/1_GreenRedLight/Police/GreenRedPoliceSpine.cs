using System;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;
namespace GamePlay._1_GreenRedLight.Police
{
    public class GreenRedPoliceSpine : MonoBehaviour
    {
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string shootAnim;

        private SkeletonAnimation _skeletonAnimation;
        private const int TrackIndex = 0;

        public event Action<TrackEntry, Event> AnimationEvent;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
        }

        private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
        {
            AnimationEvent?.Invoke(trackEntry, e);
        }

        public void PlayIdle()
        {
            if (IsPlayingAnimation(idleAnim))
                return;

            PlayAnimation(TrackIndex, idleAnim, true);
        }

        public TrackEntry PlayShoot()
        {
            _skeletonAnimation.AnimationState.ClearTracks();
            return PlayAnimation(TrackIndex, shootAnim, false);
        }

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
    }
}