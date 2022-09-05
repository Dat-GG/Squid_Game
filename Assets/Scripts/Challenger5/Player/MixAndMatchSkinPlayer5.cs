using DataAccount;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger5.Player
{
    public class MixAndMatchSkinPlayer5 : MonoBehaviour
    {
        [SpineSkin] public string baseSkin = "skin-base";
        [SpineSkin] public string knife = "knife";
        [SpineSkin] public string hammer = "hammer_2";
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

        public void UpdateCharacterSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString()));
            _characterSkin.AddSkin(skeletonData.FindSkin(knife));
        }
        
        public void UpdateHammerSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString()));
            _characterSkin.AddSkin(skeletonData.FindSkin(hammer));
        }
        
        public void UpdateBaseSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString()));
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
    }
}
