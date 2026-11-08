using System;
using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Shipping
{
    public class ShippingPopupView : PopupView
    {
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private TextMeshProUGUI shippingRateText;

        [SerializeField]
        private Slider shippingRateBar; 

        [SerializeField]
        private List<VehicleSlotView> vehicleSlots;

        public List<VehicleSlotView> VehicleSlots => vehicleSlots;

        public event Action CloseButtonPressed;

        public override void Open()
        {
            closeButton.onClick.AddListener(OnCloseButtonPressed);
            base.Open();
        }

        public override void Close()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonPressed);
            base.Close();
        }

        private void Start()
        {
            for (var i = 0; i < vehicleSlots.Count; i++)
            {
                VehicleSlots[i].SetIndex(i);
            }
        }

        public void SetShippingRateText(string text)
        {
            shippingRateText.text = text;
        }

        public void SetShippingRateBar(float barValue)
        {
            shippingRateBar.value = barValue;
        }

        private void OnCloseButtonPressed()
        {
            CloseButtonPressed?.Invoke();
        }
    }
}