using System;
using UnityEngine;

namespace OGT
{
    public class UIContentSection : UIItem
    {
        [Header("Content Settings")]
        [SerializeField] private UIContentSectionBehaviours m_contentSectionBehaviours = UIContentSectionBehaviours.DisableOnAwake;
        
        public UIContentSectionBehaviours ContentSectionBehaviours => m_contentSectionBehaviours;

        protected override void OnInit()
        {
            base.OnInit();
            if (m_contentSectionBehaviours.HasFlag(UIContentSectionBehaviours.DisableOnAwake))
            {
                m_itemBehaviours.RemoveFlag(UIItemBehaviours.PlayOnShowAnimationOnEnable);
                m_visualRoots.Disable();
            }
        }
    }

    [Flags]
    public enum UIContentSectionBehaviours
    {
        None = 0,
        DisableOnAwake = 1 << 1,
    }
}
