using System;
using IdleCorp.OOP.Persistence.Currencies;
using IdleCorp.OOP.Persistence.Player;
using IdleCorp.OOP.Services.Shipping;
using IdleCorp.OOP.Services.UserData;
using UnityEngine;

namespace IdleCorp.OOP.Services.Currencies
{
    public class EarningsService : IService
    {
        private readonly CurrenciesConfig _currenciesConfig;
        private ShippingService _shippingService;

        public EarningsService(CurrenciesConfig currenciesConfig)
        {
            _currenciesConfig = currenciesConfig;
        }

        public void Init()
        {
            _shippingService = ServiceLocator.GetService<ShippingService>();

            GrantIdleEarnings();
        }

        public void Dispose()
        {
        }

        public int GetEarningsPerSecond()
        {
            var earnings = _shippingService.GetUsedShippingRate() * 1; //TODO: multiply by current mineral value
            return earnings;
        }

        private void GrantIdleEarnings()
        {
            var playerData = ServiceLocator.GetService<UserDataService>().GetData<PlayerData>();
            var currenciesService = ServiceLocator.GetService<CurrenciesService>();

            var idleTimeSeconds = (int)TimeSpan.FromTicks(DateTime.UtcNow.Ticks - playerData.LastSessionTimeTicks).TotalSeconds;
            idleTimeSeconds = Mathf.Clamp(idleTimeSeconds, 0, _currenciesConfig.MaxIdleEarningsTimeHours * 3600);

            var earnings = idleTimeSeconds * _shippingService.GetUsedShippingRate() * 1; //TODO: multiply by current mineral value
            currenciesService.AddFunds(earnings);
        }
    }
}