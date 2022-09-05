using System;
using System.Collections.Generic;
using Spine.Unity;

namespace Challenger4.Player
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string pullAnim;
        [SpineAnimation] public string throwAnim;
        [SpineAnimation] public string dieAnim;
        [SpineAnimation] public string fallAnim;
        [SpineAnimation] public List<string> winAnims;
        [SpineAnimation] public List<string> runAnims;
    }
}
