using UnityEditor;

namespace OGT.Editor
{
    public class RequiredPropertyValidator : PropertyValidatorBase
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            RequiredAttribute requiredAttribute = PropertyUtility.GetAttribute<RequiredAttribute>(property);

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                string warning = $"{requiredAttribute.GetType().Name} works only on reference types";
                OGTEditorGUI.HelpBox_Layout(warning, MessageType.Warning, context: property.serializedObject.targetObject);
                return;
            }

            if (property.objectReferenceValue != null)
                return;

            string errorMessage = property.name + " is required";
            if (!string.IsNullOrEmpty(requiredAttribute.Message))
            {
                errorMessage = requiredAttribute.Message;
            }

            OGTEditorGUI.HelpBox_Layout(errorMessage, MessageType.Error, context: property.serializedObject.targetObject);
        }
    }
}