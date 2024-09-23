#if UNITY_EDITOR && YNL_UTILITIES
using UnityEngine.UIElements;
using System;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class FlexibleEllipsisButton : Button
    {
        private const string _styleSheet = "Style Sheets/Elements/Flexible/FlexibleEllipsisButton";

        public FlexibleEllipsisButton() : base(null) => Initialize();
        public FlexibleEllipsisButton(Action action) : base(action) => Initialize();

        private void Initialize()
        {
            this.AddStyle(_styleSheet).AddClass("Main");
        }
    }
}
#endif