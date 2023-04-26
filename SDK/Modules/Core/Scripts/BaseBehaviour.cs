using UnityEngine;

namespace OGT
{
    public class BaseBehaviour : MonoBehaviour
    {
        protected bool m_initialized = false;
            
        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (m_initialized)
                return;

            m_initialized = true;
            OnInit();
        }

        public virtual void Disable(bool softDisable = false)
        {
            gameObject.SetActive(false);
        }

        public virtual void Enable()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnInit() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
    }
}