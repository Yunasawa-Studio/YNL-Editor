using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Editors.Setups
{
    public class Packages : EditorWindow
    {
        private static Packages _instance;

        private bool _dependencies;

        private bool _startDragging;
        private Vector2 _distance;

        private static Queue<string> _packagesToInstall = new Queue<string>();
        private static Queue<string> _packagesToRemove = new Queue<string>();
        private static AddRequest _addRequest;
        private static RemoveRequest _removeRequest;

        [MenuItem("🔗 YのL/▷ YNL - Editor/🌐 Package Installer")]
        public static void ShowWindow()
        {
            if (_instance != null) return;

            Packages window = CreateInstance<Packages>();

            window._dependencies = Setups.Dependencies;

            Rect main = EditorGUIUtility.GetMainWindowPosition();
            Rect pos = new Rect(main.x + main.width / 2 - 125, main.y + main.height / 2 - 75, 330, 100);
            window.position = pos;
            window.ShowPopup();

            _instance = window;
        }

        public static void CloseInstance()
        {
            _instance?.Close();
            _instance = null;
        }


        private void CloseWindow()
        {
            Close();
            CloseInstance();
        }

        private void OnEnable()
        {
            _dependencies = Setups.Dependencies;

            StyleSheet style = Resources.Load<StyleSheet>("Style Sheets/Packages/Package");

            var root = rootVisualElement;
            if (!root.styleSheets.Contains(style)) root.styleSheets.Add(style);
            root.AddToClassList("Main");

            VisualElement bar = new VisualElement();
            bar.AddToClassList("Bar");
            root.Add(bar);

            Button barClose = new Button();
            barClose.clicked += CloseWindow;
            barClose.AddToClassList("BarClose");
            bar.Add(barClose);

            Label barLabel = new Label("YNL - General Toolbox | Packages Manager");
            barLabel.AddToClassList("BarLabel");
            bar.Add(barLabel);

            var label = new Label("Dependencies:");
            label.AddToClassList("Title");
            root.Add(label);

            PackageBox utilitiesPack = new("YNL - Utilities", _dependencies);

            root.Add(utilitiesPack);

            VisualElement space = new VisualElement();
            space.style.height = 10;

            root.Add(space);

            VisualElement buttons = new VisualElement();
            buttons.AddToClassList("ButtonBar");

            Button uninstallAll = new Button();
            uninstallAll.text = "Uninstall All";
            uninstallAll.AddToClassList("Button");
            uninstallAll.style.color = new Color(1, 0.4941f, 0.4588f);
            uninstallAll.clicked += UninstallAll;

            Button removeDefine = new Button();
            removeDefine.text = "Remove Define Symbols";
            removeDefine.AddToClassList("Button");
            removeDefine.style.color = new Color(0.9725f, 1, 0.4588f);
            removeDefine.clicked += RemoveDefineSymbols;

            Button installAll = new Button();
            installAll.text = "Install All";
            installAll.AddToClassList("Button");
            installAll.style.color = new Color(0.4588f, 1, 0.7098f);
            installAll.clicked += InstallAll;

            buttons.Add(uninstallAll);
            buttons.Add(removeDefine);
            buttons.Add(installAll);

            root.Add(buttons);

            root.RegisterCallback<MouseDownEvent>(OnMouseDown);
            root.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            root.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        private  void InstallAll()
        {
            Debug.Log("<b><color=#c5ffb0>This process can take minutes, be patient and please wait until everything is done!</color></b>");

            _packagesToInstall.Enqueue("https://github.com/Yunasawa/YNL-Utilities.git#1.5.2");

            InstallNextPackage();
        }

        private void UninstallAll()
        {
            Debug.Log("<b><color=#c5ffb0>This process can take minutes, be patient and please wait until everything is done!</color></b>");

            _packagesToRemove.Enqueue("com.yunasawa.ynl.utilities");

            RemoveNextPackage();
        }

        private static void InstallNextPackage()
        {
            if (_packagesToInstall.Count > 0)
            {
                string packageUrl = _packagesToInstall.Dequeue();
                _addRequest = Client.Add(packageUrl);
                EditorApplication.update += ProgressInstall;
            }
        }

        private static void RemoveNextPackage()
        {
            if (_packagesToRemove.Count > 0)
            {
                string packageName = _packagesToRemove.Dequeue();
                _removeRequest = Client.Remove(packageName);
                EditorApplication.update += ProgressRemove;
            }
        }

        private static void RemoveDefineSymbols()
        {
            EditorDefineSymbols.RemoveSymbols("YNL_UTILITIES", "YNL_EDITOR");
        }

        private static void ProgressInstall()
        {
            if (_addRequest.IsCompleted)
            {
                if (_addRequest.Status == StatusCode.Success)
                {
                    Debug.Log("Installed: " + _addRequest.Result.packageId);
                    InstallNextPackage();
                }
                else if (_addRequest.Status >= StatusCode.Failure)
                {
                    //Debug.LogError(_addRequest.Error.message);
                }
                EditorApplication.update -= ProgressInstall;
            }
        }

        private static void ProgressRemove()
        {
            if (_removeRequest.IsCompleted)
            {
                if (_removeRequest.Status == StatusCode.Success)
                {
                    Debug.Log("Removed package succeeded");
                    RemoveNextPackage();
                }
                else if (_removeRequest.Status >= StatusCode.Failure)
                {
                    //Debug.LogError(_removeRequest.Error.message);
                }
                EditorApplication.update -= ProgressRemove;
            }
        }

        private void OnMouseDown(MouseDownEvent evt)
        {
            Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            Vector2 position = new(this.position.x, this.position.y);

            _distance = position - mouse;
            _startDragging = true;

            evt.StopPropagation();
        }
        private void OnMouseMove(MouseMoveEvent evt)
        {
            if (_startDragging)
            {
                Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                Vector2 position = _distance + mouse;
                this.position = new Rect(position.x, position.y, this.position.width, this.position.height);
            }

            evt.StopPropagation();
        }
        private void OnMouseUp(MouseUpEvent evt)
        {
            _startDragging = false;
        }
    }

    public class PackageBox : VisualElement
    {
        private Button _status;
        private Label _label;
        private Label _type;

        public PackageBox(string label, bool status)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>("Style Sheets/Packages/Package");

            this.styleSheets.Add(styleSheet);
            this.AddToClassList("PackageBox");

            _label = new Label(label);
            _label.AddToClassList("PackageLabel");
            _label.style.color = status ? new Color(255, 255, 255) : new Color(127, 127, 127);

            _status = new Button();
            _status.AddToClassList("PackageStatus");
            if (status) _status.style.backgroundImage = new StyleBackground(Resources.Load<Texture2D>("Textures/V"));

            _type = new Label("Git URL");
            _type.AddToClassList("PackageType");

            this.Add(_status);
            this.Add(_label);
            this.Add(_type);
        }
    }
}