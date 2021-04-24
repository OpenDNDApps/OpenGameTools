using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace VGDevs
{
	public class VGDevsScriptableObject : ScriptableObject, IOnChangeHandler, IIdentifiable
	{
		#region ID / IIdentifiable Section
		
		[ToggleGroup(VGDevs.kIDGroupKeyVariable, VGDevs.kIDSortValue, VGDevs.kIDGroupTitle)]
		[SerializeField] protected bool m_hasID = false;
		[ToggleGroup(VGDevs.kIDGroupKeyVariable, VGDevs.kIDSortValue)]
		[SerializeField] protected int m_id;
		
		public int ID => m_id;
		public bool HasID => m_hasID;
		
		#endregion
		
		#region OnChange Section

		[SerializeField] protected bool m_resetInRuntime = true;

		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder, VGDevs.kOnChangeGroupTitle)]
		[SerializeField] protected bool m_onChangeToggle = false;
		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder)]
		[SerializeField] protected GameEvent m_onChangeScriptableObject;
		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder)]
		[SerializeField] protected UnityEvent m_onChangeEvent;
		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder)]
		[SerializeField] protected UnityAction m_onChangeAction;

		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder), Button(VGDevs.kOnChangeButtonTitle), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
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