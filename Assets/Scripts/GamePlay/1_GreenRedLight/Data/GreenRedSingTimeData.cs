using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay._1_GreenRedLight.Data
{
    // DO NOT change line CreateAssetMenu
    [CreateAssetMenu(menuName = "DATA/1_GreenRedLight/SingDataRate", fileName = "SingDataRate")]
    public class GreenRedSingTimeData : SerializedScriptableObject
    {
        public int minDead = 3;
        public int maxDead = 11;
        
        public List<GreenRedSingTimeRate> allRates = new List<GreenRedSingTimeRate>();
        
        public List<GreenRedDeadRate> deadRates = new List<GreenRedDeadRate>();
        
        public Dictionary<int, float> singTime = new Dictionary<int, float>();

        public GreenRedSingTimeRate GetRate(int timePlayed)
        {
            foreach (var rate in allRates)
            {
                if (timePlayed >= rate.minStage && timePlayed <= rate.maxStage)
                {
                    return rate;
                }

                if (timePlayed >= rate.minStage && rate.maxStage == -1)
                {
                    return rate;
                }
            }

            return allRates[0];
        }

        public float GetSingTime(int id)
        {
            if (singTime.ContainsKey(id))
            {
                return singTime[id];
            }

            return 4f;
        }

        public GreenRedDeadRate GetDeadRate(float currentTime)
        {
            foreach (var deadRate in deadRates)
            {
                if (currentTime >= deadRate.minTime && currentTime <= deadRate.maxTime)
                {
                    return deadRate;
                }

                if (currentTime >= deadRate.minTime && deadRate.maxTime == -1)
                {
                    return deadRate;
                }
            }

            return deadRates[0];
        }

        public int GetRandomEnemyDieInGame()
        {
            int result = Random.Range(minDead, maxDead + 1);
            return result;
        }
    }
}