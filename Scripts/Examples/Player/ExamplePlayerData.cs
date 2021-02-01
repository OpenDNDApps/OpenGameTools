using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = "New ExamplePlayerData", menuName = Anvil3D.kCreateMenuPrefixName + "Game/ExamplePlayerData")]
	public class ExamplePlayerData : AnvilScriptableObject
	{
		public MonoBehaviour Owner;

		public StringVariable Nickname;

		[BoxGroup]
		public PlayerStats Stats;

		public IntVariable Level;
		public IntVariable Experience;

		[BoxGroup]
		public PlayerWallet Wallet;
	}

	[System.Serializable]
	public struct PlayerStats
	{
		public IntVariable Agility;
		public IntVariable Strength;
		public IntVariable Intelligence;
		public IntVariable Stamina;
	}

	[System.Serializable]
	public struct PlayerWallet
	{
		public IntVariable Currency;
		public IntVariable Credits;
	}
}
