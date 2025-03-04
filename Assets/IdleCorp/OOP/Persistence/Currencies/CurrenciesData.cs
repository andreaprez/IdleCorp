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

        public void ModifyFunds(int amount)
        {
            Funds += amount;
            if (Funds < 0) 
                Funds = 0;
            SaveData();
        }

        public void ModifyParts(int amount)
        {
            Parts += amount;
            if (Parts < 0) 
                Parts = 0;
            SaveData();
        }

        public void ModifyCosmicParts(int amount)
        {
            CosmicParts += amount;
            if (CosmicParts < 0) 
                CosmicParts = 0;
            SaveData();
        }

        public void ModifyAccumulatedCosmicParts(int amount)
        {
            AccumulatedCosmicParts += amount;
            if (AccumulatedCosmicParts < 0) 
                AccumulatedCosmicParts = 0;
            SaveData();
        }

        public void ResetAccumulatedCosmicParts()
        {
            AccumulatedCosmicParts = 0;
            SaveData();
        }
    }
}
