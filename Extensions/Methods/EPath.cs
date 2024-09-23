using UnityEngine;
using YNL.Extensions.Methods;

namespace YNL.Editors.Extensions
{
    public static class ESheet
    {
        public const string FlexInspector = "Style Sheets/Elements/Flex/FlexInspector";

        public const string Font = "Style Sheets/General/USSFont";
        public const string Texture = "Style Sheets/General/USSTexture";
    }

    public static class ETexture
    {
        public static Texture2D Unity(string name) => $"Textures/Unity/{name}".LoadResource<Texture2D>();
    }
}
