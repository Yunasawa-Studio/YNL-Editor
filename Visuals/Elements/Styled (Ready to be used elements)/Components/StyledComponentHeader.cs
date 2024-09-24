#if UNITY_EDITOR && YNL_UTILITIES
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Extensions.Methods;
using YNL.Extensions.Addons;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class StyledComponentHeader : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Elements/Styled/Components/StyledComponentHeader";

        private Color _globalColor = "#AFAFAF".ToColor();

        private StyledComponentTitle _flexTitle;
        private StyledComponentIcon _flexIcon;

        public StyledComponentHeader()
        {
            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            _flexTitle = new StyledComponentTitle();
            _flexIcon = new StyledComponentIcon();
        }

        #region Flex Component Header
        public StyledComponentHeader SetGlobalColor(string color) => SetGlobalColor(color.ToColor());
        public StyledComponentHeader SetGlobalColor(Color color)
        {
            _globalColor = color;

            _flexTitle.SetGlobalColor(_globalColor);
            _flexIcon.SetGlobalColor(_globalColor);

            return this;
        }

        public StyledComponentHeader AddBottomSpace(float space = 7.5f)
        {
            this.SetMarginBottom(space);
            return this;
        }
        #endregion

        #region Flex Component Icon
        public StyledComponentHeader AddIcon(string imagePath, MAddressType type)
        {
            this.AddElements(_flexIcon.SetIcon(imagePath, type)).AddHSpace(3);
            return this;
        }
        public StyledComponentHeader AddIcon(Texture2D image)
        {
            this.AddElements(_flexIcon.SetIcon(image)).AddHSpace(3);
            return this;
        }
        public StyledComponentHeader SetIconColor(Color color)
        {
            _flexIcon.SetGlobalColor(color);
            return this;
        }
        #endregion

        #region Flex Component Title
        public StyledComponentHeader AddTitle(string title)
        {
            this.AddElements(_flexTitle.SetTitle(title));
            return this;
        }
        public StyledComponentHeader AddDocumentation(string href)
        {
            _flexTitle.AddDocumentation(href);
            return this;
        }
        #endregion
    }
}
#endif