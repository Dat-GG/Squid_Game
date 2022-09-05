using Core.Common.Sound;
using DataAccount;
using Plugins.Scripts.Core.Common.Sound;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class UiCharacterSpine : MonoBehaviour
    {
        [SerializeField] private Text playerLevelTxt;
        [SerializeField] private Button previousBtn;
        [SerializeField] private Button nextBtn;

        [SerializeField] private SkeletonGraphic spine;

        // for test
        private int _currentSkin;
        private int maxSkinIndex;

        private void Awake()
        {
            previousBtn.onClick.AddListener(OnClickPreviousBtn);
            nextBtn.onClick.AddListener(OnClickNextBtn);

            var data = spine.skeletonDataAsset.GetSkeletonData(true);
            maxSkinIndex = data.Skins.Count - 1;

            LoadPlayerLevel();
        }

        private void OnClickPreviousBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (_currentSkin <= 0)
                return;

            _currentSkin--;
            spine.initialSkinName = spine.skeletonDataAsset.GetSkeletonData(true).Skins.Items[_currentSkin].Name;
            spine.Initialize(true);

            CheckShowArrowButton();
        }

        private void OnClickNextBtn()
        {
            SoundManager.Instance.PlaySound(SoundType.ClickButton);
            if (_currentSkin >= maxSkinIndex)
                return;

            _currentSkin++;
            spine.initialSkinName = spine.skeletonDataAsset.GetSkeletonData(true).Skins.Items[_currentSkin].Name;
            spine.Initialize(true);

            CheckShowArrowButton();
        }

        private void CheckShowArrowButton()
        {
            nextBtn.gameObject.SetActive(_currentSkin < maxSkinIndex);
            previousBtn.gameObject.SetActive(_currentSkin > 0);
        }

        private void LoadPlayerLevel()
        {
            playerLevelTxt.text = $"Level {DataAccountPlayer.PlayerCharacter.playerLevel}";
        }
    }
}