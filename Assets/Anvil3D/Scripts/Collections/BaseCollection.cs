using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D {
	public abstract class BaseCollection<T> : AnvilScriptableObject, IOnChangeHandler, ISerializationCallbackReceiver, IEnumerable<T>, IList<T>
	{
		public T this[int index]
		{
			get
			{
				return _list[index];
			}
			set
			{
				_list[index] = value;
			}
		}

		[SerializeField]
		protected List<T> _list = new List<T>();

		[NonSerialized]
		protected List<T> initList = new List<T>();

		public IList List
		{
			get
			{
				return _list;
			}
		}

		public Type Type
		{
			get
			{
				return typeof(T);
			}
		}

		public int Count { get { return List.Count; } }

		public bool IsReadOnly { get { return List.IsReadOnly; } }

		public void Add(T obj)
		{
			if (!_list.Contains(obj))
				_list.Add(obj);

			OnChange();
		}

		public void Insert(int index, T value)
		{
			_list.Insert(index, value);

			OnChange();
		}

		public void Remove(T obj)
		{
			if (_list.Contains(obj))
				_list.Remove(obj);

			OnChange();
		}

		public void RemoveAt(int index)
		{
			_list.RemoveAt(index);

			OnChange();
		}

		public void Clear()
		{
			_list.Clear();

			OnChange();
		}

		public bool Contains(T value)
		{
			return _list.Contains(value);
		}

		/// <summary>
		/// Returns if the list contains an item of ID. It required a type of AnvilScriptableObject
		/// This has a lot of casts, use with caution.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool ContainsByID(int id)
		{
			if (!(this is AnvilScriptableObject))
			{
				Debug.LogError($"Cannot 'ContainsByID' on '{this.name}' of type '{this.GetType()}' is not an AnvilScriptableObject, failsafed to false", this);
				return false;
			}

			return _list.Exists((x) => ((AnvilScriptableObject)(object)x).id == id);
		}

		/// <summary>
		/// Get the Item by ID, required type AnvilScriptableObject.
		/// This has a lot of casts, use with caution.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetByID(int id)
		{
			if (!(this is AnvilScriptableObject))
			{
				Debug.LogError($"Cannot 'GetByID' on '{this.name}' of type '{this.GetType()}' is not an AnvilScriptableObject, failsafed to default", this);
				return default;
			}

			return _list.Find((x) => ((AnvilScriptableObject)(object)x).id == id);
		}

		/// <summary>
		/// Get the Item by ID, required type AnvilScriptableObject.
		/// This has a lot of casts, use with caution.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public List<T> GetAllByID(int id)
		{
			if (!(this is AnvilScriptableObject))
			{
				Debug.LogError($"Cannot 'GetAllByID' on '{this.name}' of type '{this.GetType()}' is not an AnvilScriptableObject, failsafed to default", this);
				return default;
			}

			return _list.FindAll((x) => ((AnvilScriptableObject)(object)x).id == id);
		}

		public T GetRandomItem()
		{
			return _list[UnityEngine.Random.Range(0, _list.Count - 1)];
		}

		public int IndexOf(T value)
		{
			return _list.IndexOf(value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		public override string ToString()
		{
			return "Collection<" + typeof(T) + ">(" + Count + ")";
		}

		public T[] ToArray()
		{
			return _list.ToArray();
		}

		public void OnBeforeSerialize() {}

		public void OnAfterDeserialize()
		{
			if(resetInRuntime)
				_list = initList;
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
