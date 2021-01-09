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
		#region ID / IIdentifiable Section
		
		[ToggleGroup(Anvil3D.kIDGroupKeyVariable, Anvil3D.kIDSortValue, Anvil3D.kIDGroupTitle)]
		[SerializeField] protected bool m_hasID = false;
		[ToggleGroup(Anvil3D.kIDGroupKeyVariable, Anvil3D.kIDSortValue)]
		[SerializeField] protected int m_id;
		
		public int ID => m_id;
		public bool HasID => m_hasID;
		
		#endregion
		
		#region OnChange Section

		[SerializeField] protected bool m_resetInRuntime = true;

		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder, Anvil3D.kOnChangeGroupTitle)]
		[SerializeField] protected bool m_onChangeToggle = false;
		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder)]
		[SerializeField] protected GameEvent m_onChangeScriptableObject;
		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder)]
		[SerializeField] protected UnityEvent m_onChangeEvent;
		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder)]
		[SerializeField] protected UnityAction m_onChangeAction;

		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder), Button(Anvil3D.kOnChangeButtonTitle), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public virtual void OnChange()
		{
			if (m_onChangeScriptableObject != null)
			{
				m_onChangeScriptableObject.Raise();
			}
			m_onChangeEvent?.Invoke();
			m_onChangeAction?.Invoke();
		}
		
		#endregion
	}
}