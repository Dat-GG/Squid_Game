using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GameManagerChallenger2.Gameplay
{
	public class DroneController : MonoBehaviour
	{
		[SerializeField] private float droneTime;
		private int _droneFlyStep = 0;
		private int _posHorizontal; // to keep the drone on the right track

		private void Start()
		{
			_posHorizontal = transform.position.x > 0 ? 1 : 0;

			StartCoroutine(DroneCheck());
		}

		private IEnumerator DroneCheck()
		{
			var glassPiecesList = GameManagerChallenger2.Instance.ListGlassPieces.GlassPiecesList.glassPiecesList;
			while (_droneFlyStep < 6)
			{
				yield return new WaitForSeconds(droneTime);
				transform.DOLocalMove(
					glassPiecesList[_droneFlyStep].glassPieces[_posHorizontal].transform.position
					+ new Vector3(0 , 0.5f, 0), 0.5f);

				if (glassPiecesList[_droneFlyStep].glassPieces[_posHorizontal].IsBroken)
				{
					glassPiecesList[_droneFlyStep].glassPieces[_posHorizontal].gameObject.SetActive(false);
				} // disable the broken piece of glass
				
				transform.DOScale(transform.localScale * 0.85f, 0.5f);
				_droneFlyStep++;
			}

			yield return new WaitForSeconds(droneTime);
			gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			StopCoroutine(DroneCheck());
		}
	}
}