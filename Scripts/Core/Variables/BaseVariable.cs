using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	//[CreateAssetMenu(menuName = VGDevs.kCreateMenuPrefixNameVariables + "BaseVariable")]
	public abstract class BaseVariable<T> : VGDevsScriptableObject, ISerializationCallbackReceiver
	{
		[SerializeField] protected T m_value = default;
		[System.NonSerialized] protected T m_initValue = default;

		protected virtual T Value
		{
			get => m_value;
			set
			{
				m_value = value;
				OnChange();
			}
		}

		public void OnAfterDeserialize()
		{
			if(m_resetInRuntime)
				m_value = m_initValue;
		}

		public void OnBeforeSerialize()	{}
	}
}