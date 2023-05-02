using System;
using UnityEngine;

namespace OGT
{
    public class UIItemBase : MonoBehaviour
    {
        protected RectTransform m_rectTransform;
        [NonSerialized] protected UIWindow m_window;
        [NonSerialized] protected UIContentSection m_contentSection;
        [NonSerialized] protected Canvas m_canvas;
        private bool m_initialized = false;

        public RectTransform RectTransform
        {
            get
            {
                if (m_rectTransform == default)
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
                if (m_window == default)
                    m_window = GetComponent<UIWindow>();
                if (m_window == default)
                    m_window = GetComponentInParent<UIWindow>();
                
                if (m_window == default)
                    Debug.LogError($"Window not found on '{this.name}'");
                
                return m_window;
            }
        }

        public UIContentSection ContentSection
        {
            get
            {
                if (m_contentSection == default)
                    m_contentSection = GetComponent<UIContentSection>();
                if (m_contentSection == default)
                    m_contentSection = GetComponentInParent<UIContentSection>();
                
                if (m_contentSection == default)
                    Debug.LogError($"ContentSection not found on '{this.name}'");
                
                return m_contentSection;
            }
        }

        public Canvas Canvas
        {
            get
            {
                if (m_canvas == default)
                    m_canvas = UIRuntime.GetCanvasOfType(Window.SectionType);

                return m_canvas;
            }
        }
        
        public bool IsTopLevelWindow
        {
            get
            {
                if (!(this is UIWindow))
                    return false;
                return Canvas.transform == transform.parent;
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
            m_initialized = true;

            OnInit();

            return m_initialized;
        }

        protected virtual void OnInit() { }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnDisable() { }

        protected virtual void OnDestroy() { }

        public enum UIItemVisibilityState
        {
            Undefined,
            Visible,
            Hidden,
        }
    }
}
