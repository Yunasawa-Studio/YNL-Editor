#if UNITY_EDITOR
using System;
using UnityEngine.UIElements;

namespace YNL.Editors.Visuals
{
    public class FlexibleInteractButton : Button
    {
        public Action OnPointerDown;
        public Action OnPointerUp;
        public Action OnPointerEnter;
        public Action OnPointerExit;
        public Action OnPointerMove;

        public FlexibleInteractButton() : base()
        {
            RegisterCallback<PointerDownEvent>(evt => OnEventPointerDown(evt, OnPointerDown), TrickleDown.TrickleDown);
            RegisterCallback<PointerUpEvent>(evt => OnEventPointerUp(evt, OnPointerUp));
            RegisterCallback<PointerEnterEvent>(evt => OnEventPointerEnter(evt, OnPointerEnter));
            RegisterCallback<PointerLeaveEvent>(evt => OnEventPointerLeave(evt, OnPointerExit));
            RegisterCallback<PointerMoveEvent>(evt => OnEventPointerMove(evt, OnPointerMove));
        }

        public static void OnEventPointerEnter(PointerEnterEvent evt, Action action)
        {
            var element = evt.currentTarget as FlexibleInteractButton;
            action?.Invoke();
            element.PointerEnter();
            evt.StopPropagation();
        }
        public static void OnEventPointerLeave(PointerLeaveEvent evt, Action action)
        {
            var element = evt.currentTarget as FlexibleInteractButton;
            action?.Invoke();
            element.PointerExit();
            evt.StopPropagation();
        }
        public static void OnEventPointerDown(PointerDownEvent evt, Action action)
        {
            var element = evt.currentTarget as FlexibleInteractButton;
            action?.Invoke();
            element.PointerDown();
            evt.StopPropagation();
        }
        public static void OnEventPointerUp(PointerUpEvent evt, Action action)
        {
            var element = evt.currentTarget as FlexibleInteractButton;
            action?.Invoke();
            element.PointerUp();
            evt.StopPropagation();
        }
        public static void OnEventPointerMove(PointerMoveEvent evt, Action action)
        {
            var element = evt.currentTarget as FlexibleInteractButton;
            action?.Invoke();
            element.PointerMove();
            evt.StopPropagation();
        }

        public virtual void PointerDown() { }
        public virtual void PointerUp() { }
        public virtual void PointerEnter() { }
        public virtual void PointerExit() { }
        public virtual void PointerMove() { }
    }
}
#endif