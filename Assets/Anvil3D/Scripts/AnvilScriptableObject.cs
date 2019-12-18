using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Anvil3D
{
	public class AnvilScriptableObject : ScriptableObject, IOnChangeHandler
	{
		[ToggleGroup("hasID", "Has ID", Order = -99)]
		public bool hasID = false;
		[ToggleGroup("hasID", Order = -99)]
		public int id;

		[PropertyOrder(9905)]
		public bool resetInRuntime = true;

		[PropertySpace(SpaceBefore = 40f), ToggleGroup("onChangeToggle", "On Change Events", Order = 9900)]
		public bool onChangeToggle = false;
		[ToggleGroup("onChangeToggle", Order = 9901)]
		public GameEvent OnChangeScriptableObject;
		[ToggleGroup("onChangeToggle", Order = 9902)]
		public UnityEvent OnChangeEvent;
		[ToggleGroup("onChangeToggle", Order = 9903)]
		public UnityAction OnChangeAction;

		[ToggleGroup("onChangeToggle", Order = 9901), PropertyOrder(9903), Button("Trigger OnChange"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public void OnChange()
		{
			OnChangeScriptableObject?.Raise();
			OnChangeEvent?.Invoke();
			OnChangeAction?.Invoke();
		}
	}
}