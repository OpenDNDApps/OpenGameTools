using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu( menuName = "Anvil3D/Example/WeaponData")]
	public class WeaponData : AnvilScriptableObject
	{
		public string title;
		[BoxGroup]
		public Damage damage;
		public List<Damage> modDamages;
		public int durability;
		[PreviewField(100, ObjectFieldAlignment.Center)]
		public Sprite art;
		public Transform body;
		public WeaponType type;
		public WeaponSize size;
		public WeaponUsage usage;
	}

	public enum WeaponSize
	{
		Tiny, Small, Medium, Big, Huge
	}

	public enum WeaponType
	{
		Melee, Ranged, Siege
	}

	public enum WeaponUsage
	{
		Mainhand, Offhand, Onehand, Twohanded,
	}

	[Serializable]
	public class Damage
	{
		public int min;
		public int max;
		public DamageType type;

		public int Value
		{
			get
			{
				return UnityEngine.Random.Range(min, max)  *  Anvil3D.Settings.DamageModifiers.GetModByType(type);
			}
		}
	}

	[Serializable]
	public class DamageModifiers
	{
		public List<DamageModifier> modifiers;

		public int GetModByType( DamageType _type )
		{
			return modifiers.Find((mod) => mod.type == _type).modifier;
		}
	}
	
	[Serializable]
	public struct DamageModifier
	{
		public DamageType type;
		public int modifier;
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