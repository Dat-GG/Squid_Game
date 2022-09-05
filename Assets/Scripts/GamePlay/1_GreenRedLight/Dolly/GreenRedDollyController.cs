using Spine;
using UnityEngine;

namespace GamePlay._1_GreenRedLight.Dolly
{
    public class GreenRedDollyController : MonoBehaviour
    {
        private GreenRedDollySpine _spine;

        private void Start()
        {
            _spine = GetComponentInChildren<GreenRedDollySpine>();
            this.RegisterListener(EventID.GreenRedStopMove, (sender, param) =>
            {
                PlayRotate();
            });
        }

        private void PlayIdle()
        {
            _spine.PlayIdle();
        }
        
        private void PlayRotate()
        {
            var trackEntry = _spine.PlayRotate();
            if (trackEntry is null)
                return;

            trackEntry.Complete += OnRotateComplete;
        }

        private void OnRotateComplete(TrackEntry trackEntry)
        {
            PlayIdle();
            this.PostEvent(EventID.GreenRedDollyFinishRotate);
        }
    }
}