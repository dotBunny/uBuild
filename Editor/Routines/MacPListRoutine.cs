/*
 * MacSigningRoutine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Routines
{
    using UnityEditor;
    using System.IO;

    internal class MacPListRoutine : IRoutine {
        public string GetInfoString;
        public string Copyright;
        
        public string BundleIdentifier = "com.dotBunny.Cloney.Desktop";
        public string IconFile = "PlayerIcon.icns";
        public string IconFilePath = "";
        public string ScreenSelectorPath = "";
        public string ScreenSelectorFile = "ScreenSelector.tif";
        public string DefaultCategory = "public.app-category.arcade-games";
        

        
        // GetInfoString
        
   
        
        //MacIconICNSFilePath
        //MacScreenSelectorTifPath
        //MacCategory
        //MacBundleIdentifier
        //MacGetInfoString
        
        
        // screen selector
        // default category
        
        
        
        public MacPListRoutine()
        {
            
            Copyright = Settings.SharedCopyright;
            GetInfoString = Settings.MacGetInfoString;
            BundleIdentifier = Settings.MacBundleIdentifier;
            IconFilePath = Settings.MacIconPath;
            ScreenSelectorPath = Settings.MacScreenSelectorPath;
            DefaultCategory = Settings.MacCategory;

        }


        public string PList
        {
            get
            {
                return
          "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\n<plist version=\"1.0\">\n<dict>\n\t<key>CFBundleDevelopmentRegion</key>\n\t<string>" + Settings.SharedDefaultLanguage + "</string>\n\t<key>CFBundleExecutable</key>\n\t<string>" + Build.ExecutableName + "</string>\n\t<key>CFBundleGetInfoString</key>\n\t<string>" + GetInfoString + "</string>\n\t<key>CFBundleIconFile</key>\n\t<string>" + IconFile +"</string>\n\t<key>CFBundleIdentifier</key>\n\t<string>" + BundleIdentifier + "</string>\n\t<key>CFBundleInfoDictionaryVersion</key>\n\t<string>6.0</string>\n\t<key>CFBundleName</key>\n\t<string>" + Build.ExecutableName + "</string>\n\t<key>CFBundlePackageType</key>\n\t<string>APPL</string>\n\t<key>CFBundleShortVersionString</key>\n\t<string>" + PlayerSettings.bundleVersion + "</string>\n\t<key>CFBundleSignature</key>\n\t<string>????</string>\n\t<key>CFBundleVersion</key>\n\t<string>" + PlayerSettings.bundleVersion + "</string>\n\t<key>NSMainNibFile</key>\n\t<string>MainMenu</string>\n\t<key>NSPrincipalClass</key>\n\t<string>PlayerApplication</string>\n\t<key>LSApplicationCategoryType</key>\n\t<string>" + DefaultCategory + "</string>\n\t<key>NSHumanReadableCopyright</key>\n\t<string>" + Copyright + "</string>\n</dict>\n</plist>\n";
            }
        }
        
        public string GetFriendlyName()
        {
            return "Mac PList";
        }
        public BuildFactory.Routines GetRoutineType()
        {
            return BuildFactory.Routines.MacPList;
        }
   
        public bool PreProcessor()
        {
            return true;
        }
        public bool PostProcessor()
        {

            // Output Signed PLIST
            File.WriteAllText (
                Utilities.CombinePath(Build.WorkingFolder, Build.ExecutableName + ".app", "Contents", "Info.plist"), 
                PList);
            
            // Copy Icon
            if ( string.IsNullOrEmpty(Settings.MacIconPath)) {
                UnityEngine.Debug.LogWarning(Build.Tag + "Mac ICNS File Path Empty");
            } else
            {
                File.Copy(
                    IconFilePath,
                    Utilities.CombinePath(Build.WorkingFolder, Build.ExecutableName + ".app", "Contents", "Resources", IconFile),
                    true);
            }

            // Copy over screen select, just incase
            if ( string.IsNullOrEmpty(Settings.MacScreenSelectorPath)) {
                UnityEngine.Debug.LogWarning(Build.Tag + "Mac Screen Selector File Path Empty");
            } else
            {
                File.Copy(
                    Settings.MacScreenSelectorPath,
                    Utilities.CombinePath(Build.WorkingFolder, Build.ExecutableName + ".app", "Contents", "Resources", ScreenSelectorFile),
                    true);
            }

            return true;
        }        
    }
}