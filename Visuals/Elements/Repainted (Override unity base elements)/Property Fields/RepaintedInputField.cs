#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.TextCore.Text;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class RepaintedInputField<T> : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Elements/Repainted/Property Fields/RepaintedInputField";

        protected TextValueField<T> _field;
        private VisualElement _labelField;
        private VisualElement _inputField;

        public RepaintedInputField()
        {
            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            this.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            this.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }

        #region Initialization
        public void Initialize(SerializedProperty serializedProperty)
        {
            _inputField = _field.Q("unity-text-input").AddClass("Input");
            _labelField = _field.Q(classes: "unity-label").AddClass("Label");

            _field.BindProperty(serializedProperty);

            this.AddElements(_field);
        }

        private void OnMouseEnter(MouseEnterEvent e)
        {
            _inputField.EnableClass("Input_Enter");
        }

        private void OnMouseLeave(MouseLeaveEvent e)
        {
            _inputField.DisableClass("Input_Enter");
        }
        #endregion

        #region Style
        public RepaintedInputField<T> SetAsVerticalBox(float maxWidth = -1, bool isPercent = false)
        {
            this.SetPadding(5).SetPaddingTop(2.5f).SetBackgroundColor("#303030")
                .SetBorderRadius(5).SetHeight(47.5f).SetMargin(0).SetMarginBottom(5);

            if (maxWidth == -1) this.SetWidth(StyleKeyword.Auto);
            else this.SetWidth(maxWidth, isPercent);

            _labelField.SetTextAlign(TextAnchor.UpperLeft);

            _field.SetFlexDirection(FlexDirection.Column);

            return this;
        }
        public RepaintedInputField<T> SetAsHorizontalBox(float maxWidth = -1, bool isPercent = false)
        {
            this.SetPadding(5).SetBackgroundColor("#303030")
                .SetBorderRadius(5).SetHeight(30).SetMargin(0).SetMarginBottom(5);

            _labelField.SetTextAlign(TextAnchor.UpperLeft);


            if (maxWidth == -1) this.SetWidth(StyleKeyword.Auto);
            else this.SetWidth(maxWidth, isPercent);

            return this;
        }
        public RepaintedInputField<T> SetLabelWidth(float width)
        {
            _labelField.SetMaxWidth(0);
            _inputField.SetMarginLeft(-120 + width);

            return this;
        }
        public RepaintedInputField<T> HideLabel()
        {
            _labelField.SetMaxWidth(0);
            _inputField.SetMarginLeft(-120);

            return this;
        }
        public RepaintedInputField<T> SetFontDefinition(FontAsset font)
        {
            _labelField.SetFontDefinition(font);

            return this;
        }
        #endregion
    }
}
#endif