using System;
using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    public class UITabWindow : UIWindow
    {
        [SerializeField] private List<UITabSection> m_tabSections = new List<UITabSection>();
        [SerializeField] private UITabBehaviours m_tabBehaviours = UITabBehaviours.Empty;
        [SerializeField] private string m_defaultTabID;
        
        private UITabSection m_currentTabSection;
        
        public List<UITabSection> TabSections => m_tabSections;
        public UITabSection CurrentTabSection => m_currentTabSection;
        public UITabSection GetTabSectionByID(string id) => m_tabSections.GetByID(id);
        
        public Action<UITabSection, UITabSection> OnTabChanged;
        public Action<UITabSection> OnTabAnimatedShowStart;
        public Action<UITabSection> OnTabShow;
        public Action<UITabSection> OnTabAnimatedHideStart;
        public Action<UITabSection> OnTabHide;

        protected override void OnInit()
        {
            base.OnInit();
            m_tabSections.Init(this, SetTab);

            if (m_tabBehaviours.HasFlag(UITabBehaviours.OpenDefaultTabOnInit))
            {
                SetDefaultTab();
            }
        }

        public override void AnimatedHide()
        {
            base.AnimatedHide();
            if (m_visualRoots.Count > 0 && m_currentTabSection != null)
            {
                m_currentTabSection.AnimatedHide();
                m_currentTabSection = null;
            }
            if (m_visualRoots.Count == 0 && m_currentTabSection != null)
            {
                m_currentTabSection.AnimatedHide();
                m_currentTabSection.OnHide += Hide;
            }
            if (m_visualRoots.Count == 0 && m_currentTabSection == null)
            {
                Hide();
            }
        }

        public override void Hide()
        {
            base.Hide();
            if (m_currentTabSection == null) 
                return;
            
            m_currentTabSection.OnHide -= Hide;
            m_currentTabSection = null;
        }

        public void SetDefaultTab()
        {
            if (m_tabSections.Count == 0)
                return;
            
            if (string.IsNullOrEmpty(m_defaultTabID))
                m_defaultTabID = m_tabSections[0].ID;

            SetTab(m_defaultTabID);
        }

        public void SetNextTab()
        {
            if (m_tabSections.Count == 0)
                return;

            if (m_currentTabSection == null)
                SetTab(0);
            
            int currentIndex = m_tabSections.IndexOf(m_currentTabSection);
            int nextIndex = currentIndex + 1;
            if (nextIndex >= m_tabSections.Count)
                nextIndex = 0;
            
            SetTab(nextIndex);
        }

        public void SetTab(int index)
        {
            if (index < 0 || index >= m_tabSections.Count)
                return;
            
            SetTab(m_tabSections[index]);
        }

        public void SetTab(string id)
        {
            UITabSection selected = m_tabSections.GetByID(id);
            if (selected == null)
            {
                Debug.LogError($"Tab of id '{id}' not found on '{this.name}'");
                return;
            }

            SetTab(selected);
        }

        public void SetTab(UITabSection selected)
        {
            Init();
            
            if (selected == null)
                return;
            
            // Toggle Behaviour
            if (m_tabBehaviours.HasFlag(UITabBehaviours.ToggleableTabs))
            {
                if (selected.IsActive)
                {
                    selected.AnimatedShow();
                }
                else
                {
                    selected.AnimatedHide();
                }
                return;
            }
            
            // Default Behaviour
            if (selected.Equals(m_currentTabSection))
                return;

            if(m_tabBehaviours.HasFlag(UITabBehaviours.StepByStep))
            {
                int currentIndex = m_tabSections.IndexOf(selected);
                for (int i = 0; i < m_tabSections.Count; i++)
                {
                    UITabSection tab = m_tabSections[i];
                    if (i <= currentIndex)
                    {
                        tab.TabButtons.Enable();
                    }
                    else
                    {
                        tab.TabButtons.Disable(true);
                    }
                }
            }
            
            m_currentTabSection?.AnimatedHide();
            selected.AnimatedShow();

            OnTabChanged?.Invoke(m_currentTabSection, selected);
            m_currentTabSection = selected;
        }
    }
        
    [Flags]
    public enum UITabBehaviours
    {
        Empty = 0,
        OpenDefaultTabOnInit = 1 << 0,
        StepByStep = 1 << 1,
        ToggleableTabs = 1 << 2,
    }
}