using System.Collections.Generic;
using DataAccount;
using Plugins.Scripts.Core.Common.GameResources;

namespace Plugins.Scripts.DataAccount
{
    public class PlayerSkins
    {
        public SkinType skinUsing;
        public int randomSkinNumber;
        public List<SkinType> skinOwned;

        // key: skin name; value: number of ad watch.
        public Dictionary<SkinType, int> skinAdsWatched;

        public PlayerSkins()
        {
            skinOwned = new List<SkinType>()
            {
                SkinType.skin_goku,
            };
            
            skinUsing = skinOwned[0];
            skinAdsWatched = new Dictionary<SkinType, int>();
        }
        
        public SkinType SkinJustGet { get; set; } = SkinType.none; 

        public bool IsHaveOwnedSkin(SkinType skinName)
        {
            return skinOwned.Contains(skinName);
        }

        public int GetAdsWatchedNumber(SkinType skinName)
        {
            if (skinAdsWatched.ContainsKey(skinName))
            {
                return skinAdsWatched[skinName];
            }

            return 0;
        }

        public bool OnChangeSkin(SkinType skinName)
        {
            if (IsHaveOwnedSkin(skinName))
            {
                skinUsing = skinName;
                DataAccountPlayer.SavePlayerSkins();
                return true;
            }

            return false;
        }

        public void OnWatchAds(SkinType skinName)
        {
            if (skinAdsWatched.ContainsKey(skinName))
            {
                skinAdsWatched[skinName] += 1;
            }
            else
            {
                skinAdsWatched.Add(skinName, 1);
            }

            DataAccountPlayer.SavePlayerSkins();
        }

        public void AddOwnedSkin(SkinType skinName)
        {
            if (!skinOwned.Contains(skinName))
            {
                skinOwned.Add(skinName);
                DataAccountPlayer.SavePlayerSkins();
            }
        }
    }
}
