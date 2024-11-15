#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;
#endif
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Extensions.Addons;

public abstract class StyledComponent : MonoBehaviour
{
    [HideInInspector] public bool DrawDefaultInspector = true;

    [HideInInspector] public Color ComponentColor = Color.white;
    [HideInInspector] public string ComponentTitle = "";
    [HideInInspector] public (string Path, MAddressType LoadingType, Color Color) ComponentIcon = new("", MAddressType.Assets, Color.clear);
    [HideInInspector] public string ComponentDocumentation = "";

    public abstract void Initialize();
}

#if UNITY_EDITOR
[CustomEditor(typeof(StyledComponent), true)]
public class StyledComponentDrawer : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement().SetAsFlexInsppector();

        StyledComponent component = target as StyledComponent;
        component.Initialize();

        StyledComponentHeader header = new StyledComponentHeader();
        header.SetGlobalColor(component.ComponentColor);
        if (component.ComponentIcon.Path != "") header.AddIcon(component.ComponentIcon.Path, component.ComponentIcon.LoadingType);
        if (component.ComponentIcon.Color != Color.clear) header.SetIconColor(component.ComponentIcon.Color);
        if (component.ComponentTitle != "") header.AddTitle(component.ComponentTitle);
        if (component.ComponentDocumentation != "") header.AddDocumentation(component.ComponentDocumentation);
        header.AddBottomSpace(10);

        root.AddElements(header);

        if (component.DrawDefaultInspector)
        {
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            PropertyField scriptField = root.Q("PropertyField:m_Script") as PropertyField;
            root.Remove(scriptField);
        }

        return root;
    }
}
#endif