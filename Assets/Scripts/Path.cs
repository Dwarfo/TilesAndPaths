using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public List<Tile> fullPath = new List<Tile>();
    public int currentPos = 0;


    public Tile Destination { get {return fullPath[fullPath.Count - 1];}}
    public Tile CurrentTile { get {return fullPath[currentPath];}}

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
        {
            return true;
        }

        currentPos++;
        return false;
    }

    public void ClearPath()
    {
        fullPath.Clear();
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