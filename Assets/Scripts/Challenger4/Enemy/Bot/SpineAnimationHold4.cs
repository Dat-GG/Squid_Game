using System;
using System.Collections.Generic;
using Spine.Unity;

namespace Challenger4.Enemy.Bot
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineSkin] public List<string> notUseSkins;
        
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string pullAnim;
        [SpineAnimation] public string dieAnim;
        [SpineAnimation] public string fallAnim;
        [SpineAnimation] public List<string> winAnims;
        [SpineAnimation] public List<string> runAnims;
    }
}
