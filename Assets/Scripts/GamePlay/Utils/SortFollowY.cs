using UnityEngine;
using UnityEngine.Rendering;

namespace GamePlay.Utils
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortFollowY : MonoBehaviour
    {
        private SortingGroup _sortingGroup;
        private Transform _myTransform;

        private void Awake()
        {
            _sortingGroup = GetComponent<SortingGroup>();
            _myTransform = transform;
        }

        private void LateUpdate()
        {
            _sortingGroup.sortingOrder = -(int) _myTransform.position.y;
        }
    }
}