namespace OGT
{
    using static GameResources;
    using static UIResourcesCollection;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    
    public partial class UIRuntime : MonoSingletonSelfGenerated<UIRuntime>, IGameDependency
    {
		[SerializeField] private List<UIScreenPanelContainer> m_canvases = new List<UIScreenPanelContainer>();
        
        private Dictionary<UISectionType, List<UIWindow>> m_windowHistory = new Dictionary<UISectionType, List<UIWindow>>();
        private Camera m_camera;

        public static bool IsReady { get; private set; }
        public event Action OnManualUpdate;
        public event Action OnManualLateUpdate;
        public event Action OnManualFixedUpdate;

        public event Action<UIWindow> OnWindowRemoved;
        public List<UIScreenPanelContainer> Canvases => m_canvases;
		public Camera WorldCamera
        {
            get => GameRuntime.WorldCamera;
            set => GameRuntime.WorldCamera = value;
        }
        
        public static Camera UICamera
        {
            get
            {
                if (Instance.m_camera == default)
                    Instance.m_camera = Camera.main;
                return Instance.m_camera;
            }
            set
            {
                foreach (UIScreenPanelContainer container in Instance.m_canvases)
                {
                    container.Canvas.worldCamera = value;
                }
                Instance.m_camera = value;
            }
        }

		protected override void OnSingletonAwake()
        {
            base.OnSingletonAwake();
            SceneManager.activeSceneChanged += OnActiveSceneChange;
            Initialize();
        }

        private void OnActiveSceneChange(Scene current, Scene next)
        {
            Initialize();
        }

        partial void OnUIRuntimeTMPInitialize();
        partial void OnUIRuntimeResponsivenessInitialize();

        private void Initialize()
        {
            foreach (UIScreenPanelContainer container in m_canvases)
            {
                if (!Settings.UI.Sorting.Exists(sort => sort.Type.Equals(container.Type)))
                    continue;

                UISortingKeyPair sort = Settings.UI.Sorting.Find(sort => sort.Type.Equals(container.Type));
                container.Canvas.sortingOrder = sort.SortOrder;
                container.Canvas.planeDistance = sort.PlaneDistance;
                container.Canvas.sortingLayerName = sort.Type.ToString();
            }

            m_windowHistory.Clear();
            foreach (UISectionType sectionType in Enum.GetValues(typeof(UISectionType)))
            {
                m_windowHistory.Add(sectionType, new List<UIWindow>());
            }

            IsReady = true;
            OnUIRuntimeTMPInitialize();
            OnUIRuntimeResponsivenessInitialize();
        }

        private void Update() => OnManualUpdate?.Invoke();
        private void LateUpdate() => OnManualLateUpdate?.Invoke();
        private void FixedUpdate() => OnManualFixedUpdate?.Invoke();

        public static Canvas GetCanvasOfType(UISectionType type)
        {
            UIScreenPanelContainer found = GameResources.UIRuntime.m_canvases.Find(canvas => canvas.Type == type);
            if (found.Canvas == default)
            {
                Debug.LogError($"Canvas of type '{type}' does not exist in UI Controller.");
            }
            return found.Canvas;
        }

        public void ShowTransition(Action onComplete = null, string transitionKey = null, bool addToList = false)
        {
            bool found = UI.TryGetUIItem(transitionKey, out UIItem prefab);
            if (!found)
            {
                Debug.LogWarning($"[UIRuntime] Transition id '{transitionKey}' was not found, failsafe to default transition.");
                bool foundDefault = UI.TryGetUIItem(Settings.UI.Default.Transition.name, out prefab);
                if (!foundDefault)
                {
                    Debug.LogError($"[UIRuntime] There's no default transitions, defaulting to OnComplete");
                    onComplete?.Invoke();
                    return;
                }
            }
        
            ShowTransition(prefab, onComplete, addToList);
        }

		public void ShowTransition(UIItem transitionPrefab, Action callback = null, bool addToList = false)
        {
            if (transitionPrefab == default)
            {
                Debug.LogError($"[UIRuntime] Missing Transition prefab, defaulting to OnComplete");
                callback?.Invoke();
                return;
            }
            UIItem transitionItem = InstantiateUI(transitionPrefab, addToList);
            transitionItem.OnShow += callback;
            transitionItem.AnimatedShow();
		}

        public bool TryGetUISectionByType(UISectionType type, out UIScreenPanelContainer container) => m_canvases.TryGetUISectionByType(type, out container);

        public T InstantiateUI<T>(T prefab, bool addToList = false) where T : MonoBehaviour
        {
            UISectionType sectionType = UISectionType.Undefined;
            if (prefab is UIWindow window)
                sectionType = window.SectionType;

            if (sectionType == UISectionType.Undefined)
                return default;
            
            bool isContained = m_canvases.TryGetUISectionByType(sectionType, out UIScreenPanelContainer rootContainer);
            T newItem = Instantiate(prefab, isContained ? rootContainer.Canvas.transform : null);

            if (newItem is UIWindow newWindow && addToList)
                m_windowHistory[newWindow.SectionType].Add(newWindow);
            
            return newItem;
        }

        private void CleanUISection<T>(T prefab) where T : MonoBehaviour
        {
            UISectionType sectionType = UISectionType.Undefined;
            if (prefab is UIWindow window)
                sectionType = window.SectionType;

            CleanUISection(sectionType);
        }

        private static void CleanUISection(UISectionType sectionType)
        {
            if (!GameResources.UIRuntime.m_windowHistory.ContainsKey(sectionType))
                return;
            
            for (int i = GameResources.UIRuntime.m_windowHistory[sectionType].Count - 1; i >= 0; i--)
            {
                RemoveWindow(GameResources.UIRuntime.m_windowHistory[sectionType][i]);
            }
        }

        public static void RemoveWindow<T>(T window) where T : UIWindow
        {
            window.OnHide += () =>
            {
                GameResources.UIRuntime.OnWindowRemoved?.Invoke(window);
                GameResources.UIRuntime.SafeDestroy(window.gameObject);
            };
            window.Hide();
        }

        public static bool TryCreateWindow<T>(out T spawnedWindow, bool addToList = false, Action onFailed = null) where T : UIWindow
        {
            return TryCreateWindow(typeof(T).Name, out spawnedWindow, addToList, onFailed);
        }

        public static bool TryCreateWindow<T>(string windowName, out T spawnedWindow, bool addToList = false, Action onFailed = null) where T : UIWindow
        {
            spawnedWindow = default;
            bool found = UI.TryGetUIWindowPrefab(windowName, out UIWindow window);
            if (!found)
            {
                Debug.LogError($"[UIRuntime] There's no windows of name '{windowName}'");
                onFailed?.Invoke();
                return false;
            }

            spawnedWindow = GameResources.UIRuntime.InstantiateUI(window as T, true);

            if (addToList)
                GameResources.UIRuntime.m_windowHistory[window.SectionType].Add(window);
            
            return true;
        }

        public static bool TryShowGenericError(string errorMessage)
        {
            bool valid = TryCreateWindow("UIAlert - Error", out UIAlertPopup alertPopup);
            alertPopup.Build(errorMessage);
            alertPopup.AnimatedShow();
            
            return valid;
        }

        public static void NotifyWindowDestroy(UIWindow uiWindow)
        {
            bool removed = GameResources.UIRuntime.m_windowHistory[uiWindow.SectionType].Remove(uiWindow);
            if(removed) GameResources.UIRuntime.OnWindowRemoved?.Invoke(uiWindow);
        }

        public bool TryShowGenericMessagePopup(out UIGenericTextMessagePopup messagePopup)
        {
            return TryCreateWindow(out messagePopup);
        }
    }

	public static class UIRuntimeExtensions
	{
		public static bool TryGetUISectionByType(this List<UIScreenPanelContainer> list, UISectionType type, out UIScreenPanelContainer container)
		{
			container = default;
			foreach (UIScreenPanelContainer item in list)
			{
				if (item.Type != type) continue;
				
				container = item;
				return true;
			}

			return false;
		}
	}

	[Serializable]
	public struct UIScreenPanelContainer
	{
		public UISectionType Type;
		public Canvas Canvas;
	}

	public enum UISectionType
	{
		Undefined = -1, Background, Default, Popup, Overlay
	}
}