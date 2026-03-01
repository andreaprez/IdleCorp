using IdleCorp.OOP.Business.Input;
using IdleCorp.OOP.Services;
using IdleCorp.OOP.Services.Events;
using IdleCorp.OOP.Services.Events.UI;

namespace IdleCorp.OOP.Business.UI
{
    public abstract class PopupPresenter<TPopupModel, TPopupView> : IPopupPresenter where TPopupModel : IPopupModel where TPopupView : IPopupView
    {
        protected TPopupModel Model;
        protected TPopupView View;

        protected EventsService EventsService;

        public abstract void SetModel();
        public abstract void SetView();
        public abstract bool ShouldOpenPopup(WorldInteractableTag worldInteractable);
        public abstract void SetListeners();
        public abstract void BindViewToModel();
        public abstract void UpdateModel();
        public abstract void ClearListeners();

        public virtual void Initialize()
        {
            SetModel();
            SetView();

            EventsService = ServiceLocator.GetService<EventsService>();
        }

        public virtual void TryOpenPopup(WorldInteractableTag worldInteractable)
        {
            if (!ShouldOpenPopup(worldInteractable))
                return;

            SetListeners();
            BindViewToModel();
            UpdateModel();

            View.Open();

            EventsService.GetEvent<PopupOpenedEvent>().Trigger();
        }

        public virtual void ClosePopup()
        {
            ClearListeners();
            View.Close();

            EventsService.GetEvent<PopupClosedEvent>().Trigger();
        }
    }
}