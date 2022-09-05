using Spine;
using Spine.Unity;
using UnityEngine;

namespace Challenger5.Bot
{
    public class MixAndMatchSkinBot5 : MonoBehaviour
    {
        [SpineSkin] public string knife = "knife";
        [SpineSkin] public string hammer = "hammer_2";
        private SkeletonAnimation _skeletonAnimation;
        private Skin _characterSkin;
        private BotController5 _botController5;
        private void Awake ()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _botController5 = GetComponentInParent<BotController5>();
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
            _characterSkin.AddSkin(skeletonData.FindSkin(_botController5.finalSkin.Name));
            _characterSkin.AddSkin(skeletonData.FindSkin(knife));
        }
        
        public void UpdateHammerSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(_botController5.finalSkin.Name));
            _characterSkin.AddSkin(skeletonData.FindSkin(hammer));
        }
        
        public void UpdateBaseSkin()
        {
            var skeleton = _skeletonAnimation.Skeleton;
            var skeletonData = skeleton.Data;
            _characterSkin = new Skin("character-base");
            _characterSkin.AddSkin(skeletonData.FindSkin(_botController5.finalSkin.Name));
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
