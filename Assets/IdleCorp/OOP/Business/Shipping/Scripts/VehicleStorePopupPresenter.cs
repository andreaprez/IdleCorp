using System.Collections.Generic;
using IdleCorp.OOP.Business.Input;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Currencies;
using IdleCorp.OOP.Services.Events.Currencies;
using IdleCorp.OOP.Services.Events.Shipping;
using IdleCorp.OOP.Services.Shipping;

namespace IdleCorp.OOP.Business.Shipping
{
    public class VehicleStorePopupPresenter : PopupPresenter<VehicleStorePopupModel, VehicleStorePopupView>
    {
        private ShippingService _shippingService;
        private CurrenciesService _currenciesService;

        private int _vehiclePositionId;
        private int _firstVehicleIdToShow;

        public override void Initialize()
        {
            base.Initialize();
            _shippingService = ServiceLocator.GetService<ShippingService>();
            _currenciesService = ServiceLocator.GetService<CurrenciesService>();

            EventsService.GetEvent<OpenVehicleStoreEvent>().AddListener(OpenPopup);
        }

        public override void SetModel()
        {
            Model = new VehicleStorePopupModel();
        }

        public override void SetView()
        {
            View = SceneReferencesHolder.Instance.VehicleStorePopup;
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
            Model.VehiclePurchaseSlotModels.ValueChanged += View.SetPurchaseSlots;
        }

        public override void UpdateModel()
        {
            var slotModelsList = new List<VehiclePurchaseSlotModel>();

            var allVehiclesConfig = _shippingService.GetVehicleConfigList();
            for (var i = _firstVehicleIdToShow; i < allVehiclesConfig.Count; i++)
            {
                var purchaseSlot = new VehiclePurchaseSlotModel
                {
                    VehicleImage = allVehiclesConfig[i].Image,
                    NameText = allVehiclesConfig[i].Name,
                    ShippingRateText = allVehiclesConfig[i].ShippingRate.ToString(),
                    CostText = allVehiclesConfig[i].Cost.ToString(),
                    IsButtonEnabled = _currenciesService.GetFunds() >= allVehiclesConfig[i].Cost
                };
                slotModelsList.Add(purchaseSlot);
            }

            Model.VehiclePurchaseSlotModels.Value = slotModelsList;
        }

        public override void ClearListeners()
        {
            View.CloseButtonPressed -= ClosePopup;
            View.PurchaseButtonPressed += HandlePurchase;

            Model.VehiclePurchaseSlotModels.ValueChanged -= View.SetPurchaseSlots;

            EventsService.GetEvent<FundsChangedEvent>().RemoveListener(HandleFundsChanged);
        }

        private void OpenPopup(int vehiclePositionId, int currentPurchasedVehicleId)
        {
            _vehiclePositionId = vehiclePositionId;
            _firstVehicleIdToShow = currentPurchasedVehicleId + 1;
            TryOpenPopup(WorldInteractableTag.None);
        }

        private void HandleFundsChanged(int currentFunds)
        {
            UpdateModel();
        }

        private void HandlePurchase(int slotIndex)
        {
            var purchasedVehicleId = _firstVehicleIdToShow + slotIndex;
            _firstVehicleIdToShow = purchasedVehicleId + 1;
            EventsService.GetEvent<VehiclePurchasedEvent>().Trigger(_vehiclePositionId, purchasedVehicleId);
            ClosePopup();
        }
    }
}