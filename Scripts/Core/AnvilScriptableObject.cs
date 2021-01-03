using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Anvil3D
{
	public class AnvilScriptableObject : ScriptableObject, IOnChangeHandler, IIdentifiable
	{
		[ToggleGroup("m_hasID", "Has ID", Order = -99)]
		[SerializeField] protected bool m_hasID = false;
		[ToggleGroup("m_hasID", Order = -99)]
		[SerializeField] protected int m_id;
		
		public int ID => m_id;
		public bool HasID => m_hasID;

		[PropertyOrder(9905)]
		[SerializeField] protected bool m_resetInRuntime = true;

		[PropertySpace(SpaceBefore = 40f), ToggleGroup("m_onChangeToggle", "On Change Events", Order = 9900)]
		[SerializeField] protected bool m_onChangeToggle = false;
		[ToggleGroup("m_onChangeToggle", Order = 9901)]
		[SerializeField] protected GameEvent m_onChangeScriptableObject;
		[ToggleGroup("m_onChangeToggle", Order = 9902)]
		[SerializeField] protected UnityEvent m_onChangeEvent;
		[ToggleGroup("m_onChangeToggle", Order = 9903)]
		[SerializeField] protected UnityAction m_onChangeAction;

		[ToggleGroup("m_onChangeToggle", Order = 9901), PropertyOrder(9903), Button("Trigger OnChange"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public void OnChange()
		{
			if (m_onChangeScriptableObject != null)
			{
				m_onChangeScriptableObject.Raise();
			}
			m_onChangeEvent?.Invoke();
			m_onChangeAction?.Invoke();
		}
	}
}