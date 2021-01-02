using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	public class Example_WeaponUIItemSpawner : MonoBehaviour
	{
		// This is an example of the "Access" usage.
		[ShowInInspector]
		public WeaponUIItem Prefab => Anvil.Prefabs.weaponUIItem;

		// This is an example of a direct reference usage, see this is scene independent.
		public WeaponUIItemCollection storedCollectionOfBuiltItems;

		// just a container for the instantiating.
		public RectTransform holder;

		public void Start()
		{
			//runtimeCollectionOfBuiltItems = Instantiate(new ScriptableCollection<CardWorldItem>());

			for (int x = -2; x < 2; x++)
			{
				for (int y = -2; y < 2; y++)
				{
					WeaponUIItem _newItem = Instantiate(Prefab, new Vector3(x, y, 0f), Quaternion.identity, holder);

					_newItem.data = Anvil.Database.allWeaponDatasInProject.GetRandomItem();
					_newItem.Build();

					storedCollectionOfBuiltItems.List.Add(_newItem);
				}
			}
		}
	}
}