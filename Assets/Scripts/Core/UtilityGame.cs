using System;
using System.Collections.Generic;
using Core.Common.Popup;
using Core.Common.Sound;

using DG.Tweening;
using Plugins.Scripts.Core.Common.Sound;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Common
{
    public class UtilityGame : MonoBehaviour
    {
        private static readonly Vector3[] Corners = new Vector3[4];

        #region Camera Action

        private static Camera _mainCamera;
        private static float _cameraMoveDuration = 0.5f;
        private static float _sizeBeforeFocusView = 9f;

        private static float _sizeWhenFocusView = 7f;
        private static Vector3 _reuseVector3 = Vector3.zero;

        public static void FocusView(Transform targetPos, bool isFirstPreview = true)
        {
            if (_mainCamera is null)
            {
                _mainCamera = Camera.main;
                if (_mainCamera is null)
                {
                    return;
                }
            }

            if (isFirstPreview)
            {
                _sizeBeforeFocusView = _mainCamera.orthographicSize;
            }

            var cameraPos = _mainCamera.transform.position;

            var position = targetPos.position;
            _reuseVector3.x = position.x;
            _reuseVector3.y = position.y - 1f;
            _reuseVector3.z = cameraPos.z;
            _mainCamera.transform.DOMove(_reuseVector3, _cameraMoveDuration);
            _mainCamera.DOOrthoSize(Mathf.Min(_mainCamera.orthographicSize, _sizeWhenFocusView), _cameraMoveDuration);
        }

        public static void ReleaseFocusView()
        {
            _mainCamera.DOOrthoSize(_sizeBeforeFocusView, _cameraMoveDuration);
        }

        #endregion

        #region Preview Object

        private static List<Tweener> _tweeners = new List<Tweener>();

        public static void PreviewObject(SpriteRenderer previewSpriteRenderer, bool isNew = false, bool isKillPrevious = true)
        {
            if (_tweeners.Count > 0 && isKillPrevious)
            {
                StopPreviewCurrentObject();
            }

            if (isNew)
            {
                var tween = previewSpriteRenderer.DOColor(Color.green, 1f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .OnKill(() =>
                    {
                        previewSpriteRenderer.color = Color.white;
                    });
                _tweeners.Add(tween);
            }
            else
            {
                var tween = previewSpriteRenderer.DOFade(0.5f, 1f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .OnKill(() =>
                    {
                        previewSpriteRenderer.DOFade(1f, 0f);
                    });
                _tweeners.Add(tween);
            }
        }

        public static void PreviewObject(SpriteRenderer[] previewSpriteRenderers, bool isNew = false)
        {
            for (int i = 0; i < previewSpriteRenderers.Length; i++)
            {
                if (i == 0)
                {
                    PreviewObject(previewSpriteRenderers[i], isNew);
                }
                else
                {
                    PreviewObject(previewSpriteRenderers[i], isNew, false);
                }
            }
        }

        public static void StopPreviewCurrentObject()
        {
            foreach (var tween in _tweeners)
            {
                tween.Kill();
            }

            _tweeners.Clear();
        }

        #endregion

        #region CalculateSoundVolume

        private static float _maxDistance = 15f;
        private static float _maxVolume = 0.8f;

        public static float GetVolumeFromPosition(Vector3 position)
        {
            var rect = PopupController.Instance.GetRectCanvas();
            var rectPos = GetCenterPosition(rect);
            var distance = Vector2.Distance(position, rectPos);
            if (distance > _maxDistance)
            {
                return 0;
            }

            return (_maxVolume / _maxDistance) * (_maxDistance - distance);
        }

        #endregion
        
        public static void PauseGame()
        {
            Time.timeScale = 0;
            SoundManager.Instance.PauseMusic();
        }

        public static void ResumeGame()
        {
            Time.timeScale = 1;
            SoundManager.Instance.ResumeMusic();
        }

        public static long GetCurrentTime()
        {
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
            return now;
            // var now = DateTime.UtcNow;
            // long result = now.Second + now.Minute * 60 + now.Hour * 3600 + now.Day * 86400;
            // return result;
        }

        public static Vector3 GetCenterPosition(RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(Corners);
            var rectWidth = Corners[2].x - Corners[1].x;
            var rectHeight = Corners[1].y - Corners[0].y;
            var centerY = Corners[0].y + rectHeight / 2;
            var centerX = Corners[1].x + rectWidth / 2;
            return new Vector3(centerX, centerY);
        }

        public static bool IsRandomSuccess(float percent)
        {
            int randomNumb = Random.Range(0, 101);
            if (randomNumb <= percent * 100)
            {
                return true;
            }

            return false;
        }
    }
}