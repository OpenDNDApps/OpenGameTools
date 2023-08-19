using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OGT
{
    public class UIGenericTextMessagePopup : UIWindow
    {
        [Header("Generic Text Message Settings")] 
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_Text m_message;
        [SerializeField] private UIButton m_primaryButton;
        [SerializeField] private UIButton m_secondaryButton;

        protected override void OnInit()
        {
            if (m_closeButton != default)
            {
                m_closeButton.Deactivate(true);
            }

            if (m_primaryButton != default)
            {
                m_primaryButton.Deactivate(true);
            }

            if (m_secondaryButton != default)
            {
                m_secondaryButton.Deactivate(true);
            }

            if (m_title != default)
            {
                m_title.Disable();
            }

            if (m_message != default)
            {
                m_message.Disable();
            }
        }

        public override void AnimatedHide()
        {
            if (m_primaryButton != default)
            {
                m_primaryButton.Disable();
            }

            if (m_secondaryButton != default)
            {
                m_secondaryButton.Disable();
            }

            base.AnimatedHide();
        }

        public void SetTitle(string titleKey)
        {
            if (m_title == null) return;

            m_title.SetLocalizedText(titleKey);
            m_title.Enable();
        }

        public void SetContent(string contentKey)
        {
            if (m_message == null) return;

            m_message.SetLocalizedText(contentKey);
            m_message.Enable();
        }

        public void SetPrimaryButton(string labelKey = "", Action onClick = null, bool cleanCallback = false)
        {
            SetupButton(m_primaryButton, labelKey, onClick);
        }

        public void SetSecondaryButton(string labelKey, Action onClick, bool cleanCallback = false)
        {
            SetupButton(m_secondaryButton, labelKey, onClick);
        }

        private void SetupButton(UIButton button, string labelKey, Action onClick, bool cleanCallback = false)
        {
            if (button == default || onClick == default) return;
            
            if (m_closeButton != default)
            {
                m_closeButton.Deactivate(true);
            }
            
            if (cleanCallback)
            {
                m_primaryButton.CleanOnClick();
            }
            
            m_secondaryButton.SetLabel(labelKey);
            m_secondaryButton.OnClick += onClick;
            m_secondaryButton.Enable(true);
        }
    }
}
