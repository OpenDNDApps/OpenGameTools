using System;
using System.IO;
using UnityEngine;

namespace OGT
{
    public static class EditorExtensions
    {
        public static string GetFilePathForType(Type type)
        {
            string[] sourceFiles = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

            foreach (string sourceFile in sourceFiles)
            {
                string relativePath = "Assets" + sourceFile[Application.dataPath.Length..];

                if (!relativePath.EndsWith(".cs")) 
                    continue;
                
                string className = Path.GetFileNameWithoutExtension(relativePath);

                if (className.Equals(type.Name))
                    return relativePath;
            }

            return "";
        }
    }
}