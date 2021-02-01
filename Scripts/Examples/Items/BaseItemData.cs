using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(menuName = Anvil3D.kCreateMenuPrefixName + "Game/Item")]
	public class BaseItemData : AnvilScriptableObject
	{
		public string Title;
		[BoxGroup, PreviewField(100, ObjectFieldAlignment.Center)]
		public Sprite Art;
		public Transform Body;
		public ItemType Type;
		public ItemSize Size;
		public ItemFlags Flags;
	}

	public enum ItemSize
	{
		Tiny, Small, Medium, Big, Huge
	}

	[System.Flags]
	public enum ItemType
	{
		None = 0, Consumable = 1, Equippable = 2, Weapon = 4, Container = 8
	}

	[System.Flags]
	public enum ItemFlags
	{
		None = 0, QuestItem, 
	}
}