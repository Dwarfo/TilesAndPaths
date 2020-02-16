using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2Int ToInt2(this Vector2 v)
    {
        return new Vector2Int((int)v.x, (int)v.y);
    }
}
