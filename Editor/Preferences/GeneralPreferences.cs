/*
 * GeneralPreferences.cs
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

        static Vector2 _platformsScroll;
        
        static void RenderGeneralTab()
        {           
            EditorGUILayout.LabelField("Auto Increment Build Number", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            Settings.IncrementBuildNumberOnTargetBuild = EditorGUILayout.Toggle("On Target Build", Settings.IncrementBuildNumberOnTargetBuild);
            Settings.IncrementBuildNumberOnTargetGroupBuild = EditorGUILayout.Toggle("On Target Group Build", Settings.IncrementBuildNumberOnTargetGroupBuild);
            Settings.IncrementBuildNumberOnDeployment = EditorGUILayout.Toggle("On Deployment", Settings.IncrementBuildNumberOnDeployment);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            // TODO SOME SORT OF RELATIVE SOLUTION
            // Output Base Folder
            EditorGUILayout.BeginHorizontal();
            if ( Settings.OutputFolderRelative ){
                EditorGUILayout.LabelField(new GUIContent("Output Folder", "The base folder where the builds will be made.\n" + Utilities.GetAbsolutePath(Settings.OutputFolder)), EditorStyles.boldLabel, GUILayout.Width(150));
            } else {
                EditorGUILayout.LabelField(new GUIContent("Output Folder", "The base folder where the builds will be made.\n" + Settings.OutputFolder), EditorStyles.boldLabel, GUILayout.Width(150));
            }
            GUILayout.FlexibleSpace();
            Settings.OutputFolderRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.OutputFolderRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Settings.OutputFolder = EditorGUILayout.DelayedTextField(Settings.OutputFolder);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectOutputFolder;
            }
            EditorGUILayout.EndHorizontal();
            
            
            
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Platform Tags", EditorStyles.boldLabel);
            
            _platformsScroll = EditorGUILayout.BeginScrollView(_platformsScroll, EditorStyles.helpBox, GUILayout.Height(100));
            GUILayout.Space(3);
            Settings.OutputPlatformiOSTag = EditorGUILayout.DelayedTextField("Apple iOS", Settings.OutputPlatformiOSTag);
            GUILayout.Space(3);
            Settings.OutputPlatformOSX32Tag = EditorGUILayout.DelayedTextField("Apple OSX (32-bit)", Settings.OutputPlatformOSX32Tag);
            GUILayout.Space(3);
            Settings.OutputPlatformOSX64Tag = EditorGUILayout.DelayedTextField("Apple OSX (64-bit)", Settings.OutputPlatformOSX64Tag);
            GUILayout.Space(3);
            Settings.OutputPlatformtvOSTag = EditorGUILayout.DelayedTextField("Apple tvOS", Settings.OutputPlatformtvOSTag);
            GUILayout.Space(3);
            Settings.OutputPlatformAndroidTag = EditorGUILayout.DelayedTextField("Google Android", Settings.OutputPlatformAndroidTag);
            GUILayout.Space(3);
            Settings.OutputPlatformWin32Tag = EditorGUILayout.DelayedTextField("Microsoft Windows (32-bit)", Settings.OutputPlatformWin32Tag);
            GUILayout.Space(3);
            Settings.OutputPlatformWin64Tag = EditorGUILayout.DelayedTextField("Microsoft Windows (64-bit)", Settings.OutputPlatformWin64Tag);
            GUILayout.Space(3);
            Settings.OutputPlatformLinux32Tag = EditorGUILayout.DelayedTextField("Linux (32-bit)", Settings.OutputPlatformLinux32Tag);
            GUILayout.Space(3);
            Settings.OutputPlatformLinux64Tag = EditorGUILayout.DelayedTextField("Linux (64-bit)", Settings.OutputPlatformLinux64Tag);
            GUILayout.Space(3);
            Settings.OutputPlatformWebGLTag = EditorGUILayout.DelayedTextField("Web GL", Settings.OutputPlatformWebGLTag);
            GUILayout.Space(3);
            EditorGUILayout.EndScrollView();


        }
        
        
        static void SelectOutputFolder()
        {

            // Stop editing anything
            EditorGUIUtility.editingTextField = false;
            
            var folder = EditorUtility.OpenFolderPanel("Output Folder", Settings.OutputFolder, "Build");
            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
            {
                if (Settings.OutputFolderRelative)
                {
                    Settings.OutputFolder = Utilities.MakeRelativePath(folder, Build.ProjectFolder);
                }
                else
                {
                    Settings.OutputFolder = folder;
                }
            }
        }
        
       
    }
}