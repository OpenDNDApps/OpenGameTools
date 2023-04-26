using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OGT
{
    public class UIButton : UIItem, IConditionableUIItem
    {
        [Header("UI Button")] 
        [SerializeField] private Button m_button;
        [SerializeField] private TMP_Text m_label;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private string m_localizationKey;
        
        [Header("Styling")]
        [SerializeField] protected float m_defaultFontSize;
        [SerializeField] protected float m_highlightFontSize;

        protected bool m_isHighlighted;
        protected string m_originalText;

        protected UITabSection m_tabSection;
        
        public Action<UITabSection> OnTabButtonClick;
        
        public Func<bool> Conditionable { get; set; } = () => true;
        public Action OnClick;
        public Button Button => m_button;
        
        public bool IsHighlighted
        {
            get => m_isHighlighted;
            set
            {
                Init();
                m_isHighlighted = value;
                if (value) SetStyleAsHighlighted(); else SetStyleAsDefault();
            }
        }

        protected override void OnInit()
        {
            if (m_button == null)
            {
                m_button = GetComponent<Button>();
            }

            if (m_button != null)
            {
                m_button.onClick.AddListener(TryDoClickBehaviour);
            }

            if (!string.IsNullOrEmpty(m_localizationKey))
            {
                SetLabel(m_localizationKey);
            }
        }

        public void SetLabel(string key) => m_label.SetLocalizedText(key);
        
        public virtual void TryDoClickBehaviour()
        {
            if(Conditionable != null && !Conditionable.Invoke())
                return;
            
            ClickBehaviour();
        }

        public virtual void ClickBehaviour()
        {
            OnClick?.Invoke();
        }

        public void SetupAsTabButton(UITabSection owner, Action<UITabSection> onTabButtonClick)
        {
            m_tabSection = owner;
            OnTabButtonClick = onTabButtonClick;
        }

        public void TriggerClick()
        {
            m_button.onClick.Invoke();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            m_visualRoots.StartAnimation(VisualRootAnimTriggerType.PointerClick);
        }

        protected virtual void SetStyleAsHighlighted()
        {
            SetLabel($"<b>{m_originalText}</b>");
            if (m_label != null)
            {
                m_label.fontSize = m_highlightFontSize;
            }
        }

        protected virtual void SetStyleAsDefault()
        {
            SetLabel(m_originalText);
            if (m_label != null)
            {
                m_label.fontSize = m_defaultFontSize;
            }
        }
    }

    public interface IConditionableUIItem
    {
        public Func<bool> Conditionable { get; set; }
    }
    
    public interface ISelectableUIItem<T> : IConditionableUIItem
    {
        public bool IsSelected { get; set; }
        public Action<T> OnSelected { get; set; }
        public Action<T> OnDeselected { get; set; }
        public void HandleOnSelected();
        public void HandleOnDeselected();
        public void ToggleSelection();
    }
    
    public interface IConditionedSelectableUIItem<T> : ISelectableUIItem<T>
    {
        public Action<T> OnSelectedFailed { get; set; }
    }
}