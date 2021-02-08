using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D {
	public abstract class BaseCollection<T> : AnvilScriptableObject, IOnChangeHandler, ISerializationCallbackReceiver, IList<T>
	{
		[SerializeField] protected List<T> m_list = new List<T>();
		[NonSerialized] private List<T> m_initList = new List<T>();
		
		public T this[int index]
		{
			get => m_list[index];
			set => m_list[index] = value;
		}

		public IList List => m_list;
		public int Count => List.Count;

		public Type Type => typeof(T);

		public bool IsReadOnly => List.IsReadOnly;

		public void Add(T obj)
		{
			if (!m_list.Contains(obj))
				m_list.Add(obj);

			OnChange();
		}

		public void Insert(int index, T value)
		{
			m_list.Insert(index, value);

			OnChange();
		}

		public void Remove(T obj)
		{
			if (m_list.Contains(obj))
				m_list.Remove(obj);

			OnChange();
		}

		public void RemoveAt(int index)
		{
			m_list.RemoveAt(index);

			OnChange();
		}

		public void Clear()
		{
			m_list.Clear();

			OnChange();
		}

		public bool Contains(T value)
		{
			return m_list.Contains(value);
		}

		/// <summary>
		/// Returns if the list contains an item of ID. It required a type of AnvilScriptableObject
		/// This has a lot of casts, use with caution.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsByID(int id)
		{
			return m_list.Exists((x) => ((IIdentifiable)x).ID == id);
		}

		/// <summary>
		/// Get the Item by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetByID(int id)
		{
			return m_list.Find((x) => ((IIdentifiable)x).ID == id);
		}

		/// <summary>
		/// Get the Item by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public List<T> GetAllByID(int id)
		{
			return m_list.FindAll((x) => ((IIdentifiable)x).ID == id);
		}

		public T GetRandom()
		{
			return m_list.GetRandom();
		}

		public int IndexOf(T value)
		{
			return m_list.IndexOf(value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return m_list.GetEnumerator();
		}

		public override string ToString()
		{
			return "Collection<" + typeof(T) + ">(" + Count + ")";
		}

		public T[] ToArray()
		{
			return m_list.ToArray();
		}

		public void OnBeforeSerialize() {}

		public void OnAfterDeserialize()
		{
			if(m_resetInRuntime)
				m_list = m_initList;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			List.CopyTo(array, arrayIndex);
		}

		bool ICollection<T>.Remove(T item)
		{
			return ((ICollection<T>)List).Remove(item);
		}
	}

	public class ScriptableCollection<T> : BaseCollection<T>
	{
	}
}
