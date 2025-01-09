#if UNITY_EDITOR && YNL_UTILITIES
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Utilities.Extensions;

namespace YNL.Editors.Visuals
{
    public class StyledWindowTagPanel : FlexibleInteractImage
    {
        private const string _uss_StyleSheet = "Style Sheets/Elements/Styled/Windows/StyledWindowTagPanel";

        private static readonly string _uss_panelBackground = "panel-background";
        private static readonly string _uss_titleBackground = "title-background";
        private static readonly string _uss_icon = "icon";
        private static readonly string _uss_iconHover = "icon__hover";
        private static readonly string _uss_title = "title";
        private static readonly string _uss_titleHover = "title__hover";
        private static readonly string _uss_subtitle = "subtitle";
        private static readonly string _uss_subtitleHover = "subtitle__hover";

        public Image TitleBackground = new Image();
        public Image Icon = new Image();
        public Label Title = new Label();
        public Label Subtitle = new Label();
        public ScrollView Scroll = new ScrollView();

        public StyledWindowTag[] Tags = new StyledWindowTag[0];

        private int _selectedTag = 0;
        private float _maxWidth = 0;

        public StyledWindowTagPanel(Texture2D icon, string title, string subtitle, float maxWidth, StyledWindowTag[] tags) : base()
        {
            this.AddStyle(_uss_StyleSheet, ESheet.Font);

            Icon.SetBackgroundImage(icon).AddClass(_uss_icon);
            Title.SetText(title).AddClass(_uss_title);
            Subtitle.SetText(subtitle).AddClass(_uss_subtitle);

            TitleBackground.AddClass(_uss_titleBackground).AddElements(Icon, Title, Subtitle);

            this.AddClass(_uss_panelBackground).AddElements(TitleBackground).AddSpace(0, 100);

            Tags = tags;

            Scroll.AddClass("Scroll");

            foreach (var tag in Tags)
            {
                Scroll.AddElements(tag).AddSpace(0, 45);
                tag.OnClick += () => SelectTag(tag, false);
            }

            this.AddElements(Scroll);

            SelectTag(Tags[_selectedTag], true);

            _maxWidth = maxWidth;
        }

        public override void PointerEnter()
        {
            this.SetWidth(_maxWidth);

            Icon.EnableClass(true, _uss_iconHover);
            Title.EnableClass(true, _uss_titleHover);
            Subtitle.EnableClass(true, _uss_subtitleHover);

            foreach (var tag in Tags) tag.OnExpand();
        }
        public override void PointerExit()
        {
            this.SetWidth(50);

            Icon.EnableClass(false, _uss_iconHover);
            Title.EnableClass(false, _uss_titleHover);
            Subtitle.EnableClass(false, _uss_subtitleHover);

            foreach (var tag in Tags) tag.OnCollape();
        }

        public void SelectTag(StyledWindowTag tag, bool forceSelect)
        {
            if (!forceSelect && Tags[_selectedTag] == tag) return;

            _selectedTag = Tags.IndexOf(tag);

            foreach (var item in Tags) item.OnDeselect();
            tag.OnSelect();
        }
        public void ForceSelectTag(int index)
        {
            if (_selectedTag == index) return;

            _selectedTag = index;
            StyledWindowTag tag = Tags[_selectedTag];
            SelectTag(tag, true);
        }
    }
}
#endif
