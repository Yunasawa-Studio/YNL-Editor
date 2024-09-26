#if UNITY_EDITOR && YNL_UTILITIES
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Extensions.Methods;
using YNL.Extensions.Addons;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class StyledComponentIcon : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Elements/Styled/Components/StyledComponentIcon";

        private Image _icon;

        public StyledComponentIcon()
        {
            this.AddStyle(_styleSheet).AddClass("Icon");
        }

        public StyledComponentIcon SetIcon(Texture2D image)
        {
            this.SetBackgroundImage(image);
            return this;
        }
        public StyledComponentIcon SetIcon(string path, MAddressType type)
        {
            if (type == MAddressType.Resources) return SetIcon(path.LoadResource<Texture2D>());
            else if (type == MAddressType.Assets) return SetIcon(path.LoadAsset<Texture2D>());
            return this;
        }

        public StyledComponentIcon SetGlobalColor(Color color)
        {
            this.SetBackgroundImageTintColor(color);
            return this;
        }
    }
}
#endif