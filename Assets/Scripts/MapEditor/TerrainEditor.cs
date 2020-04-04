using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEditor : Singleton_MB<TerrainEditor>
{
    private Vector2Int currentMousePositionIndex;
    private Vector2Int lastDrawnIndex;
    private TileTypes[,] tileTerrain;
    private int minX;
    private int minY;
    private int maxX;
    private int maxY;
    private Rect mousePosRectangle;
    private float rectangleOffset;
    [SerializeField]
    private TileTypes currentTileType;
    private Dictionary<Vector2Int, Tile> indexToTileDict;
    public List<Tile> duplicatesToDestroy;

    void Start()
    {
        lastDrawnIndex      = new Vector2Int(0, 0);
        rectangleOffset     = TileField.Instance.tileSize / 2;
        indexToTileDict     = new Dictionary<Vector2Int, Tile>();
        duplicatesToDestroy = new List<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMousePositionIndex = TileField.Instance.IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (lastDrawnIndex != currentMousePositionIndex) 
        {
            lastDrawnIndex = currentMousePositionIndex;
            UIManager.Instance.lineDrawer.DrawRectangle(lastDrawnIndex, rectangleOffset, rectangleOffset);
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            PutTileInField(currentMousePositionIndex, (int)currentTileType);
        }
    }

    private Rect DrawTileBounds(Vector2Int index) {
        Rect newRectangle   = new Rect();
        newRectangle.center = index.ToVec2();
        newRectangle.width  = TileField.Instance.xSize;
        newRectangle.height = TileField.Instance.ySize;

        return newRectangle;
    }

    public void PutTileInField(Vector2Int position, int tileType) 
    {
        Tile newTile = TileField.Instance.MakeTile(position.x, position.y, tileType);
        if (indexToTileDict.ContainsKey(position))
        {
            duplicatesToDestroy.Add(indexToTileDict[position]);
        }

        indexToTileDict[position] = newTile;
    }

    public MapData SerializeArray(Dictionary<Vector2Int, Tile> tilesMap) 
    {
        List<TileEntry> entryList = new List<TileEntry>();
        foreach (KeyValuePair<Vector2Int, Tile> tile in tilesMap) 
        {
            tile.Value.SetIndex(tile.Value.Index.x - minX, tile.Value.Index.y - minY);
            TileEntry entry = new TileEntry();
            entry.xPosition = tile.Value.Index.x;
            entry.yPosition = tile.Value.Index.y;
            entry.tileType = (int)tile.Value.tile.tileType;
            entryList.Add(entry);
        }

        MapData md = new MapData();
        md.tiles = entryList;

        return md;
    }

    public MapData GetEditedMapData() 
    {
        return SerializeArray(indexToTileDict);
    }

    private void DefineMinMax(Vector2Int newTileIndex)
    {
        if (minX > newTileIndex.x)
            minX = newTileIndex.x;
        if (maxX < newTileIndex.x)
            maxX = newTileIndex.x;
        if (minY > newTileIndex.y)
            minY = newTileIndex.y;
        if (maxY < newTileIndex.y)
            maxY = newTileIndex.y;
    }
}
