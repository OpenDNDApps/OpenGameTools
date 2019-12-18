using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// This collection of extensions contains custom code or from:
/// - 
/// </summary>
/// 
public static class DictionaryExtensions
{
	/// <summary>
	/// Returns true if the dictionary is null or empty
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <param name="data"></param>
	/// <returns></returns>
	public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> data)
	{
		return ((data == null) || (data.Count == 0));
	}

	/// <summary>
	/// Method that adds the given key and value to the given dictionary only if the key is NOT present in the dictionary.
	/// This will be useful to avoid repetitive "if(!containskey()) then add" pattern of coding.
	/// </summary>
	/// <param name="dict">The given dictionary.</param>
	/// <param name="key">The given key.</param>
	/// <param name="value">The given value.</param>
	/// <returns>True if added successfully, false otherwise.</returns>
	/// <typeparam name="TKey">Refers the TKey type.</typeparam>
	/// <typeparam name="TValue">Refers the TValue type.</typeparam>
	public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		if (dict.ContainsKey(key))
			return false;

		dict.Add(key, value);
		return true;
	}

	/// <summary>
	/// Method that adds the given key and value to the given dictionary if the key is NOT present in the dictionary.
	/// If present, the value will be replaced with the new value.
	/// </summary>
	/// <param name="dict">The given dictionary.</param>
	/// <param name="key">The given key.</param>
	/// <param name="value">The given value.</param>
	/// <typeparam name="TKey">Refers the Key type.</typeparam>
	/// <typeparam name="TValue">Refers the Value type.</typeparam>
	public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		if (dict.ContainsKey(key))
			dict[key] = value;
		else
			dict.Add(key, value);
	}

	/// <summary>
	/// Method that adds the list of given KeyValuePair objects to the given dictionary. If a key is already present in the dictionary,
	/// then an error will be thrown.
	/// </summary>
	/// <param name="dict">The given dictionary.</param>
	/// <param name="kvpList">The list of KeyValuePair objects.</param>
	/// <typeparam name="TKey">Refers the TKey type.</typeparam>
	/// <typeparam name="TValue">Refers the TValue type.</typeparam>
	public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, List<KeyValuePair<TKey, TValue>> kvpList)
	{
		foreach (var kvp in kvpList)
		{
			dict.Add(kvp.Key, kvp.Value);
		}
	}

	/// <summary>
	/// Converts an enumeration of groupings into a Dictionary of those groupings.
	/// </summary>
	/// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
	/// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
	/// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
	/// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
	/// <example>results = productList.GroupBy(product => product.Category).ToDictionary();</example>
	/// <remarks>http://extensionmethod.net/csharp/igrouping/todictionary-for-enumerations-of-groupings</remarks>
	public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
	{
		return groupings.ToDictionary(group => group.Key, group => group.ToList());
	}

	/// <summary>
	/// Removes items from a collection based on the condition you provide. This is useful if a query gives 
	/// you some duplicates that you can't seem to get rid of. Some Linq2Sql queries are an example of this. 
	/// Use this method afterward to strip things you know are in the list multiple times
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="list"></param>
	/// <param name="Predicate"></param>
	/// <remarks>http://extensionmethod.net/csharp/icollection-t/removeduplicates</remarks>
	/// <returns></returns>
	public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
	{
		var dict = new Dictionary<int, T>();

		foreach (var item in list)
		{
			if (!dict.ContainsKey(Predicate(item)))
			{
				dict.Add(Predicate(item), item);
			}
		}

		return dict.Values.AsEnumerable();
	}

	/// <summary>
	/// deques an item, or returns null
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="q"></param>
	/// <returns></returns>
	public static T DequeueOrNull<T>(this Queue<T> q)
	{
		try
		{
			return (q.Count > 0) ? q.Dequeue() : default;
		}

		catch (Exception)
		{
			return default;
		}
	}
}
