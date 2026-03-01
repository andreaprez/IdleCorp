using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarPurchaseSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image hangarImage;

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI capacityText;

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

        public void SetHangarInfo(Sprite image, string name, string capacity, string cost)
        {
            hangarImage.sprite = image;
            nameText.text = name;
            capacityText.text = capacity;
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