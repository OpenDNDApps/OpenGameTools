using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anvil3D;
using UnityEngine.Events;

namespace Anvil3D
{
	public class EventsManager : MonoSingletonSelfGenerated<EventsManager>
	{
		private Dictionary<string, UnityAction> m_actions;
		private static string EventPrefix => Anvil3D.kGameEventPrefix;

		protected override void OnAwake()
		{
			m_actions ??= new Dictionary<string, UnityAction>();
		}

		public static void StartListening(string eventName, UnityAction listener)
		{
			if (Instance.m_actions.TryGetValue(EventPrefix + eventName, out UnityAction thisEvent))
			{
				//Add more event to the existing one
				thisEvent += listener;

				//Update the Dictionary
				Instance.m_actions[EventPrefix + eventName] = thisEvent;
			}
			else
			{
				//Add event to the Dictionary for the first time
				thisEvent += listener;
				Instance.m_actions.Add(EventPrefix + eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			if (!Instance.m_actions.TryGetValue(EventPrefix + eventName, out UnityAction thisEvent)) 
				return;
			
			//Remove event from the existing one
			thisEvent -= listener;

			//Update the Dictionary
			Instance.m_actions[EventPrefix + eventName] = thisEvent;
		}

		public static void TriggerEvent(string eventName)
		{
			if (!Instance.m_actions.TryGetValue(EventPrefix + eventName, out UnityAction thisEvent))
				return;
			
			thisEvent.Invoke();
			// OR USE instance.eventDictionary[eventName]();
		}
	}
}