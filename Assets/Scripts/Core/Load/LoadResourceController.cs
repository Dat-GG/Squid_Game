using System.Collections.Generic;
using System.IO;
using Core.Common.Popup;
using Core.Common.Sound;
using GamePlay._1_GreenRedLight.Data;
using Plugins.Scripts.Core.Common.GameResources;
using Plugins.Scripts.Core.Common.Load;
using Plugins.Scripts.UI.ShopSkin.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Load
{
    public class LoadResourceController : SingletonMono<LoadResourceController>
    {
        private Dictionary<string, Object> _resourceCache = new Dictionary<string, Object>();

        #region LoadMethod

        private T Load<T>(string path, string fileName) where T : Object
        {
            var fullPath = Path.Combine(path, fileName);
            if (_resourceCache.ContainsKey(fullPath) is false)
            {
                _resourceCache.Add(fullPath, TryToLoad<T>(path, fileName));
            }

            return _resourceCache[fullPath] as T;
        }

        private static T TryToLoad<T>(string path, string fileName) where T : Object
        {
            var fullPath = Path.Combine(path, fileName);
            var result = Resources.Load<T>(fullPath);
            return result;
        }

        #endregion

        #region Public Load Method

        public PopupBase LoadPanel(PopupType panelType)
        {
            return Load<PopupBase>(ResourcesFolderPath.UiFolder, panelType.ToString());
        }

        public GameObject LoadGreenRedPlayer()
        {
            var path = string.Format(ResourcesFolderPath.GreenRedFolder, ResourcesFolderPath.GreenRedFolderCharacter);
            return Load<GameObject>(path, "Player");
        }

        public GameObject LoadGreenRedEnemy()
        {
            var path = string.Format(ResourcesFolderPath.GreenRedFolder, ResourcesFolderPath.GreenRedFolderCharacter);
            return Load<GameObject>(path, "Enemy");
        }


        public GameObject LoadTextHpInGame()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, "TextInGame");
            return Load<GameObject>(path, "TextHpInGame");
        }

        public GameObject LoadTextGoldInGame()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, "TextInGame");
            return Load<GameObject>(path, "TextGoldInGame");
        }

        public AudioClip LoadAudioClip(SoundType soundType)
        {
            return Load<AudioClip>(ResourcesFolderPath.SoundFolder, soundType.ToString());
        }

        public Sprite LoadSkinIcon(SkinType skinName)
        {
            var path = Path.Combine(ResourcesFolderPath.IconFolder, "Skins");
            return Load<Sprite>(path, skinName.ToString());
        }

        //Challenger1
        public GameObject LoadPlayer()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, ResourcesFolderPath.GamePlayFolderPlayer);
            return Load<GameObject>(path, "Player");
        }

        public GameObject LoadDolly()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, ResourcesFolderPath.GamePlayFolderDolly);
            return Load<GameObject>(path, "Dolly");
        }

        public GameObject LoadBot()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, ResourcesFolderPath.GamePlayFolderBot);
            return Load<GameObject>(path, "Bot");
        }

        public GameObject LoadPolice()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, ResourcesFolderPath.GamePlayFolderPolice);
            return Load<GameObject>(path, "Police");
        }

        //Challenger2
        public GameObject LoadPlayer2()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder2, ResourcesFolderPath.GamePlayFolderPlayer);
            return Load<GameObject>(path, "Player");
        }

        public GameObject LoadPolice2()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder2, ResourcesFolderPath.GamePlayFolderPolice);
            return Load<GameObject>(path, "Police");
        }

        public GameObject LoadBot2()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder2, ResourcesFolderPath.GamePlayFolderBot);
            return Load<GameObject>(path, "Bot");
        }

        //Challenger4
        public GameObject LoadPlayer4()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder4, ResourcesFolderPath.GamePlayFolderPlayer4);
            return Load<GameObject>(path, "Player4");
        }

        public GameObject LoadBot4()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder4, ResourcesFolderPath.GamePlayFolderBot4);
            return Load<GameObject>(path, "Bot4");
        }

        public GameObject LoadPolice4()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder4, ResourcesFolderPath.GamePlayFolderPolice4);
            return Load<GameObject>(path, "Police4");
        }

        //Challenger5
        public GameObject LoadPlayer5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderPlayer5);
            return Load<GameObject>(path, "Player5");
        }

        public GameObject LoadBot5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderBot5);
            return Load<GameObject>(path, "Bot5");
        }

        public GameObject LoadGold5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderGold5);
            return Load<GameObject>(path, "Gold5");
        }

        public GameObject LoadTextGoldInGame5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, "TextInGame");
            return Load<GameObject>(path, "TextGoldInGame");
        }

        public GameObject LoadHeart5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderHeart5);
            return Load<GameObject>(path, "Heart5");
        }

        public GameObject LoadThunder5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderThunder5);
            return Load<GameObject>(path, "Thunder5");
        }

        public GameObject LoadPlayerThunder5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderPlayerThunder5);
            return Load<GameObject>(path, "PlayerThunder5");
        }

        public GameObject LoadThunderTrack5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderThunderTrack5);
            return Load<GameObject>(path, "ThunderTrack5");
        }

        public GameObject LoadThunderGround5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderThunderGround5);
            return Load<GameObject>(path, "ThunderGround5");
        }

        public GameObject LoadGroundBreak5()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder5, ResourcesFolderPath.GamePlayFolderGroundBreak5);
            return Load<GameObject>(path, "GroundBreak5");
        }

        // Challenger6

        public GameObject LoadPlayer6()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder6, ResourcesFolderPath.GamePlayFolderPlayer);
            return Load<GameObject>(path, "Player");
        }

        public GameObject LoadPolice6()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder6, ResourcesFolderPath.GamePlayFolderPolice);
            return Load<GameObject>(path, "Police");
        }

        // Challenger7

        public GameObject LoadPlayer7()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder7, ResourcesFolderPath.GamePlayFolderPlayer);
            return Load<GameObject>(path, "Player");
        }

        public GameObject LoadBot7()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder7, ResourcesFolderPath.GamePlayFolderBot);
            return Load<GameObject>(path, "Bot");
        }

        public GameObject LoadGold7()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder7, ResourcesFolderPath.GamePlayFolderItem);
            return Load<GameObject>(path, "GoldItem");
        }

        public GameObject LoadKnife7()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder7, ResourcesFolderPath.GamePlayFolderItem);
            return Load<GameObject>(path, "KnifeItem");
        }

        public GameObject LoadRock7()
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder7, ResourcesFolderPath.GamePlayFolderItem);
            return Load<GameObject>(path, "RockItem");
        }

        // Game
        public GameObject LoadMap(int mapId)
        {
            var path = string.Format(ResourcesFolderPath.GamePlayFolder, ResourcesFolderPath.GamePlayFolderMap);
            return Load<GameObject>(path, $"Map {mapId}");
        }

        #endregion

        #region LoadDataAsset

        public SkinDataCollection LoadSkinDataCollection()
        {
            var path = string.Format(ResourcesFolderPath.DataFolder, ResourcesFolderPath.DataFolderSkin);
            return Load<SkinDataCollection>(path, "SkinData");
        }

        public GreenRedSingTimeData LoadGreenRedSingTimeData()
        {
            var path = string.Format(ResourcesFolderPath.DataFolder, ResourcesFolderPath.DataFolderGreenRedLight);
            return Load<GreenRedSingTimeData>(path, "SingDataRate");
        }

        #endregion
    }
}