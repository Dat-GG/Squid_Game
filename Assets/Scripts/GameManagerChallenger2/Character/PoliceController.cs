
namespace GameManagerChallenger2.Character
{
	public class PoliceController : CharacterController
	{
		private void Start()
		{
			CharacterSpine.PlayIdle();
		}

		internal void Attack()
		{
			CharacterSpine.PlayAttack();
		}
	}
}