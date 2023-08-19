namespace OGT.Editor
{
    using UnityEditor;
    
    [CustomEditor(typeof(UIItemBase), editorForChildClasses: true)]
    public class UIItemBaseEditor : InheritedGroupedEditor<UIItemBaseEditor>
    {
    }
}