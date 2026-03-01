using System;
using System.Collections.Generic;
using IdleCorp.OOP.Business.UI;
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

        private void OnCloseButtonPressed()
        {
            CloseButtonPressed?.Invoke();
        }
    }
}