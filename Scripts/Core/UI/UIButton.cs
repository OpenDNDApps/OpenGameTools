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
    public class UIButton : BaseUIScript, IPointerClickHandler
    {
        [SerializeField] private UnityEvent m_onClick;
        [SerializeField] private TMP_Text m_label;
        [SerializeField] private Image m_background;
        [SerializeField] private Image m_icon;

        public void AddClickEvent(Action onClick, bool replaceAll = false)
        {
            if (replaceAll)
            {
                m_onClick.RemoveAllListeners();
            }
            m_onClick.AddListener(onClick.Invoke);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            m_onClick?.Invoke();
        }
    }
}