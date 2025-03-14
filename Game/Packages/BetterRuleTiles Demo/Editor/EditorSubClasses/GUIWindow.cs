#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VinTools.BetterRuleTiles;

namespace VinToolsEditor.BetterRuleTiles
{
    public class GUIWindow
    {
        #region Variables
        public enum WindowLayout
        {
            Free = 0,

            SpanTop = 10,
            SpanBottom = 11,
            SpanLeft = 12,
            SpanRight = 13,

            TopLeftCorner = 20,
            TopRightCorner = 21,
            BottomLeftCorner = 22,
            BottomRightCorner = 23,
        }

        public WindowLayout layout;
        public RectOffset padding;
        public Rect rectangle;
        public bool lockPosition;
        public bool visible = false;
        public bool hideOnUnfocus = false;
        public bool autoLayout = false;
        public GUIContent windowTitle;
        public GUIStyle windowStyle;
        public GUI.WindowFunction windowFunction;
        #endregion

        #region Properties
        public float width { get => rectangle.width; set => rectangle.width = value; }
        public float height { get => rectangle.height; set => rectangle.height = value; }
        public float x { get => rectangle.x; set => rectangle.x = value; }
        public float y { get => rectangle.y; set => rectangle.y = value; }

        public Vector2 position { get => new Vector2(x, y); set { x = value.x; y = value.y; } }
        public Vector2 size { get => new Vector2(width, height); set { width = value.x; height = value.y; } }
        #endregion

        #region Constructor
        public GUIWindow(Rect rect, GUI.WindowFunction windowFunction, GUIContent title = null, bool visible = true, bool autoLayout = false, bool hideOnUnfocus = false, GUIStyle style = null)
        {
            if (title == null) title = GUIContent.none;

            this.layout = WindowLayout.Free;
            this.padding = new RectOffset(0, 0, 0, 0);
            this.rectangle = rect;
            this.visible = visible;
            this.hideOnUnfocus = hideOnUnfocus;
            this.windowFunction = windowFunction;
            this.autoLayout = autoLayout;
            this.windowTitle = title;
            this.windowStyle = style;
        }
        public GUIWindow(WindowLayout placement, RectOffset padding, Vector2 size, GUI.WindowFunction windowFunction, GUIContent title = null, bool visible = true, bool autoLayout = false, bool hideOnUnfocus = false, GUIStyle style = null)
        {
            if (title == null) title = GUIContent.none;

            this.layout = placement;
            this.padding = padding;
            this.rectangle = new Rect(Vector2.zero, size);
            this.visible = visible;
            this.hideOnUnfocus = hideOnUnfocus;
            this.windowFunction = windowFunction;
            this.autoLayout = autoLayout;
            this.windowTitle = title;
            this.windowStyle = style;
        }
        #endregion

        #region Methods
        public void Show(int WindowID, Rect MainWindowPosition)
        {
            //if not visible ignore
            if (!visible) return;

            //if auto layout is enabled
            if (lockPosition) ApplyLayout(MainWindowPosition);

            //show window
            if (autoLayout && windowStyle != null) rectangle = GUILayout.Window(WindowID, rectangle, windowFunction, windowTitle, windowStyle);
            if (autoLayout && windowStyle == null) rectangle = GUILayout.Window(WindowID, rectangle, windowFunction, windowTitle);
            if (!autoLayout && windowStyle != null) rectangle = GUI.Window(WindowID, rectangle, windowFunction, windowTitle, windowStyle);
            if (!autoLayout && windowStyle == null) rectangle = GUI.Window(WindowID, rectangle, windowFunction, windowTitle);

            //return
            if (!hideOnUnfocus) return;

            //hide window if clicked outside
            if ((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag) && !rectangle.Contains(Event.current.mousePosition)) visible = false;
        }

        public void ApplyLayout(Rect MainWindowPosition)
        {
            switch (layout)
            {
                case WindowLayout.SpanTop:
                    width = MainWindowPosition.width;
                    x = padding.left;
                    y = padding.top;
                    break;
                case WindowLayout.SpanBottom:
                    width = MainWindowPosition.width;
                    x = padding.left;
                    y = MainWindowPosition.height - height - padding.bottom;
                    break;
                case WindowLayout.SpanLeft:
                    height = MainWindowPosition.height;
                    x = padding.left;
                    y = padding.top;
                    break;
                case WindowLayout.SpanRight:
                    height = MainWindowPosition.height;
                    x = MainWindowPosition.width - width - padding.right;
                    y = padding.top;
                    break;
                case WindowLayout.TopLeftCorner:
                    x = padding.left;
                    y = padding.top;
                    break;
                case WindowLayout.TopRightCorner:
                    x = MainWindowPosition.width - width - padding.right;
                    y = padding.top;
                    break;
                case WindowLayout.BottomLeftCorner:
                    x = padding.left;
                    y = MainWindowPosition.height - height - padding.bottom;
                    break;
                case WindowLayout.BottomRightCorner:
                    x = MainWindowPosition.width - width - padding.right;
                    y = MainWindowPosition.height - height - padding.bottom;
                    break;
            }
        }
        #endregion
    }
}
#endif