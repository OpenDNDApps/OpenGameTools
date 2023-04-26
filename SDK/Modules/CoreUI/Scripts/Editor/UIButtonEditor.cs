namespace OGT.Editor
{
    using UnityEditor;

    public class CreateUIButton : Editor
    {
        private const int kCreateUIButtonBaseIndex = 40;
        private const string kCreateItemEditorPath = GameResources.kCreateUIGameObjectMenuPath + "UIButtons/";
        
        [MenuItem(kCreateItemEditorPath + "Generic", priority = kCreateUIButtonBaseIndex)]
        private static void CreateUIButtonGeneric()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIButton", out UIButton button);
        }
        
        [MenuItem(kCreateItemEditorPath + "Primary", priority = kCreateUIButtonBaseIndex + 1)]
        private static void CreateUIButtonPrimary()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIButton - Primary", out UIButton button);
        }
        
        [MenuItem(kCreateItemEditorPath + "Secondary", priority = kCreateUIButtonBaseIndex + 2)]
        private static void CreateUIButtonSecondary()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIButton - Secondary", out UIButton button);
        }
        
        [MenuItem(kCreateItemEditorPath + "Success", priority = kCreateUIButtonBaseIndex + 3)]
        private static void CreateUIButtonSuccess()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIButton - Success", out UIButton button);
        }
        
        [MenuItem(kCreateItemEditorPath + "Danger", priority = kCreateUIButtonBaseIndex + 4)]
        private static void CreateUIButtonDanger()
        {
            UIResourcesCollection.TryCreateEditorUIItem("UIButton - Danger", out UIButton button);
        }
    }
}