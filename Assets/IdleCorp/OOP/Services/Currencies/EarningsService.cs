using IdleCorp.OOP.Services.Shipping;

namespace IdleCorp.OOP.Services.Currencies
{
    public class EarningsService : IService
    {
        private ShippingService _shippingService;

        public void Init()
        {
            _shippingService = ServiceLocator.GetService<ShippingService>();
        }

        public void Dispose()
        {
        }

        public int GetEarningsPerSecond()
        {
            var earnings = _shippingService.GetUsedShippingRate() * 1; //TODO: multiply by current mineral value
            return earnings;
        }
    }
}