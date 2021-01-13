using Sirenix.OdinInspector;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(menuName = Anvil3D.kCreateMenuPrefixName + "Game/Collection/WeaponCollection")]
	public class WeaponDataCollection : BaseCollection<WeaponData>
	{
		[Button]
		public void TestGetById(int id)
		{
			var a = GetByID(id);

			if (a == null)
			{
				Debug.LogError("Test is null");
				return;
			}

			Debug.Log(a.Title);
		}
	}
}