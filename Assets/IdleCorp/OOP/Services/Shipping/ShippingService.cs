using System.Collections.Generic;
using System.Linq;
using IdleCorp.OOP.Business.Shipping;
using IdleCorp.OOP.Persistence.Shipping;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Events.Shipping;
using IdleCorp.OOP.Services.Hangars;
using IdleCorp.OOP.Services.UserData;
using UnityEngine;

namespace IdleCorp.OOP.Services.Shipping
{
    public class ShippingService : IService
    {
        private readonly ShippingConfig _shippingConfig;
        private ShippingData _data;

        private ShippingPopupPresenter _mainPopupPresenter;
        private VehicleStorePopupPresenter _storePopupPresenter;

        private EventsService _eventsService;
        private CurrenciesService _currenciesService;
        private HangarsService _hangarsService;

        public ShippingService(ShippingConfig shippingConfig)
        {
            _shippingConfig = shippingConfig;
        }

        public void Init()
        {
            _data = ServiceLocator.GetService<UserDataService>().GetData<ShippingData>();

            _mainPopupPresenter = new ShippingPopupPresenter();
            _mainPopupPresenter.Initialize();
            _storePopupPresenter = new VehicleStorePopupPresenter();
            _storePopupPresenter.Initialize();

            _eventsService = ServiceLocator.GetService<EventsService>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();
            _hangarsService = ServiceLocator.GetService<HangarsService>();
            
            _eventsService.GetEvent<VehiclePurchasedEvent>().AddListener(HandleVehiclePurchased);
            _eventsService.GetEvent<ProductionRateChangedEvent>().AddListener(HandleEggProductionRateChanged);
        }

        public void Dispose()
        {
            _data = null;
        }

        public int GetUsedShippingRate()
        {
            return _data.Vehicles.Sum(vehicle => vehicle.UsedShippingRate);
        }

        public int GetMaxShippingRate()
        {
            var maxShippingRate = 0;
            foreach (var vehicle in _data.Vehicles)
            {
                var vehicleConfig = _shippingConfig.Vehicles.Find(v => v.Id == vehicle.Id);
                maxShippingRate += vehicleConfig.ShippingRate;
            }
            return maxShippingRate;
        }

        public bool IsVehiclePurchased(int positionId)
        {
            return GetVehicleByPositionId(positionId) != null;
        }

        public int GetVehicleId(int positionId)
        {
            if (!IsVehiclePurchased(positionId))
                return -1;

            return GetVehicleByPositionId(positionId).Id;
        }

        public Sprite GetVehicleImage(int positionId)
        {
            if (!IsVehiclePurchased(positionId))
                return null;

            var vehicle = GetVehicleByPositionId(positionId);
            var vehicleConfig = _shippingConfig.Vehicles.Find(v => v.Id == vehicle.Id);
            return vehicleConfig.Image;
        }

        public int GetVehicleMaxShippingRate(int positionId)
        {
            if (!IsVehiclePurchased(positionId))
                return 0;

            var vehicle = GetVehicleByPositionId(positionId);
            var vehicleConfig = _shippingConfig.Vehicles.Find(v => v.Id == vehicle.Id);
            return vehicleConfig.ShippingRate;
        }
  
        public int GetVehicleUsedShippingRate(int positionId)
        {
            if (!IsVehiclePurchased(positionId))
                return 0;

            var vehicle = GetVehicleByPositionId(positionId);
            return vehicle.UsedShippingRate;
        }

        public GameObject GetVehiclePrefab(int vehicleId)
        {
            return _shippingConfig.Vehicles.Find(v => v.Id == vehicleId).Prefab;
        }

        public Dictionary<int, int> GetPurchasedVehiclesInfo()
        {
            var purchasedVehiclesByPositionId = new Dictionary<int, int>();
            foreach (var vehicleData in _data.Vehicles)
                purchasedVehiclesByPositionId.Add(vehicleData.PositionId, vehicleData.Id);
            return purchasedVehiclesByPositionId;
        }

        public void ResetVehicles()
        {
            _data.Vehicles.Clear();
        }

        public List<VehicleConfig> GetVehicleConfigList()
        {
            return _shippingConfig.Vehicles;
        }

        public int GetVehicleSpawnSecondsInterval()
        {
            return _shippingConfig.VehicleSpawnSecondsInterval;
        }

        private void HandleVehiclePurchased(int positionId, int vehicleId)
        {
            var vehicleConfig = _shippingConfig.Vehicles.Find(v => v.Id == vehicleId);

            _currenciesService.SubtractFunds(vehicleConfig.Cost);

            if (IsVehiclePurchased(positionId))
                ModifyVehicleId(positionId, vehicleId);
            else
                PurchaseVehicle(positionId, vehicleId);

            RedistributeShippingRates();
        }

        private void PurchaseVehicle(int positionId, int vehicleId)
        {
            if (IsVehiclePurchased(positionId))
                return;

            var vehicle = new VehicleData();
            vehicle.SetPositionId(positionId);
            vehicle.SetId(vehicleId);
            _data.AddVehicle(vehicle);
        }

        private void ModifyVehicleId(int positionId, int vehicleId)
        {
            _data.ModifyVehicleId(positionId, vehicleId);
        }

        private VehicleData GetVehicleByPositionId(int positionId)
        {
            return _data.Vehicles.Find(v => v.PositionId == positionId);
        }

        private void HandleEggProductionRateChanged(int newProductionRate)
        {
            RedistributeShippingRates();
        }

        private void RedistributeShippingRates()
        {
            var productionRate = (float)_hangarsService.GetProductionRate();
            var maxShippingRate = (float)GetMaxShippingRate();
            var usedShippingRate = Mathf.Min(productionRate, maxShippingRate);

            foreach (var vehicle in _data.Vehicles)
            {
                var vehicleConfig = _shippingConfig.Vehicles.Find(v => v.Id == vehicle.Id);

                var vehicleMaxRatePercentage = vehicleConfig.ShippingRate / maxShippingRate;
                var vehicleUsedRate = Mathf.FloorToInt(vehicleMaxRatePercentage * usedShippingRate);

                _data.ModifyVehicleUsedShippingRate(vehicle.PositionId, vehicleUsedRate);
            }
        }
    }
}