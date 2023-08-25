using System;
using UnityEngine;

namespace OGT
{
    public class MonoSingletonSelfGenerated<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool m_isPersistentThroughScenes;
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance != default)
                    return m_instance;

                m_instance = FindObjectOfType<T>();
                if (m_instance != default)
                    return m_instance;

                Type objType = typeof(T);

                bool foundPrefab = GameResources.Dependencies.TryGetPrefab(out T loadable);
                m_instance = foundPrefab ? Instantiate(loadable) : new GameObject(objType.Name).GetOrAddComponent<T>();
                
                m_instance.name = objType.Name;
                m_instance.hideFlags = HideFlags.DontSaveInEditor;
                m_instance.transform.SetAsFirstSibling();

                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_instance != default && m_instance != this as T)
            {
                Destroy(this);
                return;
            }

            if (m_instance == default)
            {
                m_instance = this as T;
            }

            if (m_isPersistentThroughScenes)
            {
                DontDestroyOnLoad(this);
            }

            OnSingletonAwake();
        }

        protected virtual void OnSingletonAwake() { }
    }
}