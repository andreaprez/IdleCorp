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
            Funds = 100;
            return this;
        }

        public void SetFunds(int funds)
        {
            Funds = funds;
            SaveData();
        }

        public void SetParts(int parts)
        {
            Parts = parts;
            SaveData();
        }

        public void SetCosmicParts(int cosmicParts)
        {
            CosmicParts = cosmicParts;
            SaveData();
        }

        public void SetAccumulatedCosmicParts(int accumulatedCosmicParts)
        {
            AccumulatedCosmicParts = accumulatedCosmicParts;
            SaveData();
        }
    }
}
