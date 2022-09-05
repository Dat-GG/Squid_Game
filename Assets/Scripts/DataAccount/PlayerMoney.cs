using System.Collections.Generic;
using Core.Common.GameResources;

namespace DataAccount
{
    public class PlayerMoney
    {
        public Dictionary<MoneyType, int> allMoneys;

        public PlayerMoney()
        {
            allMoneys = new Dictionary<MoneyType, int>()
            {
                {MoneyType.Diamond, 5}
            };
        }

        #region MoneyAction

        public bool IsEnough(MoneyType moneyType, int value)
        {
            return GetMoney(moneyType) >= value;
        }

        public bool IsEnough(MoneyItem cost)
        {
            return IsEnough(cost.moneyType, cost.value);
        }

        public int GetMoney(MoneyType moneyType)
        {
            return allMoneys.ContainsKey(moneyType) ? allMoneys[moneyType] : 0;
        }

        public int SetMoney(bool isAdd, MoneyType moneyType, int value)
        {
            if (isAdd)
            {
                if (allMoneys.ContainsKey(moneyType))
                {
                    allMoneys[moneyType] += value;
                }
                else
                {
                    allMoneys[moneyType] = value;
                }

                EventDispatcher.Instance.PostEvent(EventID.EarnMoney, moneyType);
            }
            else
            {
                if (allMoneys.ContainsKey(moneyType))
                {
                    allMoneys[moneyType] -= value;
                    if (allMoneys[moneyType] < 0)
                    {
                        allMoneys[moneyType] = 0;
                    }
                }

                EventDispatcher.Instance.PostEvent(EventID.SpendMoney, moneyType);
            }

            DataAccountPlayer.SavePlayerMoney();
            return value;
        }

        #endregion
    }
}