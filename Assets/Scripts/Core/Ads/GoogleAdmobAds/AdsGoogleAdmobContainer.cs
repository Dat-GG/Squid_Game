// using System;
// using System.Collections.Generic;
// using GoogleMobileAds.Api;
// using UnityEngine;
//
// namespace Core.Common.Ads.GoogleAdmobAds
// {
//     public class AdsGoogleAdmobContainer : MonoBehaviour
//     {
// #if UNITY_ANDROID
//         private const string TestAdPlacement = "ca-app-pub-3940256099942544/5224354917";
// #endif
// #if UNITY_IOS
//                 private const string TestAdPlacement = "ca-app-pub-3940256099942544/1712485313";
// #endif
//
//         public bool internetConnected;
//         private bool _isAdsSet;
//         private Action<ShowAdResult> _resultCallback;
//         private ShowAdResult _currentShowAdResult;
//
//         private Dictionary<string, RewardedAd> _allRewardedAds = new Dictionary<string, RewardedAd>();
//
//         private void Start()
//         {
//             CheckInternetConnection();
//             if (internetConnected)
//             {
//                 InitData();
//                 _isAdsSet = true;
//             }
//             else
//             {
//                 _isAdsSet = false;
//             }
//         }
//
//         public bool IsHaveAds(string placementId)
//         {
//             if (!_isAdsSet)
//             {
//                 GetInternetConnection();
//             }
//
//             if (IsRewardedAdReady(placementId))
//             {
//                 return true;
//             }
//
//             LoadRewardedAd(placementId);
//             return false;
//         }
//
//         public void PlayAds(string placementId, Action<ShowAdResult> resultCallBack)
//         {
//             _resultCallback = resultCallBack;
//             ShowRewardedAd(placementId);
//         }
//
//         private void InitData()
//         {
//             MobileAds.Initialize(initStatus => {});
//         }
//
//         private void ShowRewardedAd(string placementId)
//         {
//             if (_allRewardedAds.ContainsKey(placementId))
//             {
//                 var rewardedAd = _allRewardedAds[placementId];
//                 if (rewardedAd.IsLoaded())
//                 {
//                     rewardedAd.Show();
//                 }
//             }
//         }
//
//         private bool IsRewardedAdReady(string placementId)
//         {
//             if (_allRewardedAds.ContainsKey(placementId))
//             {
//                 var rewardedAd = _allRewardedAds[placementId];
//                 return rewardedAd.IsLoaded();
//             }
//
//             return false;
//         }
//
//         private void LoadRewardedAd(string placementId)
//         {
//             if (_allRewardedAds.ContainsKey(placementId))
//             {
//                 var rewardedAd = _allRewardedAds[placementId];
//                 if (rewardedAd.IsLoaded())
//                 {
//                     return;
//                 }
//
//                 var newRewardedAd = CreateNewRewardedAd(placementId);
//                 _allRewardedAds.Add(placementId, newRewardedAd);
//             }
//             else
//             {
//                 var newRewardedAd = CreateNewRewardedAd(placementId);
//                 _allRewardedAds.Add(placementId, newRewardedAd);
//             }
//         }
//
//         private RewardedAd CreateNewRewardedAd(string placementId)
//         {
//             var rewardedAd = new RewardedAd(placementId);
//             // Called when an ad request has successfully loaded.
//             rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//             // Called when an ad request failed to load.
//             rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//             // Called when an ad is shown.
//             rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//             // Called when an ad request failed to show.
//             rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//             // Called when the user should be rewarded for interacting with the ad.
//             rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//             // Called when the ad is closed.
//             rewardedAd.OnAdClosed += HandleRewardedAdClosed;
//
//             // Create an empty ad request.
//             AdRequest request = new AdRequest.Builder().Build();
//             // Load the rewarded ad with the request.
//             rewardedAd.LoadAd(request);
//             return rewardedAd;
//         }
//
//         private void GetInternetConnection()
//         {
//             CheckInternetConnection();
//             if (_isAdsSet == false)
//             {
//                 if (internetConnected)
//                 {
//                     InitData();
//                     _isAdsSet = true;
//                 }
//             }
//         }
//
//         private void CheckInternetConnection()
//         {
//             if (Application.internetReachability == NetworkReachability.NotReachable)
//             {
//                 internetConnected = false;
//             }
//             else
//             {
//                 internetConnected = true;
//             }
//         }
//
//         #region AdHandlers
//
//         private void HandleRewardedAdLoaded(object sender, EventArgs args)
//         {
//             Debug.Log("HandleRewardedAdLoaded event received");
//         }
//
//         private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//         {
//             Debug.Log(
//                 "HandleRewardedAdFailedToLoad event received with message: "
//                 + args.LoadAdError);
//         }
//
//         private void HandleRewardedAdOpening(object sender, EventArgs args)
//         {
//             Debug.Log("HandleRewardedAdOpening event received");
//         }
//
//         private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//         {
//             _currentShowAdResult = ShowAdResult.Failed;
//             UtilityGame.ResumeGame();
//         }
//
//         private void HandleRewardedAdClosed(object sender, EventArgs args)
//         {
//             if (_currentShowAdResult != ShowAdResult.Finished)
//             {
//                 _currentShowAdResult = ShowAdResult.Failed;
//             }
//
//             _resultCallback?.Invoke(_currentShowAdResult);
//             _resultCallback = null;
//             UtilityGame.ResumeGame();
//         }
//
//         private void HandleUserEarnedReward(object sender, Reward args)
//         {
//             _currentShowAdResult = ShowAdResult.Finished;
//         }
//
//         #endregion
//     }
// }