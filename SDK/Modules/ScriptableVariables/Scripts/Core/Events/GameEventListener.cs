using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OGT
{
    [AddComponentMenu(GameResources.kCreateMenuPrefixNameEvents + "GameEventListener")]
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent m_event;
        [SerializeField] private List<UnityEvent> m_responses = new List<UnityEvent>();

        public GameEvent Event => m_event;
        public List<UnityEvent> Responses => m_responses;
        
        public void OnEnable()
        {
            if (!m_event) 
                return;
            
            m_event.RegisterListener(this);
        }

        public void OnDisable()
        {
            if (!m_event) 
                return;
            
            m_event.UnregisterListener(this);
        }

        public virtual void OnEventRaised()
        {
            foreach (UnityEvent currentResponse in m_responses)
            {
                currentResponse.Invoke();
            }
        }
    }
}