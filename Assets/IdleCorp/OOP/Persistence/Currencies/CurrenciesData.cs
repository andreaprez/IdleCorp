using System;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Currencies
{
    [Serializable]
    public class CurrenciesData : UserData
    {
        public int Funds;
        public int Parts;
        public int CosmicParts;
        public int AccumulatedCosmicParts;

        public CurrenciesData()
        {
            SetDefaultValues();
        }

        public sealed override IUserData SetDefaultValues()
        {
            Funds = 20;
            return this;
        }
    }
}
