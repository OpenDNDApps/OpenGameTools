using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        
        private float m_delayedChangedTimer = 0.0f;
        private bool m_onDelayedChangedTimerActive = false;

        public Action<string> OnSelected;
        public Action<string> OnDeselected;
        public Action<string> OnValueChanged;
        public Action<string> OnDelayedValueChanged;

        public TMP_InputField InputField => m_inputField;

        public string Value
        {
            get => m_inputField.text;
            set => m_inputField.text = value;
        }

        protected override void OnInit()
        {
            SetLabel(m_localizationKey);
            SetPlaceholder(m_placeHolderLocalizationKey);

            m_inputField.onSelect.AddListener(HandleOnSelect);
            m_inputField.onDeselect.AddListener(HandleOnDeselect);
            m_inputField.onValueChanged.AddListener(HandleOnValueChanged);
        }

        private void HandleOnSelect(string _)
        {
            OnSelected?.Invoke(_);
        }
        
        private void HandleOnDeselect(string _)
        {
            OnDeselected?.Invoke(_);
        }

        private void HandleOnValueChanged(string p_newValue)
        {
            OnValueChanged?.Invoke(p_newValue);
            m_delayedChangedTimer = GameResources.Settings.UI.Default.DelayedTimeOnUIInputOnValueChanged;
            m_onDelayedChangedTimerActive = true;
            GameRuntime.ManualTickUpdater += HandleOnDelayedValueChangeTick;
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
            GameRuntime.ManualTickUpdater -= HandleOnDelayedValueChangeTick;
        }

        public void SetLabel(string newLabel) => m_label.SetLocalizedText(newLabel);
        public void SetPlaceholder(string newPlaceholder) => m_placeholder.SetLocalizedText(newPlaceholder);
    }
}
