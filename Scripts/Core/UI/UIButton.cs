using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Anvil3D
{
    public class UIButton : BaseUIScript
    {
        [SerializeField] private Button m_baseButton;
        [SerializeField] private UnityEvent m_onClick;
        [SerializeField] private TMP_Text m_label;
        [SerializeField] private Image m_background;
        [SerializeField] private Image m_icon;

        protected override void Awake()
        {
            base.Awake();
            
            if (m_onClick != null)
            {
                m_baseButton.onClick.AddListener(m_onClick.Invoke);
            }
        }

        public void AddClickEvent(Action onClick, bool replaceAll = false)
        {
            if (replaceAll)
            {
                m_onClick.RemoveAllListeners();
            }
            m_onClick.AddListener(onClick.Invoke);
        }

        public void SetLabel(string newLabel)
        {
            m_label.text = newLabel;
        }

        public void SetIcon(Sprite newSprite)
        {
            m_icon.sprite = newSprite;
            m_icon.overrideSprite = newSprite;
        }
    }
}