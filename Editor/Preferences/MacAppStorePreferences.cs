/*
 * MacAppStorePreferences.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using System.IO;
    using UnityEngine;
    using UnityEditor;

    public static partial class Preferences
    {
        static void RenderMacAppStore()
        {
            EditorGUILayout.LabelField("Apple Developer Account", EditorStyles.boldLabel);
            Settings.AppleDeveloperUsername = EditorGUILayout.TextField("Username", Settings.AppleDeveloperUsername);
            Settings.AppleDeveloperPassword = EditorGUILayout.PasswordField("Password", Settings.AppleDeveloperPassword);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Application Loader Tool Path", "Tool used to authenticate and distribute packages to Apple."), EditorStyles.boldLabel, GUILayout.Width(200));
            GUILayout.FlexibleSpace();
            Settings.AppleApplicationLoaderPathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.AppleApplicationLoaderPathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
           
            Settings.AppleApplicationLoaderToolPath = EditorGUILayout.TextField(Settings.AppleApplicationLoaderToolPath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(25), GUILayout.Height(20)))
            {
                EditorApplication.delayCall += SelectALTool;
            }
            EditorGUILayout.EndHorizontal();
        }

        static void SelectALTool()
        {
            EditorGUIUtility.editingTextField = false;
            
            var alToolFolder = Settings.AppleApplicationLoaderToolPath.Substring(0, Settings.AppleApplicationLoaderToolPath.LastIndexOf("/"));
            var newPath = EditorUtility.OpenFilePanel("Application Loader Tool (altool) Path", alToolFolder, "");
            if (!string.IsNullOrEmpty(newPath) && File.Exists(newPath))
            {
                if (Settings.AppleApplicationLoaderPathRelative)
                {
                    Settings.AppleApplicationLoaderToolPath = Utilities.MakeRelativePath(newPath, Build.ProjectFolder);
                }
                else
                {
                    Settings.AppleApplicationLoaderToolPath = newPath;
                }
            }
        }
    }
}