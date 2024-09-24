#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Extensions.Methods;

namespace YNL.Editors.Visuals
{
    public class RepaintedComponentField<T> : Button where T : Component
    {
        private const string USS_StyleSheet = "Style Sheets/Elements/Repainted/Object Fields/RepaintedObjectField";

        private const string _uss_root = "root";
        private const string _uss_background = "background";
        private const string _uss_icon = "icon";
        private const string _uss_name = "name";
        private const string _uss_ping = "ping";
        private const string _uss_pingicon = "ping-icon";

        public FlexibleComponentField<T> Background;
        public Image Icon;
        public Label Name;
        public FlexibleInteractImage Ping;
        public Image PingIcon;

        public T ReferencedObject;
        public GameObject GameObject;
        private string _typeName;
        private int _controlID;

        public RepaintedComponentField(T bindedObject) : base()
        {
            ReferencedObject = bindedObject;
            _typeName = typeof(T).Name;

            this.AddStyle(USS_StyleSheet).AddClass(_uss_root);

            Texture2D objectIcon = ETexture.Unity(_typeName);
            Icon = new Image().SetBackgroundImage(objectIcon).AddClass(_uss_icon);

            Name = new Label().AddClass(_uss_name);
            if (!ReferencedObject.IsNull()) HighlineField();
            else LowlineField();

            Background = new FlexibleComponentField<T>().AddClass(_uss_background);
            Background.OnPointerDown += PointerDownOnField;
            Background.OnPointerEnter += PointerEnterOnField;
            Background.OnPointerExit += PointerExitOnField;
            Background.OnDragEnter += DragEnterOnField;
            Background.OnDragExit += DragExitOnField;
            Background.OnDragPerform += DragPerformOnField;

            Background.AddElements(Icon, Name);

            PingIcon = new Image().AddClass(_uss_pingicon);

            Ping = new FlexibleInteractImage().AddClass(_uss_ping).AddElements(PingIcon);
            Ping.OnPointerDown += PointerDownOnSelection;
            Ping.OnPointerEnter += PointerEnterOnSelection;
            Ping.OnPointerExit += PointerExitOnSelection;

            this.AddElements(Background, Ping);
        }

        public void OnGUI()
        {
            string commandName = Event.current.commandName;

            if (_controlID == EditorGUIUtility.GetObjectPickerControlID())
            {
                if (commandName == "ObjectSelectorUpdated" || commandName == "ObjectSelectorClosed")
                {
                    GameObject = EditorGUIUtility.GetObjectPickerObject() as GameObject;

                    if (GameObject.IsNullOrDestroyed())
                    {
                        ReferencedObject = null;
                        LowlineField();
                    }
                    else
                    {
                        ReferencedObject = GameObject.GetComponent<T>();
                        DragPerformOnField(ReferencedObject);
                    }
                }
            }
        }

        public void PointerDownOnField()
        {
            EditorGUIUtility.PingObject(ReferencedObject);
        }
        public void PointerEnterOnField()
        {
            Background.EnableClass(_uss_background.EHover());
            Icon.EnableClass(_uss_icon.EHover());
            Name.EnableClass(_uss_name.EHover());
            Ping.EnableClass($"{_uss_ping}__field-hover");
        }
        public void PointerExitOnField()
        {
            Background.DisableClass(_uss_background.EHover());
            Icon.DisableClass(_uss_icon.EHover());
            Name.DisableClass(_uss_name.EHover());
            Ping.DisableClass($"{_uss_ping}__field-hover");
        }
        public void DragEnterOnField()
        {
            Ping.DisableClass($"{_uss_ping}__field-hover");
            Background.DisableClass(_uss_background.EHover());
            Background.EnableClass(_uss_background.EDrag());
            Ping.EnableClass(_uss_ping.EDrag());
        }
        public void DragExitOnField()
        {
            Background.DisableClass(_uss_background.EDrag());
            Ping.DisableClass(_uss_ping.EDrag());
        }
        public void DragPerformOnField(T referencedObject)
        {
            Background.DisableClass(_uss_background.EDrag());
            Ping.DisableClass(_uss_ping.EDrag());

            ReferencedObject = referencedObject;

            HighlineField();
        }

        public void PointerDownOnSelection()
        {
            _controlID = GUIUtility.GetControlID(FocusType.Passive);

            EditorGUIUtility.ShowObjectPicker<T>(GameObject, true, "", _controlID);
        }
        public void PointerEnterOnSelection()
        {
            Ping.EnableClass(_uss_ping.EHover());
            PingIcon.EnableClass(_uss_pingicon.EHover());
        }
        public void PointerExitOnSelection()
        {
            Ping.DisableClass(_uss_ping.EHover());
            PingIcon.DisableClass(_uss_pingicon.EHover());
        }

        private void HighlineField()
        {
            Name.text = $"{ReferencedObject.name} ({_typeName.AddSpaces()})";
            Name.SetColor("#FFFFFF");
            Icon.SetBackgroundImageTintColor("#FFFFFF".ToColor());
        }
        private void LowlineField()
        {
            Name.text = $"None ({_typeName.AddSpaces()})";
            Name.SetColor("#7D7D7D");
            Icon.SetBackgroundImageTintColor("#7F7F7F".ToColor());
        }
    }
}
#endif