using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Anvil3D
{
	public class PathPositioner : MonoBehaviour
	{
		public CubicPath path;
		public List<Transform> items;
		public float spacing = 0.2f;

		public float itemMovementDuration = 0.3f;

		public void AddItem(Transform _item)
		{
			items.Add(_item);
			SortItems();
		}

		public void RemoveItem(Transform item)
		{
			items.Remove(item);
			SortItems();
		}

		[Button]
		[ContextMenu("Sort")]
		public void SortItems()
		{
			for (int i = 0; i < items.Count-1; i++)
			{
				items[i].DOLocalMove(GetItemPositionInPath(i), itemMovementDuration);
			}
		}

		public Vector3 GetItemPositionInPath(int itemIndex)
		{
			// if you have an even number of items, add aditional spacing on the first item.
			float oddSpacing = (itemIndex == 0 && (items.Count % 2 == 0)) ? 0f : spacing / 2f;

			float pathStartPoint = 0.5f; //Center
			float itemPositionInPath = pathStartPoint;

			Vector3 _position = path.GetPoint(itemPositionInPath);
			return _position;
		}


		[Header("Debug")]
		[SerializeField]
		float _lineResolution = 10f;
		[SerializeField]
		float _spherePointSize = 1f;

		void OnDrawGizmos()
		{
			if (path.waypoints.Count != 4)
			{
				return;
			}

			Gizmos.color = new Color(1, 1, 0, 0.75F);

			List<Vector3> _positions = new List<Vector3>();
			for (float i = 0f; i <= 1f; i += 1f / _lineResolution)
			{
				Vector3 pos = path.GetPoint(i);
				_positions.Add(pos);
				Gizmos.DrawSphere(pos, _spherePointSize);

				if (_positions.Count > 1)
				{
					Gizmos.DrawLine(pos, _positions[_positions.Count - 2]);
				}
			}
		}
	}
}