using System;

namespace Core.Common.GameResources
{
    [Serializable]
    public class MoneyItem : Resource
    {
        public MoneyType moneyType;

        public MoneyItem(MoneyType moneyType, int value)
        {
            resourceType = ResourceType.Money;
            this.moneyType = moneyType;
            id = (int) moneyType;
            this.value = value;
        }
    }
}