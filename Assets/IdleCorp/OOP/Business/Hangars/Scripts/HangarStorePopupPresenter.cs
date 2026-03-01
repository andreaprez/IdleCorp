using System.Collections.Generic;
using IdleCorp.OOP.Business.Input;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events.Currencies;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Hangars;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarStorePopupPresenter : PopupPresenter<HangarStorePopupModel, HangarStorePopupView>
    {
        private HangarsService _hangarsService;
        private CurrenciesService _currenciesService;

        private int _hangarPositionId;
        private int _firstHangarIdToShow;

        public override void Initialize()
        {
            base.Initialize();
            _hangarsService = ServiceLocator.GetService<HangarsService>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();

            EventsService.GetEvent<OpenHangarStoreEvent>().AddListener(OpenPopup);
        }

        public override void SetModel()
        {
            Model = new HangarStorePopupModel();
        }

        public override void SetView()
        {
            View = SceneReferencesHolder.Instance.HangarStorePopup;
        }

        public override bool ShouldOpenPopup(WorldInteractableTag worldInteractable)
        {
            return true;
        }

        public override void SetListeners()
        {
            View.CloseButtonPressed += ClosePopup;
            View.PurchaseButtonPressed += HandlePurchase;

            EventsService.GetEvent<FundsChangedEvent>().AddListener(HandleFundsChanged);
        }

        public override void BindViewToModel()
        {
            Model.HangarPurchaseSlotModels.ValueChanged += View.SetPurchaseSlots;
        }

        public override void UpdateModel()
        {
            var slotModelsList = new List<HangarPurchaseSlotModel>();

            var allHangarsConfig = _hangarsService.GetHangarConfigList();
            for (var i = _firstHangarIdToShow; i < allHangarsConfig.Count; i++)
            {
                var purchaseSlot = new HangarPurchaseSlotModel
                {
                    HangarImage = allHangarsConfig[i].Image,
                    NameText = allHangarsConfig[i].Name,
                    CapacityText = allHangarsConfig[i].Capacity.ToString(),
                    CostText = allHangarsConfig[i].Cost.ToString(),
                    IsButtonEnabled = _currenciesService.GetFunds() >= allHangarsConfig[i].Cost
                };
                slotModelsList.Add(purchaseSlot);
            }

            Model.HangarPurchaseSlotModels.Value = slotModelsList;
        }

        public override void ClearListeners()
        {
            View.CloseButtonPressed -= ClosePopup;
            View.PurchaseButtonPressed += HandlePurchase;

            Model.HangarPurchaseSlotModels.ValueChanged -= View.SetPurchaseSlots;

            EventsService.GetEvent<FundsChangedEvent>().RemoveListener(HandleFundsChanged);
        }

        private void OpenPopup(int hangarPositionId, int currentPurchasedHangarId)
        {
            _hangarPositionId = hangarPositionId;
            _firstHangarIdToShow = currentPurchasedHangarId + 1;
            TryOpenPopup(WorldInteractableTag.None);
        }

        private void HandleFundsChanged(int currentFunds)
        {
            UpdateModel();
        }

        private void HandlePurchase(int slotIndex)
        {
            var purchasedHangarId = _firstHangarIdToShow + slotIndex;
            _firstHangarIdToShow = purchasedHangarId + 1;
            EventsService.GetEvent<HangarBuiltEvent>().Trigger(_hangarPositionId, purchasedHangarId);
            ClosePopup();
        }
    }
}