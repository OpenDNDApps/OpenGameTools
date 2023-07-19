using UnityEditor;
using System.Reflection;
using System;

namespace OGT.Editor
{
    public class ValidateInputPropertyValidator : PropertyValidatorBase
    {
        public override void ValidateProperty(SerializedProperty property)
        {
            ValidateInputAttribute validateInputAttribute = PropertyUtility.GetAttribute<ValidateInputAttribute>(property);
            object target = PropertyUtility.GetTargetObjectWithProperty(property);

            MethodInfo validationCallback = ReflectionUtility.GetMethod(target, validateInputAttribute.CallbackName);
            
            string logMsg;
            MessageType logType = MessageType.Error;

            if(validationCallback == null || validationCallback.ReturnType != typeof(bool))
                return;
            
            ParameterInfo[] callbackParameters = validationCallback.GetParameters();

            if (callbackParameters.Length == 0)
            {
                logMsg = $"{validateInputAttribute.GetType().Name} needs a callback with boolean return type and an optional single parameter of the same type as the field";
                
                if (!(bool)validationCallback.Invoke(target, null))
                {
                    logMsg = string.IsNullOrEmpty(validateInputAttribute.Message) ? $"{property.name} is not valid" : validateInputAttribute.Message;
                }
                OGTEditorGUI.HelpBox_Layout(logMsg, logType, context: property.serializedObject.targetObject);
                return;
            }

            if (callbackParameters.Length != 1)
            {
                logMsg = $"{validateInputAttribute.GetType().Name} needs a callback with boolean return type and an optional single parameter of the same type as the field";
                logType = MessageType.Warning;
                OGTEditorGUI.HelpBox_Layout(logMsg, logType, context: property.serializedObject.targetObject);
                return;
            }

            FieldInfo fieldInfo = ReflectionUtility.GetField(target, property.name);
            Type fieldType = fieldInfo.FieldType;
            Type parameterType = callbackParameters[0].ParameterType;

            if (fieldType == parameterType)
            {
                if ((bool)validationCallback.Invoke(target, new[] { fieldInfo.GetValue(target) }))
                    return;

                logMsg = string.IsNullOrEmpty(validateInputAttribute.Message) ? $"{property.name} is not valid" : validateInputAttribute.Message;
            }
            else
            {
                logMsg = "The field type is not the same as the callback's parameter type";
                logType = MessageType.Warning;
            }

            OGTEditorGUI.HelpBox_Layout(logMsg, logType, context: property.serializedObject.targetObject);
        }
    }
}