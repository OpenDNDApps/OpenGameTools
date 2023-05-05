using UnityEngine;
using UnityEngine.Serialization;

namespace OGT.Examples
{
    public class CoreUI_ExampleOverview : MonoBehaviour
    {
        [SerializeField] private UIWindow m_customWindowPrefab;

        // Used the button in the UI
        private void SpawnCustomWindow()
        {
            // Yes, only this is needed.
            UIRuntime.TryCreateWindow(m_customWindowPrefab.name, out UIWindow customWindow);
            customWindow.AnimatedShow();
        }

        private void Start()
        {
            ExampleUtils.CreateExampleButton("Open Custom Window", SpawnCustomWindow);
        }

        // Ignore the following, this is just for the example.
        // You should manually add your prefabs to the UIWindows collection in the GameResourcesCollection.
        // Check UIResources.md in the docs.
        // https://github.com/OpenDNDApps/OpenGameTools/tree/master/Documentation
        private void OnEnable() { GameResources.UI.UIWindows.AddUnique(m_customWindowPrefab); }
        private void OnDestroy() { GameResources.UI.UIWindows.Remove(m_customWindowPrefab); }
    }
}