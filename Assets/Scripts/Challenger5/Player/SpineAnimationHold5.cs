using System;
using System.Collections.Generic;
using Spine.Unity;

namespace Challenger5.Player
{
    [Serializable]
    public struct SpineAnimationHold
    {
        //Normal anim
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string runAnim;
        [SpineAnimation] public string attackAnim;
        [SpineAnimation] public string dieAnim;
        [SpineAnimation] public List<string> winAnims;
        //Special Anim
        [SpineAnimation] public string tumbleAnim;
        [SpineAnimation] public string katanaAnim;
        [SpineAnimation] public string giantAnim;
        [SpineAnimation] public string hammerAnim;
    }
}
