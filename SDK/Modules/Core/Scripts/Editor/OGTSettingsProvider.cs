using System.Collections.Generic;
using OGT;
using UnityEngine;

namespace ORC.Editor
{
    using UnityEditor;
    
    public class OGTSettingsProvider : SettingsProvider
    {
        private const SettingsScope kSettingsScope = SettingsScope.Project;
        private static Object GetSettings() => GameResources.Settings;
        //private static Object GetUIResources() => GameResources.UI;
        //private static Object GetAudioResources() => GameResources.Audio;

        private OGTSettingsProvider() : base($"{kSettingsScope}/{OGTConstants.kPluginFullName}", kSettingsScope) { }

        [SettingsProviderGroup]
        private static SettingsProvider[] CreateOGTSettingsProvider()
        {
            var providers = new List<SettingsProvider> { new OGTSettingsProvider() };

            if (GetSettings() != null)
            {
                AssetSettingsProvider provider = new AssetSettingsProvider($"{kSettingsScope}/{OGTConstants.kPluginFullName}/Settings", GetSettings);
                provider.PopulateSearchKeywordsFromGUIContentProperties<GameSettingsCollection>();
                providers.Add(provider);
            }
            
            // if (GetUIResources() != null)
            // {
            //     AssetSettingsProvider provider = new AssetSettingsProvider($"{kSettingsScope}/{OGTConstants.kPluginFullName}/UI Resources", GetUIResources);
            //     provider.PopulateSearchKeywordsFromGUIContentProperties<UIResourcesCollection>();
            //     providers.Add(provider);
            // }
            
            // if (GetAudioResources() != null)
            // {
            //     AssetSettingsProvider provider = new AssetSettingsProvider($"{kSettingsScope}/{OGTConstants.kPluginFullName}/Audio Resources", GetAudioResources);
            //     provider.PopulateSearchKeywordsFromGUIContentProperties<GameAudioResourcesCollection>();
            //     providers.Add(provider);
            // }
            
            return providers.ToArray();
        }
    }
}
