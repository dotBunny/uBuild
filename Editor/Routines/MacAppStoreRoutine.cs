﻿/*
 * MacAppStoreRoutine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Routines
{
    using System.IO;
    using UnityEditor;

    internal class MacAppStoreRoutine : IRoutine {

        public string ProvisionProfileFilename = "appstore.provisionprofile";
        public string EntitlementsFilename = "game.entitlements";
        public string Owner = "reapazor:staff";
        public string DeveloperName = "dotBunny Inc. (HQKPKDQ68P)";

        public static string PackageName = "Upload.pkg";

        bool _previousMacStoreValidation;
        
        bool _previousFullscreen = false;
        ResolutionDialogSetting _previousResolutionDialogSettings;
        
        public MacAppStoreRoutine(string ownerUser, string ownerGroup)
        {
            Owner = ownerUser + ":" + ownerGroup;
        }
        
        
        public string GetFriendlyName()
        {
            return "Mac App Store";
        }
        public BuildFactory.Routines GetRoutineType()
        {
            return BuildFactory.Routines.MacAppStore;
        }
        
        public bool PreProcessor()
        {
            _previousMacStoreValidation = UnityEditor.PlayerSettings.useMacAppStoreValidation;
            UnityEditor.PlayerSettings.useMacAppStoreValidation = true;
            
            _previousResolutionDialogSettings = PlayerSettings.displayResolutionDialog;
            PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Enabled;
            
            _previousFullscreen = PlayerSettings.defaultIsFullScreen;
            PlayerSettings.defaultIsFullScreen = false;

            return true;
        }
        
        public bool PostProcessor()
        {
            PlayerSettings.displayResolutionDialog = _previousResolutionDialogSettings;
            PlayerSettings.defaultIsFullScreen = _previousFullscreen;

            string AppPath = Utilities.CombinePath(Build.WorkingFolder, Build.ExecutableName, ".app");


            // Remove CSteamworks (have to have it set to include on OSX - but dont want it on Mac Store
            string SteamworksPath = Utilities.CombinePath(AppPath, "Contents", "Plugins", "CSteamworks.bundle");
            if ( Directory.Exists(SteamworksPath) ) {
                Stewie.SafeDeleteDirectory(SteamworksPath); 
            }
            
            // Copy over the provisioning profile for embeding
            File.Copy ( 
                Build.BuildExtrasFolder + Path.DirectorySeparatorChar + "OSX" + Path.DirectorySeparatorChar + ProvisionProfileFilename, 
                AppPath + Path.DirectorySeparatorChar +"Contents" + Path.DirectorySeparatorChar + "embedded.provisionprofile", 
                true);
            
            // Change Permissions
            //BuildSystem.CommandLine("chmod", "-R a+xr " + BuildSystem.ExecutableName + ".app", WorkingFolder + Path.DirectorySeparatorChar, false);
            Utilities.CommandLine("chmod", "-RH u+w,go-w,a+rX " + Build.ExecutableName + ".app", Build.WorkingFolder + Path.DirectorySeparatorChar, false);
            //system("/usr/sbin/chown -RH \"reapazor:staff\" \"$jEntitlementsPublishFile\" 2>> \"$jEntitlementsPublishFileName.log\"");

            // Delete all meta files / .DS_Stores / etc
            Utilities.RemoveAllFilesRecursive(AppPath + Path.DirectorySeparatorChar, "*.meta");
            Utilities.RemoveAllFilesRecursive(AppPath + Path.DirectorySeparatorChar, ".DS_Store");

            // Code Sign Plugins
            string pluginsFolder = AppPath + Path.DirectorySeparatorChar + "Contents" + Path.DirectorySeparatorChar + "Plugins";
            CodeSignFolder(pluginsFolder);

            // Code Sign Frameworks
            string frameworksFolder = AppPath + Path.DirectorySeparatorChar + "Contents" + Path.DirectorySeparatorChar + "Frameworks";
            CodeSignFolder(frameworksFolder);
            
             // Code Sign App
            CodeSign(AppPath, true);
            
            // Product Build    
            Utilities.CommandLine("productbuild",
                "--component " + Build.ExecutableName + ".app /Applications --sign \"3rd Party Mac Developer Installer: " + DeveloperName + "\" " + PackageName,
                Build.WorkingFolder + Path.DirectorySeparatorChar, false);

            UnityEditor.PlayerSettings.useMacAppStoreValidation = _previousMacStoreValidation;

            return true;
        }

        void CodeSign(string path, bool deep = false)
        {
            // NOTE ALWAYS MAKE SURE YOUR OLD CERTS ARE GONE AND NOT AMBIGUOUS
            string EntitlementsPath = 
                Build.BuildExtrasFolder + Path.DirectorySeparatorChar + 
                "OSX" + Path.DirectorySeparatorChar + EntitlementsFilename;
                
            if ( deep ) {
                Utilities.CommandLine("codesign", "-f --deep -s '3rd Party Mac Developer Application: " + DeveloperName + "' --verbose --entitlements \"" + EntitlementsPath + "\" " + path,
                    Build.WorkingFolder + Path.DirectorySeparatorChar, false);
            }
            else
            {
                Utilities.CommandLine("codesign", "-f -s '3rd Party Mac Developer Application: " + DeveloperName + "' --verbose --entitlements \"" + EntitlementsPath + "\" " + path,
                    Build.WorkingFolder + Path.DirectorySeparatorChar, false);
            }

            //UnityEngine.Debug.Log("codesign -f --deep -s '3rd Party Mac Developer Application: " + DeveloperName + "' --verbose --entitlements \"" + EntitlementsPath + "\" " + path);
        }
        
        void CodeSignFolder(string path)
        {
            string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            foreach (string foundFile in files)
            {
                // Executable Flag Check
                string stat = Utilities.CommandLine("ls", "-la " + foundFile, path, false);
                
                if (
                stat.Substring(10).Contains("x") ||
                foundFile.EndsWith(".bundle") ||
                foundFile.EndsWith(".dylib") ||
                foundFile.EndsWith(".a") ||
                foundFile.EndsWith(".so") ||
                foundFile.EndsWith(".lib"))
                {
                    
                    CodeSign(foundFile);
                }
            }

            string[] directories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            
            foreach (string foundDirectory in directories)
            {
                if ( foundDirectory.Contains("MonoEmbedRuntime")) {
                    continue;
                }
                if (
                foundDirectory.EndsWith(".bundle") ||
                foundDirectory.EndsWith(".dylib") ||
                foundDirectory.EndsWith(".a") ||
                foundDirectory.EndsWith(".so") ||
                foundDirectory.EndsWith(".lib"))
                {
                    CodeSign(foundDirectory);
                }
            }
        }
                

    }
}