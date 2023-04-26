using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    public class UIItemBase : MonoBehaviour
    {
        protected RectTransform m_rectTransform;
        protected UIWindow m_window;        
        protected Canvas m_canvas;
        protected bool m_initialized = false;
        
        public RectTransform RectTransform
        {
            get
            {
                if (m_rectTransform == null)
                {
                    m_rectTransform = (RectTransform) transform;
                }
                return m_rectTransform;
            }
        }

        public UIWindow Window
        {
            get
            {
                if (m_window == null)
                    m_window = GetComponent<UIWindow>();
                if (m_window == null)
                    m_window = GetComponentInParent<UIWindow>();
                
                if (m_window == null)
                    Debug.LogError($"Window not found on '{this.name}' gameObject");
                
                return m_window;
            }
        }

        public Canvas Canvas
        {
            get
            {
                if (m_canvas == null)
                    m_canvas = UIRuntime.GetCanvasOfType(Window.SectionType);

                return m_canvas;
            }
        }
        
        protected virtual void Awake()
        {
            Init();
        }

        public virtual bool Init()
        {
            if (m_initialized) 
                return false;

            OnInit();

            m_initialized = true;
            return m_initialized;
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnInit() { }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }
    }
}
