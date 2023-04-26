using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu(fileName = VGDevs.kMainDatabaseFileName, menuName = VGDevs.kCreateMenuPrefixName + "Base/Database")]
	public class VGDevsDatabase : ScriptableObject
	{
		// Add your data/scriptableObjects/prefabs/etc here.
		public PlayerData m_playerData;
		public List<PlayerData> m_players = new List<PlayerData>();
	}
}