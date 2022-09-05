using System;
using Plugins.Scripts.Core.Common.GameResources;
using Sirenix.OdinInspector;

namespace Plugins.Scripts.UI.ShopSkin.Data
{
    [Serializable]
    public class SkinData
    {
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        public SkinType skinName;

        public int numberOfAds;
        
        public int baseCoinSkin;

        public bool inGoldReward;
        
        public bool onlyAdsReward;
    }
}
