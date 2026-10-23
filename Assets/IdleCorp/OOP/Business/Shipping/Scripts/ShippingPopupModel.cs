using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Utils;

namespace IdleCorp.OOP.Business.Shipping
{
    public class ShippingPopupModel : IPopupModel
    {
        public readonly Observable<string> ShippingRateText = new("");
        public readonly Observable<float> ShippingRateBarValue = new(0);
        public readonly List<VehicleSlotModel> VehicleSlotModels = new(4)
        {
            new VehicleSlotModel(), new VehicleSlotModel(), new VehicleSlotModel(), new VehicleSlotModel()
        };
    }
}