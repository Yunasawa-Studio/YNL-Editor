#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine.UIElements;
using YNL.Extensions.Methods;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class RepaintedFloatField : RepaintedInputField<float>
    {
        public RepaintedFloatField(SerializedProperty serializedProperty) : base()
        {
            _field = new FloatField(serializedProperty.name.AddSpaces()).AddClass("Field", "unity-base-field__aligned");

            Initialize(serializedProperty);
        }
    }
}
#endif