using System;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu(fileName = "New PlayerData", menuName = VGDevs.kCreateMenuPrefixNameGame + "PlayerData")]
	public class PlayerData : VGDevsScriptableObject
	{
		[Header("Data")]
		public MonoBehaviour Instance;

		public AccountStats Account;
		public PlayerStats Stats;
		public PlayerWallet Wallet;
	}

	[Serializable]
	public struct AccountStats
	{
		public StringVariable Nickname;
		public IntVariable Level;
		public IntVariable Experience;
	}

	[Serializable]
	public struct PlayerStats
	{
		public IntVariable Agility;
		public IntVariable Strength;
		public IntVariable Intelligence;
		public IntVariable Stamina;
	}

	[Serializable]
	public struct PlayerWallet
	{
		public IntVariable Currency;
		public IntVariable Credits;
	}
}
