using System;
using Challenger7.Player;
using Challenger7.UI;
using UnityEngine;

namespace Challenger7.Gameplay
{
	public class PowerUpController : MonoBehaviour
	{
		[SerializeField] private ModuleUiPowerUpBtn7 powerUp7;
		private PlayerController _player;

		private void Start()
		{
			_player = GameManagerChallenger7.Instance.Player;
		}

		private void UsingShield7()
		{
			
		}
		
		private void UsingMagnet()
		{
			
		}
		private void UsingPush()
		{
			
		}
		
		private void UsingAllGoldItem()
		{
			
		}
		
		private void UsingFreeze7()
		{
			
		}
		
		private void UsingPassThrough()
		{
			
		}
	}
}