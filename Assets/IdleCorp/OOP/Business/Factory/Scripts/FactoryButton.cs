using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.Factory;
using IdleCorp.OOP.Services.Hangars;
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

        private FactoryService _factoryService;
        private CurrenciesService _currenciesService;
        private HangarsService _hangarsService;
        private EventsService _eventsService;
        private float _cooldownTimer;

        private void Start()
        {
            _factoryService = ServiceLocator.GetService<FactoryService>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();
            _hangarsService = ServiceLocator.GetService<HangarsService>();
            _eventsService = ServiceLocator.GetService<EventsService>();
        }

        private void Update()
        {
            if (_cooldownTimer >= _factoryService.GetProductionRecoveryRate())
            {
                _cooldownTimer = 0f;
                _factoryService.ModifyProductionCurrentCapacity(_factoryService.GetProductionCurrentCapacity() + 1);
            }
            cooldownFill.fillAmount = (float)_factoryService.GetProductionCurrentCapacity() / _factoryService.GetProductionMaxCapacity();
            button.interactable = CanProduce();
            _cooldownTimer += Time.deltaTime;
        }

        public void OnPressed()
        {
            if (!CanProduce())
                return;
            _eventsService.GetEvent<RobotProducedEvent>().Trigger(_factoryService.GetProductionQuantity());
            _factoryService.ModifyProductionCurrentCapacity(_factoryService.GetProductionCurrentCapacity() - _factoryService.GetProductionQuantity());
            _currenciesService.SubtractFunds(_factoryService.GetProductionCost());
        }

        private bool CanProduce()
        {
            return _factoryService.GetProductionCurrentCapacity() >= _factoryService.GetProductionQuantity()
                   && _currenciesService.GetFunds() >= _factoryService.GetProductionCost()
                   && _hangarsService.GetFreeSpace() >= _factoryService.GetProductionQuantity();
        }
    }
}
