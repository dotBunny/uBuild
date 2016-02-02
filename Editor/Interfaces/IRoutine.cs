/*
 * BuildWindow.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Routines
{
    using dotBunny.Unity.BuildSystem;
    public interface IRoutine
    {
        string GetFriendlyName();
        BuildFactory.Routines GetRoutineType();
        bool PreProcessor();
        bool PostProcessor();
    }
}