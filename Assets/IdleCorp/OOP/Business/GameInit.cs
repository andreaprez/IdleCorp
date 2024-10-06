using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.EventsService;
using UnityEngine;

namespace IdleCorp.OOP.Business
{
    public class GameInit : MonoBehaviour
    {
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<EventsService>(new EventsService());
        }
    }
}