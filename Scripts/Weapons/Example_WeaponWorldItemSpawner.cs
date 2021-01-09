using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D {
	public class Example_WeaponWorldItemSpawner : MonoBehaviour
	{
		// This is an example of the "Access" usage.
		[SerializeField] private WeaponWorldItem m_prefab => Anvil3D.Prefabs.WeaponWorldItem;

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
					WeaponWorldItem newItem = Instantiate(m_prefab, new Vector3(x * 1.5f, 0, z * 1.5f), Quaternion.identity, m_holder);

					newItem.data = Anvil3D.Database.AllWeaponDatasInProject.GetRandomItem();
					newItem.Build();

					m_storedCollectionOfBuiltItems.List.Add(newItem);
				}
			}
		}
	}
}