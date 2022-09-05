using System.Collections.Generic;
using Core.Common;

namespace DataAccount
{
    public class PlayerSettings
    {
        public bool MusicOff;
        public bool SoundOff;
        public bool VibrationOff;

        public string FlagId;

        public string Reward;
        public List<string> Decor = new List<string>();
        public string Time;
        public bool ResetTime = false;
        public float LuckySpinCoolDown;

        public void SetMusic(bool isOff)
        {
            if (MusicOff != isOff)
            {
                MusicOff = isOff;
                DataAccountPlayer.SavePlayerSettings();
                GameManager.Instance.PostEvent(EventID.OnMusicChange);
            }
        }

        public void SetSound(bool isOff)
        {
            if (SoundOff != isOff)
            {
                SoundOff = isOff;
                DataAccountPlayer.SavePlayerSettings();
                GameManager.Instance.PostEvent(EventID.OnSoundChange);
            }
        }

        public void SetVibration(bool isOff)
        {
            if (VibrationOff != isOff)
            {
                VibrationOff = isOff;
                DataAccountPlayer.SavePlayerSettings();
                GameManager.Instance.PostEvent(EventID.OnVibrationChange);
            }
        }

        public void SetFlag(string flagId)
        {
            FlagId = flagId;
            DataAccountPlayer.SavePlayerSettings();
            GameManager.Instance.PostEvent(EventID.OnCountryChange);
        }

        public void SetReward(string reward)
        {
            Reward = reward;
            DataAccountPlayer.SavePlayerSettings();
            GameManager.Instance.PostEvent(EventID.EarnReward);
        }

        public void SetInHouse(string decor)
        {
            Decor.Add(decor);
            DataAccountPlayer.SavePlayerSettings();
            GameManager.Instance.PostEvent(EventID.InHouse);
        }
        
        public void SetLuckySpinCoolDown(float time)
        {
            LuckySpinCoolDown = time;
            DataAccountPlayer.SavePlayerSettings();
            GameManager.Instance.PostEvent(EventID.QuitLuckySpin);
        }
        
        public void QuitLuckySpin(string time)
        {
            Time = time;
            DataAccountPlayer.SavePlayerSettings();
            GameManager.Instance.PostEvent(EventID.QuitLuckySpin);
        }
    }
}