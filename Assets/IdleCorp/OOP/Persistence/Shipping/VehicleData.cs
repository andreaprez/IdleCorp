using System;

namespace IdleCorp.OOP.Persistence.Shipping
{
    [Serializable]
    public class VehicleData
    {
        public int Id;
        public int PositionId;
        public int UsedShippingRate;

        public VehicleData()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Id = -1;
            PositionId = -1;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetPositionId(int positionId)
        {
            PositionId = positionId;
        }

        public void SetUsedShippingRate(int shippingRate)
        {
            UsedShippingRate = shippingRate;
        }
    }
}