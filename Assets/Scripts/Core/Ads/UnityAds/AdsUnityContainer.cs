// using System;
// using UnityEngine;
// using UnityEngine.Advertisements;
//
// namespace Core.Common.Ads.UnityAds
// {
//     public class AdsUnityContainer : MonoBehaviour, IUnityAdsListener
//     {
// #if UNITY_IOS
//     private string gameId = "4008146";
// #elif UNITY_ANDROID
//         private string gameId = "4008147";
// #endif
//
//         public bool internetConnected;
//
//         private bool _testMode = false;
//         private bool _isAdsSet;
//         private Action<ShowAdResult> _resultCallback;
//
//         private void Start()
//         {
//             CheckInternetConnection();
//             if (internetConnected)
//             {
//                 Advertisement.AddListener(this);
//                 Advertisement.Initialize(gameId, _testMode);
//                 _isAdsSet = true;
//             }
//             else
//             {
//                 _isAdsSet = false;
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
//         public bool GetInternetConnection()
//         {
//             CheckInternetConnection();
//             if (_isAdsSet == false)
//             {
//                 if (internetConnected)
//                 {
//                     Advertisement.AddListener(this);
//                     Advertisement.Initialize(gameId, _testMode);
//                     _isAdsSet = true;
//                 }
//             }
//
//             return internetConnected;
//         }
//
//         public bool IsHaveAds(string placementId)
//         {
//             if (!_isAdsSet)
//             {
//                 GetInternetConnection();
//             }
//
//             if (Advertisement.IsReady(placementId))
//             {
//                 return true;
//             }
//
//             Advertisement.Load(placementId);
//             return false;
//         }
//
//         public void PlayAds(string placementId, Action<ShowAdResult> resultCallBack)
//         {
//             _resultCallback = resultCallBack;
//             Advertisement.Show(placementId);
//         }
//
//         public void OnUnityAdsReady(string placementId)
//         {
//             if (placementId == AdsPlacementIds.adsX2GoldReceive || placementId == AdsPlacementIds.adsX2GoldReceive2)
//             {
//                 EventDispatcher.Instance.PostEvent(EventID.HaveAdsForX2Gold);
//             }
//         }
//
//         public void OnUnityAdsDidError(string message)
//         {
//             UtilityGame.ResumeGame();
//         }
//
//         public void OnUnityAdsDidStart(string placementId)
//         {
//
//         }
//
//         public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
//         {
//             var result = ShowAdResult.Finished;
//             switch (showResult)
//             {
//                 case ShowResult.Failed:
//                     result = ShowAdResult.Failed;
//                     break;
//                 case ShowResult.Skipped:
//                     result = ShowAdResult.Skipped;
//                     break;
//                 case ShowResult.Finished:
//                     result = ShowAdResult.Finished;
//                     break;
//             }
//
//             _resultCallback?.Invoke(result);
//             _resultCallback = null;
//             UtilityGame.ResumeGame();
//         }
//     }
// }