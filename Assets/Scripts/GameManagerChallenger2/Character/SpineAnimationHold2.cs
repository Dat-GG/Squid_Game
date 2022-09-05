using System;
using System.Collections.Generic;
using Spine.Unity;

namespace GameManagerChallenger2.Character
{
	[Serializable]
	public struct SpineAnimationHold
	{
		[SpineSkin] public List<String> noUseSkin;

		[SpineAnimation] public string idleAnim;
		[SpineAnimation] public List<string> runAnim;
		[SpineAnimation] public string jumpAnim;
		[SpineAnimation] public string dieAnim;
		[SpineAnimation] public string attackAnim;
	}
}