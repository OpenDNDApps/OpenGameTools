using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = GameResources.kScriptableVariablesFileName, menuName = GameResources.kCreateMenuPrefixNameResources + GameResources.kScriptableVariablesFileName)]
    public class ScriptableVariablesResourcesCollection : ScriptableObject
    {
        public PlayerData m_playerData;
        public List<PlayerData> m_players = new List<PlayerData>();
    }
}