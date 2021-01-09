using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Anvil3D
{
	[AddComponentMenu("")]
	public class AnvilMonoBehaviour : AnvilMonoBase, IOnChangeHandler, IIdentifiable
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
		
		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder, Anvil3D.kOnChangeGroupTitle)]
		[SerializeField] protected bool m_onChangeToggle = false;

		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder)]
		[SerializeField] protected GameEvent m_onChangeGameEvent;

		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder)]
		[SerializeField] protected UnityEvent m_onChangeUnityEvent;

		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder)]
		[SerializeField] protected UnityAction m_onChangeUnityAction;
		
		[ToggleGroup(Anvil3D.kOnChangeGroupKeyVariable, Anvil3D.kOnChangeOrder), Button(Anvil3D.kOnChangeButtonTitle), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
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