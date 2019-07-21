using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileSO tile;
    private Vector2Int index;
    [SerializeField]
    //private GameObject item;
    private List<Tile> neighbours;
    private int heuristic;
    public int G;
    public int F;

    public Tile previous;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int CurrentHeuristic {  get { return heuristic; } }

    public List<Tile> Neighbours { get { return neighbours; } }

    public Vector2Int Index { get { return index; } }

    public void AssignTileData(TileSO tileSO)
    {
        this.tile = tileSO;
        neighbours = new List<Tile>();
        GetComponent<SpriteRenderer>().sprite = tileSO.tileImage;
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
    public void SetHeuristicsOnParent(Tile parent, int deltaH)
    {
        this.heuristic = parent.heuristic + deltaH;
    }
}
