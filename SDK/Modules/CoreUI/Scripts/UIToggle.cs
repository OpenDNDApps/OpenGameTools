using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OGT
{
    public class UIToggle : UIItem
    {
        [SerializeField] protected Toggle m_toggle;
        [SerializeField] protected TMP_Text m_label;
        [SerializeField] private string m_localizationKey;

        public Action<bool> OnValueChanged;

        public Toggle Toggle
        {
            get
            {
                if (m_toggle == null)
                    m_toggle = GetComponent<Toggle>();

                return m_toggle;
            }
        }
        
        public bool IsOn
        {
            get => Toggle.isOn;
            set => Toggle.isOn = value;
        }

        protected override void OnInit()
        {
            if (m_toggle == null)
            {
                m_toggle = GetComponent<Toggle>();
            }

            if (m_toggle != null)
            {
                m_toggle.onValueChanged.AddListener(HandleOnValueChanged);
            }

            if (!string.IsNullOrEmpty(m_localizationKey))
            {
                SetLabel(m_localizationKey);
            }
        }

        public void SetLabel(string key)
        {
            if (m_label == null || string.IsNullOrEmpty(key))
                return;

            m_label.SetLocalizedText(key);
        }

        public virtual void HandleOnValueChanged(bool newValue)
        {
            OnValueChanged?.Invoke(newValue);
        }
    }
}