using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OGT
{
    public class UIInput : UIItem
    {
        [Header("UI Input")] 
        [SerializeField] private TMP_InputField m_inputField;
        [SerializeField] private TMP_Text m_label;
        [SerializeField] private TMP_Text m_placeholder;
        
        [SerializeField] private string m_localizationKey;
        [SerializeField] private string m_placeHolderLocalizationKey;

        private bool m_selected;
        private float m_delayedChangedTimer;
        private bool m_onDelayedChangedTimerActive;

        public Action<string> OnSelected;
        public Action<string> OnDeselected;
        public Action<string> OnValueChanged;
        public Action<string> OnDelayedValueChanged;
        public Action<string> OnSubmit;

        public TMP_InputField InputField => m_inputField;

        public string Value
        {
            get => m_inputField.text;
            set => m_inputField.text = value;
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            SetLabel(m_localizationKey);
            SetPlaceholder(m_placeHolderLocalizationKey);

            m_inputField.onSelect.AddListener(HandleOnSelect);
            m_inputField.onDeselect.AddListener(HandleOnDeselect);
            m_inputField.onValueChanged.AddListener(HandleOnValueChanged);
        }

        private void Update()
        {
            TryHandleSubmit();
        }

        private void TryHandleSubmit()
        {
            if (!Input.GetKeyDown(KeyCode.Return))
                return;

            if (!m_selected)
                return;
                    
            OnSubmit?.Invoke(Value);
            Deselect();
        }

        private void HandleOnSelect(string _)
        {
            m_selected = true;
            OnSelected?.Invoke(_);
        }
        
        private void HandleOnDeselect(string _)
        {
            m_selected = false;
            OnDeselected?.Invoke(_);
        }

        private void HandleOnValueChanged(string newValue)
        {
            OnValueChanged?.Invoke(newValue);
            m_delayedChangedTimer = GameResources.Settings.UI.Default.DelayedTimeOnUIInputOnValueChanged;
            m_onDelayedChangedTimerActive = true;
            GameRuntime.OnManualTickUpdater += HandleOnDelayedValueChangeTick;
        }

        private void HandleOnDelayedValueChangeTick()
        {
            if (!m_onDelayedChangedTimerActive) 
                return;
            
            m_delayedChangedTimer -= Time.deltaTime;

            if (!(m_delayedChangedTimer <= 0.0f)) 
                return;
            
            TriggerOnDelayedValueChange();
        }

        private void TriggerOnDelayedValueChange()
        {
            m_onDelayedChangedTimerActive = false;
            OnDelayedValueChanged?.Invoke(m_inputField.text);
            GameRuntime.OnManualTickUpdater -= HandleOnDelayedValueChangeTick;
        }

        public void SetLabel(string newLabel)
        {
            if(string.IsNullOrEmpty(newLabel))
                return;
            
            m_label.SetLocalizedText(newLabel);
        }

        public void SetPlaceholder(string newPlaceholder)
        {
            if (string.IsNullOrEmpty(newPlaceholder))
                return;
            
            m_placeholder.SetLocalizedText(newPlaceholder);
        }

        public void Select() => Focus();

        public void Deselect()
        {
            EventSystem.current.SetSelectedGameObject(null, null);
        }
        
        public void Focus()
        {
            EventSystem.current.SetSelectedGameObject(m_inputField.gameObject, null);
            m_inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
    }
}
