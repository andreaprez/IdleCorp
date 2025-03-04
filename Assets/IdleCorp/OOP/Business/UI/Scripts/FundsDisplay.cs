using IdleCorp.OOP.Persistence.Currencies;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.UserData;
using TMPro;
using UnityEngine;

namespace IdleCorp.OOP.Business.UI.Scripts
{
    public class FundsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fundsNumberText;
        [SerializeField] private TextMeshProUGUI fundsMagnitudeText;

        private CurrenciesData _currenciesData;

        private void Start()
        {
            _currenciesData = ServiceLocator.GetService<UserDataService>().GetData<CurrenciesData>();
        }

        void Update()
        {
            fundsNumberText.SetText(_currenciesData.Funds.ToString());
            // TODO: Calculate conversion with magnitudes (trillion, quadrillion, etc)
        }
    }
}
