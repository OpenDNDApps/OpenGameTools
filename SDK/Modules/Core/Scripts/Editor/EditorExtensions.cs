using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace OGT
{
    public static class EditorExtensions
    {
        public static string GetFilePathForType(Type type)
        {
            Assembly assembly = type.Assembly;
            string assemblyPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(ScriptableObject.CreateInstance(type)));
            string assemblyFolder = Path.GetDirectoryName(assemblyPath);

            string[] sourceFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

            foreach (string sourceFile in sourceFiles)
            {
                string relativePath = "Assets" + sourceFile.Substring(Application.dataPath.Length);

                if (relativePath.EndsWith(".cs"))
                {
                    string className = Path.GetFileNameWithoutExtension(relativePath);

                    if (className.Equals(type.Name))
                    {
                        return relativePath;
                    }
                }
            }

            return "";
        }
    }
}