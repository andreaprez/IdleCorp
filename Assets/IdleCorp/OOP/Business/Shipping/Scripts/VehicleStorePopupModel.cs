using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Utils;

namespace IdleCorp.OOP.Business.Shipping
{
    public class VehicleStorePopupModel : IPopupModel
    {
        public readonly Observable<List<VehiclePurchaseSlotModel>> VehiclePurchaseSlotModels = new(new List<VehiclePurchaseSlotModel>());
    }
}