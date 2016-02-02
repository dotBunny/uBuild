/*
 * Prime31Routine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Routines
{
    using System.IO;
    using System.Collections.Generic;

    internal class Prime31Routine : IRoutine {


        public BuildFactory.Routines GetRoutineType()
        {
            return BuildFactory.Routines.Prime31;
        }
        public string GetFriendlyName()
        {
            return "Prime31 File Cleanup";
        }

        public bool PreProcessor()
        {
            return true;
        }

        public bool PostProcessor()
        {
            UnityEngine.Debug.Log(Build.Tag + "Prime31 Routine POST Handler ...");

            // Remove mono bridge from project file (legacy safety check)
            Utilities.RemoveAllFilesRecursive(Build.WorkingFolder + Path.DirectorySeparatorChar, "libP31MonoBridge*");

            // Remove entry from project file
            List<string> projectFile = new List<string>();

            // Project File Path
            string filePath = Build.WorkingFolder + Path.DirectorySeparatorChar + "Unity-iPhone.xcodeproj" + Path.DirectorySeparatorChar + "project.pbxproj";
            
            string[] contents = File.ReadAllLines(filePath);

            projectFile.AddRange(contents);

            List<int> removeIndexes = new List<int>();

            for (int i = 0; i < projectFile.Count; i++)
            {
                if (projectFile[i].Contains("libP31MonoBridge"))
                {
                    removeIndexes.Add(i);
                }
            }
            
            if ( removeIndexes.Count > 0 ) {
                // Go reverse order as to not change indexes
                for (int i = (removeIndexes.Count - 1); i >= 0; i--) {
                    projectFile.RemoveAt(removeIndexes[i]);
                }
            }

            File.WriteAllLines(filePath, projectFile.ToArray());

            return true;
        }
        
                
    }
}