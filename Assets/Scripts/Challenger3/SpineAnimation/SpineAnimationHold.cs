using System;
using System.Collections.Generic;
using Spine.Unity;

namespace Challenger3.SpineAnimation
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineSkin] public List<string> notUseSkins;
        
        [Spine.Unity.SpineAnimation] public string idleAnim;
        
        [Spine.Unity.SpineAnimation] public string throwAnimStart;
        [Spine.Unity.SpineAnimation] public string throwAnimEnd;
        
        [Spine.Unity.SpineAnimation] public string shootAnim;
        [Spine.Unity.SpineAnimation] public string dieAnim;
        [Spine.Unity.SpineAnimation] public List<string> winAnims;
    }
}