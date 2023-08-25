using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    public class BaseResourcesCollection : BaseScriptableObject
    {
        public bool TryGetMono<T>(string itemName, List<T> collection, out T outItem) where T : MonoBehaviour
        {
            outItem = null;
            if (string.IsNullOrEmpty(itemName)) 
                return false;
            
            foreach (var item in collection)
            {
                if (!item.name.Equals(itemName)) continue;
				
                outItem = item;
                return true;
            }
            
            Debug.LogError($"TryGetMono Failed, Mono '{itemName}' does not exists or is not registered in collection '{collection}'.");
            return false;
        }

        public bool TryGetScriptableObject<T>(string itemName, List<T> collection, out T outItem) where T : ScriptableObject
        {
            outItem = null;
            if (string.IsNullOrEmpty(itemName)) 
                return false;
            
            foreach (var item in collection)
            {
                if (!item.name.Equals(itemName)) continue;
				
                outItem = item;
                return true;
            }
            
            Debug.LogError($"TryGetScriptableObject Failed, SO '{itemName}' does not exists or is not registered in collection '{collection}'.");
            return false;
        }
    }
}
