using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VGDevs
{
	public class VGDevsMonoBehaviour : VGDevsMonoBase
	{
		#region ID / IIdentifiable Section
		
		[SerializeField] protected bool m_hasID = false;
		[SerializeField] protected int m_id;
		
		public int ID => m_id;
		public bool HasID => m_hasID;
		
		#endregion
		
		#region OnChange Section
		
		[SerializeField] protected bool m_onChangeToggle = false;
		[SerializeField] protected GameEvent m_onChangeGameEvent;
		[SerializeField] protected UnityEvent m_onChangeUnityEvent;
		[SerializeField] protected UnityAction m_onChangeUnityAction;
		
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