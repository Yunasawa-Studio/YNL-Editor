#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using YNL.Extensions.Methods;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class RepaintedTextField : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Elements/Repainted/Property Fields/RepaintedTextField";

        public TextField Field;
        private VisualElement _labelField;
        private VisualElement _inputField;

        public RepaintedTextField(SerializedProperty serializedObject) : base()
        {
            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            Field = new TextField(serializedObject.name.AddSpaces()).AddClass("Field", "unity-base-field__aligned");
            _inputField = Field.Q("unity-text-input").AddClass("Input");
            _labelField = Field.Q(classes: "unity-label").AddClass("Label");

            this.AddElements(Field);

            Field.BindProperty(serializedObject);

            this.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            this.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }

        private void OnMouseEnter(MouseEnterEvent e)
        {
            _inputField.EnableClass("Input_Enter");
        }

        private void OnMouseLeave(MouseLeaveEvent e)
        {
            _inputField.DisableClass("Input_Enter");
        }
    }
}
#endif