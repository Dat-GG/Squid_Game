using System.Collections.Generic;
using Plugins.Scripts.Core.Common.GameResources;
using UnityEngine;

namespace Plugins.Scripts.UI.ShopSkin.Data
{
    [CreateAssetMenu(menuName = "DATA/SkinData", fileName = "SkinData")]
    public class SkinDataCollection : ScriptableObject
    {
        //public int baseCoinSkin;
        public List<SkinData> coinSkins = new List<SkinData>();

        public List<SkinData> GetSkinCollection(SkinType skinType, out SkinUnlockType skinUnlockType)
        {
            foreach (var skin in coinSkins)
            {
                if (skin.skinName == skinType)
                {
                    skinUnlockType = SkinUnlockType.Gold;
                    return coinSkins;
                }
            }
            
            skinUnlockType = SkinUnlockType.Gold;
            return coinSkins;
        }
    }
}
