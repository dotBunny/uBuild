/*
 * BundleVersionRoutine.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem.Modifiers
{
    using dotBunny.Unity.BuildSystem;
    using UnityEditor;
    internal class DefineModifier : IModifier
    {
        public string Tag;
        bool _previousDefine;
        BuildTargetGroup _routineTargetGroup;
        public string GetFriendlyName()
        {
            return "Define #" + Tag;
        }
        
        public DefineModifier(string tag)
        {
            Tag = tag;
        }
        
        public BuildFactory.Modifiers GetModifierType()
        {
            return BuildFactory.Modifiers.Define;
        }

        public bool PreProcessor()
        {
            _routineTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

            if (PlayerSettings.GetScriptingDefineSymbolsForGroup(_routineTargetGroup).Contains(Tag))
            {
                _previousDefine = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(PlayerSettings.GetScriptingDefineSymbolsForGroup(_routineTargetGroup)) && 
                PlayerSettings.GetScriptingDefineSymbolsForGroup(_routineTargetGroup).Length > 0)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(_routineTargetGroup, PlayerSettings.GetScriptingDefineSymbolsForGroup(_routineTargetGroup) + ";" + Tag);
                }
                else
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(_routineTargetGroup, Tag);
                }
            }
            return true;
        }

        public bool PostProcessor()
        {
             // Remove Define
            if (!_previousDefine)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                    _routineTargetGroup, 
                    PlayerSettings.GetScriptingDefineSymbolsForGroup(
                        _routineTargetGroup).Replace(Tag + ";", ""));
            }
            return true;
        }

    }
}