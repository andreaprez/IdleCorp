using IdleCorp.OOP.Utils;
using UnityEngine;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarSlotModel
    {
        public Observable<bool> IsUsed = new(false);
        public Observable<Sprite> HangarImage = new(null);
        public Observable<string> CapacityText = new("");
        public Observable<float> CapacityBarValue = new(0);
    }
}