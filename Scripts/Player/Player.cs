
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	public class Player : AnvilMonoBehaviour, IDataBuildable
	{
		public PlayerData data;

		public void Build()
		{
			data.owner = this;

			Debug.Log($"NotImplementedException: {data.name}");
			throw new System.NotImplementedException();
		}
	}
}