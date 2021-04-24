using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VGDevs
{
	[AddComponentMenu("")]
	public class VgDevsMonoBehaviour : VGDevsMonoBase, IOnChangeHandler, IIdentifiable
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
		
		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder, VGDevs.kOnChangeGroupTitle)]
		[SerializeField] protected bool m_onChangeToggle = false;

		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder)]
		[SerializeField] protected GameEvent m_onChangeGameEvent;

		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder)]
		[SerializeField] protected UnityEvent m_onChangeUnityEvent;

		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder)]
		[SerializeField] protected UnityAction m_onChangeUnityAction;
		
		[ToggleGroup(VGDevs.kOnChangeGroupKeyVariable, VGDevs.kOnChangeOrder), Button(VGDevs.kOnChangeButtonTitle), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public virtual void OnChange()
		{
			if (m_onChangeGameEvent != null)
			{
				m_onChangeGameEvent.Raise();
			}
			m_onChangeUnityEvent?.Invoke();
			m_onChangeUnityAction?.Invoke();
		}
		
		#endregion

		public virtual void OnEnable()
		{
			if(m_onChangeToggle)
			{
				EventsManager.StartListening(this.GetInstanceID().ToString(), OnChange);
			}
		}

		public virtual void OnDisable()
		{
			if (m_onChangeToggle)
			{
				EventsManager.StopListening(this.GetInstanceID().ToString(), OnChange);
			}
		}
	}
}