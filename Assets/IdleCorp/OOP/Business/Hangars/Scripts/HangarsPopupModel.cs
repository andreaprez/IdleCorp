using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Utils;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarsPopupModel : IPopupModel
    {
        public readonly Observable<string> CapacityText = new("");
        public readonly Observable<float> CapacityBarValue = new(0);
        public readonly List<HangarSlotModel> HangarSlotModels = new(4)
        {
            new HangarSlotModel(), new HangarSlotModel(), new HangarSlotModel(), new HangarSlotModel()
        };
    }
}