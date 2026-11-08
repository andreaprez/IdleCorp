using System.Collections.Generic;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Shipping
{
    public class ShippingData : UserData
    {
        public List<VehicleData> Vehicles;

        public ShippingData()
        {
            SetDefaultValues();
        }

        public sealed override IUserData SetDefaultValues()
        {
            Vehicles = new List<VehicleData>();
            return this;
        }

        public void AddVehicle(VehicleData vehicleData)
        {
            Vehicles.Add(vehicleData);
            SaveData();
        }

        public void ModifyVehicleId(int positionId, int vehicleId)
        {
            var vehicle = Vehicles.Find(v => v.PositionId == positionId);
            vehicle.SetId(vehicleId);
            SaveData();
        }

        public void ModifyVehicleUsedShippingRate(int positionId, int shippingRate)
        {
            var vehicle = Vehicles.Find(v => v.PositionId == positionId);
            vehicle.SetUsedShippingRate(shippingRate);
            SaveData();
        }
    }
}