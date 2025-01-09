#if YNL_UTILITIES
using UnityEngine;
using YNL.Utilities.Extensions;

namespace YNL.Editors.Extensions
{
    public static class ESheet
    {
        public const string FlexInspector = "Style Sheets/Elements/Flexible/FlexibleInspector";

        public const string Font = "Style Sheets/General/USSFont";
        public const string Texture = "Style Sheets/General/USSTexture";
    }

    public static class ETexture
    {
        public static Texture2D Unity(string name) => $"Textures/Unity/{name}".LoadResource<Texture2D>();
    }
}
#endif