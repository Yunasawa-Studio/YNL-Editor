#if UNITY_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine.UIElements;
using YNL.Utilities.Extensions;
using YNL.Editors.Extensions;

namespace YNL.Editors.Visuals
{
    public class RepaintedIntField : RepaintedInputField<int>
    {
        public RepaintedIntField(SerializedProperty serializedProperty) : base()
        {
            _field = new IntegerField(serializedProperty.name.AddSpaces()).AddClass("Field", "unity-base-field__aligned");

            Initialize(serializedProperty);
        }
    }
}
#endif