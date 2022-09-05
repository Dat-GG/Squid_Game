using DataAccount;
using Spine;
using Spine.Unity;
using UnityEngine;

public class MixAndMatchSkinPlayer : MonoBehaviour
{
    [SpineSkin] public string baseSkin = "skin-base";
    [SpineSkin] public string gun = "gun";
    private SkeletonAnimation _skeletonAnimation;
    private Skin _characterSkin;
    private void Awake ()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void UpdateCharacterSkin()
    {
        var skeleton = _skeletonAnimation.Skeleton;
        var skeletonData = skeleton.Data;
        _characterSkin = new Skin("character-base");
        _characterSkin.AddSkin(skeletonData.FindSkin(DataAccountPlayer.PlayerSkins.skinUsing.ToString()));
        _characterSkin.AddSkin(skeletonData.FindSkin(gun));
    }
    
    public void ReturnNormalCharacterSkin()
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
