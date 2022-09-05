using System.Collections.Generic;
using Core.Common;

namespace DataAccount
{
    public class PlayerDailyCheckIn
    {
        public Dictionary<int, long> ClaimedDay = new Dictionary<int, long>();

        public void CheckToRefreshDailyCheckIn()
        {
            if (!ClaimedDay.ContainsKey(6))
            {
                return;
            }

            var lastTimeClaim = ClaimedDay[6];
            if (UtilityGame.GetCurrentTime() - lastTimeClaim >= 86400)
            {
                ClaimedDay.Clear();
                DataAccountPlayer.SavePlayerDailyCheckIn();
            }
        }

        public long GetDayClaimedTime(int day)
        {
            if (ClaimedDay.ContainsKey(day))
            {
                return ClaimedDay[day];
            }

            return 0;
        }

        public bool IsDayClaimable(int day, out long lastTimeClaim)
        {
            if (day == 0)
            {
                lastTimeClaim = 0;
                return true;
            }

            int lastDayClaimed = -1;
            foreach (var dayClaimed in ClaimedDay.Keys)
            {
                if (dayClaimed > lastDayClaimed)
                {
                    lastDayClaimed = dayClaimed;
                }
            }

            if (day == lastDayClaimed + 1)
            {
                lastTimeClaim = ClaimedDay[lastDayClaimed];
                if (UtilityGame.GetCurrentTime() - lastTimeClaim >= 86400)
                {
                    return true;
                }

                return false;
            }

            lastTimeClaim = -1;
            return false;
        }

        public void ClaimReward(int day)
        {
            ClaimedDay.Add(day, UtilityGame.GetCurrentTime());
            DataAccountPlayer.SavePlayerDailyCheckIn();
        }
    }
}