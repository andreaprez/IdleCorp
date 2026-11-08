using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using TMPro;
using UnityEngine;

namespace IdleCorp.OOP.Business.UI.Currencies
{
    public class EarningsDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI earningsNumberText;
        [SerializeField] private TextMeshProUGUI earningsMagnitudeText;

        private EarningsService _earningsService;

        private void Start()
        {
            _earningsService = ServiceLocator.GetService<EarningsService>();
        }

        void Update()
        {
            earningsNumberText.SetText(_earningsService.GetEarningsPerSecond().ToString());
            // TODO: Calculate conversion with magnitudes (trillion, quadrillion, etc)
        }
    }
}