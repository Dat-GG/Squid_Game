// using System;
// using System.Collections;
// using UnityEditor;
// // using Com.LuisPedroFonseca.ProCamera2D;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace UI.LoadingScene
// {
//     public class LoadingScene : MonoBehaviour
//     {
//         [SerializeField] private Text loadingTxt;
//         
//         private static SceneName _sceneName;
//         private bool CanGoOutOfScene { get; set; }
//         private bool Transitioned { get; set; }
//         
//         public static void SetSceneName(SceneName sceneName)
//         {
//             _sceneName = sceneName;
//         }
//         
//         private void Start()
//         {
//             CanGoOutOfScene = false;
//             Transitioned = false;
//             loadingTxt.gameObject.SetActive(true);
//
//             // ProCamera2DTransitionsFX.Instance.TransitionEnter();
//             // ProCamera2DTransitionsFX.Instance.OnTransitionExitEnded = () =>
//             // {
//             //     CanGoOutOfScene = true;
//             // };
//             
//             Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
//             Resources.UnloadUnusedAssets();
//             GC.Collect();
//             this.StartCoroutine(LoadScene());
//         }
//
//         private IEnumerator LoadScene()
//         {
//             var async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName.ToString());
//             async.allowSceneActivation = false;
//
//             while (!async.isDone)
//             {
//                 if (async.progress >= 0.9f && !Transitioned)
//                 {
//                     Transitioned = true;
//                     loadingTxt.gameObject.SetActive(false);
//                     // ProCamera2DTransitionsFX.Instance.TransitionExit();
//                 }
//
//                 if (CanGoOutOfScene)
//                 {
//                     async.allowSceneActivation = true;
//                 }
//
//                 yield return null;
//             }
//         }
//     }
// }