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
		[ToggleGroup("m_hasID", Anvil.kIDPrefixINTValue, "Has ID")]
		[SerializeField] protected bool m_hasID = false;
		[ToggleGroup("m_hasID", Anvil.kIDPrefixINTValue, "Has ID")]
		[SerializeField] protected int m_id;
		
		public int ID => m_id;
		public bool HasID => m_hasID;
		
		[PropertySpace(50f), ToggleGroup("m_onChangeToggle", "On Change Events", Order = 9900)]
		[SerializeField] protected bool m_onChangeToggle = false;

		[ToggleGroup("m_onChangeToggle", Order = 9901)]
		[SerializeField] protected GameEvent m_onChangeGameEvent;

		[ToggleGroup("m_onChangeToggle", Order = 9902)]
		[SerializeField] protected UnityEvent m_onChangeUnityEvent;

		[ToggleGroup("m_onChangeToggle", Order = 9902)]
		[SerializeField] protected UnityAction m_onChangeUnityAction;
		
		[ToggleGroup("m_onChangeToggle", Order = 9901), PropertyOrder(9903), Button("Trigger OnChange"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public virtual void OnChange()
		{
			if (m_onChangeGameEvent != null)
			{
				m_onChangeGameEvent.Raise();
			}
			m_onChangeUnityEvent?.Invoke();
			m_onChangeUnityAction?.Invoke();
		}

		public void OnEnable()
		{
			if(m_onChangeToggle)
			{
				EventsManager.StartListening(this.GetInstanceID().ToString(), OnChange);
			}
		}

		public void OnDisable()
		{
			if (m_onChangeToggle)
			{
				EventsManager.StopListening(this.GetInstanceID().ToString(), OnChange);
			}
		}
	}
}