using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public List<Tile> fullPath;
    public int currentPos;

    public Tile Destination { get { return fullPath[fullPath.Count - 1]; } }
    public Tile CurrentTile { get { return fullPath[currentPos]; } }
    public Tile Start { get { return fullPath[0]; } }

    public Path() 
    {
        fullPath = new List<Tile>();
        currentPos = 0;
    }
    
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
        if (currentPos == fullPath.Count - 1)
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

    public Tile GetCurrentTile() 
    {
        return fullPath[currentPos];
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