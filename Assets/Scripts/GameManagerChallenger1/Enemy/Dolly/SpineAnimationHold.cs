using System;
using Spine.Unity;

namespace GameManagerChallenger1.Enemy.Dolly
{
        [Serializable]
        public struct SpineAnimationHold
        {
                [SpineAnimation] public string idleAnim;
                [SpineAnimation] public string singAnim;
                [SpineAnimation] public string turnBackAnim;
        }
}
