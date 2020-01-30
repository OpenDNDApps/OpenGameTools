using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(menuName = "Anvil3D/Example/ItemData")]
	public class ItemData : AnvilScriptableObject
	{
		public string title;
		[BoxGroup]
		[PreviewField(100, ObjectFieldAlignment.Center)]
		public Sprite art;
		public Transform body;
		public ItemType type;
		public ItemSize size;
		public ItemFlags flags;
	}

	public enum ItemSize
	{
		Tiny, Small, Medium, Big, Huge
	}

	[System.Flags]
	public enum ItemType
	{
		None = 0, Consumable = 1, Equipable = 2, Weapon = 4, Container = 8
	}

	[System.Flags]
	public enum ItemFlags
	{
		None = 0, QuestItem, 
	}
}