using Core.Common;
using DataAccount;

namespace Plugins.Scripts.DataAccount
{
    public class Challenger1Spin
    {
        public long lastTimeSpin;
        public int adWatchInDay;

        public void OnNormalSpin()
        {
            lastTimeSpin = UtilityGame.GetCurrentTime();
            adWatchInDay = 0;
            DataAccountPlayer.SaveChallenger1Spin();
        }

        public void OnWatchAdSpin()
        {
            adWatchInDay += 1;
            DataAccountPlayer.SaveChallenger1Spin();
        }
    }
}
