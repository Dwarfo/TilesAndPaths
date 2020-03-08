using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileSO tile;
    public bool invalid = false;

    private Vector2Int index;
    [SerializeField]
    private List<Tile> neighbours;
    private int heuristic = 0;
    private int g = 0;
    private int f = 0;
    private Tile parent;
    [SerializeField]
    public IPickable item;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int H { get { return heuristic; } }
    public int G { get { return g; } }
    public int F { get { return f; } }
    public Tile Parent { get { return parent; } }

    public List<Tile> Neighbours { get { return neighbours; } }

    public Vector2Int Index { get { return index; } }

    public void AssignTileData(TileSO tileSO)
    {
        this.tile = tileSO;
        neighbours = new List<Tile>();
        GetComponent<SpriteRenderer>().sprite = tileSO.tileImage;
        GetComponent<SpriteRenderer>().sortingLayerName = "Terrain";
    }

    public void SetIndex(int x, int y)
    {
        this.index = new Vector2Int(x, y);
        gameObject.transform.position = new Vector3Int(this.Index.x, this.Index.y, 0);
    }
    public void SetIndex(Vector2Int v)
    {
        this.index = v;
    }

    public void AddNeighbour(Tile neighbour)
    {
        this.neighbours.Add(neighbour);
    }

    public void CountHeuristics(Tile start)
    {
        heuristic = Mathf.Abs(Index.x - start.Index.x) * 10 + Mathf.Abs(Index.y - start.Index.y) * 10;
    }

    public void SetHeuristics(int h)
    {
        this.heuristic = h;
    }
    public void SetParent(Tile parent)
    {
        this.g = parent.g + tile.terrainDifficulty;
        this.f = g + heuristic;
        this.parent = parent;
    }
    public void Clear()
    {
        this.g = 0;
        this.parent = null;
    }

    public void SetPickable(IPickable newItem) 
    {
        item = newItem;
    }

    public IPickable GetPickable() 
    {
        if (item == null)
            return null;
        return item;
    }
}
