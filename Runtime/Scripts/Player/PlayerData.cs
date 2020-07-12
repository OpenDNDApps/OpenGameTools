using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = "New PlayerData", menuName = "Anvil3D/PlayerData")]
	public class PlayerData : AnvilScriptableObject
	{
		public MonoBehaviour owner;

		public StringVariable nickname;

		[BoxGroup]
		public PlayerStats stats;

		public IntVariable level;
		public IntVariable experience;

		[BoxGroup]
		public PlayerWallet wallet;
	}

	[System.Serializable]
	public class PlayerStats
	{
		public IntVariable agility;
		public IntVariable strength;
		public IntVariable intelligence;
		public IntVariable stamina;
	}

	[System.Serializable]
	public class PlayerWallet
	{
		public IntVariable currency;
		public IntVariable credits;
	}
}
