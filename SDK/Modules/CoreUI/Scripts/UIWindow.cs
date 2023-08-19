using System;
using UnityEngine;

namespace OGT
{
    public class UIWindow : UIContentSection
    {
        [SerializeField] protected UIWindowBehaviours m_uiWindowBehaviours = UIWindowBehaviours.AutoHideWhenCloseCalled;
        [SerializeField] protected UISectionType m_uiSectionType = UISectionType.Default;
        [SerializeField] protected UIButton m_closeButton;
        
        [SerializeField] private InputBlockerSettings m_inputBlockerSettings;

        public UISectionType SectionType => m_uiSectionType;
        public InputBlockerSettings InputBlocker => m_inputBlockerSettings;

        public event Action OnCloseTrigger;
        
        protected override void OnInit()
        {
            base.OnInit();
            
            if (m_closeButton != default)
            {
                m_closeButton.OnClick -= OnCloseButtonClick;
                m_closeButton.OnClick += OnCloseButtonClick;
            }

            if (m_inputBlockerSettings.AutoGenerate)
            {
                TryAddInputBlocker(m_inputBlockerSettings.Prefab);
            }
        }

        public virtual void OnCloseButtonClick()
        {
            if(m_uiWindowBehaviours.HasFlag(UIWindowBehaviours.AutoHideWhenCloseCalled))
                AnimatedHide();
            
            OnCloseTrigger?.Invoke();
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

        private bool TryAddInputBlocker(UIInputBlocker inputBlocker = null)
        {
            if (inputBlocker == default)
                inputBlocker = GameResources.Settings.UI.Default.InputBlocker;

            if (m_inputBlockerSettings.Instance != default)
                return false;
            
            m_inputBlockerSettings.Instance = Instantiate(inputBlocker, RectTransform);
            return true;
        }
    }

    [Serializable]
    public struct InputBlockerSettings
    {
        public bool AutoGenerate;
        public UIInputBlocker Prefab;
        public UIInputBlocker Instance;
        public InputBlockClickBehaviour ClickBehaviour;
    }
        
    public enum InputBlockClickBehaviour
    {
        None,
        Hide,
        AnimatedHide,
        Show,
        AnimatedShow,
    }

    [Flags]
    public enum UIWindowBehaviours
    {
        None,
        AutoHideWhenCloseCalled = 1 << 0,
    }
}