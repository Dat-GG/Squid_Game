using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger3.SpineAnimation
{
    public class MixAndMatchSkin : MonoBehaviour
    {
        [SpineSkin] public string baseSkin = "skin-base";
        [SpineSkin] public string gun = "gun";
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        private Skin _characterSkin;

        private void Start()
        {
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        private void UpdateCharacterSkin()
        {
            var skeleton = skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(baseSkin));
            _characterSkin.AddSkin(skeletonData.FindSkin(gun));
        }

        private void AddEquipmentSkinsTo(Skin combinedSkin)
        {
        }

        private void UpdateCombinedSkin()
        {
            var skeleton = skeletonAnimation.Skeleton;
            var resultCombinedSkin = new Skin("character-combined");
            resultCombinedSkin.AddSkin(_characterSkin);
            AddEquipmentSkinsTo(resultCombinedSkin);
            skeleton.SetSkin(resultCombinedSkin);
            skeleton.SetSlotsToSetupPose();
        }
    }
}