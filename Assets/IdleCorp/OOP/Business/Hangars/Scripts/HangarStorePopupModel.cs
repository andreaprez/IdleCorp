using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Utils;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarStorePopupModel : IPopupModel
    {
        public readonly Observable<List<HangarPurchaseSlotModel>> HangarPurchaseSlotModels = new(new List<HangarPurchaseSlotModel>());
    }
}