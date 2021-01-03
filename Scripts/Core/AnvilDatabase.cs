using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(menuName = "Anvil3D/Base/Database")]
	public class AnvilDatabase : AnvilScriptableObject
	{
		public PlayerData playerData;

		public WeaponDataCollection allWeaponDatasInProject;

		public WeaponWorldItemCollection allWeaponWorldItemsInGameBuilt;
		public WeaponUIItemCollection allWeaponUIItemsInGameBuilt;
	}
}