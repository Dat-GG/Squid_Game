using System;
using Spine.Unity;
using UnityEngine;

namespace GameManagerChallenger1.Enemy.Police
{
    [Serializable]
    public struct SpineAnimationHold
    {
        [SpineAnimation] public string idleAnim;
        [SpineAnimation] public string shootAnim;
        [SpineAnimation] public string dieAnim;
    }
}
