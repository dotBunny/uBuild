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
        static void RenderSharedTab()
        {
            Settings.SharedCopyright = EditorGUILayout.DelayedTextField("Copyright Info", Settings.SharedCopyright);
            Settings.SharedDefaultLanguage = EditorGUILayout.DelayedTextField("Default Language", Settings.SharedDefaultLanguage);
        }
    }
}