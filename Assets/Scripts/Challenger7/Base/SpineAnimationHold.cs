using System;
using System.Collections.Generic;
using Spine.Unity;

namespace Challenger7.Base
{
	[Serializable]
	public struct SpineAnimationHold
	{
		[SpineSkin] public List<string> noUseSkin;

		[SpineAnimation] public string idleAnim;
		[SpineAnimation] public string runAnim;
		[SpineAnimation] public string winAnim;
		[SpineAnimation] public string dieAnim;
		[SpineAnimation] public string attackAnim;
	}
}