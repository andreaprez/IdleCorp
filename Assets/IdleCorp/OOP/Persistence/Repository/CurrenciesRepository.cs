using IdleCorp.OOP.Persistence.Data;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Persistence.Repository
{
    public class CurrenciesRepository
    {
        public int GetFunds()
        {
            return GetData().Funds;
        }

        public void SetFunds(int amount)
        {
            var data = GetData();
            data.Funds = amount;
            SaveData(data);
        }

        public int GetParts()
        {
            return GetData().Parts;
        }

        public void SetParts(int amount)
        {
            var data = GetData();
            data.Parts = amount;
            SaveData(data);
        }

        public int GetCosmicParts()
        {
            return GetData().CosmicParts;
        }

        public void SetCosmicParts(int amount)
        {
            var data = GetData();
            data.CosmicParts = amount;
            SaveData(data);
        }

        public int GetAccumulatedCosmicParts()
        {
            return GetData().AccumulatedCosmicParts;
        }

        public void SetAccumulatedCosmicParts(int amount)
        {
            var data = GetData();
            data.AccumulatedCosmicParts = amount;
            SaveData(data);
        }

        private CurrenciesData GetData()
        {
            return ServiceLocator.GetService<UserDataService>().GetData<CurrenciesData>();
        }

        private void SaveData(CurrenciesData data)
        {
            ServiceLocator.GetService<UserDataService>().SaveData(data);
        }
    }
}
