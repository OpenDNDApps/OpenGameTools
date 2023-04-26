using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OGT
{
	[CreateAssetMenu(fileName = "New_GameEvent", menuName = GameResources.kCreateMenuPrefixNameEvents + "GameEvent")]
	public class GameEvent : ScriptableObject
	{
		protected List<GameEventListener> m_baseListeners = new List<GameEventListener>();
		protected bool m_hasBeenRaised = false;

		public void Raise()
		{
			foreach (GameEventListener listener in m_baseListeners)
			{
				if (listener != null)
				{
					listener.OnEventRaised();	
				}
			}

			EventsManager.TriggerEvent(this.name);
			m_hasBeenRaised = true;
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
			if (!m_baseListeners.Contains(eventToAdd))
			{
				m_baseListeners.Add(eventToAdd);
			}
		}

		public void UnregisterListener(GameEventListener eventToRemove)
		{
			if (m_baseListeners.Contains(eventToRemove))
			{
				m_baseListeners.Remove(eventToRemove);
			}
		}

		#if UNITY_EDITOR
		public void OnEnable()
		{
			m_baseListeners.Clear();
			m_hasBeenRaised = false;
			UnityEditor.EditorUtility.SetDirty(this);
		}
		#endif
	}
}