using Spine;
using Spine.Unity;
using UnityEngine;

namespace GameManagerChallenger1.Enemy.Police
{
    public class MixAndMatchSkin : MonoBehaviour
    {
        [SpineSkin] public string baseSkin = "skin-base";
        [SpineSkin] public string gun = "gun";
        private SkeletonAnimation _skeletonAnimation;
        private Skin _characterSkin;
        private void Awake ()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }
        private void Start()
        {
            UpdateCharacterSkin();
            UpdateCombinedSkin();
        }

        private void UpdateCharacterSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(baseSkin));
            _characterSkin.AddSkin(skeletonData.FindSkin(gun));
        }
        
        private void AddEquipmentSkinsTo(Skin combinedSkin)
        {
        }

        public void UpdateCombinedSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var resultCombinedSkin = new Skin("character-combined");
            resultCombinedSkin.AddSkin(_characterSkin);
            AddEquipmentSkinsTo(resultCombinedSkin);
            skeleton.SetSkin(resultCombinedSkin);
            skeleton.SetSlotsToSetupPose();
        }

        public void UpdateBaseSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(baseSkin));
        }
    }
}
