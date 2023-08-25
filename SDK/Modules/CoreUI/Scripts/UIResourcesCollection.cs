using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = nameof(UIResourcesCollection), menuName = OGTConstants.kCreateMenuPrefixNameResources + nameof(UIResourcesCollection))]
    public partial class UIResourcesCollection : BaseResourcesCollection
    {
	    [SerializeField] private List<UIItemBase> m_uiItemsInEditor = new List<UIItemBase>();
	    
	    [SerializeField] private List<UIItem> m_uiItemPrefabs = new List<UIItem>();
        [SerializeField] private List<UIWindow> m_uiWindowPrefabs = new List<UIWindow>();
        [SerializeField] private List<UIAnimation> m_uiAnimations = new List<UIAnimation>();

        public List<UIWindow> UIWindowPrefabs => m_uiWindowPrefabs;
        public List<UIAnimation> UIAnimations => m_uiAnimations;
		public List<UIItem> UIItemPrefabs => m_uiItemPrefabs;
		
        [Serializable]
        public struct UISortingKeyPair
        {
	        public int SortOrder;
	        public int PlaneDistance;
	        public UISectionType Type;
        }

        public static bool TryCreateEditorUIItem<T>(string uiItemName, out T item) where T : UIItemBase
        {
	        if (!GameResources.UI.TryGetEditorUIItem(uiItemName, out item))
	        {
		        Debug.LogError(@$"There is no '{uiItemName}' item registered. 
		                       Please add it to the UIItemsInEditor collection.
		                       This can be found in the GameUIResourceCollection.
		                       You can use the shortcut in the menu item. \n(TopMenu/Tools/OGT/Module Resources/Select UI)\n\n");
		        return false;
	        }
	        
	        T newItem = PrefabUtility.InstantiatePrefab(item, (Selection.activeObject as GameObject)?.transform) as T;
	        if (newItem == null)
	        {
		        Debug.LogError($"The UI element '{uiItemName}' could not be instantiated.");
		        return false;
	        }
	        
	        newItem.name = uiItemName;
	        #if UNITY_EDITOR
	        Selection.activeGameObject = newItem.gameObject;
	        #endif
	        return false;
        }

        public static bool TryCreateEditorUnlinkedUIItem<T>(string uiItemName, out T item) where T : UIItemBase
        {
	        if (!GameResources.UI.TryGetEditorUIItem(uiItemName, out item))
	        {
		        Debug.LogError(@$"There is no '{uiItemName}' item registered. 
		                       Please add it to the UIItemsInEditor collection.
		                       This can be found in the GameUIResourceCollection.
		                       You can use the shortcut in the menu item. \n(TopMenu/Tools/OGT/Module Resources/Select UI)\n\n");
		        return false;
	        }
	        
	        T newItem = Instantiate(item, (Selection.activeObject as GameObject)?.transform) as T;
	        if (newItem == default)
	        {
		        Debug.LogError($"The UI element '{uiItemName}' could not be instantiated.");
		        return false;
	        }
	        
	        newItem.name = uiItemName;
	        #if UNITY_EDITOR
	        Selection.activeGameObject = newItem.gameObject;
	        #endif
	        return false;
        }
		
        public bool TryGetEditorUIItem<T>(string itemName, out T uiItem) where T : UIItemBase
        {
	        uiItem = null;
	        if (string.IsNullOrEmpty(itemName)) 
		        return false;
            
	        foreach (UIItemBase item in m_uiItemsInEditor)
	        {
		        if(item == default) continue;
		        if (!item.name.Equals(itemName)) continue;
				
		        uiItem = item as T;
		        return true;
	        }
	        return false;
        }
		
		public bool TryGetUIItem<T>(string itemName, out T uiItem) where T : UIItemBase
		{
			uiItem = null;
			if (string.IsNullOrEmpty(itemName)) 
				return false;
            
			foreach (UIItem item in m_uiItemPrefabs)
			{
				if(item == default) continue;
				if (!item.name.Equals(itemName)) continue;
				
				uiItem = item as T;
				return true;
			}
			return false;
		}

		public bool TryGetUIWindowPrefab<T>(string itemName, out T uiWindow) where T : UIWindow
		{
			uiWindow = null;
			if (string.IsNullOrEmpty(itemName)) 
				return false;
            
			foreach (UIWindow window in m_uiWindowPrefabs)
			{
				if(window == default) continue;
				if (!window.name.Equals(itemName)) continue;
				
				uiWindow = window as T;
				return true;
			}
			return false;
		}

        public bool TryGetUIAnimation(string animName, out UIAnimation uiAnimation)
        {
	        return TryGetScriptableObject(animName, m_uiAnimations, out uiAnimation);
        }
        
        [ContextMenu("Validate")]
        private void OnValidate()
        {
	        if (LayersAreValid(out List<string> wrongLayers))
		        return;
	        
	        foreach (string wrongLayer in wrongLayers)
	        {
		        Debug.LogError($"The UI SortingLayer '{wrongLayer}' does not exists but you're trying to use it. You need to add it manually.");
	        }
        }

        private static bool LayersAreValid(out List<string> wrongLayers)
        {
	        wrongLayers = new List<string>();
	        
	        List<SortingLayer> list = new List<SortingLayer>();
	        foreach (SortingLayer layer in SortingLayer.layers) 
		        list.Add(layer);
	        
	        foreach (UISortingKeyPair sorting in GameResources.Settings.UI.Sorting)
	        {
		        if (list.Exists(layer => layer.name.Equals(sorting.Type.ToString())))
			        continue;

		        wrongLayers.Add(sorting.Type.ToString());
	        }

	        return wrongLayers.Count == 0;
        }
    }
}