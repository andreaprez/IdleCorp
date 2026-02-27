using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarSlotView : MonoBehaviour
    {
        [SerializeField]
        private GameObject emptySlotDisplay;

        [SerializeField]
        private GameObject usedSlotDisplay;

        [SerializeField]
        private Image hangarImage;

        [SerializeField]
        private TextMeshProUGUI capacityText;

        [SerializeField]
        private Slider capacityBar;

        [SerializeField]
        private Button buildButton;

        [SerializeField]
        private Button upgradeButton;

        public event Action<int> BuildButtonPressed;
        public event Action<int> UpgradeButtonPressed;

        private int _slotIndex;

        public void SetIndex(int index)
        {
            _slotIndex = index;
        }

        public void SetEmpty()
        {
            emptySlotDisplay.SetActive(true);
            usedSlotDisplay.SetActive(false);
        }

        public void SetUsed()
        {
            emptySlotDisplay.SetActive(false);
            usedSlotDisplay.SetActive(true);
        }

        public void SetImage(Sprite image)
        {
            hangarImage.sprite = image;
        }

        public void SetCapacityText(string capacity)
        {
            capacityText.text = capacity;
        }

        public void SetCapacityBar(float capacityBarValue)
        {
            capacityBar.value = capacityBarValue;
        }

        private void OnEnable()
        {
            buildButton.onClick.AddListener(OnBuildButtonPressed);
            upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
        }

        private void OnDisable()
        {
            buildButton.onClick.RemoveListener(OnBuildButtonPressed);
            upgradeButton.onClick.RemoveListener(OnUpgradeButtonPressed);
        }

        private void OnBuildButtonPressed()
        {
            BuildButtonPressed?.Invoke(_slotIndex);
        }

        private void OnUpgradeButtonPressed()
        {
            UpgradeButtonPressed?.Invoke(_slotIndex);
        }
    }
}