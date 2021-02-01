using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu( menuName = Anvil3D.kCreateMenuPrefixName + "Game/Item - Weapon")]
	public class WeaponData : BaseItemData
	{
		[BoxGroup]
		public Damage Damage;
		public List<Damage> ExtraDamages;
		public int Durability;
		
		public WeaponType WeaponType;
		public WeaponFlags WeaponFlags;
	}

	[System.Flags]
	public enum WeaponType
	{
		Melee, Ranged, Siege
	}

	[System.Flags]
	public enum WeaponFlags
	{
		MainHand, Offhand, OneHand, TwoHanded,
	}

	[Serializable]
	public class Damage
	{
		public int Min;
		public int Max;
		public DamageType Type;

		public int Value
		{
			get
			{
				return UnityEngine.Random.Range(Min, Max)  *  Anvil3D.Database.DamageModifiers.GetModByType(Type);
			}
		}
	}

	[Serializable]
	public class DamageModifiers
	{
		public List<DamageModifier> Modifiers;

		public int GetModByType( DamageType type )
		{
			return Modifiers.Find((mod) => mod.Type == type).Modifier;
		}
	}
	
	[Serializable]
	public struct DamageModifier
	{
		public DamageType Type;
		public int Modifier;
	}

	[Serializable]
	public enum DamageType
	{
		None = 0,
		Physical,
		Magical,
		Piercing,
		Bludgeoning,
	}
}