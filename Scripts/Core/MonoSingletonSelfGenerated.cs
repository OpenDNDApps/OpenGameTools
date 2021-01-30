using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
    public class MonoSingletonSelfGenerated<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool m_isPersistentThroughScenes;
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance != null)
                    return m_instance;

                var target = FindObjectOfType<T>();
                if (target != null)
                    m_instance = target;

                if (m_instance != null)
                    return m_instance;

                var newObject = new GameObject($@"{typeof(T).Name} (SelfGenerated)");
                newObject.hideFlags = HideFlags.HideAndDontSave;
                m_instance = newObject.AddComponent<T>();
                m_instance.transform.SetAsFirstSibling();

                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_instance != null && m_instance != this as T)
            {
                Destroy(this);
                return;
            }

            if (m_instance == null)
            {
                m_instance = this as T;
            }

            if (m_isPersistentThroughScenes)
            {
                DontDestroyOnLoad(this);
            }
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}