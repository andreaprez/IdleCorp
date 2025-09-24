using IdleCorp.OOP.Persistence.Hangars;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Factory;
using IdleCorp.OOP.Services.Hangars;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField]
        private Spawner spawner;

        [Header("Configuration")]
        [SerializeField]
        private HangarsConfig hangarsConfig;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            spawner.SpawnWorld();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<EventsService>(new EventsService());
            ServiceLocator.RegisterService<UserDataService>(new UserDataService());
            ServiceLocator.RegisterService<CurrenciesService>(new CurrenciesService());
            ServiceLocator.RegisterService<FactoryService>(new FactoryService());
            ServiceLocator.RegisterService<HangarsService>(new HangarsService(hangarsConfig));
        }
    }
}