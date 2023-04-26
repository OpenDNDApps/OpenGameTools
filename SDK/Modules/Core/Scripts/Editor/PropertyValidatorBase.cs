using System;
using System.Collections.Generic;
using UnityEditor;

namespace OGT.Editor
{
    public abstract class PropertyValidatorBase
    {
        public abstract void ValidateProperty(SerializedProperty property);
    }

    public static class ValidatorAttributeExtensions
    {
        private static readonly Dictionary<Type, PropertyValidatorBase> m_validatorsByAttributeType;

        static ValidatorAttributeExtensions()
        {
            m_validatorsByAttributeType = new Dictionary<Type, PropertyValidatorBase>
            {
                [typeof(MinValueAttribute)] = new MinValuePropertyValidator(),
                [typeof(MaxValueAttribute)] = new MaxValuePropertyValidator(),
                [typeof(RequiredAttribute)] = new RequiredPropertyValidator(),
                [typeof(ValidateInputAttribute)] = new ValidateInputPropertyValidator()
            };
        }

        public static PropertyValidatorBase GetValidator(this ValidatorAttribute attr)
        {
            if (m_validatorsByAttributeType.TryGetValue(attr.GetType(), out PropertyValidatorBase validator))
            {
                return validator;
            }

            return null;
        }
    }
}