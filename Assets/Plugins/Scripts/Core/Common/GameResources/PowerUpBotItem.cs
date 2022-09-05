using System;

namespace Core.Common.GameResources
{
    [Serializable]
    public class PowerUpBotItem : Resource
    {
        public PowerUpBotType powerUpBotType;

        public PowerUpBotItem(PowerUpBotType powerUpBotType, int value)
        {
            resourceType = ResourceType.PowerUpBot;
            this.powerUpBotType = powerUpBotType;
            id = (int)powerUpBotType;
            this.value = value;
        }
    }
}
