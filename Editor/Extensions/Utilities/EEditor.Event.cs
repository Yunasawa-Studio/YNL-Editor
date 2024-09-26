using System;
using UnityEngine;

namespace YNL.Editors.Extensions
{
    public static partial class EEditor
    {
        public class Event
        {
            public static Action<GameObject> OnHierarchyObjectCreated;
            public static Action<(string, GameObject)[]> OnHierarchyObjectDestroyed;
            public static Action<(GameObject, string)[]> OnHierarchyObjectRenamed; // (Renamed object, Previous name)
            public static Action<(GameObject, string, string)[]> OnHierarchyObjectMoved; // (Moved object, previous path, current path)
        }
    }
}