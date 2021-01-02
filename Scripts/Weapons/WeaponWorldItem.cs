using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Anvil3D
{
	public class WeaponWorldItem : AnvilMonoBehaviour, IDataBuildable
	{
		public WeaponData data;

		public Transform pivot;
		public Transform body;

		public TextMeshProUGUI title;
		public TextMeshProUGUI damage;
		public TextMeshProUGUI durability;

		public void Build()
		{
			if(data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - Build()", this);
				return;
			}

			title.text = $"{data.title}";
			damage.text = $"{data.damage.Value}";
			durability.text = $"{data.durability}";

			SpawnBody();
		}

		public void SpawnBody()
		{
			if (data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - SpawnBody()", this);
				return;
			}

			if (body != null)
			{
				DestroyImmediate(body.gameObject);
				body = null;
			}

			body = Instantiate<Transform>(data.body, pivot);
		}
	}
}