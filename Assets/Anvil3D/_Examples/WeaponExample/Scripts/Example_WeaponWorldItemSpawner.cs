using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D {
	public class Example_WeaponWorldItemSpawner : MonoBehaviour
	{
		// This is an example of the "Access" usage.
		[ShowInInspector]
		public WeaponWorldItem Prefab => Anvil.Prefabs.weaponWorldItem;

		// This is an example of a direct reference usage, see this is scene independent.
		public WeaponWorldItemCollection storedCollectionOfBuiltItems;

		// just a container for the instantiating.
		public Transform holder;

		public void Start()
		{
			//runtimeCollectionOfBuiltItems = Instantiate(new ScriptableCollection<CardWorldItem>());

			for(int x = -2; x < 2; x++)
			{
				for (int z = -2; z < 2; z++)
				{
					WeaponWorldItem _newItem = Instantiate(Prefab, new Vector3(x * 1.5f, 0, z * 1.5f), Quaternion.identity, holder);

					_newItem.data = Anvil.Database.allWeaponDatasInProject.GetRandomItem();
					_newItem.Build();

					storedCollectionOfBuiltItems.List.Add(_newItem);
				}
			}
		}
	}
}