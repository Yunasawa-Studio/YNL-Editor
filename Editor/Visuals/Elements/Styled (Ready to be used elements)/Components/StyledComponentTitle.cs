#if UNITY_EDITOR && YNL_UTILITIES
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Utilities.Extensions;

namespace YNL.Editors.Visuals
{
    public class StyledComponentTitle : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Elements/Styled/Components/StyledComponentTitle";

        private Color _globalColor = "#AFAFAF".ToColor();

        private Label _title;
        private Image _container;
        private Button _documentation;

        public StyledComponentTitle()
        {
            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            _title = new Label().AddClass("Title");
            _container = new Image().AddClass("Container");

            this.AddElements(_title, _container);

            this.RegisterCallback<MouseEnterEvent>((evt) =>
            {
                _title.EnableClass("Title_Enter");
            });
            this.RegisterCallback<MouseLeaveEvent>((evt) =>
            {
                _title.DisableClass("Title_Enter");
            });
        }

        public StyledComponentTitle SetGlobalColor(Color color)
        {
            _globalColor = color;
            _title.SetColor(color);
            return this;
        }

        public StyledComponentTitle SetTitle(string title)
        {
            _title.SetText(title);
            return this;
        }

        public StyledComponentTitle AddDocumentation(string href)
        {
            _documentation = new Button().AddClass("Documentation");
            _container.AddElements(_documentation);

            _documentation.SetTooltip("Documentation");
            _documentation.SetHyperlink(href);
            _documentation.RegisterCallback<MouseEnterEvent>((evt) => _documentation.SetBackgroundImageTintColor(_globalColor));
            _documentation.RegisterCallback<MouseLeaveEvent>((evt) => _documentation.SetBackgroundImageTintColor("#AFAFAF".ToColor()));


            return this;
        }
    }
}
#endif