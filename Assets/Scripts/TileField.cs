using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileField : Singleton_MB<TileField>
{
    [SerializeField]
    TileSO[] tiles;
    TileTypes[,] tilesPlanar;
    public int xSize;
    public int ySize;
    public GameObject tilePrefab;

    private TileTypes[][] tipowoi;
    private Dictionary<int, TileSO> intToTileTypes;
    private Dictionary<Vector2Int, Tile> vectorsToTiles;

    void Start()
    {
        tilesPlanar = new TileTypes[xSize, ySize];
        intToTileTypes = new Dictionary<int, TileSO>();

        AddTilesToDict();
        DebugStructs();
        DrawTiles(tipowoi);
        SetNeighbours();
        //GraphLine();
        string neighboursStr = "";
        foreach (var n in vectorsToTiles[new Vector2Int(0,1)].Neighbours)
        {
            neighboursStr += n.Index + " ";
        }
        Debug.Log("Tile " + vectorsToTiles[new Vector2Int(0, 1)].Index + " has neighbours: " + neighboursStr + " length " + vectorsToTiles[new Vector2Int(0, 1)].Neighbours.Count);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public Path GetPath(Vector2Int start, Vector2Int toGo)
    {
        CalculateHeuristics(vectorsToTiles[toGo]);
        Debug.Log("Outside: " + vectorsToTiles[toGo].gameObject.name + "  Num of neighbours: " + vectorsToTiles[toGo].Neighbours.Count);
        return CreatePath(vectorsToTiles[start], vectorsToTiles[toGo]);
    }

    public void DebugStructs()
    {
        tipowoi = new TileTypes[5][];
        for (int i = 0; i < tipowoi.Length; i++)
        {
            tipowoi[i] = new TileTypes[5];
        }

        for (int i = 0; i < tipowoi.Length; i++)
        {
            for (int j = 0; j < tipowoi[i].Length; j++)
            {
                tipowoi[i][j] = (TileTypes)1;
            }
        }

        for (int i = 0; i < tipowoi[1].Length; i++)
        {
            tipowoi[i][1] = (TileTypes)2;
            tipowoi[i][4] = (TileTypes)2;
        }
        tipowoi[0][3] = (TileTypes)2;
        tipowoi[3][3] = (TileTypes)2;
        tipowoi[3][2] = (TileTypes)2;
        tipowoi[4][3] = (TileTypes)2;
        tipowoi[1][1] = (TileTypes)2;
        tipowoi[2][1] = (TileTypes)2;
        tipowoi[1][0] = (TileTypes)2;
    }

    public void DrawTiles(TileTypes[][] tiles)
    {
        List<Tile> allTiles = new List<Tile>();
        vectorsToTiles = new Dictionary<Vector2Int, Tile>();

        for (int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < tiles[i].Length; j++)
            {
                if ((int)tiles[i][j] != -1)
                {
                    var newTile = MakeTile(i, j, (int)tiles[i][j]);
                    allTiles.Add(newTile);
                    vectorsToTiles.Add(newTile.Index, newTile);
                }
            }
        }
    }

    private void SetNeighbours()
    {
        foreach (Tile t in vectorsToTiles.Values)
        {
            t.Neighbours.Clear();
        }
        HashSet<Tile> tilesToCheck = new HashSet<Tile>();
        HashSet<Tile> newToCheck = new HashSet<Tile>();
        HashSet<Vector2Int> ignore = new HashSet<Vector2Int>();
        tilesToCheck.Add(vectorsToTiles[new Vector2Int(0,0)]);

        while (tilesToCheck.Count != 0)
        {
            foreach (var tile in tilesToCheck)
            {
                AddNeighbours(newToCheck, ignore, tile, tile.Index);
            }

            tilesToCheck = new HashSet<Tile>(newToCheck);
            newToCheck = new HashSet<Tile>();
        }
    }
    private void AddNeighbours(HashSet<Tile> newToCheck, HashSet<Vector2Int> ignore, Tile centerTile, Vector2Int ind)
    {
        Vector2Int[] neighbourIndexes = new Vector2Int[] { ind + new Vector2Int(0, 1) , ind + new Vector2Int(0, -1) ,
                ind + new Vector2Int(-1, 0), ind + new Vector2Int(1, 0)};
        Tile neighbour;


        foreach (var index in neighbourIndexes)
        {
            if (vectorsToTiles.ContainsKey(index))
            {
                if (!ignore.Contains(index))
                {
                    neighbour = vectorsToTiles[index];
                    centerTile.AddNeighbour(neighbour);
                    neighbour.AddNeighbour(centerTile);
                    newToCheck.Add(neighbour);
                }
            }
        }

        ignore.Add(centerTile.Index);
    }
    private void ChooseTileType()
    {

    }

    private void AssignTileTypes()
    {
        foreach (var tileType in tiles)
        {
            intToTileTypes.Add((int)tileType.tileType, tileType);
        }
    }

    private Tile MakeTile(int i, int j, int tileInd)
    {
        GameObject tile = Instantiate(tilePrefab);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(intToTileTypes[tileInd]);
        tileScript.SetIndex(i, j);
        tile.name = "Tile" + tileScript.Index;
        return tileScript;
    }

    private void GraphLine()
    {
        foreach (var tile in vectorsToTiles.Keys)
        {
            //Debug.Log(vectorsToTiles[tile].gameObject.name + " count " + vectorsToTiles[tile].Neighbours.Count);
            string neighboursStr = "";
            foreach (var n in vectorsToTiles[tile].Neighbours)
            {
                neighboursStr += n.Index + " ";
            }
            Debug.Log("Tile " + vectorsToTiles[tile].Index + " has neighbours: " + neighboursStr);
        }
    }

    private void AddTilesToDict()
    {
        foreach (var tile in tiles)
            intToTileTypes.Add((int)tile.tileType, tile);
    }

    private void CalculateHeuristics(Tile start)
    {
        foreach (var tile in vectorsToTiles.Keys)
        {
            vectorsToTiles[tile].CountHeuristics(start);
            //Debug.Log(tile.gameObject.name + " " + tile.CurrentHeuristic);
        }
    }

    private Path CreatePath(Tile start, Tile end)
    {
        Path path = new Path();
        int iterations = 0;
        List<Tile> toCheck = new List<Tile>();
        HashSet<Tile> ignore = new HashSet<Tile>();

        Tile currentTile = start;
        start.G = 0;
        start.F = start.G + start.CurrentHeuristic;

        toCheck.Add(currentTile);
        Debug.Log("Inside: " + start.gameObject.name + "  Num of neighbours: " + start.Neighbours.Count);

        
        while (toCheck.Count != 0)
        {
            currentTile = GetTileWithLowestF(toCheck);
            if (currentTile == end)
                break;

            toCheck.Remove(currentTile);
            ignore.Add(currentTile);
            Debug.Log("Num of neighbours: " + currentTile.Neighbours.Count);

            foreach (var neighbour in CheckNeighboursPath(currentTile))
            {
                if (!ignore.Contains(neighbour))
                {
                    if (!toCheck.Contains(neighbour))
                    {
                        toCheck.Add(neighbour);
                        neighbour.G = currentTile.G + neighbour.tile.terrainDifficulty;
                        neighbour.F = neighbour.G + neighbour.CurrentHeuristic;
                        neighbour.previous = currentTile;
                    }
                    else if(currentTile.G + neighbour.tile.terrainDifficulty < neighbour.G)
                    {
                        neighbour.G = currentTile.G + neighbour.tile.terrainDifficulty;
                        neighbour.F = neighbour.G + neighbour.CurrentHeuristic;
                        neighbour.previous = currentTile;
                    }
                }
            }
        }


        while (currentTile != start)
        {
            path.AddPath(currentTile.Index);
            currentTile = currentTile.previous;
        }
        
        Debug.Log("Length: " + path.fullPath.Count);
        return path;
    }

    private List<Tile> CheckNeighboursPath(Tile tile)
    {
        List<Tile> validNeighbours = new List<Tile>();
        foreach (Tile neighbour in tile.Neighbours)
        {
            Debug.Log(neighbour.Index);
            if (!neighbour.tile.impassible)
            {
                validNeighbours.Add(neighbour);
            }
        }
        //Debug.Log("NeighboursLength: " + validNeighbours.Count);
        return validNeighbours;
    }

    private Tile GetTileWithLowestF(List<Tile> tiles)
    {
        tiles.Sort(delegate (Tile t1, Tile t2)
        {
            if (t1.F > t2.F) return 1;
            else if (t1.F < t2.F) return -1;
            else return 0;
        }
        );

        return tiles[0];
    }
}
