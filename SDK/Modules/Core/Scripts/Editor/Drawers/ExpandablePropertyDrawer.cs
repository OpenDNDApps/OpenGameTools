using UnityEngine;
using UnityEditor;

namespace OGT.Editor
{
    [CustomPropertyDrawer(typeof(ExpandableAttribute))]
    public class ExpandablePropertyDrawer : PropertyDrawerBase
    {
        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue == null)
            {
                return GetPropertyHeight(property);
            }

            System.Type propertyType = PropertyUtility.GetPropertyType(property);
            if (!typeof(ScriptableObject).IsAssignableFrom(propertyType))
                return GetPropertyHeight(property) + GetHelpBoxHeight();
            
            ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
            if (scriptableObject == null)
            {
                return GetPropertyHeight(property);
            }

            if (!property.isExpanded)
                return GetPropertyHeight(property);

            using SerializedObject serializedObject = new SerializedObject(scriptableObject);
            float totalHeight = EditorGUIUtility.singleLineHeight;

            using (var iterator = serializedObject.GetIterator())
            {
                if (iterator.NextVisible(true))
                {
                    do
                    {
                        SerializedProperty childProperty = serializedObject.FindProperty(iterator.name);
                        if (childProperty.name.Equals("m_Script", System.StringComparison.Ordinal))
                        {
                            continue;
                        }

                        bool visible = PropertyUtility.IsVisible(childProperty);
                        if (!visible)
                        {
                            continue;
                        }

                        float height = GetPropertyHeight(childProperty);
                        totalHeight += height;
                    }
                    while (iterator.NextVisible(false));
                }
            }

            totalHeight += EditorGUIUtility.standardVerticalSpacing;
            return totalHeight;
        }

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);
            void EndPropertyLogic()
            {
                property.serializedObject.ApplyModifiedProperties();
                EditorGUI.EndProperty();
            }

            if (property.objectReferenceValue == null)
            {
                EditorGUI.PropertyField(rect, property, label, false);
                EndPropertyLogic();
                return;
            }

            System.Type propertyType = PropertyUtility.GetPropertyType(property);
            if (!typeof(ScriptableObject).IsAssignableFrom(propertyType))
            {
                string message = $"{nameof(ExpandableAttribute)} can only be used on scriptable objects";
                DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
                EndPropertyLogic();
                return;
            }

            ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
            if (scriptableObject == null)
            {
                EditorGUI.PropertyField(rect, property, label, false);
                EndPropertyLogic();
                return;
            }

            // Draw a foldout
            Rect foldoutRect = new Rect()
            {
                x = rect.x,
                y = rect.y,
                width = EditorGUIUtility.labelWidth,
                height = EditorGUIUtility.singleLineHeight
            };

            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, toggleOnLabelClick: true);

            // Draw the scriptable object field
            Rect propertyRect = new Rect()
            {
                x = rect.x,
                y = rect.y,
                width = rect.width,
                height = EditorGUIUtility.singleLineHeight
            };

            EditorGUI.PropertyField(propertyRect, property, label, false);

            // Draw the child properties
            if (property.isExpanded)
            {
                DrawChildProperties(rect, property);
            }

            EndPropertyLogic();
        }

        private void DrawChildProperties(Rect rect, SerializedProperty property)
        {
            ScriptableObject scriptableObject = property.objectReferenceValue as ScriptableObject;
            if (scriptableObject == null)
            {
                return;
            }

            Rect boxRect = new Rect()
            {
                x = 0.0f,
                y = rect.y + EditorGUIUtility.singleLineHeight,
                width = rect.width * 2.0f,
                height = rect.height - EditorGUIUtility.singleLineHeight
            };

            GUI.Box(boxRect, GUIContent.none);

            using (new EditorGUI.IndentLevelScope())
            {
                SerializedObject serializedObject = new SerializedObject(scriptableObject);
                serializedObject.Update();

                using (var iterator = serializedObject.GetIterator())
                {
                    float yOffset = EditorGUIUtility.singleLineHeight;

                    if (iterator.NextVisible(true))
                    {
                        do
                        {
                            SerializedProperty childProperty = serializedObject.FindProperty(iterator.name);
                            if (childProperty.name.Equals("m_Script", System.StringComparison.Ordinal))
                            {
                                continue;
                            }

                            bool visible = PropertyUtility.IsVisible(childProperty);
                            if (!visible)
                            {
                                continue;
                            }

                            float childHeight = GetPropertyHeight(childProperty);
                            Rect childRect = new Rect()
                            {
                                x = rect.x,
                                y = rect.y + yOffset,
                                width = rect.width,
                                height = childHeight
                            };

                            OGTEditorGUI.PropertyField(childRect, childProperty, true);

                            yOffset += childHeight;
                        }
                        while (iterator.NextVisible(false));
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}