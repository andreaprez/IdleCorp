using System.Collections;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using UnityEngine;

namespace IdleCorp.OOP.Business.UI.Currencies
{
    public class EarningsHandler : MonoBehaviour
    {
        private EarningsService _earningsService;
        private CurrenciesService _currenciesService;

        private void Start()
        {
            _earningsService = ServiceLocator.GetService<EarningsService>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();

            StartCoroutine(GrantEarningsOverTime());
        }

        private IEnumerator GrantEarningsOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                var earnings = _earningsService.GetEarningsPerSecond();
                _currenciesService.AddFunds(earnings);
            }
        }
    }
}