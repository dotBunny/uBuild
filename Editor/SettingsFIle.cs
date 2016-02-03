/*
* SettingsFile.cs
*
* Author:
*   Matthew Davey <matthew.davey@dotbunny.com>
*/
namespace dotBunny.Unity.BuildSystem
{
    [System.Serializable]
    public class SettingsFile
    {

        public string OutputFolder;
        public bool OutputFolderRelative;
        public string OutputPlatformWin32Tag;
        public string OutputPlatformWin64Tag;
        public string OutputPlatformOSX32Tag;
        public string OutputPlatformOSX64Tag;
        public string OutputPlatformLinux32Tag;
        public string OutputPlatformLinux64Tag;
        public string OutputPlatformiOSTag;
        public string OutputPlatformtvOSTag;
        public string OutputPlatformWebGLTag;
        public string OutputPlatformAndroidTag;

        public string SharedCopyright;
        public string SharedDefaultLanguage;

        public string MacBundleIdentifier;
        public string MacCategory;
        public string MacGetInfoString;
        public string MacIconPath;
        public bool MacIconPathRelative;
        public string MacScreenSelectorPath;
        public bool MacScreenSelectorPathRelative;

        public string AppleDeveloperUsername;
        public string AppleDeveloperPassword;
        public string AppleApplicationLoaderToolPath;
        public bool AppleApplicationLoaderPathRelative;


        public string IOSProvisioningProfile;
        public string IOSSigningAuthority;

        public string SteamSDKFolder;
        public bool SteamSDKFolderRelative;
        public string SteamUsername;
        public string SteamPassword;
        public string SteamScriptPath;
        public bool SteamScriptPathRelative;
        public int SteamAppID;
        
        

        public bool IncrementBuildNumberOnTargetBuild;
        public bool IncrementBuildNumberOnTargetGroupBuild;
        public bool IncrementBuildNumberOnDeployment;

        public void Apply()
        {

            Settings.OutputFolder = this.OutputFolder;
            Settings.OutputFolderRelative = this.OutputFolderRelative;

            Settings.OutputPlatformWin32Tag = this.OutputPlatformWin32Tag;
            Settings.OutputPlatformWin64Tag = this.OutputPlatformWin64Tag;
            Settings.OutputPlatformOSX32Tag = this.OutputPlatformOSX32Tag;
            Settings.OutputPlatformOSX64Tag = this.OutputPlatformOSX64Tag;
            Settings.OutputPlatformLinux32Tag = this.OutputPlatformLinux32Tag;
            Settings.OutputPlatformLinux64Tag = this.OutputPlatformLinux64Tag;
            Settings.OutputPlatformiOSTag = this.OutputPlatformiOSTag;
            Settings.OutputPlatformtvOSTag = this.OutputPlatformtvOSTag;
            Settings.OutputPlatformWebGLTag = this.OutputPlatformWebGLTag;
            Settings.OutputPlatformAndroidTag = this.OutputPlatformAndroidTag;


            Settings.IncrementBuildNumberOnTargetBuild = this.IncrementBuildNumberOnTargetBuild;
            Settings.IncrementBuildNumberOnTargetGroupBuild = this.IncrementBuildNumberOnTargetGroupBuild;
            Settings.IncrementBuildNumberOnDeployment = this.IncrementBuildNumberOnDeployment;


            Settings.SharedCopyright = this.SharedCopyright;
            Settings.SharedDefaultLanguage = this.SharedDefaultLanguage;

            Settings.MacBundleIdentifier = this.MacBundleIdentifier;
            Settings.MacCategory = this.MacCategory;
            Settings.MacGetInfoString = this.MacGetInfoString;
            Settings.MacIconPath = this.MacIconPath;
            Settings.MacIconPathRelative = this.MacIconPathRelative;
            Settings.MacScreenSelectorPath = this.MacScreenSelectorPath;
            Settings.MacScreenSelectorPathRelative = this.MacScreenSelectorPathRelative;

            Settings.IOSProvisioningProfile = this.IOSProvisioningProfile;
            Settings.IOSSigningAuthority = this.IOSSigningAuthority;


            Settings.AppleApplicationLoaderPathRelative = this.AppleApplicationLoaderPathRelative;
            Settings.AppleDeveloperUsername = this.AppleDeveloperUsername;
            Settings.AppleDeveloperPassword = this.AppleDeveloperPassword;
            Settings.AppleApplicationLoaderToolPath = this.AppleApplicationLoaderToolPath;
            Settings.AppleApplicationLoaderPathRelative = this.AppleApplicationLoaderPathRelative;

            Settings.SteamSDKFolder = this.SteamSDKFolder;
            Settings.SteamSDKFolderRelative = this.SteamSDKFolderRelative;

            Settings.SteamUsername = this.SteamUsername;
            Settings.SteamPassword = this.SteamPassword;
            Settings.SteamAppID = this.SteamAppID;
            Settings.SteamScriptPath = this.SteamScriptPath;
            Settings.SteamScriptPathRelative = this.SteamScriptPathRelative;
        }

        public void Read()
        {
            this.OutputFolder = Settings.OutputFolder;
            this.OutputFolderRelative = Settings.OutputFolderRelative;

            this.OutputPlatformWin32Tag = Settings.OutputPlatformWin32Tag;
            this.OutputPlatformWin64Tag = Settings.OutputPlatformWin64Tag;
            this.OutputPlatformOSX32Tag = Settings.OutputPlatformOSX32Tag;
            this.OutputPlatformOSX64Tag = Settings.OutputPlatformOSX64Tag;
            this.OutputPlatformLinux32Tag = Settings.OutputPlatformLinux32Tag;
            this.OutputPlatformLinux64Tag = Settings.OutputPlatformLinux64Tag;
            this.OutputPlatformiOSTag = Settings.OutputPlatformiOSTag;
            this.OutputPlatformtvOSTag = Settings.OutputPlatformtvOSTag;
            this.OutputPlatformWebGLTag = Settings.OutputPlatformWebGLTag;
            this.OutputPlatformAndroidTag = Settings.OutputPlatformAndroidTag;

            this.IncrementBuildNumberOnTargetBuild = Settings.IncrementBuildNumberOnTargetBuild;
            this.IncrementBuildNumberOnTargetGroupBuild = Settings.IncrementBuildNumberOnTargetGroupBuild;
            this.IncrementBuildNumberOnDeployment = Settings.IncrementBuildNumberOnDeployment;

            this.SharedCopyright = Settings.SharedCopyright;
            this.SharedDefaultLanguage = Settings.SharedDefaultLanguage;

            this.MacBundleIdentifier = Settings.MacBundleIdentifier;
            this.MacCategory = Settings.MacCategory;
            this.MacGetInfoString = Settings.MacGetInfoString;
            this.MacIconPath = Settings.MacIconPath;
            this.MacIconPathRelative = Settings.MacIconPathRelative;
            this.MacScreenSelectorPath = Settings.MacScreenSelectorPath;
            this.MacScreenSelectorPathRelative = Settings.MacScreenSelectorPathRelative;

            this.IOSProvisioningProfile = Settings.IOSProvisioningProfile;
            this.IOSSigningAuthority = Settings.IOSSigningAuthority;

            this.AppleApplicationLoaderPathRelative = Settings.AppleApplicationLoaderPathRelative;
            this.AppleDeveloperUsername = Settings.AppleDeveloperUsername;
            this.AppleDeveloperPassword = Settings.AppleDeveloperPassword;
            this.AppleApplicationLoaderToolPath = Settings.AppleApplicationLoaderToolPath;
            this.AppleApplicationLoaderPathRelative = Settings.AppleApplicationLoaderPathRelative;

            this.SteamSDKFolder = Settings.SteamSDKFolder;
            this.SteamSDKFolderRelative = Settings.SteamSDKFolderRelative;
            this.SteamUsername = Settings.SteamUsername;
            this.SteamPassword = Settings.SteamPassword;
            this.SteamAppID = Settings.SteamAppID;
            this.SteamScriptPath = Settings.SteamScriptPath;
            this.SteamScriptPathRelative = Settings.SteamScriptPathRelative;
        }
    }
}