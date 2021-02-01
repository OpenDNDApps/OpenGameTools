
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	public class ExamplePlayerController : AnvilMonoBehaviour, IDataBuildable<ExamplePlayerData>
	{
		public ExamplePlayerData m_data;

		public void Build(ExamplePlayerData newData = null)
		{
			if (newData != null)
			{
				m_data = newData;
			}
			
			if (m_data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - Build()", this);
				return;
			}
			
			m_data.Owner = this;
			
			// Build
		}
	}
}