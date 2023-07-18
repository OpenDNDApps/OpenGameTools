using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OGT
{
    public partial class GameRuntime : MonoSingletonSelfGenerated<GameRuntime>, IGameDependency
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private CameraController m_mainCameraController;
        
        private SceneManager m_sceneManager;

        public static event Action<CameraController, CameraController> OnCameraControllerChanged;
        public static event Action OnManualTickUpdater;
        public static bool IsReady { get; private set; }

        public static Camera WorldCamera
        {
            get
            {
                if (Instance.m_camera == null)
                    Instance.m_camera = Camera.main;
                return Instance.m_camera;
            }
            set => Instance.m_camera = value;
        }
        
        public Camera UICamera
        {
            get => UIRuntime.UICamera;
            set => UIRuntime.UICamera = value;
        }

        protected override void OnSingletonAwake()
        {
            base.OnSingletonAwake();
            SceneManager.activeSceneChanged += OnActiveSceneChange;

            IsReady = true;
        }

        private void Update()
        {
            OnManualTickUpdater?.Invoke();
        }

        private void OnActiveSceneChange(Scene current, Scene next)
        {
            m_camera = Camera.main;
        }

        public CameraController MainCameraController
        {
            get => m_mainCameraController;
            set
            {
                OnCameraControllerChanged?.Invoke(m_mainCameraController, value);
                m_mainCameraController = value;
            }
        }
    }
}