using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Anvil3D
{
	[AddComponentMenu("")]
	public class AnvilMonoBehaviour : AnvilMonoBase, IOnChangeHandler
	{
		[PropertySpace(50f), ToggleGroup("onChangeToggle", "On Change Events", Order = 9900)]
		public bool onChangeToggle = false;

		[ToggleGroup("onChangeToggle", Order = 9901)]
		public GameEvent OnChangeGameEvent;

		[ToggleGroup("onChangeToggle", Order = 9902)]
		public UnityEvent OnChangeUnityEvent;

		[ToggleGroup("onChangeToggle", Order = 9902)]
		public UnityAction OnChangeUnityAction;

		[ToggleGroup("onChangeToggle", Order = 9901), PropertyOrder(9903), Button("Trigger OnChange"), GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public virtual void OnChange()
		{
			OnChangeGameEvent?.Raise();
			OnChangeUnityEvent?.Invoke();
			OnChangeUnityAction?.Invoke();
		}

		public void OnEnable()
		{
			if(onChangeToggle)
			{
				EventsManager.StartListening(this.GetInstanceID().ToString(), OnChange);
			}
		}

		public void OnDisable()
		{
			if (onChangeToggle)
			{
				EventsManager.StopListening(this.GetInstanceID().ToString(), OnChange);
			}
		}
	}
}