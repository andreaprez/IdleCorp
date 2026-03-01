using IdleCorp.OOP.Business.UI.Animations;
using UnityEngine;

namespace IdleCorp.OOP.Business.UI
{
    public class PopupView : MonoBehaviour, IPopupView
    {
        [SerializeField]
        private Animator animator;

        public virtual void Open()
        {
            animator.SetTrigger(AnimationTriggerKeys.POPUP_OPEN);
        }

        public virtual void Close()
        {
            animator.SetTrigger(AnimationTriggerKeys.POPUP_CLOSE);
        }
    }
}