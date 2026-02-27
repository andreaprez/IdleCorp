using System;
using System.Collections.Generic;
using IdleCorp.OOP.Business.UI.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarsPopupView : PopupView
    {
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private TextMeshProUGUI capacityText;

        [SerializeField]
        private Slider capacityBar; 

        [SerializeField]
        private List<HangarSlotView> hangarSlots;

        public List<HangarSlotView> HangarSlots => hangarSlots;

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
            for (var i = 0; i < hangarSlots.Count; i++)
            {
                HangarSlots[i].SetIndex(i);
            }
        }

        public void SetCapacityText(string text)
        {
            capacityText.text = text;
        }

        public void SetCapacityBar(float barValue)
        {
            capacityBar.value = barValue;
        }

        public void SetSlotEmpty(int slotIndex)
        {
            HangarSlots[slotIndex].SetEmpty();
        }

        public void SetSlotUsed(int slotIndex)
        {
            HangarSlots[slotIndex].SetUsed();
        }

        public void SetSlotImage(int slotIndex, Sprite image)
        {
            HangarSlots[slotIndex].SetImage(image);
        }

        public void SetSlotCapacityText(int slotIndex, string capacity)
        {
            HangarSlots[slotIndex].SetCapacityText(capacity);
        }

        public void SetSlotCapacityBar(int slotIndex, float capacityBarValue)
        {
            HangarSlots[slotIndex].SetCapacityBar(capacityBarValue);
        }

        private void OnCloseButtonPressed()
        {
            CloseButtonPressed?.Invoke();
        }
    }
}