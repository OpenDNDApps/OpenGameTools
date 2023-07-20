namespace OGT
{
    using System;
    using UnityEngine;
    using UnityEditor;
    using System.IO;
    using UnityEditor.Callbacks;

    public class CreateModuleWindow : EditorWindow
    {
        private static EditorWindow m_window;
        private static bool m_waitingForAssetCreation;

        private const int kFramesToWait = 100;
        private static int m_frameCount = 0;
        private static string m_generatedReadmePath = "";
        private static CreationState m_creationState;
        private static Action m_afterScriptBuildCallback;

        private static string m_moduleFolderPath;
        private static string m_moduleScriptsFolderPath;
        private static string m_modulePrefabsFolderPath;
        
        private static string m_modulePath = kModulesFolder;
        private static string m_moduleName = "MyModule";
        private static string m_moduleFullPath;
        
        private static string m_collectionPath;
        private static string m_collectionFullPath;
        private static string m_moduleResourcesPath;
        
        private static string m_settingsPath;
        private static string m_settingsFullPath;
        
        private const string kModulesFolder = "Assets/OpenGameTools/SDK/Modules/";
        private const string kModulesEditorFolder = kModulesFolder + "Core/Scripts/Editor/";
        private const string kResourceDefinitionsName = "ResourceDefinitions";
        private const string kResourcesCollectionName = "ResourcesCollection";
        private const string kSettingsCollectionName = "SettingsCollection";
        private const string kRuntimeName = "Runtime";
        private const string kTemporalScriptName = "TemporalPrefabMakingScript";
        private const string kCreateModuleMenuPath = kModulesFolder + "Core/Scripts/Editor/CreateModule/Template";
        private const string kResourceDefinitionsTemplatePath = kCreateModuleMenuPath + kResourceDefinitionsName + ".txt";
        private const string kResourcesCollectionTemplatePath = kCreateModuleMenuPath + kResourcesCollectionName + ".txt";
        private const string kSettingsCollectionTemplatePath = kCreateModuleMenuPath + kSettingsCollectionName + ".txt";
        private const string kRuntimeTemplatePath = kCreateModuleMenuPath + kRuntimeName + ".txt";
        private const string kTemporalScriptTemplatePath = kCreateModuleMenuPath + kTemporalScriptName + ".txt";

        [MenuItem(GameResources.kMenuPath + "Create Module")]
        public static void ShowWindow()
        {
            m_window = GetWindow<CreateModuleWindow>(true, "Create Module", true);
            m_window.position = new Rect(Screen.height * 0.5f - 275, Screen.width * 0.5f - 128, 520, 256);
        }

        private static void CloseWindow()
        {
            if (m_window == null)
                return;
            
            m_window.Close();
        }

        private void OnEnable()
        {
            m_window = this;
        }

        private void OnGUI()
        {
            DrawMenu();
        }

        private void DrawMenu()
        {
            GUI.enabled = !m_waitingForAssetCreation;
            m_moduleName = EditorGUILayout.TextField("Module Name", m_moduleName).UcFirst();
            m_modulePath = EditorGUILayout.TextField("Module Path", m_modulePath).UcFirst();
            GUI.enabled = true;

            UpdatePaths();
            GUI.enabled = false;
            GUILayout.Space(20f);
            GUILayout.Label("This will make:");
            GUILayout.Space(4f);
            GUILayout.Label($"Folder Path: {m_moduleFolderPath}");
            GUILayout.Label($"Module Definitions: <module>/Scripts/{m_moduleName}{kResourceDefinitionsName}.cs");

            GUILayout.Space(4f);
            GUILayout.Label($"Collection: <module>/Scripts/{m_moduleName}{kResourcesCollectionName}.cs");
            GUILayout.Label($"Collection Resources: <module>/Resources/{m_moduleName}{kResourcesCollectionName}.asset");

            GUILayout.Space(4f);
            GUILayout.Label($"Settings: <module>/Scripts/{m_moduleName}{kSettingsCollectionName}.cs");
            GUILayout.Label($"Runtime: <module>/Scripts/{m_moduleName}{kRuntimeName}.cs");
            GUI.enabled = true;

            GUILayout.Space(20f);
            GUI.enabled = !m_waitingForAssetCreation;
            if (GUILayout.Button(!m_waitingForAssetCreation ? "Create" : "Waiting for compilation. Do not close..."))
            {
                CreateModuleFiles();
            }

            GUI.enabled = true;
        }

        private void CreateModuleFiles()
        {
            m_waitingForAssetCreation = true;
            
            StartDelayedCreation();
            
            UpdatePaths();
            
            CreateDirectory(m_moduleFolderPath);
            CreateDirectory(m_moduleScriptsFolderPath);
            CreateDirectory(m_moduleResourcesPath);
            CreateDirectory(m_modulePrefabsFolderPath);
            
            string resourceDefinitionsNameScript = CreateScriptFile(m_moduleScriptsFolderPath, $"{m_moduleName}{kResourceDefinitionsName}", GetModuleDefinitionsTemplate());
            string settingsCollectionScript = CreateScriptFile(m_moduleScriptsFolderPath, $"{m_moduleName}{kSettingsCollectionName}", GetModuleSettingsTemplate());
            string resourcesCollectionScript = CreateScriptFile(m_moduleScriptsFolderPath, $"{m_moduleName}{kResourcesCollectionName}", GetModuleCollectionTemplate());
            string runtimeScript = CreateScriptFile(m_moduleScriptsFolderPath, $"{m_moduleName}{kRuntimeName}", GetModuleRuntimeTemplate());
            string temporalScript = CreateScriptFile(kModulesEditorFolder, kTemporalScriptName, GetModuleTemporalScriptTemplate());
            
            AssetDatabase.ImportAsset(resourceDefinitionsNameScript);
            AssetDatabase.ImportAsset(settingsCollectionScript);
            AssetDatabase.ImportAsset(resourcesCollectionScript);
            AssetDatabase.ImportAsset(runtimeScript);
            AssetDatabase.ImportAsset(temporalScript);
            
            AssetDatabase.Refresh();
        }

        private enum CreationState
        {
            WaitingForScriptReload,
            WaitingForAssetLoads,
            Idle,
            Complete
        }

        private void StartDelayedCreation()
        {
            m_creationState = CreationState.Idle;
            m_frameCount = 0;
            m_waitingForAssetCreation = true;
            EditorApplication.update -= DelayedCreationUpdate;
            EditorApplication.update += DelayedCreationUpdate;
        }

        public static void NotifyTemporalScriptCreation(Action callback)
        {
            m_frameCount = 0;
            m_afterScriptBuildCallback = callback;
            m_creationState = CreationState.WaitingForScriptReload;
            m_waitingForAssetCreation = true;
            EditorApplication.update -= DelayedCreationUpdate;
            EditorApplication.update += DelayedCreationUpdate;
        }

        public static void NotifyTemporalScriptAssetsLoad(string newReadmePath)
        {
            m_frameCount = 0;
            m_generatedReadmePath = newReadmePath;
            m_creationState = CreationState.WaitingForAssetLoads;
            m_waitingForAssetCreation = true;
            EditorApplication.update -= DelayedCreationUpdate;
            EditorApplication.update += DelayedCreationUpdate;
        }

        private static void DelayedCreationUpdate()
        {
            switch (m_creationState)
            {
                case CreationState.WaitingForScriptReload:
                    m_frameCount++;
                    if (m_frameCount < kFramesToWait)
                        return;

                    m_creationState = CreationState.Idle;
                    m_afterScriptBuildCallback?.Invoke();
                    break;
                case CreationState.WaitingForAssetLoads:
                    m_frameCount++;
                    if (m_frameCount < kFramesToWait)
                        return;
                
                    Selection.activeObject = AssetDatabase.LoadAssetAtPath<GameModulePackageReadme>(m_generatedReadmePath);
                    EditorGUIUtility.PingObject(Selection.activeObject);
                
                    m_creationState = CreationState.Complete;
                    break;
                case CreationState.Complete:
                    m_waitingForAssetCreation = false;
                    DestroyTemp();
                    break;
                case CreationState.Idle:
                default: break;
            }
        }

        [DidReloadScripts]
        private static void AfterReloadScripts()
        {
            if (!File.Exists(Path.Combine(kModulesEditorFolder, $"{kTemporalScriptName}.cs"))) 
                return;
            
            EditorApplication.ExecuteMenuItem($"{GameResources.kMenuPath}{kTemporalScriptName}");
        }

        private static void DestroyTemp()
        {
            EditorApplication.update -= DelayedCreationUpdate;
            
            if (!File.Exists(Path.Combine(kModulesEditorFolder, $"{kTemporalScriptName}.cs"))) 
                return;

            CloseWindow();
            AssetDatabase.DeleteAsset(Path.Combine(kModulesEditorFolder, $"{kTemporalScriptName}.cs"));
        }

        private string ParseTemplate(string template, string moduleName)
        {
            return template.Replace("#MODULE_NAME#", moduleName).Replace("#MODULE_NAME_LOWER#", moduleName.ToLowerFirst());
        }
        
        private string GetModuleRuntimeTemplate()
        {
            if (!File.Exists(kRuntimeTemplatePath))
            {
                Debug.LogError("Definitions template file not found!");
                return "";
            }

            return ParseTemplate(File.ReadAllText(kRuntimeTemplatePath), m_moduleName);
        }
        
        private string GetModuleTemporalScriptTemplate()
        {
            if (!File.Exists(kTemporalScriptTemplatePath))
            {
                Debug.LogError("Definitions template file not found!");
                return "";
            }

            string template = File.ReadAllText(kTemporalScriptTemplatePath)
                    .Replace("#MODULE_FOLDER_PATH#", m_moduleFolderPath)
                    .Replace("#MODULE_RESOURCES_COLLECTION_PATH#", m_moduleResourcesPath)
                    .Replace("#MODULE_PREFABS_FOLDER_PATH#", m_modulePrefabsFolderPath)
                    .Replace("#TEMP_SCRIPT_NAME#", kTemporalScriptName)
                    .Replace("#MODULE_NAME#", m_moduleName)
                    .Replace("#MODULE_NAME_LOWER#", m_moduleName.ToLowerFirst())
                    ;
            return template;
        }
        
        private string GetModuleDefinitionsTemplate()
        {
            if (!File.Exists(kResourceDefinitionsTemplatePath))
            {
                Debug.LogError("Definitions template file not found!");
                return "";
            }

            return ParseTemplate(File.ReadAllText(kResourceDefinitionsTemplatePath), m_moduleName);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private string GetModuleCollectionTemplate()
        {
            if (!File.Exists(kResourcesCollectionTemplatePath))
            {
                Debug.LogError("Collection template file not found!");
                return "";
            }

            return ParseTemplate(File.ReadAllText(kResourcesCollectionTemplatePath), m_moduleName);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private string GetModuleSettingsTemplate()
        {
            if (!File.Exists(kSettingsCollectionTemplatePath))
            {
                Debug.LogError("Settings template file not found!");
                return "";
            }

            return ParseTemplate(File.ReadAllText(kSettingsCollectionTemplatePath), m_moduleName);
        }
        
        private string CreateScriptFile(string directoryPath, string fileName, string fileContent)
        {
            string filePath = Path.Combine(directoryPath, $"{fileName}.cs");

            using StreamWriter streamWriter = File.CreateText(filePath);
            streamWriter.Write(fileContent);

            return filePath;
        }

        private void CreateDirectory(string basePath)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
        }

        private string GetScriptPath(string scriptName)
        {
            return Path.Combine(m_moduleFolderPath, $"{m_moduleName}{scriptName}.cs");
        }
        
        private void OnInspectorUpdate()
        {
            UpdatePaths();
            Repaint();
        }

        private void UpdatePaths()
        {
            m_moduleFolderPath = $"{Path.Combine(m_modulePath, m_moduleName)}/";
            m_moduleScriptsFolderPath = $"{Path.Combine(m_moduleFolderPath, "Scripts")}/";
            m_moduleResourcesPath = $"{Path.Combine(m_moduleFolderPath, "Resources")}/";
            m_modulePrefabsFolderPath = $"{Path.Combine(m_moduleFolderPath, "Prefabs")}/";
            m_moduleFullPath = GetScriptPath(kResourceDefinitionsName);
            m_collectionFullPath = GetScriptPath(kResourcesCollectionName);
            m_settingsFullPath = GetScriptPath(kSettingsCollectionName);
        }
    }
}