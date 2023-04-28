using System;
using UnityEngine;

namespace OGT
{
    // This is an example of usage, feel free to modify.
    [CreateAssetMenu(fileName = "New PlayerData", menuName = GameResources.kCreateMenuPrefixNameGame + "PlayerData")]
    public class PlayerData : ScriptableVariableBase
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
