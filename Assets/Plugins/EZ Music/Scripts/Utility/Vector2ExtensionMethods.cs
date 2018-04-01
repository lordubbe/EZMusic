using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2ExtensionMethods
{
    public static Vector2 WithY(this Vector2 v, float newY)
    {
        v.y = newY;
        return v;
    }

    public static Vector2 WithX(this Vector2 v, float newX)
    {
        v.x = newX;
        return v;
    }
}
