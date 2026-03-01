using IdleCorp.OOP.Persistence.Currencies;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Currencies;
using IdleCorp.OOP.Services.UserData;

namespace IdleCorp.OOP.Services.Currencies
{
    public class CurrenciesService : IService
    {
        private CurrenciesData _data;

        public void Init()
        {
            _data = ServiceLocator.GetService<UserDataService>().GetData<CurrenciesData>();
        }

        public void Dispose()
        {
            _data = null;
        }

        public int GetFunds()
        {
            return _data.Funds;
        }

        public void AddFunds(int amount)
        {
            var funds = GetFunds();
            funds += amount;
            _data.SetFunds(funds);
            ServiceLocator.GetService<EventsService>().GetEvent<FundsChangedEvent>().Trigger(funds);
        }

        public void SubtractFunds(int amount)
        {
            var funds = GetFunds();
            funds -= amount;
            _data.SetFunds(funds);
            ServiceLocator.GetService<EventsService>().GetEvent<FundsChangedEvent>().Trigger(funds);
        }

        public int GetParts()
        {
            return _data.Parts;
        }

        public void AddParts(int amount)
        {
            var parts = GetParts();
            parts += amount;
            _data.SetParts(parts);
        }

        public void SubtractParts(int amount)
        {
            var parts = GetParts();
            parts -= amount;
            _data.SetParts(parts);
        }

        public int GetCosmicParts()
        {
            return _data.CosmicParts;
        }

        public void AddCosmicParts(int amount)
        {
            var cosmicParts = GetCosmicParts();
            cosmicParts += amount;
            _data.SetCosmicParts(cosmicParts);
        }

        public void SubtractCosmicParts(int amount)
        {
            var cosmicParts = GetCosmicParts();
            cosmicParts -= amount;
            _data.SetCosmicParts(cosmicParts);
        }

        public int GetAccumulatedCosmicParts()
        {
            return _data.AccumulatedCosmicParts;
        }

        public void AddAccumulatedCosmicParts(int amount)
        {
            var accumulatedCosmicParts = GetAccumulatedCosmicParts();
            accumulatedCosmicParts += amount;
            _data.SetAccumulatedCosmicParts(accumulatedCosmicParts);
        }

        public void ResetAccumulatedCosmicParts()
        {
            _data.SetAccumulatedCosmicParts(0);
        }
    }
}
