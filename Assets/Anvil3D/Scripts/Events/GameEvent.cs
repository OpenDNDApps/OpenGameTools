using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = "New GameEvent", menuName = "Anvil3D/Events/GameEvent")]
	public class GameEvent : ScriptableObject
	{
		public List<GameEventListener> baseListeners = new List<GameEventListener>();
		public List<GameBehaviour> gameBehaviours = new List<GameBehaviour>();
		internal bool hasBeenRaised = false;

		public void Raise()
		{
			foreach (GameEventListener _listener in baseListeners)
			{
				_listener?.OnEventRaised();
			}

			foreach (GameBehaviour _behaviour in gameBehaviours)
			{
				_behaviour?.Behave();
			}

			EventsManager.TriggerEvent(this.name);
			hasBeenRaised = true;
		}

		public void RegisterAction(UnityAction newAction)
		{
			EventsManager.StartListening(this.name, newAction);
		}

		public void UnregisterAction(UnityAction newAction)
		{
			EventsManager.StopListening(this.name, newAction);
		}

		public void RegisterListener(GameEventListener _eventableToAdd)
		{
			if (!baseListeners.Contains(_eventableToAdd))
			{
				baseListeners.Add(_eventableToAdd);
			}
		}

		public void RegisterListener(GameBehaviour _eventableToAdd)
		{
			if (!gameBehaviours.Contains(_eventableToAdd))
			{
				gameBehaviours.Add(_eventableToAdd);
			}
		}

		public void UnregisterListener(GameEventListener _eventableToRemove)
		{
			if (baseListeners.Contains(_eventableToRemove))
			{
				baseListeners.Remove(_eventableToRemove);
			}
		}

		public void UnregisterListener(GameBehaviour _eventableToRemove)
		{
			if (gameBehaviours.Contains(_eventableToRemove))
			{
				gameBehaviours.Remove(_eventableToRemove);
			}
		}

		#if UNITY_EDITOR
		public void OnEnable()
		{
			baseListeners.Clear();
			gameBehaviours.Clear();
			hasBeenRaised = false;
			UnityEditor.EditorUtility.SetDirty(this);
		}
		#endif
	}
}