using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField]
        private Spawner spawner;

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
        }
    }
}