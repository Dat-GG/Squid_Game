using System;
using Random = UnityEngine.Random;

namespace GamePlay._1_GreenRedLight.Data
{
    [Serializable]
    public struct GreenRedSingTimeRate
    {
        public int minStage;
        public int maxStage;

        public float firstRate;
        public float secondRate;
        public float thirdRate;
        public float fourthRate;
        public float fifthRate;
        public float sixthRate;
        public float seventhRate;

        public int GetTimeSingId()
        {
            float result = Random.Range(0f, 1f);

            float minPercent = 0f;

            if (result >= minPercent && result <= minPercent + firstRate)
            {
                return 1;
            }

            minPercent += firstRate;
            if (result >= minPercent && result <= minPercent + secondRate)
            {
                return 2;
            }

            minPercent += secondRate;
            if (result >= minPercent && result <= minPercent + thirdRate)
            {
                return 3;
            }

            minPercent += thirdRate;
            if (result >= minPercent && result <= minPercent + fourthRate)
            {
                return 4;
            }

            minPercent += fourthRate;
            if (result >= minPercent && result <= minPercent + fifthRate)
            {
                return 5;
            }

            minPercent += fifthRate;
            if (result >= minPercent && result <= minPercent + sixthRate)
            {
                return 6;
            }

            minPercent += sixthRate;
            if (result >= minPercent && result <= minPercent + seventhRate)
            {
                return 7;
            }

            return 1;
        }
    }
}