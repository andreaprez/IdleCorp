using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Shipping
{
    public class VehiclePurchaseSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image vehicleImage;

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI shippingRateText;

        [SerializeField]
        private TextMeshProUGUI costText;

        [SerializeField]
        private Button purchaseButton;

        public event Action<int> PurchaseButtonPressed;

        private int _slotIndex;

        public void SetIndex(int index)
        {
            _slotIndex = index;
        }

        public void SetVehicleInfo(Sprite image, string name, string shippingRate, string cost)
        {
            vehicleImage.sprite = image;
            nameText.text = name;
            shippingRateText.text = shippingRate;
            costText.text = cost;
        }

        public void SetButton(bool buttonEnabled)
        {
            purchaseButton.interactable = buttonEnabled;
        }

        private void OnEnable()
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
        }

        private void OnDisable()
        {
            purchaseButton.onClick.RemoveListener(OnPurchaseButtonPressed);
        }

        private void OnPurchaseButtonPressed()
        {
            PurchaseButtonPressed?.Invoke(_slotIndex);
        }
    }
}