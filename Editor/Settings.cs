/*
 * Settings.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using UnityEditor;

    // NOTE : All folders need to end in the character seperator

    public static class Settings
    {
        /// <summary>
        /// The current selected tab in the settings window
        public static int SettingsTab
        {
            get
            {
                return EditorPrefs.GetInt("BuildSystem_SettingsTab", 0);
            }
            set
            {
                if (SettingsTab != value)
                    EditorPrefs.SetInt("BuildSystem_SettingsTab", value);
            }
        }

        public static bool IncrementBuildNumberOnTargetBuild
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_IncrementBuildNumberOnTargetBuild", false);
            }
            set
            {
                EditorPrefs.SetBool("BuildSystem_IncrementBuildNumberOnTargetBuild", value);
            }
        }
        public static bool IncrementBuildNumberOnTargetGroupBuild
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_IncrementBuildNumberOnTargetGroupBuild", true);
            }
            set
            {
                EditorPrefs.SetBool("BuildSystem_IncrementBuildNumberOnTargetGroupBuild", value);
            }
        }
        public static bool IncrementBuildNumberOnDeployment
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_IncrementBuildNumberOnDeployment", true);
            }
            set
            {
                EditorPrefs.SetBool("BuildSystem_IncrementBuildNumberOnDeployment", value);
            }
        }

        /// <summary>
        /// The root folder where builds are to take place
        /// </summary>
        public static string OutputFolder
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputFolder", Utilities.CombinePath(Build.ProjectFolder, "Build"));
            }
            set
            {
                if (OutputFolder != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputFolder",
                        Utilities.ForceEndsWith(value, System.IO.Path.DirectorySeparatorChar.ToString()));
                }
            }
        }

        /// <summary>
        /// Should the Output folder be relative to the ProjectFolder
        /// </summary>
        public static bool OutputFolderRelative
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_OutputFolderRelative", true);
            }
            set
            {
                if (OutputFolderRelative != value)
                {
                    EditorPrefs.SetBool("BuildSystem_OutputFolderRelative", value);
                }
            }
        }

        public static string OutputPlatformWin32Tag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformWin32", "Windows_x86");
            }
            set
            {
                if (OutputPlatformWin32Tag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformWin32", value);
                }
            }
        }
        public static string OutputPlatformWin64Tag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformWin64", "Windows_x64");
            }
            set
            {
                if (OutputPlatformWin64Tag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformWin64", value);
                }
            }
        }

        public static string OutputPlatformOSX32Tag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformOSX32", "OSX_x86");
            }
            set
            {
                if (OutputPlatformOSX32Tag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformOSX32", value);
                }
            }
        }

        public static string OutputPlatformOSX64Tag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformOSX64", "OSX_x64");
            }
            set
            {
                if (OutputPlatformOSX64Tag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformOSX64", value);
                }
            }
        }
        public static string OutputPlatformLinux32Tag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformLinux32", "Linux_x86");
            }
            set
            {
                if (OutputPlatformLinux32Tag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformLinux32", value);
                }
            }
        }
        public static string OutputPlatformLinux64Tag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformLinux64", "Linux_x64");
            }
            set
            {
                if (OutputPlatformLinux64Tag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformLinux64", value);
                }
            }
        }

        public static string OutputPlatformiOSTag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformiOS", "iOS");
            }
            set
            {
                if (OutputPlatformiOSTag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformiOS", value);
                }
            }
        }
        public static string OutputPlatformtvOSTag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformtvOS", "tvOS");
            }
            set
            {
                if (OutputPlatformtvOSTag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformtvOS", value);
                }
            }
        }

        public static string OutputPlatformWebGLTag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformWebGL", "WebGL");
            }
            set
            {
                if (OutputPlatformWebGLTag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformWebGL", value);
                }
            }
        }
        public static string OutputPlatformAndroidTag
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_OutputPlatformAndroid", "Android");
            }
            set
            {
                if (OutputPlatformAndroidTag != value)
                {
                    EditorPrefs.SetString("BuildSystem_OutputPlatformAndroid", value);
                }
            }
        }


        public static string SharedCopyright
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_SharedCopyright", "(c) 2016 dotBunny");
            }
            set
            {
                if (SharedCopyright != value)
                {
                    EditorPrefs.SetString("BuildSystem_SharedCopyright", value);
                }
            }
        }
        public static string SharedDefaultLanguage
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_SharedDefaultLanguage", "English");
            }
            set
            {
                if (SharedCopyright != value)
                {
                    EditorPrefs.SetString("BuildSystem_SharedDefaultLanguage", value);
                }
            }
        }

        public static string MacIconPath
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_MacIconPath", "");
            }
            set
            {
                if (MacIconPath != value)
                {
                    EditorPrefs.SetString("BuildSystem_MacIconPath", value);
                }
            }
        }

        public static bool MacIconPathRelative
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_MacIconPathRelative", false);
            }
            set
            {
                if (MacIconPathRelative != value)
                {
                    EditorPrefs.SetBool("BuildSystem_MacIconPathRelative", value);
                }
            }
        }

        public static string MacScreenSelectorPath
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_MacScreenSelectorPath", "");
            }
            set
            {
                if (MacScreenSelectorPath != value)
                {
                    EditorPrefs.SetString("BuildSystem_MacScreenSelectorPath", value);
                }
            }
        }

        public static bool MacScreenSelectorPathRelative
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_MacScreenSelectorPathRelative", false);
            }
            set
            {
                if (MacScreenSelectorPathRelative != value)
                {
                    EditorPrefs.SetBool("BuildSystem_MacScreenSelectorPathRelative", value);
                }
            }
        }

        public static string MacCategory
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_MacCategory", "");
            }
            set
            {
                if (MacCategory != value)
                {
                    EditorPrefs.SetString("BuildSystem_MacCategory", value);
                }
            }
        }
        public static string MacBundleIdentifier
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_MacBundleIdentifier", "");
            }
            set
            {
                if (MacBundleIdentifier != value)
                {
                    EditorPrefs.SetString("BuildSystem_MacBundleIdentifier", value);
                }
            }
        }
        public static string MacGetInfoString
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_MacGetInfoString", "");
            }
            set
            {
                if (MacGetInfoString != value)
                {
                    EditorPrefs.SetString("BuildSystem_MacGetInfoString", value);
                }
            }
        }

        public static string AppleDeveloperUsername
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_AppleDeveloperUsername", "Username");
            }
            set
            {
                EditorPrefs.SetString("BuildSystem_AppleDeveloperUsername", value);
            }
        }


        public static string AppleDeveloperPassword
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_AppleDeveloperPassword", "Password");
            }
            set
            {
                EditorPrefs.SetString("BuildSystem_AppleDeveloperPassword", value);
            }
        }

        public static string AppleApplicationLoaderToolPath
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_AppleApplicationLoaderToolPath", "/Applications/Application Loader.app/Contents/Frameworks/ITunesSoftwareService.framework/Support/altool");
            }
            set
            {
                EditorPrefs.SetString("BuildSystem_AppleApplicationLoaderToolPath", value);
            }
        }

        public static bool AppleApplicationLoaderPathRelative
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_AppleApplicationLoaderPathRelative", false);
            }
            set
            {
                if (OutputFolderRelative != value)
                {
                    EditorPrefs.SetBool("BuildSystem_AppleApplicationLoaderPathRelative", value);
                }
            }
        }






















        public static string SteamSDKFolder
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_SteamSDKFolder", "");
            }
            set
            {
                if (SteamSDKFolder != value)
                {
                    EditorPrefs.SetString("BuildSystem_SteamSDKFolder",
                            Utilities.ForceEndsWith(value, System.IO.Path.DirectorySeparatorChar.ToString()));
                }

            }
        }

        public static bool SteamSDKFolderRelative
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_SteamSDKFolderRelative", true);
            }
            set
            {
                EditorPrefs.SetBool("BuildSystem_SteamSDKFolderRelative", value);
            }
        }

        public static string SteamUsername
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_SteamUsername", "username");
            }
            set
            {
                EditorPrefs.SetString("BuildSystem_SteamUsername", value);
            }
        }

        public static string SteamPassword
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_SteamPassword", "password");
            }
            set
            {
                EditorPrefs.SetString("BuildSystem_SteamPassword", value);
            }
        }
        public static string SteamScriptPath
        {
            get
            {
                return EditorPrefs.GetString("BuildSystem_SteamScriptPath", "");
            }
            set
            {
                EditorPrefs.SetString("BuildSystem_SteamScriptPath", value);
            }
        }
        public static bool SteamScriptPathRelative
        {
            get
            {
                return EditorPrefs.GetBool("BuildSystem_SteamScriptPathRelative", true);
            }
            set
            {
                EditorPrefs.SetBool("BuildSystem_SteamScriptPathRelative", value);
            }
        }


        public static int SteamAppID
        {
            get
            {
                return EditorPrefs.GetInt("BuildSystem_SteamAppID", 0);
            }
            set
            {
                EditorPrefs.SetInt("BuildSystem_SteamAppID", value);
            }
        }
    }
}