using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using UnityEditor.Callbacks;

namespace OGT
{
    [Serializable]
    public class CreateModuleData
    {
        public string SoCollectionAssetPath;
        public string ResourcesCollectionFileName;
        public string RuntimeFileName;
        public string ModulePrefabsFolderPath;
    }
    
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public class CreateModuleWindow : EditorWindow
    {
        private static EditorWindow m_window;
        private static bool m_waitingForAssetCreation;
        
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
        private const string kTemporalDataFilePath = "Assets/OpenGameTools/CreateMenuTempData.json";
        private const string kResourceDefinitionsName = "ResourceDefinitions";
        private const string kResourcesCollectionName = "ResourcesCollection";
        private const string kSettingsCollectionName = "SettingsCollection";
        private const string kRuntimeName = "Runtime";
        private const string kResourceDefinitionsTemplatePath = kModulesFolder + "Core/Scripts/Editor/CreateModule/Template" + kResourceDefinitionsName + ".txt";
        private const string kResourcesCollectionTemplatePath = kModulesFolder + "Core/Scripts/Editor/CreateModule/Template" + kResourcesCollectionName + ".txt";
        private const string kSettingsCollectionTemplatePath = kModulesFolder + "Core/Scripts/Editor/CreateModule/Template" + kSettingsCollectionName + ".txt";
        private const string kRuntimeTemplatePath = kModulesFolder + "Core/Scripts/Editor/CreateModule/Template" + kRuntimeName + ".txt";

        [MenuItem(GameResources.kPluginName + "/Create Module")]
        public static void ShowWindow()
        {
            m_window = GetWindow<CreateModuleWindow>(true, "Create Module", true);
            m_window.position = new Rect(0, 0, 520, 256)
            {
                center = new Vector2(Screen.width, Screen.height) * 0.5f
            };
        }

        private void OnGUI()
        {
            GUI.enabled = !m_waitingForAssetCreation;
            m_moduleName = EditorGUILayout.TextField("Module Name", m_moduleName).UcFirst();
            m_modulePath = EditorGUILayout.TextField("Module Path", m_modulePath).UcFirst();
            GUI.enabled = true;
            
            UpdatePaths();
            GUI.enabled = false;
            GUILayout.Space(20f);
            GUILayout.Label($"This will make:");
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
            if (GUILayout.Button(!m_waitingForAssetCreation ? "Create" : "Waiting Compilation - Do not close..."))
            {
                CreateModuleFiles();
            }
            GUI.enabled = true;
        }

        private void CreateModuleFiles()
        {
            m_waitingForAssetCreation = true;
            UpdatePaths();

            SaveCreateModuleData(new CreateModuleData
            {
                SoCollectionAssetPath = Path.Combine(m_moduleResourcesPath, $"{m_moduleName}{kResourcesCollectionName}.asset"),
                ResourcesCollectionFileName = m_moduleName + kResourcesCollectionName,
                RuntimeFileName = m_moduleName + kRuntimeName,
                ModulePrefabsFolderPath = m_modulePrefabsFolderPath
            });
            
            CreateDirectory(m_moduleFolderPath);
            CreateDirectory(m_moduleScriptsFolderPath);
            CreateDirectory(m_moduleResourcesPath);
            CreateDirectory(m_modulePrefabsFolderPath);
            
            var resourceDefinitionsNameScript = CreateScriptFile(m_moduleScriptsFolderPath, m_moduleName + kResourceDefinitionsName, GetModuleDefinitionsTemplate());
            var settingsCollectionScript = CreateScriptFile(m_moduleScriptsFolderPath, m_moduleName + kSettingsCollectionName, GetModuleSettingsTemplate());
            var resourcesCollectionScript = CreateScriptFile(m_moduleScriptsFolderPath, m_moduleName + kResourcesCollectionName, GetModuleCollectionTemplate());
            var runtimeScript = CreateScriptFile(m_moduleScriptsFolderPath, m_moduleName + kRuntimeName, GetModuleRuntimeTemplate());
            
            AssetDatabase.ImportAsset(resourceDefinitionsNameScript);
            AssetDatabase.ImportAsset(settingsCollectionScript);
            AssetDatabase.ImportAsset(resourcesCollectionScript);
            AssetDatabase.ImportAsset(runtimeScript);
            AssetDatabase.ImportAsset(kTemporalDataFilePath);
            
            AssetDatabase.Refresh();
        }

        [DidReloadScripts]
        private static void CreateAssets()
        {
            if (!File.Exists(kTemporalDataFilePath))
                return;
        
            CreateModuleData tempData = LoadCreateModuleData();
            if (tempData == null)
                return;

            Type runtimeType = Type.GetType(tempData.RuntimeFileName);
            string runtimeFullPath = $"{tempData.ModulePrefabsFolderPath}{tempData.RuntimeFileName}.prefab";

            if (runtimeType != null)
            {
                GameObject gameObject = new GameObject()
                {
                    name = tempData.RuntimeFileName
                };
                gameObject.AddComponent(runtimeType);
                PrefabUtility.SaveAsPrefabAsset(gameObject, runtimeFullPath);
                UnityEngine.Object.DestroyImmediate(gameObject);
            }
        
            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(tempData.ResourcesCollectionFileName);
        
            AssetDatabase.CreateAsset(scriptableObject, tempData.SoCollectionAssetPath);
        
            AssetDatabase.ImportAsset(tempData.SoCollectionAssetPath);
            if (runtimeType != null)
            {
                AssetDatabase.ImportAsset(runtimeFullPath);
            }
        
            AssetDatabase.Refresh();

            if (m_window != null)
            {
                m_window.Close();
            }
        }

        // void OnEnable()
        // {
        //     m_window = this;
        //     AssemblyReloadEvents.afterAssemblyReload += CreateAssets;
        // }
        //
        // void OnDisable()
        // {
        //     AssemblyReloadEvents.afterAssemblyReload -= CreateAssets;
        // }

        private static void SaveCreateModuleData(CreateModuleData data)
        {
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(kTemporalDataFilePath, jsonData);
        }

        private static CreateModuleData LoadCreateModuleData()
        {
            if (!File.Exists(kTemporalDataFilePath)) 
                return null;
            
            string jsonData = File.ReadAllText(kTemporalDataFilePath);
            CreateModuleData data = JsonConvert.DeserializeObject<CreateModuleData>(jsonData);
            File.Delete(kTemporalDataFilePath);
            File.Delete(kTemporalDataFilePath + ".meta");
            return data;
        }

        private string ParseTemplate(string template, string moduleName)
        {
            return template.Replace("#MODULE_NAME#", moduleName).Replace("#MODULE_NAME_LOWER#", moduleName.ToLower());
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
            string filePath = Path.Combine(directoryPath, fileName + ".cs");

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
            return Path.Combine(m_moduleFolderPath, m_moduleName + scriptName + ".cs");
        }
        
        private void OnInspectorUpdate()
        {
            UpdatePaths();
            Repaint();
        }

        private void UpdatePaths()
        {
            m_moduleFolderPath = Path.Combine(m_modulePath, m_moduleName) + "/";
            m_moduleScriptsFolderPath = Path.Combine(m_moduleFolderPath, "Scripts") + "/";
            m_moduleResourcesPath = Path.Combine(m_moduleFolderPath, "Resources") + "/";
            m_modulePrefabsFolderPath = Path.Combine(m_moduleFolderPath, "Prefabs") + "/";
            m_moduleFullPath = GetScriptPath(kResourceDefinitionsName);
            m_collectionFullPath = GetScriptPath(kResourcesCollectionName);
            m_settingsFullPath = GetScriptPath(kSettingsCollectionName);
        }
    } 
}