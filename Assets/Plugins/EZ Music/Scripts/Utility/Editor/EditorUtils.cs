using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EZMusic
{
    public static class EditorUtils
    {
        public static void DrawOutlineRect(Rect rect, Color fillColor, Color strokeColor, float strokewidth = 1f)
        {
            EditorGUI.DrawRect(rect, fillColor);
            for (int i = 0; i < strokewidth; i++)
            {
                rect.x -= 1;
                rect.y -= 1;
                rect.xMax += 2;
                rect.yMax += 2;
                Handles.DrawSolidRectangleWithOutline(rect, Color.white.WithAlpha(0f), strokeColor);
            }
        }
    }
}