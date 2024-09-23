#if UNITY_EDITOR
using UnityEngine.UIElements;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class FlexibleLine : Image
    {
        public enum Line { Vertical, Horizontal }

        private const string USS_StyleSheet = "Style Sheets/Elements/Flexible/FlexibleLine";

        public FlexibleLine(Line mode) : base()
        {
            this.AddStyle(USS_StyleSheet);
            if (mode == Line.Horizontal) this.SetName("Horizontal");
            if (mode == Line.Vertical) this.SetName("Vertical");
        }
    }
}
#endif