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
            get { return Build.BuildExtrasFolder + Path.DirectorySeparatorChar + "Steamworks"; }
        }

        public static string RedistributableFolder
        {
            get
            {                
                return SteamworksFolder + Path.DirectorySeparatorChar + "sdk" +
                Path.DirectorySeparatorChar + "redistributable_bin";
            }
        }

        public static string ContentPrepCommand
        {
            get
            {
                return SteamworksFolder + Path.DirectorySeparatorChar + "sdk" + Path.DirectorySeparatorChar +
                "tools" + Path.DirectorySeparatorChar + "ContentPrep.app" + Path.DirectorySeparatorChar +
                "Contents" + Path.DirectorySeparatorChar + "MacOS" + Path.DirectorySeparatorChar + "contentprep.py";
            }
        }


        public static string GetRedistributableFolder(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows64:
                    return RedistributableFolder + Path.DirectorySeparatorChar + "win64";
                case BuildTarget.StandaloneOSXIntel:
                    return RedistributableFolder + Path.DirectorySeparatorChar + "osx32";
                case BuildTarget.StandaloneLinux:
                    return RedistributableFolder + Path.DirectorySeparatorChar + "linux32";
                case BuildTarget.StandaloneLinux64:
                    return RedistributableFolder + Path.DirectorySeparatorChar + "linux64";
                case BuildTarget.StandaloneWindows:
                default:
                    return RedistributableFolder + Path.DirectorySeparatorChar + "win32";
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
            UnityEngine.Debug.Log(Build.Tag + "Steam Routine PRE Routine for " + Build.WorkingTarget.ToString() + " ... ");
            
            // Add Defines To Build
            

            _previousResolutionDialogSettings = PlayerSettings.displayResolutionDialog;
            PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.HiddenByDefault;

            _previousFullscreen = PlayerSettings.defaultIsFullScreen;
            // PlayerSettings.defaultIsFullScreen = true;

            _previousMacStoreValidation = PlayerSettings.useMacAppStoreValidation;
            PlayerSettings.useMacAppStoreValidation = false;

            return true;
        }


        public string GetFriendlyName()
        {
            return "Steam API Library";
        }
        public BuildFactory.Routines GetRoutineType()
        {
            return BuildFactory.Routines.Steam;
        }
        public bool PostProcessor()
        {
            UnityEngine.Debug.Log(Build.Tag + "Steam Routine POST Routine for " + Build.WorkingTarget.ToString() + " ... ");


            // Remove resolution dialog settings
            PlayerSettings.displayResolutionDialog = _previousResolutionDialogSettings;
            PlayerSettings.defaultIsFullScreen = _previousFullscreen;
            PlayerSettings.useMacAppStoreValidation = _previousMacStoreValidation;
            // TODO: Remove DS STORE FILES



            // Copy Libraries To Root
            if (Build.WorkingTarget == BuildTarget.StandaloneWindows)
            {
                File.Copy(GetRedistributableFolder(Build.WorkingTarget) + Path.DirectorySeparatorChar + "steam_api.dll", Build.WorkingFolder + Path.DirectorySeparatorChar + "steam_api.dll", true);
            }
            else if (Build.WorkingTarget == BuildTarget.StandaloneWindows64)
            {
                File.Copy(GetRedistributableFolder(Build.WorkingTarget) + Path.DirectorySeparatorChar + "steam_api64.dll", Build.WorkingFolder + Path.DirectorySeparatorChar + "steam_api64.dll", true);
            }

            // Dont have to do mac / or linux?




            // Mac Content Prep
            if (Build.WorkingTarget == BuildTarget.StandaloneOSXIntel || Build.WorkingTarget == BuildTarget.StandaloneOSXIntel64)
            {
                // This is used for additional things
                string tempFolder = Build.GetBuildFolder(Build.WorkingTarget, Build.Tag + "_TEMP");

                // Remove folder contents
                // If the destination directory doesn't exist, create it. 
                if (Directory.Exists(tempFolder))
                {
                    Stewie.SafeDeleteDirectory(tempFolder);
                }

                // Create Empty Folder
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                // Execute Contenet Prep Script - Unity Scripts do not need wrapping
                string arguements = " --console -v --nowrap --app " + _appID +
                     " --source " + Build.GetUnityBuildPath(Build.WorkingTarget, Build.Tag) + " --dest " +
                     tempFolder + Path.DirectorySeparatorChar;

                // Execute Content Prep
                string output = Utilities.CommandLine(ContentPrepCommand, arguements, Build.WorkingFolder, true);
                Debug.Log(output);

                // Remove Original Directory
                if (Directory.Exists(Build.WorkingFolder))
                {
                    Stewie.SafeDeleteDirectory(Build.WorkingFolder);
                }

                Directory.CreateDirectory(Build.WorkingFolder);

                // Copy Temp to Original
                Utilities.DirectoryCopy(tempFolder, Build.WorkingFolder, true);

                // Remove Temp
                if (Directory.Exists(tempFolder))
                {
                    Stewie.SafeDeleteDirectory(tempFolder);
                }
            }
            return true;
        }
    }
}