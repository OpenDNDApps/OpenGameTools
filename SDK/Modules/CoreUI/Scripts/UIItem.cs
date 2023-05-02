using System;
using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    public class UIItem : UIItemBase
    {
        [Header("UIItem Settings")] 
        [SerializeField] protected UIItemBehaviours m_itemBehaviours = UIItemBehaviours.None;
        [SerializeField] protected List<UIVisualRoot> m_visualRoots = new List<UIVisualRoot>();

        public List<UIVisualRoot> VisualRoots => m_visualRoots;

        public event Action OnShow;
        public event Action OnAnimatedShowStart;
        public event Action OnAnimatedHideStart;
        public event Action OnHide;

        protected VisualRootAnimTriggerType m_currentAnimationState = VisualRootAnimTriggerType.None;

        protected override void OnInit()
        {
            m_visualRoots.Init(this);
            base.OnInit();
        }

        protected virtual void OnEnable()
        {
            if (m_itemBehaviours.HasFlag(UIItemBehaviours.PlayOnShowAnimationOnEnable))
            {
                AnimatedShow();
            }
        }

        public virtual void AnimatedShow()
        {
            Init();
            
            if((VisualRootAnimTriggerType.AnimatedShow | VisualRootAnimTriggerType.OnShowOrEnable).HasFlag(m_currentAnimationState))
                return;

            OnAnimatedShowStart?.Invoke();
            
            List<UIVisualRoot> byTriggerType = m_visualRoots.StartAnimation(VisualRootAnimTriggerType.OnShowOrEnable, Show);
            m_currentAnimationState = VisualRootAnimTriggerType.OnShowOrEnable;
            if (byTriggerType.Count == 0)
            {
                byTriggerType = m_visualRoots.StartAnimation(VisualRootAnimTriggerType.AnimatedShow, Show);
                m_currentAnimationState = VisualRootAnimTriggerType.AnimatedShow;
            }
            if (byTriggerType.Count == 0)
            {
                Show();
            }
        }

        public virtual void Show(bool includeRoot)
        {
            if (includeRoot)
            {
                base.Activate();
            }

            Show();
        }

        public virtual void Show()
        {
            m_visualRoots.GetVisualRootsByTriggerType(VisualRootAnimTriggerType.AnimatedShow).Enable();
            m_currentAnimationState = VisualRootAnimTriggerType.None;
            OnShow?.Invoke();
        }

        public virtual void AnimatedHide()
        {
            if(m_currentAnimationState.Equals(VisualRootAnimTriggerType.AnimatedHide))
                return;

            m_currentAnimationState = VisualRootAnimTriggerType.AnimatedHide;
            
            OnAnimatedHideStart?.Invoke();
            
            List<UIVisualRoot> byTriggerType = m_visualRoots.StartAnimation(VisualRootAnimTriggerType.AnimatedHide, Hide); 
            if (byTriggerType.Count == 0)
            {
                Hide();
            }
        }
        
        public virtual void Hide(bool includeRoot)
        {
            if (includeRoot)
            {
                base.Deactivate();
            }

            Hide();
        }
        
        public virtual void Hide()
        {
            m_visualRoots.Deactivate();

            if (m_itemBehaviours.HasFlag(UIItemBehaviours.DestroyOnHide))
            {
                this.SafeDestroy(this.gameObject);
            }

            m_currentAnimationState = VisualRootAnimTriggerType.None;
            OnHide?.Invoke();
        }

        public virtual void Disable(bool softDisable = false)
        {
            Init();
            
            m_visualRoots.Disable(softDisable);
        }

        public virtual void Deactivate(bool includeRoot = false)
        {
            if (includeRoot)
            {
                base.Deactivate();
            }
            m_visualRoots.Deactivate();
        }

        public virtual void Enable(bool includeRoot = false)
        {
            Init();
            
            if (includeRoot)
            {
                base.Activate();
            }
            m_visualRoots.Enable();
        }
        
        public virtual void Activate(bool includeRoot = false)
        {
            if (includeRoot)
            {
                base.Activate();
            }
            m_visualRoots.Activate();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_visualRoots.DOKill();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
    
    [Flags]
    public enum UIItemBehaviours
    {
        None = 0,
        DestroyOnHide = 1 << 1,
        PlayOnShowAnimationOnEnable = 1 << 2,
    }
}
