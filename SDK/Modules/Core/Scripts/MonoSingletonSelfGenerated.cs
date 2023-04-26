using OGT;
using UnityEngine;

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

            GameObject loadable = GameResources.General.GetLoadablePrefab(typeof(T).Name);
            GameObject newObject;
            if (loadable != null)
            {
                newObject = Instantiate(loadable);
                newObject.name = typeof(T).Name;
                m_instance = newObject.GetComponent<T>();
            }
            else
            {
                newObject = new GameObject($@"{typeof(T).Name}")
                {
                    hideFlags = HideFlags.DontSaveInEditor
                };
            }
            
            m_instance = newObject.GetOrAddComponent<T>();
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

        OnSingletonAwake();
    }

    protected virtual void OnSingletonAwake()
    {
        
    }
}