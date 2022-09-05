using Core.Pool;
using UnityEngine;

namespace Challenger7.Item
{
	public class ItemController : MonoBehaviour
	{
		[SerializeField] private ItemType itemType;
		internal ItemType ItemType => itemType;
		[SerializeField] private float travelLength;

		private void Update()
		{
			var tranPos = transform.position;
			tranPos.y -= travelLength;
			transform.position = tranPos;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.gameObject.CompareTag("GroundBreak")) return;
			SmartPool.Instance.Despawn(gameObject);
		}
	}
}