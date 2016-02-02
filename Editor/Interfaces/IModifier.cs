/*
 * BuildWindow.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Modifiers
{
    using dotBunny.Unity.BuildSystem;
    public interface IModifier
    {

        string GetFriendlyName();
        BuildFactory.Modifiers GetModifierType();
        bool PreProcessor();
        bool PostProcessor();
    }
}