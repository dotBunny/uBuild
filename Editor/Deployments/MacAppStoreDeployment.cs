/*
 * MacAppStoreDeployment.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Deployments
{
    using System.IO;
    using UnityEditor;
    using dotBunny.Unity.BuildSystem.Routines;
    public class MacAppStoreDeployment : IDeployment
    {
        string _username;
        string _password;

        string _script;
        public MacAppStoreDeployment()
        {
            _username = Settings.AppleDeveloperUsername;
            _password = Settings.AppleDeveloperPassword;
        }
        public MacAppStoreDeployment(string username, string password)
        {
            _username = username;
            _password = password;
        }
        
        
        public BuildFactory.Deployments GetDeploymentType()
        {
            return BuildFactory.Deployments.MacAppStore;
        }
        
        public string GetFriendlyName()
        {
            return "Mac App Store";
        }

        public bool Process()
        {
            UnityEngine.Debug.Log(Build.Tag + "Deploying to Mac App Store ...");
            // Use 32 bit OSX for now
            var folder = Build.GetBuildFolder(BuildTarget.StandaloneOSXIntel, "");

            // Get package filename (from output folder?)
            string package = folder + Path.DirectorySeparatorChar + MacAppStoreRoutine.PackageName;

            string verifyArguements = "--validate-app -f \"" + package + "\" -u " + _username + " -p " + _password;
            string verifyOutput = Utilities.CommandLine(Settings.AppleApplicationLoaderToolPath, verifyArguements, "", true);

            // Upload
            if (verifyOutput.Contains("No errors validating archive"))
            {
                UnityEngine.Debug.Log(Build.Tag + "Package Validated");
                string uploadArguements = "--upload-app -f \"" + package + "\" -u " + _username + " -p " + _password;
                string uploadOutput = Utilities.CommandLine(Settings.AppleApplicationLoaderToolPath, uploadArguements, "", true);

                if (uploadOutput.Contains("*** Error: ***"))
                {
                    UnityEngine.Debug.LogError(Build.Tag + uploadOutput);
                    return false;
                }
                else
                {
                    UnityEngine.Debug.Log(Build.Tag + uploadOutput);
                    return true;
                }
            }
            else
            {
                UnityEngine.Debug.LogError(Build.Tag + verifyOutput);
                return false;
            }
        }

        public bool BuildPackage()
        {
            return false;

        }
    }
}