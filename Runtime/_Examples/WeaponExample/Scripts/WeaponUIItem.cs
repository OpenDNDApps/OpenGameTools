using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Anvil3D
{
	public class WeaponUIItem : AnvilMonoBehaviour
	{
		public WeaponData data;

		public TextMeshProUGUI title;
		public Image icon;
		public TextMeshProUGUI damage;
		public TextMeshProUGUI durability;

		[Button]
		public void Build()
		{
			if (data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - Build()", this);
				return;
			}

			title.text = $"{data.title}";
			damage.text = $"{data.damage.Value}";
			durability.text = $"{data.durability}";

			icon.overrideSprite = data.art;
		}
	}
}