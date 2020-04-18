using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileField : MonoBehaviour
{
    
    public float tileSize;
    public GameObject tilePrefab;
    public float pixelsOffset; // Offset to make sure index is counted based on center of each tile

    [SerializeField]
    private SO_Tile[] tiles;
    private TileTypes[][] tipowoi;
    private Dictionary<int, SO_Tile> intToTileTypes;
    private Dictionary<Vector2Int, Tile> vectorsToTiles;

    void Start()
    {
        vectorsToTiles = new Dictionary<Vector2Int, Tile>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public SO_Tile GetTileTypeByInt(TileTypes type)
    {
        return intToTileTypes[(int)type];
    }

    public void Process()
    {
        intToTileTypes = new Dictionary<int, SO_Tile>();

        AddTilesToDict(intToTileTypes, tiles); //Adding tiletypes to dictionary for generating map
        DebugStructs(); // populating test tilemap tipowoi with int values of tiles
        //DrawTiles(tipowoi); // drawing and instantiating tiles by tipowoi structure
        //SetNeighbours(); // creating a graph structure by assigning neighbours
        //GraphLine();
    }

    public Path GetPath(Vector2Int start, Vector2Int toGo)
    {
        if (!vectorsToTiles.ContainsKey(toGo))
            return null;
        if (vectorsToTiles[toGo].tile.impassible)
            return null;

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

    public void DrawTilesFromMap(MapData mdata)
    {
        foreach (TileEntry entry in mdata.tiles)
        {
            var newTile = MakeTileFromEntry(entry);
            vectorsToTiles.Add(newTile.Index, newTile);
        }
    }

    private Tile MakeTileFromEntry(TileEntry entry) 
    {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.SetParent(transform);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(intToTileTypes[entry.tileType], tileSize);
        tileScript.SetIndex(new Vector2Int(entry.xPosition, entry.yPosition));
        tile.name = "Tile" + tileScript.Index;
        return tileScript;
    }


    public Tile GetTileByIndex(Vector2Int positionIndex) 
    {
        if (vectorsToTiles.ContainsKey(positionIndex))
        {
            return vectorsToTiles[positionIndex];
        }
        else
            return null;
    }

    public void ClearMap() 
    {
        foreach (KeyValuePair<Vector2Int, Tile> tile in vectorsToTiles)
        {
            Destroy(tile.Value.gameObject);
        }

        vectorsToTiles = new Dictionary<Vector2Int, Tile>();
    }


    public void SetNeighbours()
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

        //Diagonal Indexes
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

    private void AssignTileTypes()
    {
        foreach (var tileType in tiles)
        {
            intToTileTypes.Add((int)tileType.tileType, tileType);
        }
    }

    private void WriteDebugNeighbours()
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

    public static void AddTilesToDict(Dictionary<int, SO_Tile> dict, SO_Tile[] tiles)
    {
        foreach (var tile in tiles)
            dict.Add((int)tile.tileType, tile);
    }

    private void CalculateHeuristics(Tile start)
    {
        foreach (var tile in vectorsToTiles.Keys)
        {
            vectorsToTiles[tile].CountHeuristics(start);
        }
    }

    private Path CreatePath(Tile start, Tile end)
    {
        Path path = new Path();
        List<Tile> toCheck = new List<Tile>();
        HashSet<Tile> ignore = new HashSet<Tile>();

        foreach (var tile in vectorsToTiles.Values)
            tile.Clear();

        Tile currentTile = start;
        toCheck.Add(currentTile);

        while (toCheck.Count != 0)
        {
            currentTile = GetTileWithLowestF(toCheck);
            if (currentTile == end)
                break;

            toCheck.Remove(currentTile);
            ignore.Add(currentTile);

            foreach (var neighbour in CheckNeighboursPath(currentTile))
            {
                if (!ignore.Contains(neighbour))
                {
                    if (!toCheck.Contains(neighbour))
                    {
                        toCheck.Add(neighbour);
                        neighbour.SetParent(currentTile);
                    }
                    else if(currentTile.G + neighbour.tile.terrainDifficulty < neighbour.G)
                    {
                        neighbour.SetParent(currentTile);
                    }
                }
            }
        }


        while (currentTile != start)
        {
            path.AddPath(currentTile);
            currentTile = currentTile.Parent;
        }
        path.AddPath(currentTile);
        path.ActualPath();
        return path;
    }

    private List<Tile> CheckNeighboursPath(Tile tile)
    {
        List<Tile> validNeighbours = new List<Tile>();
        foreach (Tile neighbour in tile.Neighbours)
        {
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

    public static Vector2Int IndexOfPosition(Vector3 mp, float pixelsOffset)
    {
        return new Vector2Int(Mathf.FloorToInt(mp.x + pixelsOffset), Mathf.FloorToInt(mp.y + pixelsOffset));
    }

    public void SetSettings(SO_GameSettings settings) 
    {
        this.tileSize       = settings.tileSize;
        this.pixelsOffset   = settings.pixelOffset;
        this.tilePrefab     = settings.tilePrefab;

        if (settings.allTileTypes.Length != 0)
        {
            this.tiles = settings.allTileTypes;
        }
    }
}
