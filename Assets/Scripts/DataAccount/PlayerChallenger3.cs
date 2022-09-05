using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
    public class PlayerChallenger3
    {
        public int _winCount;

        public int _loseCount;
        
        public int WinCount
        {
            get => _winCount;

            set
            {
                _winCount = value;
                DataAccountPlayer.SavePlayerChallenger3();
            }
        }
        
        public int LoseCount
        {
            get => _loseCount;

            set
            {
                _loseCount = value;
                DataAccountPlayer.SavePlayerChallenger3();
            }
        }
    }
}
