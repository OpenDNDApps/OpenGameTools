using System;
using TMPro;
using UnityEngine;

namespace OGT
{
    public class UIAlertPopup : UIWindow
    {
        [SerializeField] private TMP_Text m_message;
        
        public void Build(string message, Action onClose = null)
        {
            #if UNITY_EDITOR
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame stackFrame = stackTrace.GetFrame(1); 
            System.Diagnostics.StackFrame stackClass = stackTrace.GetFrame(2); 
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            Debug.LogError($"'{stackClass}' - '{stackTrace}' - '{methodBase}' - '{message}'");
            #endif
            
            m_message.SetLocalizedText(message);
            if (onClose != null)
            {
                OnCloseTrigger += onClose;
            }
        }
    }
}
