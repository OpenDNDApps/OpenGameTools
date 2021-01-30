using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
    public class MonoSingletonUnmanaged<T> : MonoBehaviour where T : MonoBehaviour
    {
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

                Debug.LogError($@"You're trying to access the '{typeof(T).Name}' singleton, but it is missing and unmanaged.");

                return null;
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
        }
    }
}