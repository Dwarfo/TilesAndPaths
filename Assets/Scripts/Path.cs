using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public List<Vector2Int> fullPath = new List<Vector2Int>();

    public void AddPath(Vector2Int v)
    {
        fullPath.Add(v);
    }

}

public class PathPart
{
    Tile tile;

    public PathPart(Tile tile)
    {
        this.tile = tile;
    }

}