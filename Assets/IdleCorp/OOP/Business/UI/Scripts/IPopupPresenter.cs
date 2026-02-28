using IdleCorp.OOP.Business.Input;

namespace IdleCorp.OOP.Business.UI
{
    public interface IPopupPresenter
    {
        public void SetModel();
        public void SetView();
        public bool ShouldOpenPopup(WorldInteractableTag worldInteractable);
        public void SetListeners();
        public void BindViewToModel();
        public void UpdateModel();
        public void ClearListeners();
        public void Initialize();
        public void TryOpenPopup(WorldInteractableTag worldInteractable);
        public void ClosePopup();
    }
}