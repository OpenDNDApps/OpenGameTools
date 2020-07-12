using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Anvil3D
{
	[CreateAssetMenu(menuName = "Anvil3D/Behaviours/GameBehaviour")]
	public class GameBehaviour : AnvilScriptableObject
	{
		[PropertySpace(50), ToggleGroup("onChangeToggle", "On Change Events", Order = 9900)]
		public bool onBehavedToggle = false;
		[ToggleGroup("onChangeToggle", Order = 9901)]
		public GameEvent OnBehavedScriptableObject;
		[ToggleGroup("onChangeToggle", Order = 9902)]
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