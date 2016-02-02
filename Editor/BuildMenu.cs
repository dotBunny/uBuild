/*
 * BuildMenu.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using UnityEditor;
    using UnityEngine;
    using dotBunny;
    using dotBunny.Unity.BuildSystem;
    using dotBunny.Unity.BuildSystem.Routines;
    using dotBunny.Unity.BuildSystem.Deployments;
    using dotBunny.Unity.BuildSystem.Modifiers;
    using System.Collections.Generic;

    public static class BuildMenu
    {

        // NORMAL BUILDS

        [MenuItem("File/uBuild/Build for Target/Desktop/Windows 32-bit")]
        public static void BuildForStandaloneWindows32()
        {
            // Increment Build?
            if (Settings.IncrementBuildNumberOnTargetBuild) dotBunny.Unity.BuildSystem.Utilities.IncrementBuild();

            // Setup base level modifiers
            List<IModifier> modifiers = new List<IModifier>();
            modifiers.Add(new BuildInfoModifier());

            // Build
            Build.BuildPlayer(BuildTarget.StandaloneWindows, string.Empty, modifiers, null);
        }

        [MenuItem("File/uBuild/Build for Target/Desktop/Windows 64-bit")]
        public static void BuildForStandaloneWindows64()
        {
            // Increment Build?
            if (Settings.IncrementBuildNumberOnTargetBuild) dotBunny.Unity.BuildSystem.Utilities.IncrementBuild();

            // Setup base level modifiers
            List<IModifier> modifiers = new List<IModifier>();
            modifiers.Add(new BuildInfoModifier());

            // Build
            Build.BuildPlayer(BuildTarget.StandaloneWindows64, string.Empty, modifiers, null);
        }


        [MenuItem("File/uBuild/Build for Target/Desktop/OSX 32-bit")]
        public static void BuildForOSX32()
        {
            List<IModifier> modifiers = new List<IModifier>();
            modifiers.Add(new BuildInfoModifier());

            List<IRoutine> routines = new List<IRoutine>();
            routines.Add(new MacPListRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneOSXIntel, string.Empty, modifiers, routines);
        }

        [MenuItem("File/uBuild/Build for Target/Desktop/OSX 64-bit")]
        public static void BuildForOSX64()
        {
            List<IModifier> modifiers = new List<IModifier>();
            modifiers.Add(new BuildInfoModifier());

            List<IRoutine> routines = new List<IRoutine>();
            routines.Add(new MacPListRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneOSXIntel64, string.Empty, modifiers, routines);
        }

        [MenuItem("File/uBuild/Build for Target/Desktop/Linux 32-bit")]
        public static void BuildForLinux32()
        {
            List<IModifier> modifiers = new List<IModifier>();

            modifiers.Add(new BuildInfoModifier());

            Build.BuildPlayer(BuildTarget.StandaloneLinux, string.Empty, modifiers, null);
        }

        [MenuItem("File/uBuild/Build for Target/Desktop/Linux 64-bit")]
        public static void BuildForLinux64()
        {
            List<IModifier> modifiers = new List<IModifier>();

            modifiers.Add(new BuildInfoModifier());

            Build.BuildPlayer(BuildTarget.StandaloneLinux64, string.Empty, modifiers, null);
        }

        [MenuItem("File/uBuild/Build for Target/Web GL")]
        public static void BuildForWebGL()
        {
            List<IModifier> modifiers = new List<IModifier>();

            modifiers.Add(new BuildInfoModifier());

            Build.BuildPlayer(BuildTarget.WebGL, string.Empty, modifiers, null);
        }




        // STEAM BUILDS
        [MenuItem("File/uBuild/Build for Deployment/Steam/Windows 32-bit")]
        public static void BuildForSteamWindows32()
        {
            List<IModifier> modifiers = new List<IModifier>();

            List<IRoutine> routines = new List<IRoutine>();

            modifiers.Add(new BuildInfoModifier());
            modifiers.Add(new DefineModifier("STEAM"));

            routines.Add(new SteamRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneWindows, "steam", modifiers, routines);
        }
        [MenuItem("File/uBuild/Build for Deployment/Steam/Windows 64-bit")]
        public static void BuildForSteamWindows64()
        {
            List<IModifier> modifiers = new List<IModifier>();

            List<IRoutine> routines = new List<IRoutine>();

            modifiers.Add(new BuildInfoModifier());
            modifiers.Add(new DefineModifier("STEAM"));

            routines.Add(new SteamRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneWindows64, "steam", modifiers, routines);
        }
        [MenuItem("File/uBuild/Build for Deployment/Steam/OSX 32-bit")]
        public static void BuildForSteamOSX32()
        {
            List<IModifier> modifiers = new List<IModifier>();

            List<IRoutine> routines = new List<IRoutine>();

            modifiers.Add(new BuildInfoModifier());
            modifiers.Add(new DefineModifier("STEAM"));

            routines.Add(new MacPListRoutine());
            routines.Add(new SteamRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneOSXIntel, "steam", modifiers, routines);
        }
        [MenuItem("File/uBuild/Build for Deployment/Steam/OSX 64-bit")]
        public static void BuildForSteamOSX64()
        {
            List<IModifier> modifiers = new List<IModifier>();

            List<IRoutine> routines = new List<IRoutine>();

            modifiers.Add(new BuildInfoModifier());
            modifiers.Add(new DefineModifier("STEAM"));

            routines.Add(new MacPListRoutine());
            routines.Add(new SteamRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneOSXIntel64, "steam", modifiers, routines);
        }
        [MenuItem("File/uBuild/Build for Deployment/Steam/Linux 32-bit")]
        public static void BuildForSteamLinux32()
        {
            List<IModifier> modifiers = new List<IModifier>();

            List<IRoutine> routines = new List<IRoutine>();

            modifiers.Add(new BuildInfoModifier());
            modifiers.Add(new DefineModifier("STEAM"));

            routines.Add(new SteamRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneLinux, "steam", modifiers, routines);
        }
        [MenuItem("File/uBuild/Build for Deployment/Steam/Linux 64-bit")]
        public static void BuildForSteamLinux64()
        {
            List<IModifier> modifiers = new List<IModifier>();

            List<IRoutine> routines = new List<IRoutine>();

            modifiers.Add(new BuildInfoModifier());
            modifiers.Add(new DefineModifier("STEAM"));

            routines.Add(new SteamRoutine());

            Build.BuildPlayer(BuildTarget.StandaloneLinux64, "steam", modifiers, routines);
        }
        
         [MenuItem("File/uBuild/Build for Deployment/Steam/All", false, 100)]
        public static void BuildForSteamGroup()
        {
            Debug.Log(Build.Tag + "Starting Steam Group Build ...");
            BuildForSteamWindows32();
            BuildForSteamWindows64();
            BuildForSteamOSX32();
            BuildForSteamOSX64();
            BuildForSteamLinux32();
            BuildForSteamLinux64();
            Debug.Log(Build.Tag + "Finished Steam Group Build.");
        }


        // Mac App Store
        [MenuItem("File/uBuild/Build for Deployment/Mac App Store")]
        static void BuildForMacAppStore()
        {
            List<IModifier> modifiers = new List<IModifier>();
            List<IRoutine> routines = new List<IRoutine>();
            
            modifiers.Add(new BuildInfoModifier());

            routines.Add(new MacPListRoutine());

            routines.Add(new MacAppStoreRoutine("reapazor", "staff"));

            Build.BuildPlayer(BuildTarget.StandaloneOSXIntel64, string.Empty, modifiers, routines);
        }


        // Deployments
        [MenuItem("File/uBuild/Deploy/Steam")]
        public static void DeployToSteam()
        {
            SteamDeployment steam = new SteamDeployment();
            steam.Process();
        }

        [MenuItem("File/uBuild/Deploy/Mac App Store")]
        public static void DeployToMacAppStore()
        {
            MacAppStoreDeployment macstore = new MacAppStoreDeployment();
            macstore.Process();
        }

        // Build & Deploy
        [MenuItem("File/uBuild/Build and Deploy/Steam")]
        public static void BuildForSteamAndDeploy()
        {
            BuildForSteamGroup();
            DeployToSteam();
        }


        [MenuItem("File/uBuild/Build and Deploy/Mac App Store")]
        public static void BuildForMacAppStoreAndDeploy()
        {
            BuildForMacAppStore();
            DeployToMacAppStore();
        }

    }
}
