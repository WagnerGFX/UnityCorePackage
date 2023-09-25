using UnityEditor;
using UnityEngine;

namespace CorePackage
{
    public static class ExportPackages
    {
        private static string _outputFolder;

        [MenuItem("Tools/Export UnityPackages")]
        public static void Export()
        {
            string selectedFolder = EditorUtility.OpenFolderPanel("Select output folder", _outputFolder, "");

            if (string.IsNullOrEmpty(selectedFolder))
            { return; }

            _outputFolder = selectedFolder + "/";

            Export_All();
            Export_Debugging();
            Export_EventClassic();
            Export_EventUnity();
            Export_GameObjectGUID();
            Export_ScriptTemplates();
            Export_Singleton();
            Export_StateMachine();
            Export_Utilities();

            Debug.Log($"All Packages exported to: <a href=\"file:///{_outputFolder}\">{_outputFolder}</a>");

            var explorerPath = _outputFolder.Replace("/", @"\");
            System.Diagnostics.Process.Start("explorer.exe", explorerPath);
        }

        private static void Export_All()
        {
            string[] exportAssets = {
                "Assets/CorePackage",
                "Assets/CorePackageSamples"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_Debugging()
        {
            string[] exportAssets = {
                "Assets/CorePackage/Debugging",
                "Assets/CorePackageSamples/Debugging"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_Debugging.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_EventClassic()
        {
            string[] exportAssets = {
                "Assets/CorePackage/EventSystems/Classic",
                "Assets/CorePackageSamples/EventSystems/Classic",
                "Assets/CorePackageSamples/_Assets",
                "Assets/CorePackageSamples/_Settings",
                "Assets/CorePackage/Utilities/Extensions"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_EventSystems Classic.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_EventUnity()
        {
            string[] exportAssets = {
                "Assets/CorePackage/EventSystems/Unity",
                "Assets/CorePackageSamples/EventSystems/Unity"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_EventSystems Unity.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_GameObjectGUID()
        {
            string[] exportAssets = {
                "Assets/CorePackage/GameObjectGUID",
                "Assets/CorePackageSamples/GameObjectGUID"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_GameObjectGUID.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_ScriptTemplates()
        {
            string[] exportAssets = {
                "Assets/CorePackage/ScriptTemplates",
                "Assets/CorePackageSamples/ScriptTemplates",
                "Assets/CorePackage/Utilities/Extensions"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_ScriptTemplates.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_Singleton()
        {
            string[] exportAssets = {
                "Assets/CorePackage/Singleton",
                "Assets/CorePackageSamples/Singleton"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_Singleton.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_StateMachine()
        {
            string[] exportAssets = {
                "Assets/CorePackage/StateMachine"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_StateMachine.unitypackage",
                ExportPackageOptions.Recurse);
        }

        private static void Export_Utilities()
        {
            string[] exportAssets = {
                "Assets/CorePackage/Utilities"
            };

            AssetDatabase.ExportPackage(
                exportAssets,
                _outputFolder + "CorePackage_Utilities.unitypackage",
                ExportPackageOptions.Recurse);
        }
    }
}
