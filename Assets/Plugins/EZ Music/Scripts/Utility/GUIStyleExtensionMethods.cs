using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIStyleExtensionMethods
{
    public static GUIStyle WithWordWrap(this GUIStyle style)
    {
        GUIStyle newStyle = new GUIStyle(style);
        newStyle.wordWrap = true;
        return newStyle;
    }

    public static GUIStyle WithAlignment(this GUIStyle style, TextAnchor alignment)
    {
        GUIStyle newStyle = new GUIStyle(style);
        newStyle.alignment = alignment;
        return newStyle;
    }

    public static GUIStyle Centered(this GUIStyle style)
    {
        return style.WithAlignment(TextAnchor.MiddleCenter);
    }

    public static GUIStyle WithFontStyle(this GUIStyle style, FontStyle fontStyle)
    {
        GUIStyle newStyle = new GUIStyle(style);
        newStyle.fontStyle = fontStyle;
        return newStyle;
    }

    public static GUIStyle WithRichText(this GUIStyle style)
    {
        GUIStyle newStyle = new GUIStyle(style);
        newStyle.richText = true;
        return newStyle;
    }
}
