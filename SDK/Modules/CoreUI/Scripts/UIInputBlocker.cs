using System;
using UnityEngine.EventSystems;

namespace OGT
{
    using static UIWindow;
    
    public class UIInputBlocker : UIItem, IPointerClickHandler
    {
        private UIWindow m_owner;

        protected override void OnInit()
        {
            m_owner = GetComponentInParent<UIWindow>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_owner == null)
                return;

            OnInputBlockerClick();
        }

        public virtual void OnInputBlockerClick()
        {
            if (m_owner == null)
                return;
            
            switch (m_owner.InputBlockerBehaviour)
            {
                case InputBlockClickBehaviour.Hide:
                    m_owner.Hide();
                    return;
                case InputBlockClickBehaviour.AnimatedHide:
                    m_owner.AnimatedHide();
                    return;
                case InputBlockClickBehaviour.Show:
                    m_owner.Show();
                    return;
                case InputBlockClickBehaviour.AnimatedShow:
                    m_owner.AnimatedShow();
                    return;
                case InputBlockClickBehaviour.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}