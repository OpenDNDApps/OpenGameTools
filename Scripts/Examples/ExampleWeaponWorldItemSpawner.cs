using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D {
	public class ExampleWeaponWorldItemSpawner : MonoBehaviour
	{
		// This is an example of the "Access" usage.
		[SerializeField] private WeaponWorldItem Prefab => Anvil3D.Prefabs.WeaponWorldItem;

		// This is an example of a direct reference usage, see this is scene independent.
		[SerializeField] private WeaponWorldItemCollection m_storedCollectionOfBuiltItems;

		// just a container for the instantiating.
		[SerializeField] private Transform m_holder;

		public void Start()
		{
			BuildDemoItems();
		}

		private void BuildDemoItems()
		{
			for(int x = -2; x < 2; x++)
			{
				for (int z = -2; z < 2; z++)
				{
					WeaponWorldItem newItem = Instantiate(Prefab, new Vector3(x * 1.5f, 0, z * 1.5f), Quaternion.identity, m_holder);

					var randomItemData = Anvil3D.Database.m_allItemDefinitionsInProject.GetRandom() as WeaponData;
					newItem.Build(randomItemData);

					m_storedCollectionOfBuiltItems.List.Add(newItem);
				}
			}
		}
	}
}