/*
 * SteamDeployment.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Deployments
{
    using System.IO;
    using dotBunny.Unity.BuildSystem.Routines;
    public class SteamDeployment : IDeployment
    {
        public static string SteamCommandBuildScriptFolder
        {
            get
            {
                return SteamRoutine.SteamworksFolder + Path.DirectorySeparatorChar + "sdk" + Path.DirectorySeparatorChar +
                "tools" + Path.DirectorySeparatorChar + "ContentBuilder" + Path.DirectorySeparatorChar +
                "scripts";
            }
        }

        public static string SteamCommand
        {
            get
            {
                return SteamRoutine.SteamworksFolder + Path.DirectorySeparatorChar + "sdk" + Path.DirectorySeparatorChar +
                "tools" + Path.DirectorySeparatorChar + "ContentBuilder" + Path.DirectorySeparatorChar +
                "builder_osx" + Path.DirectorySeparatorChar + "steamcmd.sh";
            }
        }

        string _username;
        string _password;

        string _script;
        
        public SteamDeployment()
        {
             _username = Settings.SteamUsername;
            _password = Settings.SteamPassword;;
            _script = Settings.SteamScriptPath;
        }
        public SteamDeployment(string username, string password)
        {
            _username = username;
            _password = password;
            _script = Settings.SteamScriptPath;
        }
        public SteamDeployment(string username, string password, string script)
        {
            _username = username;
            _password = password;
            _script = script;
        }
        
        
        
        public BuildFactory.Deployments GetDeploymentType()
        {
            return BuildFactory.Deployments.Steam;
        }
        public string GetFriendlyName()
        {
            return "Steam CLI";
        }
        
        
        public bool Process()
        {
            string steamArguements = "+login " + _username + " " + _password + " +run_app_build " + _script + " +quit";

            // Execute Steam Build
            string steamOutput = Utilities.CommandLine(SteamCommand, steamArguements, "", true);

            if ( steamOutput.Contains("error") ) {
                UnityEngine.Debug.LogError(Build.Tag + steamOutput);
                return false;
            } else {
                UnityEngine.Debug.Log(Build.Tag + steamOutput);
                return true;
            }
        }


        public bool BuildPackage()
        {
            return false;
        }

    }
}