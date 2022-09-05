using Challenger3.SpineAnimation;
using UnityEngine;

namespace Challenger3.Police
{
    public class PoliceController3 : MonoBehaviour
    {
        [SerializeField] private Spine3 policeSpine;
        internal Spine3 PoliceSpine => policeSpine;
    }
}