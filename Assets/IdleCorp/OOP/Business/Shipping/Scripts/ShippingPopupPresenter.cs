using IdleCorp.OOP.Business.Input;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Events.Input;
using IdleCorp.OOP.Services.Events.Shipping;
using IdleCorp.OOP.Services.Hangars;
using IdleCorp.OOP.Services.Shipping;

namespace IdleCorp.OOP.Business.Shipping
{
    public class ShippingPopupPresenter : PopupPresenter<ShippingPopupModel, ShippingPopupView>
    {
        private ShippingService _shippingService;
        private HangarsService _hangarsService;

        public override void Initialize()
        {
            base.Initialize();
            _shippingService = ServiceLocator.GetService<ShippingService>();
            _hangarsService = ServiceLocator.GetService<HangarsService>();

            EventsService.GetEvent<InputTappedOnWorldInteractableEvent>().AddListener(TryOpenPopup);
        }

        public override void SetModel()
        {
            Model = new ShippingPopupModel();
        }

        public override void SetView()
        {
            View = SceneReferencesHolder.Instance.ShippingPopup;
        }

        public override bool ShouldOpenPopup(WorldInteractableTag worldInteractable)
        {
            return worldInteractable == WorldInteractableTag.ShippingDepot;
        }

        public override void SetListeners()
        {
            View.CloseButtonPressed += ClosePopup;
            foreach (var vehicleSlotView in View.VehicleSlots)
            {
                vehicleSlotView.PurchaseButtonPressed += HandleSlotPurchase;
                vehicleSlotView.UpgradeButtonPressed += HandleSlotUpgrade;
            }

            EventsService.GetEvent<ProductionRateChangedEvent>().AddListener(HandleEggProductionRateChanged);
            EventsService.GetEvent<VehiclePurchasedEvent>().AddListener(HandleVehiclePurchased);
        }

        public override void BindViewToModel()
        {
            Model.ShippingRateText.ValueChanged += View.SetShippingRateText;
            Model.ShippingRateBarValue.ValueChanged += View.SetShippingRateBar;
            for (var i = 0; i < Model.VehicleSlotModels.Count; i++)
            {
                var slotModel = Model.VehicleSlotModels[i];
                slotModel.ShippingRateText.ValueChanged += View.VehicleSlots[i].SetShippingRateText;
                slotModel.ShippingRateBarValue.ValueChanged += View.VehicleSlots[i].SetShippingRateBar;
                slotModel.VehicleImage.ValueChanged += View.VehicleSlots[i].SetImage;
                slotModel.IsUsed.ValueChanged += View.VehicleSlots[i].SetUsed;
            }
        }

        public override void UpdateModel()
        {
            var productionRate = _hangarsService.GetProductionRate();
            var maxShippingRate = _shippingService.GetMaxShippingRate();
            Model.ShippingRateText.Value = $"{productionRate}/{maxShippingRate}";
            Model.ShippingRateBarValue.Value = (float)productionRate/maxShippingRate;

            for (var i = 0; i < Model.VehicleSlotModels.Count; i++)
            {
                if (_shippingService.IsVehiclePurchased(i))
                {
                    var vehicleUsedCapacity = _shippingService.GetVehicleUsedShippingRate(i);
                    var vehicleMaxCapacity = _shippingService.GetVehicleMaxShippingRate(i);
                    Model.VehicleSlotModels[i].ShippingRateText.Value = $"{vehicleUsedCapacity}/{vehicleMaxCapacity}";
                    Model.VehicleSlotModels[i].ShippingRateBarValue.Value = (float)vehicleUsedCapacity/vehicleMaxCapacity;
                    Model.VehicleSlotModels[i].VehicleImage.Value = _shippingService.GetVehicleImage(i);
                    Model.VehicleSlotModels[i].IsUsed.Value = true;
                    continue;
                }
                Model.VehicleSlotModels[i].IsUsed.Value = false;
            }
        }

        public override void ClearListeners()
        {
            View.CloseButtonPressed -= ClosePopup;
            foreach (var vehicleSlotView in View.VehicleSlots)
            {
                vehicleSlotView.PurchaseButtonPressed -= HandleSlotPurchase;
                vehicleSlotView.UpgradeButtonPressed -= HandleSlotUpgrade;
            }

            Model.ShippingRateText.ValueChanged -= View.SetShippingRateText;
            Model.ShippingRateBarValue.ValueChanged -= View.SetShippingRateBar;
            for (var i = 0; i < Model.VehicleSlotModels.Count; i++)
            {
                var slotModel = Model.VehicleSlotModels[i];
                slotModel.VehicleImage.ValueChanged -= View.VehicleSlots[i].SetImage;
                slotModel.ShippingRateText.ValueChanged -= View.VehicleSlots[i].SetShippingRateText;
                slotModel.ShippingRateBarValue.ValueChanged -= View.VehicleSlots[i].SetShippingRateBar;
                slotModel.IsUsed.ValueChanged -= View.VehicleSlots[i].SetUsed;
            }

            EventsService.GetEvent<ProductionRateChangedEvent>().RemoveListener(HandleEggProductionRateChanged);
            EventsService.GetEvent<VehiclePurchasedEvent>().RemoveListener(HandleVehiclePurchased);
        }

        private void HandleSlotPurchase(int slotIndex)
        {
            var currentVehicleId = _shippingService.GetVehicleId(slotIndex);
            EventsService.GetEvent<OpenVehicleStoreEvent>().Trigger(slotIndex, currentVehicleId);
        }

        private void HandleSlotUpgrade(int slotIndex)
        {
            var currentVehicleId = _shippingService.GetVehicleId(slotIndex);
            EventsService.GetEvent<OpenVehicleStoreEvent>().Trigger(slotIndex, currentVehicleId);
        }

        private void HandleEggProductionRateChanged(int newProductionRate)
        {
            _shippingService.RedistributeShippingRates(newProductionRate);
            UpdateModel();
        }

        private void HandleVehiclePurchased(int positionId, int vehicleId)
        {
            _shippingService.RedistributeShippingRates(_hangarsService.GetProductionRate());
            UpdateModel();
        }
    }
}