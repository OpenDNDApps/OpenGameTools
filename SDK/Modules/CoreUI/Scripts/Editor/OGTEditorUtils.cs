namespace OGT.Editor
{
    using System;
    using System.Reflection;
    
    public static class OGTEditorUtils
    {
        public static readonly BindingFlags BindingFlags = BindingFlags.Default | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        
        public static FieldInfo GetField(this Type type, string fieldName, BindingFlags flags, bool includeParents)
        {
            FieldInfo fieldInfo = type.GetField(fieldName, flags);
            if (fieldInfo != null)
                return fieldInfo;

            if (!includeParents)
                return null;
            
            Type typeCopy = type.BaseType;
            while (typeCopy != null)
            {
                fieldInfo = typeCopy.GetField(fieldName, flags);
                if (fieldInfo != null)
                    return fieldInfo;
                typeCopy = typeCopy.BaseType;
            }
            return null;
        }
    }
}