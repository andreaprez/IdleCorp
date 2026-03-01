using IdleCorp.OOP.Business.Input;
using IdleCorp.OOP.Business.UI;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events.Factory;
using IdleCorp.OOP.Services.Events.Hangars;
using IdleCorp.OOP.Services.Events.Input;
using IdleCorp.OOP.Services.Hangars;

namespace IdleCorp.OOP.Business.Hangars
{
    public class HangarsPopupPresenter : PopupPresenter<HangarsPopupModel, HangarsPopupView>
    {
        private HangarsService _hangarsService;

        public override void Initialize()
        {
            base.Initialize();
            _hangarsService = ServiceLocator.GetService<HangarsService>();

            EventsService.GetEvent<InputTappedOnWorldInteractableEvent>().AddListener(TryOpenPopup);
        }

        public override void SetModel()
        {
            Model = new HangarsPopupModel();
        }

        public override void SetView()
        {
            View = SceneReferencesHolder.Instance.HangarsPopup;
        }

        public override bool ShouldOpenPopup(WorldInteractableTag worldInteractable)
        {
            return worldInteractable == WorldInteractableTag.Hangars;
        }

        public override void SetListeners()
        {
            View.CloseButtonPressed += ClosePopup;
            foreach (var hangarSlotView in View.HangarSlots)
            {
                hangarSlotView.BuildButtonPressed += HandleSlotBuild;
                hangarSlotView.UpgradeButtonPressed += HandleSlotUpgrade;
            }

            EventsService.GetEvent<RobotProducedEvent>().AddListener(HandleRobotCountChanged);
            EventsService.GetEvent<HangarBuiltEvent>().AddListener(HandleHangarBuilt);
        }

        public override void BindViewToModel()
        {
            Model.CapacityText.ValueChanged += View.SetCapacityText;
            Model.CapacityBarValue.ValueChanged += View.SetCapacityBar;
            for (var i = 0; i < Model.HangarSlotModels.Count; i++)
            {
                var slotModel = Model.HangarSlotModels[i];
                slotModel.CapacityText.ValueChanged += View.HangarSlots[i].SetCapacityText;
                slotModel.CapacityBarValue.ValueChanged += View.HangarSlots[i].SetCapacityBar;
                slotModel.HangarImage.ValueChanged += View.HangarSlots[i].SetImage;
                slotModel.IsUsed.ValueChanged += View.HangarSlots[i].SetUsed;
            }
        }

        public override void UpdateModel()
        {
            var usedCapacity = _hangarsService.GetTotalRobotCount();
            var maxCapacity = _hangarsService.GetMaxCapacity();
            Model.CapacityText.Value = $"{usedCapacity}/{maxCapacity}";
            Model.CapacityBarValue.Value = (float)usedCapacity/maxCapacity;

            for (var i = 0; i < Model.HangarSlotModels.Count; i++)
            {
                if (_hangarsService.IsHangarBuilt(i))
                {
                    var hangarUsedCapacity = _hangarsService.GetHangarUsedCapacity(i);
                    var hangarMaxCapacity = _hangarsService.GetHangarMaxCapacity(i);
                    Model.HangarSlotModels[i].CapacityText.Value = $"{hangarUsedCapacity}/{hangarMaxCapacity}";
                    Model.HangarSlotModels[i].CapacityBarValue.Value = (float)hangarUsedCapacity/hangarMaxCapacity;
                    Model.HangarSlotModels[i].HangarImage.Value = _hangarsService.GetHangarImage(i);
                    Model.HangarSlotModels[i].IsUsed.Value = true;
                    continue;
                }
                Model.HangarSlotModels[i].IsUsed.Value = false;
            }
        }

        public override void ClearListeners()
        {
            View.CloseButtonPressed -= ClosePopup;
            foreach (var hangarSlotView in View.HangarSlots)
            {
                hangarSlotView.BuildButtonPressed -= HandleSlotBuild;
                hangarSlotView.UpgradeButtonPressed -= HandleSlotUpgrade;
            }

            Model.CapacityText.ValueChanged -= View.SetCapacityText;
            Model.CapacityBarValue.ValueChanged -= View.SetCapacityBar;
            for (var i = 0; i < Model.HangarSlotModels.Count; i++)
            {
                var slotModel = Model.HangarSlotModels[i];
                slotModel.HangarImage.ValueChanged -= View.HangarSlots[i].SetImage;
                slotModel.CapacityText.ValueChanged -= View.HangarSlots[i].SetCapacityText;
                slotModel.CapacityBarValue.ValueChanged -= View.HangarSlots[i].SetCapacityBar;
                slotModel.IsUsed.ValueChanged -= View.HangarSlots[i].SetUsed;
            }

            EventsService.GetEvent<RobotProducedEvent>().RemoveListener(HandleRobotCountChanged);
            EventsService.GetEvent<HangarBuiltEvent>().RemoveListener(HandleHangarBuilt);
        }

        private void HandleSlotBuild(int slotIndex)
        {
            var currentHangarId = _hangarsService.GetHangarId(slotIndex);
            EventsService.GetEvent<OpenHangarStoreEvent>().Trigger(slotIndex, currentHangarId);
        }

        private void HandleSlotUpgrade(int slotIndex)
        {
            var currentHangarId = _hangarsService.GetHangarId(slotIndex);
            EventsService.GetEvent<OpenHangarStoreEvent>().Trigger(slotIndex, currentHangarId);
        }

        private void HandleRobotCountChanged(int amountAdded)
        {
            UpdateModel();
        }

        private void HandleHangarBuilt(int positionId, int hangarId)
        {
            UpdateModel();
        }
    }
}
