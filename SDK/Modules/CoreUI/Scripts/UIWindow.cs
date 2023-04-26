using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace OGT
{
    public class UIWindow : UIContentSection
    {
        [Header("Generic Window Settings")]
        [SerializeField] protected UIWindowBehaviours m_uiWindowBehaviours = UIWindowBehaviours.AutoHideWhenCloseCalled;
        [SerializeField] protected UISectionType m_uiSectionType = UISectionType.Default;
        [SerializeField] protected UIButton m_closeButton;
        
        [Header("Notch Settings")]
        [SerializeField] protected NotchBehaviour m_notchBehaviour = NotchBehaviour.Ignore;
        
        [Header("Popup Settings")] 
        [SerializeField] protected bool m_autoGenerateInputBlocker = false;
        [SerializeField] protected UIInputBlocker m_inputBlocker;
        [SerializeField] protected InputBlockClickBehaviour m_inputBlockClickBehaviour = InputBlockClickBehaviour.AnimatedHide;

        public UIWindowBehaviours WindowBehaviours => m_uiWindowBehaviours;
        public UISectionType SectionType => m_uiSectionType;
        public NotchBehaviour NotchBehaviour => m_notchBehaviour;
        public InputBlockClickBehaviour InputBlockerBehaviour => m_inputBlockClickBehaviour;
        public bool IsTopLevelWindow => this.Canvas.transform == transform.parent;

        public event Action OnCloseTrigger;
        
        protected override void OnInit()
        {
            base.OnInit();
            
            if (m_closeButton != null)
            {
                m_closeButton.OnClick = OnCloseButtonClick;
            }

            if (m_autoGenerateInputBlocker && m_inputBlocker == null)
            {
                m_inputBlocker = AddInputBlocker(this);
            }

            this.ApplySafeArea();
        }

        public virtual void OnCloseButtonClick()
        {
            if(m_uiWindowBehaviours.HasFlag(UIWindowBehaviours.AutoHideWhenCloseCalled))
                AnimatedHide();
            
            OnCloseTrigger?.Invoke();
        }
        
        public override void Show()
        {
            base.Show();
            if (m_inputBlocker != null)
            {
                m_inputBlocker.Enable();
            }
        }
    
        public override void AnimatedShow()
        {
            if (m_inputBlocker != null)
            {
                m_inputBlocker.AnimatedShow();
            }
            base.AnimatedShow();
        }

        public override void Hide()
        {
            base.Hide();
            m_visualRoots.Disable();
            if (m_inputBlocker != null)
            {
                m_inputBlocker.Disable();
            }
        }
        
        public override void AnimatedHide()
        {
            base.AnimatedHide();
            if (m_inputBlocker != null)
            {
                m_inputBlocker.AnimatedHide();
            }
        }

        protected override void OnDestroy()
        {
            UIRuntime.NotifyWindowDestroy(this);
        }

        public void SetVerticalPosition(float bottomToTop = 0.5f, bool changePivot = true)
        {
            foreach (UIVisualRoot visualRoot in m_visualRoots)
            {
                RectTransform rectTransform = (RectTransform) visualRoot.transform;
                rectTransform.SetVerticalPosition(bottomToTop, changePivot);
            }
        }
        
        public UIInputBlocker AddInputBlocker(UIWindow window, UIInputBlocker inputBlocker = null)
        {
            if (inputBlocker == null)
                inputBlocker = GameResources.Settings.UI.Default.InputBlocker;
            
            UIInputBlocker newInputBlocker = Instantiate(inputBlocker, window.VisualRoots.First().transform);
            var ibTransform = (RectTransform)newInputBlocker.transform;
            ibTransform.localPosition = Vector3.zero;
            ibTransform.localScale = Vector3.one;
            ibTransform.SetAsFirstSibling();
            newInputBlocker.Disable();

            return newInputBlocker;
        }

        public virtual void SetInputBlockerBehaviour(InputBlockClickBehaviour newBehaviour)
        {
            m_inputBlockClickBehaviour = newBehaviour;
        }

        public virtual void OnInputBlockerClick()
        {
            switch (m_inputBlockClickBehaviour)
            {
                case InputBlockClickBehaviour.Hide:
                    Hide();
                    return;
                case InputBlockClickBehaviour.AnimatedHide:
                    AnimatedHide();
                    return;
                case InputBlockClickBehaviour.Show:
                    Show();
                    return;
                case InputBlockClickBehaviour.AnimatedShow:
                    AnimatedShow();
                    return;
                default:
                case InputBlockClickBehaviour.None:
                    break;
            }
        }
        
        public enum InputBlockClickBehaviour
        {
            None,
            Hide,
            AnimatedHide,
            Show,
            AnimatedShow,
        }
    }

    [Flags]
    public enum UIWindowBehaviours
    {
        None,
        AutoHideWhenCloseCalled = 1 << 0,
    }
}