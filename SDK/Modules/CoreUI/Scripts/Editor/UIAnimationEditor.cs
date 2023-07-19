using DG.Tweening;
using UnityEditorInternal;
using UnityEngine;

namespace OGT
{
    using UnityEditor;

    [CustomEditor(typeof(UIAnimation))]
    public class UIAnimationEditor : UnityEditor.Editor
    {
        private SerializedProperty m_stepsProp;
        private ReorderableList m_stepsList;
        
        private void OnEnable()
        {
            serializedObject.Update();
            
            m_stepsProp = serializedObject.FindProperty("Steps");
            m_stepsList = new ReorderableList(serializedObject, m_stepsProp, true, true, true, true);
            m_stepsList.drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Steps"); };
            m_stepsList.elementHeightCallback = StructsGetElementHeight;
            m_stepsList.drawElementCallback = StructsDrawStepElement;
            m_stepsList.onAddDropdownCallback = StructsGetAddDropdownElements;

            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(true);
            GUILayout.Label("Script:");
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            EditorGUILayout.ObjectField(scriptProp.objectReferenceValue, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            
            m_stepsList.DoLayoutList();
            
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(true);
            GUILayout.Label("OverallDuration:");
            UIAnimation uiAnimation = (UIAnimation)target;
            EditorGUILayout.FloatField(uiAnimation.GetOverallDuration());
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            serializedObject.ApplyModifiedProperties();
        }

        private void StructsGetAddDropdownElements(Rect buttonRect, ReorderableList list)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Alpha"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.Alpha;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty alphaProp = stepProp.FindPropertyRelative("Alpha");
                alphaProp.FindPropertyRelative("Duration").floatValue = 1f;
                alphaProp.FindPropertyRelative("Delay").floatValue = 0f;
                alphaProp.FindPropertyRelative("TargetValue").floatValue = 0f;
                alphaProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("Scaling"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.Scaling;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty scalingProp = stepProp.FindPropertyRelative("Scaling");
                scalingProp.FindPropertyRelative("Duration").floatValue = 1f;
                scalingProp.FindPropertyRelative("Delay").floatValue = 0f;
                scalingProp.FindPropertyRelative("TargetValue").vector3Value = Vector3.one;
                scalingProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("Animation"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.Animation;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty animationProp = stepProp.FindPropertyRelative("Animation");
                animationProp.FindPropertyRelative("Animator").objectReferenceValue = null;
                animationProp.FindPropertyRelative("MotionKey").stringValue = string.Empty;
                animationProp.FindPropertyRelative("TriggerKey").stringValue = string.Empty;
                animationProp.FindPropertyRelative("Params.Duration").floatValue = 1f;
                animationProp.FindPropertyRelative("Params.Delay").floatValue = 0f;
                animationProp.FindPropertyRelative("Params.Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("AnchorMin"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.AnchorMin;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty anchorProp = stepProp.FindPropertyRelative("AnchorMin");
                anchorProp.FindPropertyRelative("Duration").floatValue = 1f;
                anchorProp.FindPropertyRelative("Delay").floatValue = 0f;
                anchorProp.FindPropertyRelative("TargetValue").vector2Value = Vector2.one;
                anchorProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("AnchorMax"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.AnchorMax;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty anchorProp = stepProp.FindPropertyRelative("AnchorMax");
                anchorProp.FindPropertyRelative("Duration").floatValue = 1f;
                anchorProp.FindPropertyRelative("Delay").floatValue = 0f;
                anchorProp.FindPropertyRelative("TargetValue").vector2Value = Vector2.one;
                anchorProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("AnchorPositions"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.AnchorPositions;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty anchorProp = stepProp.FindPropertyRelative("AnchorPositions");
                anchorProp.FindPropertyRelative("Duration").floatValue = 1f;
                anchorProp.FindPropertyRelative("Delay").floatValue = 0f;
                anchorProp.FindPropertyRelative("TargetValue").vector2Value = Vector2.one;
                anchorProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("AnchorPositionX"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.AnchorPositionX;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty anchorProp = stepProp.FindPropertyRelative("AnchorPositionX");
                anchorProp.FindPropertyRelative("Duration").floatValue = 1f;
                anchorProp.FindPropertyRelative("Delay").floatValue = 0f;
                anchorProp.FindPropertyRelative("TargetValue").floatValue = 0f;
                anchorProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.AddItem(new GUIContent("AnchorPositionY"), false, () =>
            {
                int index = m_stepsProp.arraySize;
                m_stepsProp.InsertArrayElementAtIndex(index);
                SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
                stepProp.FindPropertyRelative("Type").enumValueIndex = (int)UIAnimationStepType.AnchorPositionY;
                stepProp.FindPropertyRelative("JoinType").enumValueIndex = (int)ScriptableAnimationJoinType.Join;
                SerializedProperty anchorProp = stepProp.FindPropertyRelative("AnchorPositionY");
                anchorProp.FindPropertyRelative("Duration").floatValue = 1f;
                anchorProp.FindPropertyRelative("Delay").floatValue = 0f;
                anchorProp.FindPropertyRelative("TargetValue").floatValue = 0f;
                anchorProp.FindPropertyRelative("Ease").enumValueIndex = (int)Ease.Linear;
                serializedObject.ApplyModifiedProperties();
            });
            menu.ShowAsContext();
        }

        private float StructsGetElementHeight(int index)
        {
            SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
            UIAnimationStepType stepType = (UIAnimationStepType)stepProp.FindPropertyRelative("Type").enumValueIndex;

            float height = EditorGUIUtility.singleLineHeight; // for type and join type

            switch (stepType)
            {
                case UIAnimationStepType.Alpha:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
                case UIAnimationStepType.Scaling:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
                case UIAnimationStepType.Animation:
                    height += EditorGUIUtility.singleLineHeight * 10;
                    break;
                case UIAnimationStepType.AnchorMin:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
                case UIAnimationStepType.AnchorMax:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
                case UIAnimationStepType.AnchorPositions:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
                case UIAnimationStepType.AnchorPositionX:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
                case UIAnimationStepType.AnchorPositionY:
                    height += EditorGUIUtility.singleLineHeight * 6;
                    break;
            }

            return height;
        }

        private void StructsDrawStepElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty stepProp = m_stepsProp.GetArrayElementAtIndex(index);
            SerializedProperty alphaProp = stepProp.FindPropertyRelative("Alpha");
            SerializedProperty scalingProp = stepProp.FindPropertyRelative("Scaling");
            SerializedProperty anchorMinProp = stepProp.FindPropertyRelative("AnchorMin");
            SerializedProperty anchorMaxProp = stepProp.FindPropertyRelative("AnchorMax");
            SerializedProperty anchorPosProp = stepProp.FindPropertyRelative("AnchorPositions");
            SerializedProperty anchorPosXProp = stepProp.FindPropertyRelative("AnchorPositionX");
            SerializedProperty anchorPosYProp = stepProp.FindPropertyRelative("AnchorPositionY");
            SerializedProperty animationProp = stepProp.FindPropertyRelative("Animation");
            UIAnimationStepType stepType = (UIAnimationStepType) stepProp.FindPropertyRelative("Type").enumValueIndex;

            EditorGUI.BeginProperty(rect, GUIContent.none, stepProp);

            float yPosition = rect.y + EditorGUIUtility.standardVerticalSpacing;

            switch (stepType)
            {
                case UIAnimationStepType.Alpha:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("Alpha"));
                    break;
                case UIAnimationStepType.Scaling:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("Scaling"));
                    break;
                case UIAnimationStepType.Animation:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("Animation"));
                    break;
                case UIAnimationStepType.AnchorMin:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("AnchorMin"));
                    break;
                case UIAnimationStepType.AnchorMax:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("AnchorMax"));
                    break;
                case UIAnimationStepType.AnchorPositions:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("AnchorPositions"));
                    break;
                case UIAnimationStepType.AnchorPositionX:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("AnchorPositionX"));
                    break;
                case UIAnimationStepType.AnchorPositionY:
                    EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("AnchorPositionY"));
                    break;
            }

            yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.indentLevel++;

            EditorGUI.PropertyField(new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight), stepProp.FindPropertyRelative("JoinType"));

            yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            switch (stepType)
            {
                case UIAnimationStepType.Alpha:
                    string[] alphaPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in alphaPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, alphaProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.Scaling:
                    string[] scalingPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in scalingPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, scalingProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.Animation:
                    string[] animationPropNames = new[] { "Animator", "MotionKey", "TriggerKey", "Params.Duration", "Params.Delay", "Params.Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in animationPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, animationProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.AnchorMin:
                    string[] anchorMinPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in anchorMinPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, anchorMinProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.AnchorMax:
                    string[] anchorMaxPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in anchorMaxPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, anchorMaxProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.AnchorPositions:
                    string[] anchorPosPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in anchorPosPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, anchorPosProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.AnchorPositionX:
                    string[] anchorPosXPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in anchorPosXPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, anchorPosXProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
                case UIAnimationStepType.AnchorPositionY:
                    string[] anchorPosYPropNames = new[] { "Duration", "Delay", "TargetValue", "Ease" };
                    yPosition += EditorGUIUtility.standardVerticalSpacing;
                    foreach (string propName in anchorPosYPropNames)
                    {
                        Rect propRect = new Rect(rect.x, yPosition, rect.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(propRect, anchorPosYProp.FindPropertyRelative(propName));
                        yPosition += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    }
                    break;
            }

            EditorGUI.indentLevel--;

            EditorGUI.EndProperty();
        }
    }
}