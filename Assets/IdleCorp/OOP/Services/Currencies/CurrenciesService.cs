using IdleCorp.OOP.Persistence.Repository;

namespace IdleCorp.OOP.Services.Currencies
{
    public class CurrenciesService : IService
    {
        private CurrenciesRepository _repository;

        public void Init()
        {
            _repository = new CurrenciesRepository();
        }

        public void Dispose()
        {
        
        }

        public int GetFunds()
        {
            return _repository.GetFunds();
        }

        public void AddFunds(int amount)
        {
            var funds = GetFunds();
            funds += amount;
            _repository.SetFunds(funds);
        }

        public void SubtractFunds(int amount)
        {
            var funds = GetFunds();
            funds -= amount;
            _repository.SetFunds(funds);
        }

        public int GetParts()
        {
            return _repository.GetParts();
        }

        public void AddParts(int amount)
        {
            var parts = GetParts();
            parts += amount;
            _repository.SetParts(parts);
        }

        public void SubtractParts(int amount)
        {
            var parts = GetParts();
            parts -= amount;
            _repository.SetParts(parts);
        }

        public int GetCosmicParts()
        {
            return _repository.GetCosmicParts();
        }

        public void AddCosmicParts(int amount)
        {
            var cosmicParts = GetCosmicParts();
            cosmicParts += amount;
            _repository.SetCosmicParts(cosmicParts);
        }

        public void SubtractCosmicParts(int amount)
        {
            var cosmicParts = GetCosmicParts();
            cosmicParts -= amount;
            _repository.SetCosmicParts(cosmicParts);
        }

        public int GetAccumulatedCosmicParts()
        {
            return _repository.GetAccumulatedCosmicParts();
        }

        public void AddAccumulatedCosmicParts(int amount)
        {
            var accumulatedCosmicParts = GetAccumulatedCosmicParts();
            accumulatedCosmicParts += amount;
            _repository.SetAccumulatedCosmicParts(accumulatedCosmicParts);
        }

        public void ResetAccumulatedCosmicParts()
        {
            _repository.SetAccumulatedCosmicParts(0);
        }
    }
}
