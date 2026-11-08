using System;
using System.Collections.Generic;
using IdleCorp.OOP.Persistence.Currencies;
using IdleCorp.OOP.Persistence.Factory;
using IdleCorp.OOP.Persistence.Hangars;
using IdleCorp.OOP.Persistence.Player;
using IdleCorp.OOP.Persistence.Shipping;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Player;

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
            
            ServiceLocator.GetService<EventsService>().GetEvent<SessionEndedEvent>().AddListener(OnSessionEnded);
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
            LoadData<HangarsData>();
            LoadData<ShippingData>();
            LoadData<PlayerData>();
        }

        private void LoadData<T>() where T : IUserData, new()
        {
            var data = _jsonDataHandler.ReadData<T>() ?? new T();
            _dataModels[typeof(T)] = data;
        }

        private void OnSessionEnded()
        {
            GetData<PlayerData>().SetLastSessionTime(DateTime.UtcNow.Ticks);
        }
    }
}
