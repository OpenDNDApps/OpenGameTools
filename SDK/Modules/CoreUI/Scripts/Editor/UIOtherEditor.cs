using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OGT
{
    public class UIOtherEditor : MonoBehaviour
    {
        private const int kCreateUIOtherBaseIndex = 40;
        private const string kCreateItemEditorPath = GameResources.kCreateUIGameObjectMenuPath + "/";
            
        [MenuItem(kCreateItemEditorPath + "VisualRoot", priority = kCreateUIOtherBaseIndex)]
        private static void CreateUIVisualRoot()
        {
            UIResourcesCollection.TryCreateEditorUnlinkedUIItem("UIVisualRoot", out UIVisualRoot visualRoot);
        }
    }
}
