using System;

namespace OGT
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ShowNativePropertyAttribute : SpecialCaseDrawerAttribute
    {
    }
}