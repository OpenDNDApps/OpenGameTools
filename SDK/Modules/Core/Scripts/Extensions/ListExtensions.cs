using System.Collections.Generic;

public static class ListExtensions
{
    public static bool AddUnique<T>(this List<T> collection, T item)
    {
        if (collection.Contains(item))
            return false;

        collection.Add(item);
        return true;
    }
    
    /// <summary>
    /// Shuffles the elements of the List at random.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="slowerButBetterRandom"></param>
    public static void Shuffle<T>(this IList<T> list, bool slowerButBetterRandom = false)
    {
        int n = list.Count;

        if (!slowerButBetterRandom)
        {
            System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
            while (n > 1)
            {
                byte[] box = new byte[1];
                do
                    provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
        else
        {
            System.Random rng = new System.Random();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }

    /// <summary>
    /// Returns true if the array is null or empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this T[] data)
    {
        return data == null || data.Length == 0;
    }

    /// <summary>
    /// Returns true if the list is null or empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this List<T> data)
    {
        return data == null || data.Count == 0;
    }

    /// <summary>
    /// Returns a random element from the list, using UnityEngine.Random
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T GetRandom<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count-1)];
    }
}
