using System;
using System.Collections.Generic;
using Spine.Unity;

namespace GamePlay._1_GreenRedLight.Characters
{
    [Serializable]
    public struct CharacterSpineAnimationHold
    {
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string dieAnim;
        [SpineAnimation] public string shootAnim;

        [SpineAnimation] public List<string> runAnims;
        [SpineAnimation] public List<string> winAnims;
    }
}