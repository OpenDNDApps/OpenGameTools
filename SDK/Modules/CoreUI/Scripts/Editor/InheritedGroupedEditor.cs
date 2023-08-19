namespace OGT.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;
    
    public class InheritedGroupedEditor<T> : Editor where T : Editor
    {
        private class FieldGroup
        {
            public readonly HashSet<string> Properties = new HashSet<string>();
            public bool FoldoutState;
        }

        private readonly Dictionary<Type, FieldGroup> m_fieldGroups = new Dictionary<Type, FieldGroup>();
        private GUIStyle m_sectionStyleInternal;
        private GUIStyle m_headerStyleInternal;
        private bool m_showDefault;
        
        private readonly BindingFlags m_bindingFlags = BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        
        private Type m_targetType = typeof(T);
        private const string kScriptPropertyName = "m_Script";
        
        private class UIExtra { }
        
        protected virtual void OnEnable()
        {
            m_targetType = target.GetType();
            
            FieldGroup currentGroup = new FieldGroup();
            m_fieldGroups.TryAdd(m_targetType, currentGroup);

            SerializedProperty property = serializedObject.GetIterator();
            while (property.NextVisible(true))
            {
                if(property.name.Equals(kScriptPropertyName) || !property.name.Equals(property.propertyPath))
                    continue;
                
                FieldInfo fieldInfo = m_targetType.GetField(property.name, m_bindingFlags, includeParents: true);

                Type type = typeof(UIExtra);
                if (fieldInfo != null)
                    type = fieldInfo.DeclaringType ?? type;

                if (!m_fieldGroups.TryGetValue(type, out currentGroup))
                {
                    currentGroup = new FieldGroup();
                    m_fieldGroups.TryAdd(type, currentGroup);
                }
                
                currentGroup.Properties.Add(property.name);
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUI.BeginChangeCheck();
            
            DrawScriptName();
            DrawFoldoutGroups();

            EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndVertical();
        }

        private void DrawFoldoutGroups()
        {
            foreach ((Type type, FieldGroup group) in m_fieldGroups)
            {
                if (group.Properties.Count == 0)
                    continue;

                if (type == m_targetType)
                {
                    DrawFieldsInGroup(group);
                    EditorGUILayout.Space();
                    continue;
                }

                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(SectionStyle);

                group.FoldoutState = EditorGUILayout.BeginFoldoutHeaderGroup(group.FoldoutState, type.Name, HeaderStyle);
                EditorGUILayout.EndFoldoutHeaderGroup();

                if (group.FoldoutState)
                    DrawFieldsInGroup(group);

                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
                EditorGUI.indentLevel--;
            }
        }

        private void DrawFieldsInGroup(FieldGroup group)
        {
            foreach (string propertyName in group.Properties)
            {
                SerializedProperty property = serializedObject.FindProperty(propertyName);
                if (property == null) continue;
                EditorGUILayout.PropertyField(property, true);
            }
        }

        private void DrawScriptName()
        {
            SerializedProperty scriptProperty = serializedObject.FindProperty(kScriptPropertyName);
            if (scriptProperty == null)
                return;
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(scriptProperty, true);
            EditorGUILayout.Space(2f);
            EditorGUI.EndDisabledGroup();
        }
        
        private GUIStyle SectionStyle
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.helpBox);
                style.margin.right = 0;
                style.margin.left = 0;
                style.padding.left = -1;
                style.padding.right = 2;
                style.padding.bottom = 3;
                style.padding.top = 1;
                return m_sectionStyleInternal ??= style;
            }
        }

        private GUIStyle HeaderStyle
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.foldoutHeader);
                style.stretchWidth = true;
                style.padding.left = 20;
                style.padding.right = 0;
                style.padding.bottom = 3;
                style.padding.top = 3;
                style.margin.left = 15;
                style.margin.right = 6;
                style.margin.bottom = 6;
                style.margin.top = 0;
                return m_headerStyleInternal ??= style;
            }
        }
    }
}