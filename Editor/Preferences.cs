/*
 * Preferences.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using UnityEngine;
    using UnityEditor;

    public static partial class Preferences
    {
        const int GENERAL_TAB = 0;
        const int SHARED_TAB = 1;
        const int MAC_TAB = 2;
        const int STEAM_TAB = 3;
        const int APP_STORE_TAB = 4;
        const int IOS_TAB = 5;

        static string[] PreferencesTabNames = new string[] {
            "General Settings",
            "Shared Settings",
            "Mac Settings",
            "iOS Settings",
            "Steam Deployment",
            "App Store Deployment"
        };
        static int[] PreferencesTabValues = new int[] {
            GENERAL_TAB, SHARED_TAB, MAC_TAB, IOS_TAB, STEAM_TAB, APP_STORE_TAB
        };

        static Rect _preferencesSettingTab = new Rect(250, 15, 130, 15);
        static Rect _preferencesSave = new Rect(390, 15, 50, 15);
        static Rect _preferencesLoad = new Rect(440, 15, 50, 15);

        /// <summary>
        /// uBuild Preferences Item
        /// </summary>
        [PreferenceItem("uBuild")]
        static void BuildSystemPreferencesItem()
        {
            
            if ( EditorApplication.isCompiling ) {
                EditorGUILayout.HelpBox("The editor is currently compiling, please wait ...", MessageType.Info);
                return;
            }
             
            int newValue = EditorGUI.IntPopup(_preferencesSettingTab,
                Settings.SettingsTab, 
                PreferencesTabNames, 
                PreferencesTabValues); 
                
            if ( newValue != Settings.SettingsTab ){
                Settings.SettingsTab = newValue;
                EditorGUIUtility.editingTextField = false;
                
                _platformsScroll = Vector2.zero;
            }
            
            
            if (GUI.Button(_preferencesSave, "Save", EditorStyles.miniButtonLeft))
            {
                EditorApplication.delayCall += SaveSettings;
            }

            if (GUI.Button(_preferencesLoad, "Load", EditorStyles.miniButtonRight))
            {
                EditorApplication.delayCall += LoadSettings;

            }

            EditorGUILayout.Space();

            switch (Settings.SettingsTab)
            {
                default:
                    RenderGeneralTab();
                    break;
                case SHARED_TAB:
                    RenderSharedTab();
                    break;
                case MAC_TAB:
                    RenderMacTab();
                    break;                    
                case STEAM_TAB:
                    RenderSteamTab();
                    break;
                case APP_STORE_TAB:
                    RenderAppStore();
                    break;
                case IOS_TAB:
                    RenderiOSTab();
                    break;
            }
        }
        
        static void SaveSettings()
        {
            // Stop editing anything
            EditorGUIUtility.editingTextField = false;
            
            SettingsFile settings = new SettingsFile();

            // Set all settings
            settings.Read();

            var outputFile = EditorUtility.SaveFilePanel("Save Settings", Build.ProjectFolder, "uBuildSettings", "json");

            if (!string.IsNullOrEmpty(outputFile))
            {
                System.IO.File.WriteAllText(outputFile, JsonUtility.ToJson(settings));
            }
        }
        static void LoadSettings()
        {
            // Stop editing anything
            EditorGUIUtility.editingTextField = false;
            
            var loadPath = EditorUtility.OpenFilePanel("Open Settings", Build.ProjectFolder, "json");
            if (!string.IsNullOrEmpty(loadPath))
            {

                string rawSettings = System.IO.File.ReadAllText(loadPath);

                SettingsFile settings = JsonUtility.FromJson<SettingsFile>(rawSettings);
                settings.Apply();
            }
        }
    }
}