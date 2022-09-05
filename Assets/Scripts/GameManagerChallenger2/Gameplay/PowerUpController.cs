using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameManagerChallenger2.GlassPiece;
using GameManagerChallenger2.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameManagerChallenger2.Gameplay
{
	public class PowerUpController : MonoBehaviour
	{
		[SerializeField] private ModuleUiPowerUpBtn2 powerUp2;

		[SerializeField] private Color brokenGlass;
		private List<GlassPieceController> glassPieces = new List<GlassPieceController>();

		[SerializeField] private GameObject dronePref;
		[SerializeField] private Transform droneSpawnPosRight, droneSpawnPosLeft;
		private Transform _spawnPos;
		
		private void Awake()
		{
			glassPieces = new List<GlassPieceController>(FindObjectsOfType<GlassPieceController>());
		}

		private void Start()
		{
			StartCoroutine(ScanPowerUp());
			StartCoroutine(DronePowerUp());
			StartCoroutine(ReinforcePowerUp());
		}

		private IEnumerator ScanPowerUp()
		{
			while (!powerUp2.HasScan)
				yield return null;
			
			foreach (var piece in glassPieces.Where(piece => piece.IsBroken))
				piece.Sprite.color = brokenGlass;
		}
		
		private IEnumerator DronePowerUp()
		{
			while (!powerUp2.HasDrone)
				yield return null;
			
			var index = Random.Range(0, 2);
			_spawnPos = index <= 0 ? droneSpawnPosLeft : droneSpawnPosRight;
			Instantiate(dronePref, _spawnPos.position, Quaternion.identity);
		}
		
		private IEnumerator ReinforcePowerUp()
		{
			while (!powerUp2.HasReinforce)
				yield return null;
			StartCoroutine(GameManagerChallenger2.Instance.ListGlassPieces.ReinforceGlasses());
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}
	}
}