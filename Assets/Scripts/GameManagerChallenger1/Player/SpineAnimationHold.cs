using System;
using System.Collections.Generic;
using Spine.Unity;

namespace GameManagerChallenger1.Player
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string runAnim;
        [SpineAnimation] public string dieAnim;
        [SpineAnimation] public string shootAnim;
        [SpineAnimation] public List<string> winAnims;
    }
}
