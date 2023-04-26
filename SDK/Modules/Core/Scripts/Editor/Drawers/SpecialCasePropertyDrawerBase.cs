using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OGT.Editor
{
    public abstract class SpecialCasePropertyDrawerBase
    {
        public void OnGUI(Rect rect, SerializedProperty property)
        {
            // Check if visible
            bool visible = PropertyUtility.IsVisible(property);
            if (!visible)
            {
                return;
            }

            // Validate
            ValidatorAttribute[] validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);
            foreach (ValidatorAttribute validatorAttribute in validatorAttributes)
            {
                validatorAttribute.GetValidator().ValidateProperty(property);
            }

            // Check if enabled and draw
            EditorGUI.BeginChangeCheck();
            bool enabled = PropertyUtility.IsEnabled(property);

            using (new EditorGUI.DisabledScope(disabled: !enabled))
            {
                OnGUI_Internal(rect, property, PropertyUtility.GetLabel(property));
            }

            // Call OnValueChanged callbacks
            if (EditorGUI.EndChangeCheck())
            {
                PropertyUtility.CallOnValueChangedCallbacks(property);
            }
        }

        public float GetPropertyHeight(SerializedProperty property)
        {
            return GetPropertyHeight_Internal(property);
        }

        protected abstract void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label);
        protected abstract float GetPropertyHeight_Internal(SerializedProperty property);
    }

    public static class SpecialCaseDrawerAttributeExtensions
    {
        private static readonly Dictionary<Type, SpecialCasePropertyDrawerBase> m_drawersByAttributeType;

        static SpecialCaseDrawerAttributeExtensions()
        {
            m_drawersByAttributeType = new Dictionary<Type, SpecialCasePropertyDrawerBase>
             {
                 [typeof(ReorderableListAttribute)] = ReorderableListPropertyDrawer.Instance
             };
        }

        public static SpecialCasePropertyDrawerBase GetDrawer(this SpecialCaseDrawerAttribute attr)
        {
            if (m_drawersByAttributeType.TryGetValue(attr.GetType(), out SpecialCasePropertyDrawerBase drawer))
            {
                return drawer;
            }

            return null;
        }
    }
}