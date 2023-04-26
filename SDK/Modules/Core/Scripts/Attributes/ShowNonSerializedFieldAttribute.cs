using System;

namespace OGT
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ShowNonSerializedFieldAttribute : SpecialCaseDrawerAttribute
    {
    }
}