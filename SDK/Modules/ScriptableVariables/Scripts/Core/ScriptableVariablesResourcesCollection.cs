using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = nameof(ScriptableVariablesResourcesCollection), menuName = OGTConstants.kCreateMenuPrefixNameResources + nameof(ScriptableVariablesResourcesCollection))]
    public class ScriptableVariablesResourcesCollection : ScriptableObject
    {
        public PlayerData m_playerData;
        public List<PlayerData> m_players = new List<PlayerData>();
    }
}