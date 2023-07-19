namespace OGT.Editor
{
    using UnityEditor;

    public class UIInputEditor : Editor
    {
        private const int kCreateUIInputBaseIndex = 50;
        private const string kCreateItemEditorPath = GameResources.kCreateUIGameObjectMenuPath + "/";
        
        [MenuItem(kCreateItemEditorPath + "UIInput", priority = kCreateUIInputBaseIndex)]
        private static void CreateUIButtonGeneric()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIInput", out UIInput _);
        }
    }
}