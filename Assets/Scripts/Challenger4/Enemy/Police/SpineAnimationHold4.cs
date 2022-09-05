using System;
using Spine.Unity;

namespace Challenger4.Enemy.Police
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string shootAnim;
    }
}
