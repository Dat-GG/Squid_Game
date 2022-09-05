using System;

namespace Core.Common.GameResources
{
    [Serializable]
    public class PowerUpItem : Resource
    {
        public PowerUpType powerUpType;

        public PowerUpItem(PowerUpType powerUpType, int value)
        {
            resourceType = ResourceType.PowerUp;
            this.powerUpType = powerUpType;
            id = (int)powerUpType;
            this.value = value;
        }
    }
}
