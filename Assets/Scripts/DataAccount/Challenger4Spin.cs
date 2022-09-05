using Core.Common;
using DataAccount;

namespace Plugins.Scripts.DataAccount
{
    public class Challenger4Spin
    {
        public long lastTimeSpin;
        public int adWatchInDay;

        public void OnNormalSpin()
        {
            lastTimeSpin = UtilityGame.GetCurrentTime();
            adWatchInDay = 0;
            DataAccountPlayer.SaveChallenger4Spin();
        }

        public void OnWatchAdSpin()
        {
            adWatchInDay += 1;
            DataAccountPlayer.SaveChallenger4Spin();
        }
    }
}
