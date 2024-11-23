using System;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Data
{
    [Serializable]
    public class CurrenciesData : IUserData
    {
        public int Funds;
        public int Parts;
        public int CosmicParts;
        public int AccumulatedCosmicParts;

        public CurrenciesData()
        {
            SetDefaultValues();
        }

        public IUserData SetDefaultValues()
        {
            Funds = 20;
            return this;
        }
    }
}
