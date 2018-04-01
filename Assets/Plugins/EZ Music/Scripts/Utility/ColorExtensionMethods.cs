using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensionMethods
{
    public static Color WithAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

	public static float Distance(this Color color, Color otherColor)
	{
		float valueA = color.a + color.r + color.g + color.b;
		float valueB = otherColor.a + otherColor.r + otherColor.g + otherColor.b;
		float dist = Mathf.Abs (valueA - valueB);
		return dist;
	}

}
