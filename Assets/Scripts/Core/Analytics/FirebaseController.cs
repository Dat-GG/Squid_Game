// using Core.Common.GameResources;
// using Core.Common.Tutorial;
// using Data.Config.Buff;
// using Firebase.Analytics;
// using UnityEngine;
//
// namespace Core.Common.Analytics
// {
//     public class FirebaseController : SingletonMono<FirebaseController>
//     {
//         private bool _isInitialized = false;
//         protected override void Awake()
//         {
//             base.Awake();
// #if !UNITY_EDITOR
//             Init();
// #endif
//         }
// #if !UNITY_EDITOR
//          private void Init()
//         {
//             Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
//                 var dependencyStatus = task.Result;
//                 if (dependencyStatus == Firebase.DependencyStatus.Available) {
//                     // Create and hold a reference to your FirebaseApp,
//                     // where app is a Firebase.FirebaseApp property of your application class.
//                     var app = Firebase.FirebaseApp.DefaultInstance;
//                     _isInitialized = true;
//                     SetAnalyticsCollectionEnabled();
//
//                     // Set a flag here to indicate whether Firebase is ready to use by your app.
//                     Debug.Log($"Firebase set! {app}");
//                 } else {
//                     Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
//                     // Firebase Unity SDK is not safe to use here.
//                 }
//             });
//         }
//
//         private void SetAnalyticsCollectionEnabled()
//         {
//             FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
//         }
// #endif
//
//         #region Trackings
//
//         #region Tutorial
//
//         public void TutorialBegin(TutorialStep tutorialStep, string stepValue)
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialBegin, tutorialStep.ToString(), stepValue);
//         }
//
//         public void TutorialComplete(TutorialStep tutorialStep, string stepValue)
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventTutorialComplete, tutorialStep.ToString(), stepValue);
//         }
//
//         #endregion
//
//         #region Unlock Build
//
//         public void UnlockBuild(BuildType buildType, int buildId)
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             FirebaseAnalytics.LogEvent("unlock_build", buildType.ToString(), buildId);
//         }
//
//         #endregion
//
//         #region Talent Upgrade
//
//         public void UpgradeTalent(BuffType buffType, int level)
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             FirebaseAnalytics.LogEvent("upgrade_talent", buffType.ToString(), level);
//         }
//
//         #endregion
//
//         #region Ultis
//
//         public void FailPayMarketing()
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             FirebaseAnalytics.LogEvent("fail_pay_marketing");
//         }
//
//         public void FailPaySalary()
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             FirebaseAnalytics.LogEvent("fail_pay_salary");
//         }
//
//         public void WatchAdRequest(string placementId)
//         {
//             if (!_isInitialized)
//             {
//                 return;
//             }
//
//             string platform = Application.platform.ToString();
//             FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, placementId, platform);
//         }
//
//         #endregion
//
//         #endregion
//     }
// }