using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = GameResources.kGeneralFileName, menuName = GameResources.kCreateMenuPrefixNameResources + GameResources.kGeneralFileName)]
    public class GameResourcesCollection : BaseResourcesCollection
    {
        [Header("Game Resources")]
        [SerializeField] private List<GameObject> m_generalPrefabs = new List<GameObject>();
        
        public bool TryGetGeneralPrefab(string prefabName, out GameObject toReturn)
        {
            return TryGetPrefab(prefabName, m_generalPrefabs, out toReturn);
        }
    }
}