/*
 * SteamPreferences.cs
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
        static void RenderSteamTab()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Steam SDK Folder", "The root Steam SDK extracted folder."), EditorStyles.boldLabel, GUILayout.Width(150));
            GUILayout.FlexibleSpace();
            Settings.SteamSDKFolderRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.SteamSDKFolderRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Settings.SteamSDKFolder = EditorGUILayout.TextField(Settings.SteamSDKFolder);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectSteamSDKFolder;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Steam Account", EditorStyles.boldLabel);
            Settings.SteamUsername = EditorGUILayout.TextField("Username", Settings.SteamUsername);
            Settings.SteamPassword = EditorGUILayout.PasswordField("Password", Settings.SteamPassword);
            EditorGUILayout.Space();

            Settings.SteamAppID = EditorGUILayout.IntField("App ID", Settings.SteamAppID);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Build Script (vdf)", "The packaging script for Steam"), EditorStyles.boldLabel, GUILayout.Width(150));
            GUILayout.FlexibleSpace();
            Settings.SteamScriptPathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.SteamScriptPathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Settings.SteamScriptPath = EditorGUILayout.TextField(Settings.SteamScriptPath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectSteamBuildScript;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

        }

        static void SelectSteamBuildScript()
        {
            EditorGUIUtility.editingTextField = false;
            var baseFolder = Settings.SteamScriptPath.Substring(0, Settings.SteamScriptPath.LastIndexOf("/"));
            var path = EditorUtility.OpenFilePanel("Steam Build Script", baseFolder, "");
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                if (Settings.SteamScriptPathRelative)
                {
                    Settings.SteamScriptPath = Utilities.MakeRelativePath(path, Build.ProjectFolder);
                }
                else
                {
                    Settings.SteamScriptPath = path;
                }
            }
        }

        static void SelectSteamSDKFolder()
        {
            EditorGUIUtility.editingTextField = false;
            var folder = EditorUtility.OpenFolderPanel("Steam SDK Folder", Settings.SteamSDKFolder, "sdk");
            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
            {
                if (Settings.SteamSDKFolderRelative)
                {
                    Settings.SteamSDKFolder = Utilities.MakeRelativePath(folder, Build.ProjectFolder);
                }
                else
                {
                    Settings.SteamSDKFolder = folder;
                }
            }
        }
    }
}