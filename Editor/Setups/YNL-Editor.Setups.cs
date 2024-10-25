#if UNITY_EDITOR && UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using System.Linq;

namespace YNL.Editors.Setups
{
    public class Setups : AssetPostprocessor
    {
        public const string DependenciesKey = "YNL - Editor | dependencies";

        private static ListRequest _request;
        public static bool Dependencies;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var inPackages = importedAssets.Any(path => path.StartsWith("Packages/")) ||
                deletedAssets.Any(path => path.StartsWith("Packages/")) ||
                movedAssets.Any(path => path.StartsWith("Packages/")) ||
                movedFromAssetPaths.Any(path => path.StartsWith("Packages/"));

            if (inPackages)
            {
                EditorApplication.update += OnEditorApplicationUpdate;
            }
        }

        private static void OnEditorApplicationUpdate()
        {
            EditorApplication.update -= OnEditorApplicationUpdate;

            _request = Client.List();
            while (!_request.IsCompleted) { }

            if (_request.Status == StatusCode.Success)
            {
                Dependencies = false;

                IsPackageInstalled(_request.Result, "com.yunasawa.ynl.utilities", ref Dependencies);
            }

            bool dependenciesResolver = EditorPrefs.GetBool(DependenciesKey);

            if ((!Dependencies) && !dependenciesResolver) Packages.ShowWindow();

            EditorPrefs.SetBool(DependenciesKey, true);

            EditorDefineSymbols.AddSymbols("YNL_EDITOR");
        }

        private static void IsPackageInstalled(PackageCollection packages, string name, ref bool checker)
        {
            if (packages == null) return;

            foreach (var package in packages)
            {
                if (package.name == name) checker = true;
            }
        }
    }
}
#endif