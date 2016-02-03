/*
 * SteamRoutine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Routines
{
    using UnityEditor;
    using UnityEngine;
    using System.IO;
    using dotBunny;

    internal class SteamRoutine : IRoutine
    {
        public static string SteamworksFolder
        {
            get
            {
                if (Settings.SteamSDKFolderRelative)
                {
                    return Utilities.GetAbsolutePath(Settings.SteamSDKFolder);
                }
                else
                {
                    return Settings.SteamSDKFolder;
                }
            }
        }

        public static string RedistributableFolder
        {
            get
            {
                return Utilities.CombinePath(SteamworksFolder, "redistributable_bin");
            }
        }

        public static string ContentPrepCommand
        {
            get
            {
                return Utilities.CombinePath(SteamworksFolder, "tools", "ContentPrep.app", "Contents", "MacOS", "contentprep.py");
            }
        }


        public static string GetRedistributableFolder(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows64:
                    return Utilities.CombinePath(RedistributableFolder, "win64");
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
                    return Utilities.CombinePath(RedistributableFolder, "osx32");
                case BuildTarget.StandaloneLinux:
                    return Utilities.CombinePath(RedistributableFolder,"linux32");
                case BuildTarget.StandaloneLinux64:
                    return Utilities.CombinePath(RedistributableFolder, "linux64");
                case BuildTarget.StandaloneWindows:
                default:
                    return Utilities.CombinePath(RedistributableFolder, "win32");
            }
        }



        public bool ExecuteUpload = true;

        bool _previousFullscreen = false;
        ResolutionDialogSetting _previousResolutionDialogSettings;
        int _appID;


        public SteamRoutine()
        {
            _appID = Settings.SteamAppID;
        }

        public SteamRoutine(int appID)
        {
            _appID = appID;
        }



        bool _previousMacStoreValidation;

        public bool PreProcessor()
        {
            // Disable configuration window by default, better for Steam Link, etc.
            _previousResolutionDialogSettings = PlayerSettings.displayResolutionDialog;
            PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.HiddenByDefault;

            // Turn off Mac Store Validation for sure
            _previousMacStoreValidation = PlayerSettings.useMacAppStoreValidation;
            PlayerSettings.useMacAppStoreValidation = false;

            return true;
        }


        public string GetFriendlyName()
        {
            return "Steam Bundle ";
        }
        public BuildFactory.Routines GetRoutineType()
        {
            return BuildFactory.Routines.Steam;
        }
        public bool PostProcessor()
        {
            // Remove resolution dialog settings
            PlayerSettings.displayResolutionDialog = _previousResolutionDialogSettings;
            PlayerSettings.useMacAppStoreValidation = _previousMacStoreValidation;

            // Copy Libraries To Root (for Windows)
            if (Build.WorkingTarget == BuildTarget.StandaloneWindows)
            {
                FileUtil.CopyFileOrDirectory(
                    Utilities.CombinePath(GetRedistributableFolder(Build.WorkingTarget), "steam_api.dll"), 
                    Utilities.CombinePath(Build.WorkingFolder,"steam_api.dll"));
            }
            else if (Build.WorkingTarget == BuildTarget.StandaloneWindows64)
            {
                FileUtil.CopyFileOrDirectory(
                    Utilities.CombinePath(GetRedistributableFolder(Build.WorkingTarget), "steam_api64.dll"),
                    Utilities.CombinePath(Build.WorkingFolder, "steam_api64.dll"));
            }


            // Mac Content Prep
            if (Build.WorkingTarget == BuildTarget.StandaloneOSXIntel || 
                Build.WorkingTarget == BuildTarget.StandaloneOSXIntel64)
            {
                // This is used for additional things
                string tempFolder = Build.GetBuildFolder(Build.WorkingTarget, Build.WorkingTag + "_TEMP");

                // Remove folder contents
                // If the destination directory doesn't exist, create it. 
                if (Directory.Exists(tempFolder))
                {
                    FileUtil.DeleteFileOrDirectory(tempFolder);
                }

                // Create Empty Folder
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                // Execute Contenet Prep Script - Unity Scripts do not need wrapping
                string arguements = " --console -v --nowrap --app " + _appID +
                     " --source " + Build.GetUnityBuildPath(Build.WorkingTarget, Build.WorkingTag) + " --dest " +
                     tempFolder + Path.DirectorySeparatorChar;

                // Execute Content Prep
                string output = Utilities.CommandLine(ContentPrepCommand, arguements, Build.WorkingFolder, true);
                Debug.Log(Build.Tag + output);

                // Remove Original Directory
                if (Directory.Exists(Build.WorkingFolder))
                {
                    FileUtil.DeleteFileOrDirectory(Build.WorkingFolder);
                }

                Directory.CreateDirectory(Build.WorkingFolder);

                // Copy Temp to Original
                Utilities.DirectoryCopy(tempFolder, Build.WorkingFolder, true);

                // Remove Temp
                if (Directory.Exists(tempFolder))
                {
                    FileUtil.DeleteFileOrDirectory(tempFolder);
                }
            }
            return true;
        }
    }
}