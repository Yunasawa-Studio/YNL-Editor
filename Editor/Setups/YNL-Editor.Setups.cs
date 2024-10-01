#if !YNL_CREATOR
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using YNL.Editors.Setups;

namespace YNL.Editors.Setups
{
    [InitializeOnLoad]
    public class Setups
    {
        public const string DependenciesKey = "YNL - Editor | dependencies";

        private static ListRequest _request;
        public static bool Dependencies;

        static Setups()
        {
            EditorApplication.update += OnEditorApplicationUpdate;
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

            EditorDefineSymbols.AddSymbols("YNL_UTILITIES");
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