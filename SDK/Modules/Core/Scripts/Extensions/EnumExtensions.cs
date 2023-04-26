using System;
using System.Linq;

public static class EnumExtensions
{
    public static string ToFlagString<T>(this T value, string separator = " ") where T : struct, Enum
    {
        if (!typeof(T).IsDefined(typeof(FlagsAttribute), false))
        {
            throw new ArgumentException($"Type '{typeof(T).Name}' is not a flag enum.");
        }

        var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
        if (!value.Equals(default(T)))
        {
            values.Remove(default(T));
        }

        var flags = values.Where(flag => value.HasFlag(flag)).Select(flag => flag.ToString());
        return flags.Any() ? string.Join(separator, flags) : "Undefined";
    }
    
    public static bool HasFlag<T>(this T value, T flag) where T : struct, Enum
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        int valueAsInt = Convert.ToInt32(value);
        int flagAsInt = Convert.ToInt32(flag);

        return (valueAsInt & flagAsInt) == flagAsInt;
    }

    public static T AddFlag<T>(this T value, T flag) where T : struct, Enum
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        int valueAsInt = Convert.ToInt32(value);
        int flagAsInt = Convert.ToInt32(flag);

        return (T)(object)(valueAsInt | flagAsInt);
    }

    public static T RemoveFlag<T>(this T value, T flag) where T : struct, Enum
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }

        int valueAsInt = Convert.ToInt32(value);
        int flagAsInt = Convert.ToInt32(flag);

        return (T)(object)(valueAsInt & ~flagAsInt);
    }
}

