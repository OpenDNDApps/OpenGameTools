using System;
using UnityEngine;

namespace OGT
{
	//[CreateAssetMenu(menuName = VGDevs.kCreateMenuPrefixNameVariables + "BaseVariable")]
	public abstract class BaseVariable<T> : ScriptableVariableBase
	{
		[Header("Data")]
		[SerializeField] protected T m_value = default;
		[NonSerialized] protected T m_initValue = default;

		protected virtual T Value
		{
			get => m_value;
			set
			{
				m_value = value;
				OnChange();
			}
		}
		
		public override void OnResetAfterRuntime()
		{
			m_value = m_initValue;
		}
	}
}