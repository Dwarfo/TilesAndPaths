using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainEditor : MonoBehaviour
{
    public GameObject tilePrefab;

    private Vector2Int currentMousePositionIndex;
    private Vector2Int lastDrawnIndex;
    private int minX;
    private int minY;
    private int maxX;
    private int maxY;
    [SerializeField]
    private float pixelOffset;
    private float tileSize;
    [SerializeField]
    private TileTypes currentTileType;
    private Dictionary<Vector2Int, Tile> indexToTileDict;
    public List<Tile> duplicatesToDestroy;
    private Dictionary<int, SO_Tile> intToTileTypes;

    void Start()
    {
        lastDrawnIndex      = new Vector2Int(0, 0);
        //rectangleOffset     = TileField.Instance.tileSize / 2;
        indexToTileDict     = new Dictionary<Vector2Int, Tile>();
        duplicatesToDestroy = new List<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        currentMousePositionIndex = TileField.IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), pixelOffset);
        if (lastDrawnIndex != currentMousePositionIndex) 
        {
            lastDrawnIndex = currentMousePositionIndex;
            //redo by event
            UIManager.Instance.lineDrawer.DrawRectangle(lastDrawnIndex, pixelOffset, pixelOffset);
            if (Input.GetMouseButton(0))
            {
                PutTileInField(currentMousePositionIndex, (int)currentTileType);
            }

        }

        if (Input.GetMouseButtonDown(0)) 
        {
            PutTileInField(currentMousePositionIndex, (int)currentTileType);
        }

    }

    private Rect DrawTileBounds(Vector2Int index) {
        Rect newRectangle   = new Rect();
        newRectangle.center = index.ToVec2();
        newRectangle.width = 5;// TileField.Instance.xSize;
        newRectangle.height = 5;// TileField.Instance.ySize;

        return newRectangle;
    }

    public void PutTileInField(Vector2Int position, int tileType) 
    {
        if (tileType == 0) 
        {
            return;
        }
        Tile newTile = MakeTile(tilePrefab, intToTileTypes[tileType], position);//GameManager.Instance.fieldScript.MakeTile(position.x, position.y, tileType);//redo
        if (indexToTileDict.ContainsKey(position))
        {
            duplicatesToDestroy.Add(indexToTileDict[position]);
            indexToTileDict[position].gameObject.SetActive(false);
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

    public void SetSettings(SO_GameSettings gameSettings) 
    {
        this.pixelOffset = gameSettings.pixelOffset;
        this.tileSize = gameSettings.tileSize;
        intToTileTypes = new Dictionary<int, SO_Tile>();

        TileField.AddTilesToDict(intToTileTypes, gameSettings.allTileTypes);
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

    private Tile MakeTile(GameObject tilePrefab, SO_Tile tileData, Vector2Int position)
    {
        GameObject tile = Instantiate(tilePrefab);
        tile.transform.SetParent(transform);
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.AssignTileData(tileData, tileSize);
        tileScript.SetIndex(position);
        tile.name = "Tile" + tileScript.Index;
        return tileScript;
    }

    public void SetCurrentTileType(TileTypes tileType) 
    {
        currentTileType = tileType;
    }
}
