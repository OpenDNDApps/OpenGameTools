using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = GameResources.kUIFileName, menuName = GameResources.kCreateMenuPrefixNameResources + GameResources.kUIFileName)]
    public partial class UIResourcesCollection : BaseResourcesCollection
    {
	    [Header("UI Resources")]
	    [SerializeField] private UIRuntime m_uiRuntime;
	    [SerializeField] private List<UIItem> m_uiItems = new List<UIItem>();
	    [SerializeField] private List<UIItem> m_uiItemsInEditor = new List<UIItem>();
        [SerializeField] private List<UIWindow> m_uiWindows = new List<UIWindow>();
        [SerializeField] private List<UIAnimation> m_uiAnimations = new List<UIAnimation>();

        public UIRuntime UIRuntime => m_uiRuntime;
        public List<UIWindow> UIWindows => m_uiWindows;
        public List<UIAnimation> UIAnimations => m_uiAnimations;
		public List<UIItem> UIItems => m_uiItems;
		
        [Serializable]
        public struct UISortingKeyPair
        {
	        public int SortOrder;
	        public int PlaneDistance;
	        public UISectionType Type;
        }

        public static bool TryCreateEditorUIItem<T>(string uiItemName, out T item) where T : UIItem
        {
	        if (!GameResources.UI.TryGetEditorUIItem(uiItemName, out item))
	        {
		        Debug.LogError($"There is no '{uiItemName}' item registered. " +
		                       $"\nPlease add it to the UIItemsInEditor collection." +
		                       $"\nThis can be found in the GameUIResourceCollection." +
		                       $"\nYou can use the shortcut in the menu item. \n(TopMenu/OGT/Module Resources/Select UI)\n\n");
		        return false;
	        }
            
	        T newItem = Instantiate(item);
	        newItem.name = uiItemName;
	        #if UNITY_EDITOR
	        UnityEditor.Selection.activeGameObject = newItem.gameObject;
	        #endif
	        return false;
        }
		
        public bool TryGetEditorUIItem<T>(string itemName, out T uiItem) where T : UIItem
        {
	        uiItem = null;
	        if (string.IsNullOrEmpty(itemName)) 
		        return false;
            
	        foreach (UIItem window in m_uiItemsInEditor)
	        {
		        if (!window.name.Equals(itemName)) continue;
				
		        uiItem = window as T;
		        return true;
	        }
	        return false;
        }
		
		public bool TryGetUIItem<T>(string itemName, out T uiItem) where T : UIItem
		{
			uiItem = null;
			if (string.IsNullOrEmpty(itemName)) 
				return false;
            
			foreach (UIItem window in m_uiItems)
			{
				if (!window.name.Equals(itemName)) continue;
				
				uiItem = window as T;
				return true;
			}
			return false;
		}

		public bool TryGetUIWindow<T>(string itemName, out T uiWindow) where T : UIWindow
		{
			uiWindow = null;
			if (string.IsNullOrEmpty(itemName)) 
				return false;
            
			foreach (var window in m_uiWindows)
			{
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
	        if (m_uiRuntime == null)
	        {
		        AddLoadablePrefabs(m_uiRuntime.gameObject);
	        }

	        if (LayersAreValid(out List<string> wrongLayers))
		        return;
	        
	        foreach (string wrongLayer in wrongLayers)
	        {
		        Debug.LogError($"The UI SortingLayer '{wrongLayer}' does not exists but you're trying to use it.");
	        }
        }

        private static bool LayersAreValid(out List<string> wrongLayers)
        {
	        wrongLayers = new List<string>();
	        foreach (UISortingKeyPair sorting in GameResources.Settings.UI.Sorting)
	        {
		        if (SortingLayer.layers.ToList().Exists(layer => layer.name.Equals(sorting.Type.ToString())))
			        continue;

		        wrongLayers.Add(sorting.Type.ToString());
	        }

	        return wrongLayers.Count == 0;
        }
    }
}