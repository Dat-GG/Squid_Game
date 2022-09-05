using System;
using System.Collections.Generic;
using Spine.Unity;

namespace GameManagerChallenger1.Enemy.Bot
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineSkin] public List<string> notUseSkins;
        
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string dieAnim;
        [SpineAnimation] public List<string> runAnims;
        [SpineAnimation] public List<string> winAnims;
    }
}
