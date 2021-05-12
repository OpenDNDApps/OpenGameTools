
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	public class ExamplePlayerController : VGDevsMonoBehaviour, IDataBuildable<ExamplePlayerData>
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