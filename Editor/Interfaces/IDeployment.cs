/*
 * BuildWindow.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Deployments
{
    using dotBunny.Unity.BuildSystem;
    public interface IDeployment
    {
        BuildFactory.Deployments GetDeploymentType();
        string GetFriendlyName();
        bool Process();

        bool BuildPackage();
    }
}