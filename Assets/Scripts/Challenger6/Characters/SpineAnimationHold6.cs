using System;
using Spine.Unity;

namespace Challenger6.Characters
{
	[Serializable]
	public struct SpineAnimationHold
	{
		[SpineAnimation] public string idleAnim;
		[SpineAnimation] public string winAnim;
		[SpineAnimation] public string dieAnim;
		[SpineAnimation] public string attackAnim;
	}
}