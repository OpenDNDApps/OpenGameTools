using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OGT
{
    public class UIButton : UIItem, IConditionableUIItem, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI Button")]
        [SerializeField] protected Button m_button;
        [SerializeField] protected TMP_Text m_label;
        [SerializeField] protected Sprite m_icon;
        [SerializeField] protected string m_localizationKey;
        
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
            base.OnInit();
            
            if (m_button == default)
                m_button = GetComponent<Button>();

            m_button.onClick.AddListener(TryDoClickBehaviour);

            if (!string.IsNullOrEmpty(m_localizationKey))
            {
                SetLabel(m_localizationKey);
            }

            if (m_label != default)
            {
                m_defaultFontSize = m_label.fontSize;
                m_originalText = m_label.text;
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
            OnTabButtonClick?.Invoke(m_tabSection);
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

        protected virtual void SetStyleAsHighlighted()
        {
            SetLabel($"<b>{m_originalText}</b>");
            if (m_label != default)
            {
                m_label.fontSize = m_highlightFontSize;
            }
        }

        protected virtual void SetStyleAsDefault()
        {
            SetLabel(m_originalText);
            if (m_label != default)
            {
                m_label.fontSize = m_defaultFontSize;
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_currentAnimationState != VisualRootAnimTriggerType.None)
                return;
            
            m_visualRoots.HandleOnPointerClick(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_currentAnimationState != VisualRootAnimTriggerType.None)
                return;
            
            m_visualRoots.HandleOnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_currentAnimationState != VisualRootAnimTriggerType.None)
                return;
            
            m_visualRoots.HandleOnPointerExit(eventData);
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