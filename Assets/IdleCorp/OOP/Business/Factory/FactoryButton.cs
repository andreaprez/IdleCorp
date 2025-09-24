using IdleCorp.OOP.Persistence.Currencies;
using IdleCorp.OOP.Persistence.Factory;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.UserData;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Factory
{
    public class FactoryButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image cooldownFill;

        private CurrenciesService _currenciesService;
        private EventsService _eventsService;
        private FactoryData _factoryData;
        private float _cooldownTimer;

        private void Start()
        {
            _eventsService = ServiceLocator.GetService<EventsService>();
            _factoryData = ServiceLocator.GetService<UserDataService>().GetData<FactoryData>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();
        }

        private void Update()
        {
            if (_cooldownTimer >= _factoryData.ProductionRecoveryRate)
            {
                _cooldownTimer = 0f;
                _factoryData.SetProductionCurrentCapacity(_factoryData.ProductionCurrentCapacity + 1);
            }
            cooldownFill.fillAmount = (float)_factoryData.ProductionCurrentCapacity / _factoryData.ProductionMaxCapacity;
            button.interactable = CanProduce();
            _cooldownTimer += Time.deltaTime;
        }

        public void OnPressed()
        {
            if (!CanProduce())
                return;
            _eventsService.GetEvent<RobotProducedEvent>().Trigger(_factoryData.ProductionQuantity);
            _factoryData.SetProductionCurrentCapacity(_factoryData.ProductionCurrentCapacity - _factoryData.ProductionQuantity);
            _currenciesService.SubtractFunds(_factoryData.ProductionCost);
        }

        private bool CanProduce()
        {
            return _factoryData.ProductionCurrentCapacity >= _factoryData.ProductionQuantity
                   && _currenciesService.GetFunds() >= _factoryData.ProductionCost;
        }
    }
}
