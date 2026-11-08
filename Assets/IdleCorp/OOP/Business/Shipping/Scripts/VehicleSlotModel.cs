using IdleCorp.OOP.Utils;
using UnityEngine;

namespace IdleCorp.OOP.Business.Shipping
{
    public class VehicleSlotModel
    {
        public Observable<bool> IsUsed = new(false);
        public Observable<Sprite> VehicleImage = new(null);
        public Observable<string> ShippingRateText = new("");
        public Observable<float> ShippingRateBarValue = new(0);
    }
}