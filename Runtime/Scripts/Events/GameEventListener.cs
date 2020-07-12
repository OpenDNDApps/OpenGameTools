using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Anvil3D
{
	[AddComponentMenu("Anvil3D/Events/GameEventListener")]
	public class GameEventListener : MonoBehaviour
	{
		public GameEvent Event;

		public List<UnityEvent> Responses = new List<UnityEvent>();

		public void OnEnable()
		{
			if (Event)
				Event.RegisterListener(this);
		}

		public void OnDisable()
		{
			if (Event)
				Event.UnregisterListener(this);
		}

		public virtual void OnEventRaised()
		{
			foreach (UnityEvent currentResponse in Responses)
			{
				currentResponse.Invoke();
			}
		}
	}
}