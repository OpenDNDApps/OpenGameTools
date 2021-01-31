using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//
//      Make your game in any way you want in the scenes AFTER requirements scene.
//      If you feel this is unnecessary for your game, just let them be but don't delete them, you'll thank me later ;) 
//
//   
//    +--------------+
//    |  Init Scene  |      // So, this Scene is super lightweight and only loads other stuff.
//    +------+-------+    
//           |
//           |
//           v
//    +--------------+
//    | Requirements |      // This Scene contains the heavy stuff, textures, sprites, models, whatever you need.
//    |    Scene     |      // This Scene also contains Anvil's Event systems if you want to use it :D 
//    +------+-------+
//           |
//           |
//           v
//    +--------------+        +-------------+
//    |  First Game  +------> |  Any Other  |
//    | Scene (menu?)| <------+  Game Scene |
//    +--------------+        +-------------+
//   

namespace  Anvil3D
{
    public class AppInitialization : MonoBehaviour
    {
        [Header("Scene Config - Important stuff here")] 
        [SerializeField] private string m_requirementSceneName;
        [SerializeField] private string m_firstGameSceneName;
        [SerializeField] private bool m_doDebug = true;
        
        [Header("Loading Messages - Flavour Things")] 
        [SerializeField] private Slider m_loadingSlider;
        [SerializeField] private TMP_Text m_loadingMessage;
        [SerializeField] private string m_messageWhileLoadingRequirements;
        [SerializeField] private string m_messageWhileLoadingFirstScene;
        
        
        private void Awake()
        {
            // We need to handle the game up to the Menu or HUB scene. so.
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            //LoadRequirements();

            // This is for DEMO purposes, it delays the call for 3 seconds, delete if you want, just use the method right away.
            string fakeMessage = "Fake Loading... for shows :D";
            m_loadingMessage.text = fakeMessage;
            DOVirtual.Float(0f, 1f, 3f, (value) =>
            {
                m_loadingSlider.value = value;
                m_loadingMessage.text = $"{fakeMessage} - ({ (int)value*100f }%)";
            }).SetEase(Ease.OutSine).OnComplete(LoadRequirements);
        }

        private void LoadRequirements()
        {
            this.StartCoroutine(
                IELoadScene(m_requirementSceneName, 
                        m_loadingMessage,
                        m_loadingSlider, 
                        m_messageWhileLoadingRequirements,
                        LoadFirstScene
                    )
                );
        }

        private void LoadFirstScene()
        {
            this.StartCoroutine(
                IELoadScene(m_firstGameSceneName, 
                    m_loadingMessage,
                    m_loadingSlider, 
                    m_messageWhileLoadingFirstScene,
                    OnFinishLoadingAllInitScenes
                )
            );
        }

        private IEnumerator IELoadScene(string sceneName, TMP_Text loadingTMP, Slider progressSlider, string loadingMessage, Action onComplete = null)
        {
            if(m_doDebug)
                Debug.Log($"Loading '{sceneName}' scene...");
            
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            if (!(progressSlider is null))
            {
                loadingTMP.text = loadingMessage;
            }
            
            while (!operation.isDone)
            {
                if (!(progressSlider is null))
                {
                    progressSlider.value = operation.progress;
                    loadingTMP.text = $"{loadingMessage} - ({ (int)operation.progress*100f }%)";
                }
                yield return new WaitForEndOfFrame();
            }
            
            if(m_doDebug)
                Debug.Log($"'{sceneName}' - Load Finished.");

            onComplete?.Invoke();
        }

        private void OnFinishLoadingAllInitScenes()
        {
            Destroy(this.gameObject);
        }
    }
}
