using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using TMPro;
using UnityEngine;

namespace IdleCorp.OOP.Business.UI.Currencies
{
    public class FundsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fundsNumberText;
        [SerializeField] private TextMeshProUGUI fundsMagnitudeText;

        private CurrenciesService _currenciesService;

        private void Start()
        {
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();
        }

        void Update()
        {
            fundsNumberText.SetText(_currenciesService.GetFunds().ToString());
            // TODO: Calculate conversion with magnitudes (trillion, quadrillion, etc)
        }
    }
}
