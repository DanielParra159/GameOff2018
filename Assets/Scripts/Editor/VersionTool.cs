using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class VersionTool
    {
        [MenuItem("Tools/Version/Increment Patch")]
        public static void IncrementPatch()
        {
            var currentVersion = new Version(PlayerSettings.bundleVersion);
            currentVersion = currentVersion.IncrementPatch();
            SetVersion(currentVersion);
        }

        [MenuItem("Tools/Version/Increment Minor")]
        public static void IncrementMinor()
        {
            var currentVersion = new Version(PlayerSettings.bundleVersion);
            currentVersion = currentVersion.IncrementMinor();
            SetVersion(currentVersion);
        }

        [MenuItem("Tools/Version/Increment Major")]
        public static void IncrementMajor()
        {
            var currentVersion = new Version(PlayerSettings.bundleVersion);
            currentVersion = currentVersion.IncrementMajor();
            SetVersion(currentVersion);
        }

        private static void SetVersion(Version currentVersion)
        {
            PlayerSettings.macOS.buildNumber =  PlayerSettings.bundleVersion = currentVersion.ToString();
            Debug.Log("Setting New Bundle Version :: " + PlayerSettings.bundleVersion);
            Debug.Log("Setting New MacOs BuildNumber :: " + PlayerSettings.macOS.buildNumber);

            PlayerSettings.Android.bundleVersionCode = currentVersion.Major * 1000000 + currentVersion.Minor * 1000 + currentVersion.Build;
            Debug.Log("Setting Android bundleVersionCode :: " + PlayerSettings.Android.bundleVersionCode);

            PlayerSettings.iOS.buildNumber = PlayerSettings.Android.bundleVersionCode.ToString();
            Debug.Log("Setting iOS buildNumber :: " + PlayerSettings.iOS.buildNumber);
        }

        private static Version IncrementPatch(this Version version) {
            return new Version(version.Major, version.Minor, version.Build + 1);
        }
    
        private static Version IncrementMinor(this Version version) {
            return new Version(version.Major, version.Minor + 1, 0);
        }
    
        private static Version IncrementMajor(this Version version) {
            return new Version(version.Major + 1, 0, 0);
        }
    }
}
