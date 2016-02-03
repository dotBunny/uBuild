// GetInfoString

// Bundle Identifier
// Icon file path
// screen selector
// default category
/*
* MacPreferences.cs
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
        static void RenderMacTab()
        {
            Settings.MacBundleIdentifier = EditorGUILayout.DelayedTextField("Bundle Identifier", Settings.MacBundleIdentifier);
            Settings.MacCategory = EditorGUILayout.DelayedTextField("Category", Settings.MacCategory);
            Settings.MacGetInfoString = EditorGUILayout.DelayedTextField("GetInfo() String", Settings.MacGetInfoString);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Mac Icon File (ICNS)", "The Mac ICNS file that will be copied into the compiled application"), EditorStyles.boldLabel, GUILayout.Width(150));
            GUILayout.FlexibleSpace();
            Settings.MacIconPathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.MacIconPathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Settings.MacIconPath = EditorGUILayout.DelayedTextField(Settings.MacIconPath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectMacIconPath;;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Mac Screen Selector (TIF)", "The Screen Selector image shown on the configuration screen."), EditorStyles.boldLabel, GUILayout.Width(150));
            GUILayout.FlexibleSpace();
            Settings.MacScreenSelectorPathRelative = EditorGUILayout.ToggleLeft(new GUIContent("Relative", "Relative path to the projects root folder"), Settings.MacScreenSelectorPathRelative, GUILayout.Width(60));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Settings.MacScreenSelectorPath = EditorGUILayout.DelayedTextField(Settings.MacScreenSelectorPath);
            if (GUILayout.Button("...", EditorStyles.miniButton, GUILayout.Width(20)))
            {
                EditorApplication.delayCall += SelectMacScreenSelectorPath;
            }
            EditorGUILayout.EndHorizontal();
        }
        
        static void SelectMacIconPath()
        {
            // Stop editing anything
            EditorGUIUtility.editingTextField = false;

            var file = EditorUtility.OpenFilePanel("Icon ICNS", Build.ProjectFolder, "icns");
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                if (Settings.MacIconPathRelative)
                {
                    Settings.MacIconPath = Utilities.MakeRelativePath(file, Build.ProjectFolder);
                }
                else
                {
                    Settings.MacIconPath = file;
                }
            }
        }
        
        static void SelectMacScreenSelectorPath()
        {
            // Stop editing anything
            EditorGUIUtility.editingTextField = false;

            var file = EditorUtility.OpenFilePanel("Screen Selector TIF", Build.ProjectFolder, "tif");
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                if (Settings.MacScreenSelectorPathRelative)
                {
                    Settings.MacScreenSelectorPath = Utilities.MakeRelativePath(file, Build.ProjectFolder);
                }
                else
                {
                    Settings.MacScreenSelectorPath = file;
                }
            }
        }
    }
}