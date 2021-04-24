using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// WIP
namespace VGDevs
{
	[CreateAssetMenu(menuName = VGDevs.kCreateMenuPrefixName + "/Behaviours/BaseBehaviour")]
	public class BaseBehaviour : VGDevsScriptableObject
	{
		[PropertySpace(50), ToggleGroup("onBehavedToggle", "On Change Events", Order = 9900)]
		public bool onBehavedToggle = false;
		[ToggleGroup("onBehavedToggle", Order = 9901)]
		public GameEvent OnBehavedScriptableObject;
		[ToggleGroup("onBehavedToggle", Order = 9902)]
		public UnityEvent OnBehavedEvent;

		public void Behave()
		{
			PreBehaviourLogic();
		}

		public virtual void PreBehaviourLogic()
		{
			BehaviourLogic();
		}

		public virtual void BehaviourLogic()
		{
			PostBehaviourLogic();
		}

		public virtual void PostBehaviourLogic()
		{
			OnBehaved();
		}

		[EnableIf("onChangeToggle"), PropertyOrder(9903), Button("Trigger OnChange"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public void OnBehaved()
		{
			OnBehavedScriptableObject?.Raise();
			OnBehavedEvent?.Invoke();
		}
	}
}