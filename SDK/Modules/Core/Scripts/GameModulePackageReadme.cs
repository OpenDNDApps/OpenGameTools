using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = "Readme", menuName = OGTConstants.kCreateMenuPrefixNameResources + "📃 Module Readme")]
    public class GameModulePackageReadme : ScriptableObject
    {
        public string ModuleName;

        public string Description;

        public string DocumentationURL;

        public string TaskManagerURL;

        public List<string> Dependencies = new List<string>();
    
        private void OpenJira()
        {
            if (!TaskManagerURL.IsValidUrl())
                return;
            Application.OpenURL(TaskManagerURL);
        }
        
        private void OpenConfluence()
        {
            if (!DocumentationURL.IsValidUrl())
                return;
            Application.OpenURL(DocumentationURL);
        }
        
        #region Validation
        
        private bool ValidateURL(string url)
        {
            return url.IsValidUrl();
        }
        
        #endregion
    }
}