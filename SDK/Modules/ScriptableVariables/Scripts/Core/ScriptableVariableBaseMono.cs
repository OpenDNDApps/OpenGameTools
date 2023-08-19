using UnityEngine;
using UnityEngine.Events;

namespace OGT
{
    public class ScriptableVariableBaseMono : BaseBehaviour
    {
        [SerializeField] protected bool m_hasID;
        [SerializeField] protected int m_id;
        
        [SerializeField] protected bool m_onChangeToggle;
        [SerializeField] protected GameEvent m_onChangeGameEvent;
        [SerializeField] protected UnityEvent m_onChangeUnityEvent;
        [SerializeField] protected UnityAction m_onChangeUnityAction;
        
        public int ID => m_id;
        public bool HasID => m_hasID;
        
        public virtual void OnChange()
        {
            if (m_onChangeGameEvent != null)
            {
                m_onChangeGameEvent.Raise();
            }
            m_onChangeUnityEvent?.Invoke();
            m_onChangeUnityAction?.Invoke();
        }
        
        protected override void OnEnable()
        {
            if(m_onChangeToggle)
            {
                EventsManager.StartListening(GetInstanceID().ToString(), OnChange);
            }
        }

        protected override void OnDisable()
        {
            if (m_onChangeToggle)
            {
                EventsManager.StopListening(GetInstanceID().ToString(), OnChange);
            }
        }
    }
}