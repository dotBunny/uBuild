/*
 * Build.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    public static class BuildFactory
    {
        public enum Routines
        {
            Default,
            Define,
            BundleVersion,
            MacAppStore,
            Prime31,
            Steam,
            XCode,
            MacPList
        }
        
        public enum Modifiers
        {
            Define,
            BuildInfo
        }

        public enum Deployments
        {
            Default,
            MacAppStore,
            Steam
        }

      //  public static IDeployment CreateDeployment(Deployments type)
      //  {
            // switch (type)
            // {
            //     case Deployments.MacAppStore:
            //         return new BaseDeployment();
            //     case Deployments.Steam:
            //         return new BaseDeployment();
            //     default:
            //         return null;
            // }
      //  }

        //public static BaseRoutine CreateRoutine(Routines type)
       // {
            // switch (type)
            // {
            //     case Routines.BundleVersion:
            //         return new BaseRoutine();
            //     case Routines.MacAppStore:
            //         return new BaseRoutine();
            //     case Routines.MacSigning:
            //         return new BaseRoutine();
            //     case Routines.Prime31:
            //         return new BaseRoutine();
            //     case Routines.Steam:
            //         return new BaseRoutine();
            //     case Routines.XCode:
            //         return new BaseRoutine();
            //     default:
            //         return null;
            // }
       // }
    }
}

