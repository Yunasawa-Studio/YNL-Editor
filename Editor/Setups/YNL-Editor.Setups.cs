#if YNL_CREATOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using YNL.Extensions.Methods;

namespace YNL.Editors.Setups
{
    public class Setups : AssetPostprocessor
    {
        private static ListRequest _request;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var inPackages = importedAssets.Any(path => path.StartsWith("Packages/")) ||
                deletedAssets.Any(path => path.StartsWith("Packages/")) ||
                movedAssets.Any(path => path.StartsWith("Packages/")) ||
                movedFromAssetPaths.Any(path => path.StartsWith("Packages/"));

            if (inPackages)
            {
                InitializeOnLoad();
            }
        }

        private static void InitializeOnLoad()
        {
            EditorApplication.update += OnEditorApplicationUpdate;
            EditorDefineSymbols.AddSymbols("YNL_EDITOR");
        }

        private static void OnEditorApplicationUpdate()
        {
            EditorApplication.update -= OnEditorApplicationUpdate;

            _request = Client.List();
            while (!_request.IsCompleted) { }

            if (_request.Status == StatusCode.Success)
            {
                TryInstallPackage(Client.List().Result, "com.yunasawa.ynl.utilities", "https://github.com/Yunasawa/YNL-Utilities.git", "1.5.2");
            }
            else Debug.LogWarning("Failed to list packages: " + _request.Error.message);
        }

        private static void TryInstallPackage(PackageCollection packages, string name, string url, string version)
        {
            if (packages.IsNull())
            {
                Debug.LogError("Package collection is null");
                return;
            }

            foreach (var package in packages)
            {
                if (package.name == name)
                {
                    if (IsNewerThan(version, package.version))
                    {
                        Client.Add($"{url}#{version}");
                        return;
                    }
                    else return;
                }
            }

            Client.Add($"{url}#{version}");
        }
        public static bool IsNewerThan(string currentVersion, string newVersion)
        {
            var currentParts = currentVersion.Split('.');
            var newParts = newVersion.Split('.');

            for (int i = 0; i < Math.Max(currentParts.Length, newParts.Length); i++)
            {
                int currentPart = i < currentParts.Length ? int.Parse(currentParts[i]) : 0;
                int newPart = i < newParts.Length ? int.Parse(newParts[i]) : 0;

                if (newPart > currentPart) return true;
                else if (newPart < currentPart) return false;
            }

            return false;
        }
    }
}
#endif