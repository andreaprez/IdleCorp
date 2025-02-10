using System;
using System.Collections.Generic;
using IdleCorp.OOP.Persistence.Currencies;
using IdleCorp.OOP.Persistence.Factory;

namespace IdleCorp.OOP.Services.UserData
{
    public class UserDataService : IService
    {
        private JsonDataHandler _jsonDataHandler;
        private Dictionary<Type, IUserData> _dataModels;

        public void Init()
        {
            _jsonDataHandler = new JsonDataHandler();
            LoadDataModels();
        }

        public void Dispose()
        {
        }

        public T GetData<T>() where T : IUserData
        {
            if (_dataModels.TryGetValue(typeof(T), out var data))
                return (T)data;
            return default;
        }

        public void SaveData<T>(T data) where T : IUserData
        {
            _dataModels[typeof(T)] = data;
            _jsonDataHandler.SaveData(data);
        }

        private void LoadDataModels()
        {
            _dataModels = new Dictionary<Type, IUserData>();
            LoadData<CurrenciesData>();
            LoadData<FactoryData>();
        }

        private void LoadData<T>() where T : IUserData, new()
        {
            var data = _jsonDataHandler.ReadData<T>() ?? new T().SetDefaultValues();
            _dataModels[typeof(T)] = data;
        }
    }
}
