/*
 * Build.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    using dotBunny.Unity.BuildSystem.Routines;
    using dotBunny.Unity.BuildSystem.Modifiers;

    public static class Build
    {
        public const string Tag = "[<b>uBuild</b>] ";
        
        public static string ExecutableName
        {
            get { return PlayerSettings.productName; }
        }
        public static string ProjectFolder
        {
            get
            {
                return Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
            }
        }
        

       

        
        


        /// <summary>
        /// The current folder being built too
        /// </summary> 
        public static string WorkingFolder = "";

        public static BuildTarget WorkingTarget;
        public static BuildTargetGroup WorkingTargetGroup;

        public static string WorkingTag = "";


        public static string BuildExtrasFolder
        {
            get
            {
                return Settings.OutputFolder + Path.DirectorySeparatorChar + "Extras";
            }
        }
        
        public static string GetBuildFolder(BuildTarget target, string tag)
        {
            // Handle tag
            if (!string.IsNullOrEmpty(tag))
            {
                tag = "_" + tag.ToUpper();
            }

            if (Settings.OutputFolderRelative)
            {
                return Utilities.GetAbsolutePath(Settings.OutputFolder) + Utilities.GetPlatformTag(target) + tag;
            }
            else
            {
                return Settings.OutputFolder + Utilities.GetPlatformTag(target) + tag;
            }
        }
        
        public static string GetUnityBuildPath(BuildTarget target, string tag)
        {
            // Process Pathing
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return GetBuildFolder(target, tag) + Path.DirectorySeparatorChar + ExecutableName + ".exe";
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                    return GetBuildFolder(target, tag) + Path.DirectorySeparatorChar + ExecutableName + ".app";
                case BuildTarget.StandaloneLinux:
                case BuildTarget.StandaloneLinux64:
                    return GetBuildFolder(target, tag) + Path.DirectorySeparatorChar + ExecutableName;
                case BuildTarget.Android:
                    return GetBuildFolder(target, tag) + Path.DirectorySeparatorChar + ExecutableName + ".apk";
                case BuildTarget.iOS:
                case BuildTarget.WebGL:
                default:
                    return GetBuildFolder(target, tag);
            }
        }


        public static string BuildPlayer(BuildTarget buildTarget)
        {
            List<IRoutine> routines = new List<IRoutine>();
            List<IModifier> modifiers = new List<IModifier>();
            return BuildPlayer(buildTarget, string.Empty, modifiers, routines);
        }
        public static string BuildPlayer(BuildTarget buildTarget, string tag)
        {
            List<IRoutine> empty = new List<IRoutine>();
            List<IModifier> modifiers = new List<IModifier>();

            return BuildPlayer(buildTarget, tag, modifiers, empty);
        }

        public static string BuildPlayer(BuildTarget buildTarget, string tag, List<IModifier> modifiers, List<IRoutine> routines)
        {
            // Rebuild Cache
            UnityEngine.Debug.Log(Tag + "Rebuilding Sprite Cache ...");
            UnityEditor.Sprites.Packer.RebuildAtlasCacheIfNeeded(buildTarget);

            // Switch to platform
            UnityEngine.Debug.Log(Tag + "Switching Platforms to " + buildTarget.ToString() + " ... ");
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

            // Timestamp Build
            Utilities.TimestampBuild();
            
            // Default Output Folder
            string baseFolder = GetBuildFolder(buildTarget, tag);



            string outputFolder = GetUnityBuildPath(buildTarget, tag);

            UnityEngine.Debug.Log(Tag + "Building to " + baseFolder);

            // Remove folder contents
            // If the destination directory doesn't exist, create it. 
            if (Directory.Exists(baseFolder))
            {
                Utilities.RemoveAllFilesRecursive(baseFolder, "*");
            }

            // Create Empty Folder
            if (!Directory.Exists(baseFolder))
            {
                Directory.CreateDirectory(baseFolder);
            }

            // Set the current Working folder
            WorkingFolder = baseFolder;
            WorkingTarget = buildTarget;
            WorkingTag = tag;


            // Execute Modifiers
            if (modifiers != null)
            {
                foreach (IModifier m in modifiers)
                {
                    Debug.Log(Build.Tag + "[<i>MODIFIER</i>] " + m.GetFriendlyName() + " Pre Process ...");
                    m.PreProcessor();
                }
            }
            // Execute Routines
            if (routines != null)
            {
                foreach (IRoutine h in routines)
                {
                    Debug.Log(Build.Tag + "[<i>ROUTINE</i>] " + h.GetFriendlyName() + " Pre Process ...");
                    h.PreProcessor();
                }
            }

            string buildMessage = BuildPipeline.BuildPlayer(Utilities.GetScenePaths(), outputFolder, buildTarget, BuildOptions.None);

            if (!string.IsNullOrEmpty(buildMessage))
            {
                Debug.Log(buildMessage);
            }

            // Execute Modifiers
            if (modifiers != null)
            {
                foreach (IModifier m in modifiers)
                {
                    Debug.Log(Build.Tag + "[<i>MODIFIER</i>] " + m.GetFriendlyName() + " Post Process ...");
                    m.PostProcessor();
                }
            }

            if (routines != null)
            {
                foreach (IRoutine h in routines)
                {
                    Debug.Log(Build.Tag + "[<i>ROUTINE</i>] " + h.GetFriendlyName() + " Post Process ...");
                    h.PostProcessor();
                }
            }

            if (!string.IsNullOrEmpty(buildMessage))
            {
                Debug.LogError(Build.Tag + "[<color=red><i>COMPLETED</i></color>] Issues with build for " + buildTarget.ToString() + " - " + buildMessage);
            }
            else
            {
                Debug.Log(Build.Tag + "[<color=green><i>COMPLETED</i></color>] Successful build for " + buildTarget.ToString());
            }
            return buildMessage;
        }
    }
}