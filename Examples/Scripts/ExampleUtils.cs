using System;

namespace OGT.Examples
{
    public static class ExampleUtils
    {
        public static void CreateExampleButton(string label, Action onClick)
        {
            GameResources.UI.TryGetEditorUIItem("UIButton", out UIButton buttonPrefab);

            GameResources.UIRuntime.TryGetUISectionByType(UISectionType.Default, out UIScreenPanelContainer container);

            UIButton button = UnityEngine.Object.Instantiate(buttonPrefab, container.Canvas.transform);
            button.SetLabel(label);
            button.OnClick += onClick;
        }
    }
}