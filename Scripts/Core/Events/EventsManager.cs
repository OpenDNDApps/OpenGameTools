using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil3D;
using UnityEngine.Events;

namespace Anvil3D
{
	[AddComponentMenu("")]
	public class EventsManager : MonoBehaviour
	{
		private Dictionary<string, UnityAction> m_actions;

		private static EventsManager m_eventsManager;

		public static EventsManager Instance
		{
			get
			{
				if (m_eventsManager) 
					return m_eventsManager;
				
				m_eventsManager = FindObjectOfType(typeof(EventsManager)) as EventsManager;

				if (!m_eventsManager)
				{
					Debug.LogError("There needs to be one active EventsManager script on a GameObject in your scene.");
				}
				else
				{
					m_eventsManager.Init();
				}
				return m_eventsManager;
			}
		}

		private void Init()
		{
			if (m_actions == null)
			{
				m_actions = new Dictionary<string, UnityAction>();
			}
			DontDestroyOnLoad(this);
		}

		public static void StartListening(string eventName, UnityAction listener)
		{
			if (Instance.m_actions.TryGetValue(eventName, out UnityAction thisEvent))
			{
				//Add more event to the existing one
				thisEvent += listener;

				//Update the Dictionary
				Instance.m_actions[eventName] = thisEvent;
			}
			else
			{
				//Add event to the Dictionary for the first time
				thisEvent += listener;
				Instance.m_actions.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			if (m_eventsManager == null)
				return;

			if (Instance.m_actions.TryGetValue(eventName, out UnityAction thisEvent))
			{
				//Remove event from the existing one
				thisEvent -= listener;

				//Update the Dictionary
				Instance.m_actions[eventName] = thisEvent;
			}
		}

		public static void TriggerEvent(string eventName)
		{
			if (Instance.m_actions.TryGetValue(eventName, out UnityAction thisEvent))
			{
				thisEvent.Invoke();
				// OR USE instance.eventDictionary[eventName]();
			}
		}
	}

}