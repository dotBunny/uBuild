/*
 * Utilities.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System;
    using UnityEngine;
    using UnityEditor;

    public static class Utilities
    {
        public static string ForceEndsWith(string source, string end)
        {
            if (!source.EndsWith(end))
            {
                source += end;
            }
            return source;

        }
        public static string MakeRelativePath(string filePath, string relativeTo)
        {
            // Handle File
            if (!File.Exists(filePath) && !filePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                filePath += Path.DirectorySeparatorChar.ToString();
            }

            // Folders must end in a slash
            if (!relativeTo.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                relativeTo += Path.DirectorySeparatorChar.ToString();
            }

            Uri pathUri = new Uri(filePath, UriKind.Absolute);
            Uri folderUri = new Uri(relativeTo, UriKind.Absolute);

            return ".." + Path.DirectorySeparatorChar + Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));


        }

        public static string GetAbsolutePath(string relativePath)
        {
            return Path.GetFullPath(Build.ProjectFolder + Path.DirectorySeparatorChar + relativePath);
        }

        public static string CombinePath(params string[] items)
        {
            string returnValue = "";

            foreach (string item in items)
            {
                returnValue += Path.DirectorySeparatorChar + item;
            }
            return Path.GetFullPath(returnValue);
        }

        public static string CommandLine(string command, string arguements, string workingDirectory, bool showWindow)
        {
            showWindow = false;

            var p = new System.Diagnostics.Process();

            p.StartInfo.WorkingDirectory = workingDirectory;
            p.StartInfo.UseShellExecute = false;


            p.StartInfo.RedirectStandardError = !showWindow;
            p.StartInfo.RedirectStandardOutput = !showWindow;

            p.StartInfo.FileName = command;
            p.StartInfo.Arguments = arguements;

            p.StartInfo.CreateNoWindow = !showWindow;
            if (!showWindow)
            {
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }

            p.Start();

            if (!showWindow)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                const int bufferSize = 1024;
                char[] buffer = new char[bufferSize];
                int count = bufferSize;
                while (count > 0)
                {
                    count = p.StandardOutput.Read(buffer, 0, 1024);
                    sb.Append(buffer, 0, count);
                }

                string error = p.StandardError.ReadToEnd();
                p.WaitForExit();

                string ret = sb.ToString();
                if (error.Length > 0)
                {
                    ret = error;
                }
                return ret;
            }
            else
            {
                return "Completed.";
            }
        }


        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static string GetProjectName()
        {
            string[] s = Application.dataPath.Split('/');
            return s[s.Length - 5];
        }

        public static string[] GetScenePaths()
        {
            var EditorScenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled)
                    continue;
                EditorScenes.Add(scene.path);
            }
            return EditorScenes.ToArray();
        }

        public static void IncrementBuild()
        {
            var GUIDs = AssetDatabase.FindAssets("t:Script BuildInfo");
            var path = Application.dataPath.Substring(0, Application.dataPath.Length - "/Assets".Length) + System.IO.Path.DirectorySeparatorChar + AssetDatabase.GUIDToAssetPath(GUIDs[0]).Replace('/', System.IO.Path.DirectorySeparatorChar);

            string[] file = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains("BuildNumber"))
                {
                    string data = file[i];
                    data = data.Replace("public static int BuildNumber = ", "");
                    data = data.Replace(";", "");
                    data = data.Trim();
                    int build = int.Parse(data);
                    build++;
                    file[i] = "\tpublic static int BuildNumber = " + build + ";";
                }
            }

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            fileInfo.IsReadOnly = false;
            File.WriteAllLines(path, file);

            // Force reimport
            AssetDatabase.ImportAsset(AssetDatabase.GUIDToAssetPath(GUIDs[0]), ImportAssetOptions.ForceUpdate);
        }
        
        public static void TimestampBuild()
        {
            var GUIDs = AssetDatabase.FindAssets("t:Script BuildInfo");
            var path = Application.dataPath.Substring(0, Application.dataPath.Length - "/Assets".Length) + System.IO.Path.DirectorySeparatorChar + AssetDatabase.GUIDToAssetPath(GUIDs[0]).Replace('/', System.IO.Path.DirectorySeparatorChar);

            string[] file = System.IO.File.ReadAllLines(path);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains("Timestamp"))
                {
                    file[i] = "\tpublic static string Timestamp = \"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\";";
                }
            }

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            fileInfo.IsReadOnly = false;
            File.WriteAllLines(path, file);

            // Force reimport
            AssetDatabase.ImportAsset(AssetDatabase.GUIDToAssetPath(GUIDs[0]), ImportAssetOptions.ForceUpdate);
        }

        public static void RemoveAllFilesRecursive(string directory, string searchPattern)
        {
            string[] files = Directory.GetFiles(directory, searchPattern, SearchOption.AllDirectories);
            foreach (string file in files)
            {
                FileUtil.DeleteFileOrDirectory(file);
            }
        }

        public static void AskToIncrememnt()
        {
            if (EditorUtility.DisplayDialog("Increment Build Number?",
            "Do you want to increment the build number?", "Yes", "No"))
            {
                IncrementBuild();
            }
        }

        public static string GetPlatformTag(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return Settings.OutputPlatformAndroidTag;
                case BuildTarget.iOS:
                    return Settings.OutputPlatformiOSTag;
                case BuildTarget.WebGL:
                    return Settings.OutputPlatformWebGLTag;
                case BuildTarget.StandaloneWindows:
                    return Settings.OutputPlatformWin32Tag;
                case BuildTarget.StandaloneWindows64:
                    return Settings.OutputPlatformWin64Tag;
                case BuildTarget.StandaloneLinux:
                    return Settings.OutputPlatformLinux32Tag;
                case BuildTarget.StandaloneLinux64:
                    return Settings.OutputPlatformLinux64Tag;
                case BuildTarget.StandaloneOSXIntel:
                    return Settings.OutputPlatformOSX32Tag;
                case BuildTarget.StandaloneOSXIntel64:
                    return Settings.OutputPlatformOSX64Tag;
                default:
                    return "Unknown";
            }
        }
    }
}