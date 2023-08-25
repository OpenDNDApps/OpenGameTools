using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = nameof(GameDependenciesCollection), menuName = OGTConstants.kCreateMenuPrefixNameResources + nameof(GameDependenciesCollection))]
    public class GameDependenciesCollection : BaseResourcesCollection
    {
        [SerializeField] protected List<MonoBehaviour> m_prefabs = new List<MonoBehaviour>();
        
        public bool TryGetPrefab<T>(out T outItem) where T : MonoBehaviour
        {
            outItem = null;
            
            foreach(MonoBehaviour item in m_prefabs)
            {
                if (item.GetType() != typeof(T)) 
                    continue;
				
                outItem = item as T;
                return true;
            }
            
            Debug.LogError($"TryGetPrefab Failed, Prefab '{typeof(T).Name}' does not exists or is not registered in collection '{nameof(m_prefabs)}'.");
            return false;
        }
    }
}