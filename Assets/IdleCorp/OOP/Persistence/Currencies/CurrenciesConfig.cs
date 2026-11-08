using UnityEngine;

namespace IdleCorp.OOP.Persistence.Currencies
{
    [CreateAssetMenu(menuName = "IdleCorp/Config/Currencies")]
    public class CurrenciesConfig : ScriptableObject
    {
        public int MaxIdleEarningsTimeHours;
    }
}