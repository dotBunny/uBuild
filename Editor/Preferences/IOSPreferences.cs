/*
 * IOSPreferences.cs
 *
 * Author:
 *   Matthew Davey <matthew.davey@dotbunny.com>
 */
namespace dotBunny.Unity.BuildSystem
{
    using UnityEditor;

    public static partial class Preferences
    {
        static void RenderiOSTab()
        {
            Settings.IOSProvisioningProfile = EditorGUILayout.DelayedTextField("Provisioning Profile", Settings.IOSProvisioningProfile);
            Settings.IOSSigningAuthority = EditorGUILayout.DelayedTextField("Signing Authority", Settings.IOSSigningAuthority);
        }
    }
}