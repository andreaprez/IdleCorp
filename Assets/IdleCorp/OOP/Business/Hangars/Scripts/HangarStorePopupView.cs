using System;
using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarStorePopupView : PopupView
    {
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private HangarPurchaseSlotView purchaseSlotPrefab;

        [SerializeField]
        private Transform purchaseSlotsContainer;

        private List<HangarPurchaseSlotView> _purchaseSlots;

        public event Action CloseButtonPressed;
        public event Action<int> PurchaseButtonPressed;

        private void Start()
        {
            _purchaseSlots = new List<HangarPurchaseSlotView>();
        }

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

        public void SetPurchaseSlots(List<HangarPurchaseSlotModel> slotModels)
        {
            var newSlots = new List<HangarPurchaseSlotView>();

            for (var i = 0; i < slotModels.Count; i++)
            {
                if (i < _purchaseSlots.Count)
                {
                    _purchaseSlots[i].SetHangarInfo(slotModels[i].HangarImage, slotModels[i].NameText, slotModels[i].CapacityText, slotModels[i].CostText);
                    _purchaseSlots[i].SetButton(slotModels[i].IsButtonEnabled);
                }
                else
                {
                    var slot = Instantiate(purchaseSlotPrefab, purchaseSlotsContainer);
                    slot.SetIndex(i);
                    slot.SetHangarInfo(slotModels[i].HangarImage, slotModels[i].NameText, slotModels[i].CapacityText, slotModels[i].CostText);
                    slot.SetButton(slotModels[i].IsButtonEnabled);
                    slot.PurchaseButtonPressed += PurchaseButtonPressed;
                    newSlots.Add(slot);
                }
            }

            if (newSlots.Count > 0)
            {
                _purchaseSlots.AddRange(newSlots);
            }
            else if (_purchaseSlots.Count > slotModels.Count)
            {
                var excessSlots = _purchaseSlots.Count - slotModels.Count;
                for (var i = 1; i <= excessSlots; i++)
                {
                    Destroy(_purchaseSlots[_purchaseSlots.Count - i].gameObject);
                }
                _purchaseSlots.RemoveRange(slotModels.Count, excessSlots);
            }
        }

        private void OnCloseButtonPressed()
        {
            CloseButtonPressed?.Invoke();
        }
    }
}