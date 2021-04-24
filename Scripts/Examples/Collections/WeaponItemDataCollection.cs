using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VGDevs
{
    [CreateAssetMenu(menuName = VGDevs.kCreateMenuPrefixName + "Game/Collection/WeaponItemDataCollection")]
    public class WeaponItemDataCollection : ItemDataCollectionDefinition<WeaponData>
    {
        /// <summary>
        /// Get all Items by WeaponType.
        /// </summary>
        /// <param name="weaponType"></param>
        /// <returns></returns>
        public List<WeaponData> GetAllByWeaponType(WeaponType weaponType)
        {
            return m_list.FindAll(x => x.WeaponType == weaponType);
        }
    }
}