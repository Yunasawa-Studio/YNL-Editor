#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
using System;
using YNL.Editors.Extensions;
using UnityEngine;

namespace YNL.Editors.Visuals
{
    public class RepaintedEnumField : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Elements/Repainted/Property Fields/RepaintedEnumField";

        public EnumField Field;
        private VisualElement _labelField;
        public VisualElement Label => _labelField;
        private VisualElement _enumField;
        public VisualElement Enum => _enumField;

        public RepaintedEnumField(SerializedProperty serializedObject, string name) : base()
        {
            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            Type type = MType.GetTypeIgnoreAssembly(name);

            Field = new EnumField(serializedObject.name.AddSpaces(), (Enum)Activator.CreateInstance(type)).AddClass("Field", "unity-base-field__aligned");
            _enumField = Field.Q(classes: "unity-enum-field__input").AddClass("Input");
            _labelField = Field.Q(classes: "unity-label").AddClass("Label");

            this.AddElements(Field);

            Field.BindProperty(serializedObject);

            this.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            this.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }

        private void OnMouseEnter(MouseEnterEvent e)
        {
            _enumField.EnableClass("Input_Enter");
        }

        private void OnMouseLeave(MouseLeaveEvent e)
        {
            _enumField.DisableClass("Input_Enter");
        }

        #region Style
        public RepaintedEnumField SetAsBoxedField(float maxWidth = -1, bool isPercent = false)
        {
            this.SetPadding(5).SetPaddingTop(2.5f).SetBackgroundColor("#303030")
                .SetBorderRadius(5).SetHeight(47.5f).SetMargin(0).SetMarginBottom(5);

            if (maxWidth == -1) this.SetWidth(StyleKeyword.Auto);
            else this.SetWidth(maxWidth, isPercent);

            _labelField.SetTextAlign(TextAnchor.UpperLeft);

            Field.SetFlexDirection(FlexDirection.Column);

            return this;
        }
        #endregion
    }
}
#endif