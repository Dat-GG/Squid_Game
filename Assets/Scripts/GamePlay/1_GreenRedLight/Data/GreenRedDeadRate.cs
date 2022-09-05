using System;

namespace GamePlay._1_GreenRedLight.Data
{
    [Serializable]
    public struct GreenRedDeadRate
    {
        public int minTime;
        public int maxTime;

        public float slowSingDeadRate;
        public float mediumSingDeadRate;
        public float highSingDeadRate;

        public float GetRate(int timeId)
        {
            if (timeId <= 2)
                return slowSingDeadRate;

            if (timeId >= 6)
                return highSingDeadRate;

            return mediumSingDeadRate;
        }
    }
}