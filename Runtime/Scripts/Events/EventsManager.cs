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
		private Dictionary<string, UnityAction> actions;

		private static EventsManager eventsManager;

		public static EventsManager instance
		{
			get
			{
				if (!eventsManager)
				{
					eventsManager = FindObjectOfType(typeof(EventsManager)) as EventsManager;

					if (!eventsManager)
					{
						Debug.LogError("There needs to be one active EventsManager script on a GameObject in your scene.");
					}
					else
					{
						eventsManager.Init();
					}
				}
				return eventsManager;
			}
		}

		void Init()
		{
			if (actions == null)
			{
				actions = new Dictionary<string, UnityAction>();
			}
		}

		public static void StartListening(string eventName, UnityAction listener)
		{
			if (instance.actions.TryGetValue(eventName, out UnityAction thisEvent))
			{
				//Add more event to the existing one
				thisEvent += listener;

				//Update the Dictionary
				instance.actions[eventName] = thisEvent;
			}
			else
			{
				//Add event to the Dictionary for the first time
				thisEvent += listener;
				instance.actions.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			if (eventsManager == null)
				return;

			if (instance.actions.TryGetValue(eventName, out UnityAction thisEvent))
			{
				//Remove event from the existing one
				thisEvent -= listener;

				//Update the Dictionary
				instance.actions[eventName] = thisEvent;
			}
		}

		public static void TriggerEvent(string eventName)
		{
			if (instance.actions.TryGetValue(eventName, out UnityAction thisEvent))
			{
				thisEvent.Invoke();
				// OR USE instance.eventDictionary[eventName]();
			}
		}
	}

}