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
        static void RenderAppStore()
        {
            EditorGUILayout.LabelField("Apple Developer Account", EditorStyles.boldLabel);
            Settings.AppleDeveloperUsername = EditorGUILayout.TextField("Username", Settings.AppleDeveloperUsername);
            Settings.AppleDeveloperPassword = EditorGUILayout.PasswordField("Password", Settings.AppleDeveloperPassword);
            EditorGUILayout.Space();
            Settings.AppleDeveloperName = EditorGUILayout.TextField("Developer Name", Settings.AppleDeveloperName);

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Application Loader Tool Path", "Tool used to authenticate and distribute packages to Apple."), EditorStyles.boldLabel, GUILayout.Width(200));
            GUILayout.FlexibleSpace();
            Settings.AppleApplicationLoaderPathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.AppleApplicationLoaderPathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            Settings.AppleApplicationLoaderToolPath = EditorGUILayout.DelayedTextField(Settings.AppleApplicationLoaderToolPath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectALTool;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Mac Deployment", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            
            EditorGUILayout.LabelField("File Permissions", EditorStyles.boldLabel);
            Settings.MacFileOwner = EditorGUILayout.DelayedTextField("Owner", Settings.MacFileOwner);
            Settings.MacFileGroup = EditorGUILayout.DelayedTextField("Group", Settings.MacFileGroup);

            EditorGUILayout.Space();


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Provisioning Profile", EditorStyles.boldLabel, GUILayout.Width(200));
            GUILayout.FlexibleSpace();
            Settings.MacProvisioningProfilePathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.MacProvisioningProfilePathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            Settings.MacProvisioningProfilePath = EditorGUILayout.DelayedTextField(Settings.MacProvisioningProfilePath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectProvisioningProfile;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Entitlements File", EditorStyles.boldLabel, GUILayout.Width(200));
            GUILayout.FlexibleSpace();
            Settings.MacEntitlementsPathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.MacEntitlementsPathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Settings.MacEntitlementsPath = EditorGUILayout.DelayedTextField(Settings.MacEntitlementsPath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectEntitlement;
            }
            EditorGUILayout.EndHorizontal();
            


        }

        static void SelectProvisioningProfile()
        {
            EditorGUIUtility.editingTextField = false;
            
            var baseFolder = Utilities.GetBaseFolder(Settings.MacProvisioningProfilePath);
            var path = EditorUtility.OpenFilePanel("Select Provisioing Profile", baseFolder, "provisionprofile");
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                if (Settings.MacProvisioningProfilePathRelative)
                {
                    Settings.MacProvisioningProfilePath = Utilities.MakeRelativePath(path, Build.ProjectFolder);
                }
                else
                {
                    Settings.MacProvisioningProfilePath = path;
                }
            }
        }

        static void SelectEntitlement()
        {
            EditorGUIUtility.editingTextField = false;
            var baseFolder = Utilities.GetBaseFolder(Settings.MacEntitlementsPath);
            var path = EditorUtility.OpenFilePanel("Select Entitlement File", baseFolder, "entitlements");
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                if (Settings.MacEntitlementsPathRelative)
                {
                    Settings.MacEntitlementsPath = Utilities.MakeRelativePath(path, Build.ProjectFolder);
                }
                else
                {
                    Settings.MacEntitlementsPath = path;
                }
            }
        }

        static void SelectALTool()
        {
            EditorGUIUtility.editingTextField = false;

            var alToolFolder = Utilities.GetBaseFolder(Settings.AppleApplicationLoaderToolPath);
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