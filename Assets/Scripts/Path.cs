using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public List<Tile> fullPath = new List<Tile>();
    public int currentPos;

    public void AddPath(Tile t)
    {
        fullPath.Add(t);
    }

    public void ActualPath()
    {
        fullPath.Reverse();
        currentPos = 0;
    }

    public bool NextPosition()
    {
        if (currentPos >= fullPath.Count - 1)
            return true;

        currentPos++;
        return false;

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