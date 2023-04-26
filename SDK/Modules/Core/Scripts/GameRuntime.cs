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

        public static Action<CameraController, CameraController> OnCameraControllerChanged;

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

        public static event Action ManualTickUpdater;

        protected override void OnSingletonAwake()
        {
            base.OnSingletonAwake();
            SceneManager.activeSceneChanged += OnActiveSceneChange;

            IsReady = true;
        }

        private void Update()
        {
            ManualTickUpdater?.Invoke();
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

        public bool IsReady { get; set; }
    }
}