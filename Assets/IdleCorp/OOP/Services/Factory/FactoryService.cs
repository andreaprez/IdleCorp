using System;
using IdleCorp.OOP.Business;
using IdleCorp.OOP.Persistence.Factory;
using IdleCorp.OOP.Persistence.Robots;
using IdleCorp.OOP.Services.UserData;
using UnityEngine;

namespace IdleCorp.OOP.Services.Factory
{
    public class FactoryService : IService
    {
        private readonly RobotsConfig _robotsConfig;
        private FactoryData _data;

        public FactoryService(RobotsConfig robotsConfig)
        {
            _robotsConfig = robotsConfig;
        }

        public void Init()
        {
            _data = ServiceLocator.GetService<UserDataService>().GetData<FactoryData>();
        }

        public void Dispose()
        {
            _data = null;
        }

        public int GetProductionQuantity()
        {
            return _data.ProductionQuantity;
        }

        public void ModifyProductionQuantity(int productionQuantity)
        {
            _data.SetProductionQuantity(productionQuantity);
        }
        
        public int GetProductionMaxCapacity()
        {
            return _data.ProductionMaxCapacity;
        }

        public void ModifyProductionMaxCapacity(int productionMaxCapacity)
        {
            _data.SetProductionMaxCapacity(productionMaxCapacity);
        }

        public int GetProductionCurrentCapacity()
        {
            return _data.ProductionCurrentCapacity;
        }

        public void ModifyProductionCurrentCapacity(int productionCurrentCapacity)
        {
            productionCurrentCapacity = Math.Clamp(productionCurrentCapacity, 0, _data.ProductionMaxCapacity);
            _data.SetProductionCurrentCapacity(productionCurrentCapacity);
        }
        
        public float GetProductionRecoveryRate()
        {
            return _data.ProductionRecoveryRate;
        }

        public void ModifyProductionRecoveryRate(float recoveryRate)
        {
            _data.SetProductionRecoveryRate(recoveryRate);
        }
        
        public int GetProductionCost()
        {
            return _data.ProductionCost;
        }

        public void ModifyProductionCost(int productionCost)
        {
            _data.SetProductionCost(productionCost);
        }

        public Transform GetSpawnPoint()
        {
            return SceneReferencesHolder.Instance.FactorySpawnPoint;
        }

        public float GetTargetReachedThresholdForRobots()
        {
            return _robotsConfig.TargetReachedThresholdForRobots;
        }
    }
}