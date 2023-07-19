namespace OGT.Editor
{
    using UnityEditor;
    
    public class UIToggleEditor : Editor
    {
        private const int kCreateUIToggleBaseIndex = 60;
        private const string kCreateItemEditorPath = GameResources.kCreateUIGameObjectMenuPath + "/";
        
        [MenuItem(kCreateItemEditorPath + "UIToggle", priority = kCreateUIToggleBaseIndex)]
        private static void CreateUIButtonGeneric()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIToggle", out UIToggle _);
        }
    }
}