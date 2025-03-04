using System;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Factory
{
    [Serializable]
    public class FactoryData : UserData
    {
        public int ProductionQuantity;
        public int ProductionMaxCapacity;
        public int ProductionCurrentCapacity;
        public float ProductionRecoveryRate;
        public int ProductionCost;

        public FactoryData()
        {
            SetDefaultValues();
        }

        public sealed override IUserData SetDefaultValues()
        {
            ProductionQuantity = 1;
            ProductionMaxCapacity = 20;
            ProductionCurrentCapacity = ProductionMaxCapacity;
            ProductionRecoveryRate = 0.6f;
            ProductionCost = 5;
            return this;
        }

        public void SetProductionQuantity(int quantity)
        {
            ProductionQuantity = quantity;
            SaveData();
        }

        public void SetProductionMaxCapacity(int maxCapacity)
        {
            ProductionMaxCapacity = maxCapacity;
            SaveData();
        }

        public void SetProductionCurrentCapacity(int currentCapacity)
        {
            ProductionCurrentCapacity = Math.Clamp(currentCapacity, 0, ProductionMaxCapacity);
            SaveData();
        }

        public void SetProductionRecoveryRate(float recoveryRate)
        {
            ProductionRecoveryRate = recoveryRate;
            SaveData();
        }

        public void SetProductionCost(int cost)
        {
            ProductionCost = cost;
            SaveData();
        }
    }
}