namespace DataAccount
{
    public class Player1GreenRedLight
    {
        public int totalWin;
        public int totalLose;

        public int GetTotalTimePlayed()
        {
            return totalWin + totalLose;
        }

        public void OnWin()
        {
            totalWin += 1;
            DataAccountPlayer.SavePlayer1GreenRedLight();
        }

        public void OnLose()
        {
            totalLose += 1;
            DataAccountPlayer.SavePlayer1GreenRedLight();
        }
    }
}