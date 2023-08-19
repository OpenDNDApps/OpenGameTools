using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OGT
{
    [CreateAssetMenu(fileName = "New_GameEvent", menuName = GameResources.kCreateMenuPrefixNameEvents + "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [NonSerialized]
        protected List<GameEventListener> m_baseListeners = new List<GameEventListener>();

        [Button("Test Trigger")]
        public void Raise()
        {
            foreach (GameEventListener listener in m_baseListeners)
            {
                if (listener == null) 
                    continue;
                
                listener.OnEventRaised();
            }

            EventsManager.TriggerEvent(this.name);
        }

        public void RegisterAction(UnityAction newAction)
        {
            EventsManager.StartListening(this.name, newAction);
        }

        public void UnregisterAction(UnityAction newAction)
        {
            EventsManager.StopListening(this.name, newAction);
        }

        public void RegisterListener(GameEventListener eventToAdd)
        {
            m_baseListeners.AddUnique(eventToAdd);
        }

        public void UnregisterListener(GameEventListener eventToRemove)
        {
            m_baseListeners.Remove(eventToRemove);
        }
    }
}