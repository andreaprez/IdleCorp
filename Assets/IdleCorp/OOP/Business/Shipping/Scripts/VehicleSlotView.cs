using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Shipping
{
    public class VehicleSlotView : MonoBehaviour
    {
        [SerializeField]
        private GameObject emptySlotDisplay;

        [SerializeField]
        private GameObject usedSlotDisplay;

        [SerializeField]
        private Image vehicleImage;

        [SerializeField]
        private TextMeshProUGUI shippingRateText;

        [SerializeField]
        private Slider shippingRateBar;

        [SerializeField]
        private Button purchaseButton;

        [SerializeField]
        private Button upgradeButton;

        public event Action<int> PurchaseButtonPressed;
        public event Action<int> UpgradeButtonPressed;

        private int _slotIndex;

        public void SetIndex(int index)
        {
            _slotIndex = index;
        }

        public void SetUsed(bool isUsed)
        {
            emptySlotDisplay.SetActive(!isUsed);
            usedSlotDisplay.SetActive(isUsed);
        }

        public void SetImage(Sprite image)
        {
            vehicleImage.sprite = image;
        }

        public void SetShippingRateText(string shippingRate)
        {
            shippingRateText.text = shippingRate;
        }

        public void SetShippingRateBar(float shippingRateBarValue)
        {
            shippingRateBar.value = shippingRateBarValue;
        }

        private void OnEnable()
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonPressed);
            upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
        }

        private void OnDisable()
        {
            purchaseButton.onClick.RemoveListener(OnPurchaseButtonPressed);
            upgradeButton.onClick.RemoveListener(OnUpgradeButtonPressed);
        }

        private void OnPurchaseButtonPressed()
        {
            PurchaseButtonPressed?.Invoke(_slotIndex);
        }

        private void OnUpgradeButtonPressed()
        {
            UpgradeButtonPressed?.Invoke(_slotIndex);
        }
    }
}