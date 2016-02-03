/*
 * XCodeRoutine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Routines
{
    using System.IO;

    internal class XCodeRoutine : IRoutine {
        public string ProvisioningProfile = "";
        public string SigningAuthority = "";
        public string ArchiveName = "Upload";
        public string ArchiveFormat = "ipa";

        public XCodeRoutine()
        {
            ArchiveName = Build.ExecutableName;
            ProvisioningProfile = Settings.IOSProvisioningProfile;
            SigningAuthority = Settings.IOSSigningAuthority;
        }
        public XCodeRoutine(string archiveName, string provisioningProfile, string signingAuthority)
        {
            ArchiveName = archiveName;
            ProvisioningProfile = provisioningProfile;
            SigningAuthority = signingAuthority;
        }
        
        public bool PreProcessor()
        {
            return true;
        }
        
        public bool PostProcessor()
        {
            // Add struct line
            string targetProblem = "typedef void	(*UnityPluginLoadFunc)(IUnityInterfaces* unityInterfaces);";
            string targetReplace = "typedef void	(*UnityPluginLoadFunc)(struct IUnityInterfaces* unityInterfaces);";

            string filePath = Utilities.CombinePath(Build.WorkingFolder, "Classes", "Unity", "UnityInterface.h");

            string fileContent = File.ReadAllText(filePath);
            fileContent = fileContent.Replace(targetProblem, targetReplace);
            File.WriteAllText(filePath, fileContent);
            
            // Clean Build Area
            string cleanArguements = "clean -project Unity-iPhone.xcodeproj";
            string cleanOutput = Utilities.CommandLine("xcodebuild", cleanArguements, Build.WorkingFolder, true);

            UnityEngine.Debug.Log(Build.Tag + cleanOutput);

            // Archive Build
            string archiveArguements = "archive -project Unity-iPhone.xcodeproj -scheme Unity-iPhone -archivePath Unity-iPhone.xcarchive";
            string archiveOutput = Utilities.CommandLine("xcodebuild", archiveArguements, Build.WorkingFolder, true);
            UnityEngine.Debug.Log(Build.Tag + archiveOutput);

            string packageArguements = "-exportArchive -archivePath Unity-iPhone.xcarchive -exportPath " + ArchiveName + " -exportFormat " + ArchiveFormat + " -exportProvisioningProfile \"" + ProvisioningProfile + "\"";// -exportSigningIdentity \"" + SigningAuthority + "\"";
            string packageOutput = Utilities.CommandLine("xcodebuild", packageArguements, Build.WorkingFolder, true);
            UnityEngine.Debug.Log(Build.Tag + packageOutput);

            return true;
        }
        
        public string GetFriendlyName()
        {
            return "XCode IPA";
        }
                
        public BuildFactory.Routines GetRoutineType()
        {
            return BuildFactory.Routines.XCode;
        }
    }
}