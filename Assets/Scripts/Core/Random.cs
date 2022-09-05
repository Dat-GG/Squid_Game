using System;

namespace Core.Common
{
    public static class RandNumber
    {
        private static Random Rand
        {
            get
            {
                if (_rand == null)
                {
                    ResetRandom();
                }

                return _rand;
            }
        }
        private static Random _rand;

        private static void ResetRandom()
        {
            //isLog = true;
            _rand = new Random((int)DateTime.Now.Ticks);
        }

        public static int Random(int inclusiveMin, int exclusiveMax)
        {
            var value = Rand.Next(inclusiveMin, exclusiveMax);
            return value;
        }

        public static float Random(float inclusiveMin, float inclusiveMax)
        {
            var value = (float)(Rand.NextDouble() * (inclusiveMax - inclusiveMin) + inclusiveMin);
            return value;
        }
    }
}