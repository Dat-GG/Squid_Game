// using System;
// using Core.Common.Ads.GoogleAdmobAds;
// using Core.Common.Ads.UnityAds;
// using Core.Common.Analytics;
// using UnityEngine;
//
// namespace Core.Common.Ads
// {
//     public class AdsManager : SingletonMonoDontDestroy<AdsManager>
//     {
//         [SerializeField] private AdsUnityContainer adsUnityContainer;
//         [SerializeField] private AdsGoogleAdmobContainer adsGoogleAdmobContainer;
//
//         public AdsManager(string className) : base(className)
//         {
//         }
//
//         public bool IsHaveAds(string placementId)
//         {
//             // if (adsUnityContainer.IsHaveAds(placementId))
//             // {
//             //     return true;
//             // }
//
//             if (adsGoogleAdmobContainer.IsHaveAds(placementId))
//             {
//                 return true;
//             }
//
//             // if have more ads api --> check other.
//             return false;
//         }
//
//         public void PlayAds(string placementId, Action<ShowAdResult> resultAction)
//         {
//             UtilityGame.PauseGame();
//             // adsUnityContainer.PlayAds(placementId, resultAction);
//             adsGoogleAdmobContainer.PlayAds(placementId, resultAction);
//             FirebaseController.Instance.WatchAdRequest(placementId);
//         }
//
//         public void PlayInterstitialAds()
//         {
//             string placementId = string.Empty;
//             if (IsHaveAds(AdsPlacementIds.adsInterstitial1))
//             {
//                 placementId = AdsPlacementIds.adsInterstitial1;
//             }
//
//             if (placementId == string.Empty)
//             {
//                 if (IsHaveAds(AdsPlacementIds.adsInterstitial2))
//                 {
//                     placementId = AdsPlacementIds.adsInterstitial2;
//                 }
//             }
//
//             if (placementId != string.Empty)
//             {
//                 PlayAds(placementId, null);
//                 FirebaseController.Instance.WatchAdRequest(placementId);
//             }
//         }
//     }
// }