/*
 * BundleVersionRoutine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Modifiers
{
    using dotBunny.Unity.BuildSystem;
    using UnityEditor;
    internal class BuildInfoModifier : IModifier
    {
        public string GetFriendlyName()
        {
            return "Build Info";
        }
        public BuildFactory.Modifiers GetModifierType()
        {
            return BuildFactory.Modifiers.BuildInfo;
        }

        public bool PreProcessor()
        {
            try {
                PlayerSettings.bundleVersion = BuildInfo.MajorVersion + "." + BuildInfo.MinorVersion + "." + BuildInfo.BuildNumber;
                PlayerSettings.iOS.buildNumber = BuildInfo.BuildNumber.ToString();
            } catch ( System.Exception e ) {
                UnityEngine.Debug.LogWarning(Build.Tag + e.Message);
                return false;
            }
            return true;
        }

        public bool PostProcessor()
        {
            return true;
        }

    }
}