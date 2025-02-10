using System;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Factory
{
    [Serializable]
    public class FactoryData : IUserData
    {
        public int ProductionQuantity;
        public int ProductionMaxCapacity;
        public int ProductionCurrentCapacity;
        public float ProductionRecoveryRate;

        public FactoryData()
        {
            SetDefaultValues();
        }

        public IUserData SetDefaultValues()
        {
            ProductionQuantity = 1;
            ProductionMaxCapacity = 50;
            ProductionCurrentCapacity = ProductionMaxCapacity;
            ProductionRecoveryRate = 0.5f;
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
            ProductionCurrentCapacity = currentCapacity;
            SaveData();
        }

        public void SetProductionRecoveryRate(float recoveryRate)
        {
            ProductionRecoveryRate = recoveryRate;
            SaveData();
        }

        private void SaveData()
        {
            ServiceLocator.GetService<UserDataService>().SaveData(this);
        }
    }
}