using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OGT
{
    public partial class UIRuntime
    {
        public static event Action<RectTransform, TMP_LinkInfo> OnLinkPointerEnter;
        public static event Action<RectTransform, TMP_LinkInfo> OnLinkPointerExit;
        public static event Action<RectTransform, TMP_LinkInfo> OnLinkPointerStay;

        partial void OnUIRuntimeTMPInitialize()
        {
            OnManualUpdate += OnUIRuntimeTMPUpdate;
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnAnyTMPChanged);
        }

        private void OnAnyTMPChanged(Object obj)
        {
            if (!(obj is TMP_Text tmp))
                return;
            
            
        }

        private void OnUIRuntimeTMPUpdate()
        {
            
        }
        
        public static void NotifyOnLinkPointerEnter(RectTransform target, TMP_LinkInfo info)
        {
            OnLinkPointerEnter?.Invoke(target, info);
        }
        
        public static void NotifyOnLinkPointerExit(RectTransform target, TMP_LinkInfo info) 
        {
            OnLinkPointerExit?.Invoke(target, info);
        }
        
        public static void NotifyOnLinkPointerStay(RectTransform target, TMP_LinkInfo info) 
        {
            OnLinkPointerStay?.Invoke(target, info);
        }

        public static Vector3 GetLinkPositionInRect(RectTransform target)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(target, Input.mousePosition, UICamera, out Vector3 worldPointInRectangle);

            return worldPointInRectangle;
        }
    }
}