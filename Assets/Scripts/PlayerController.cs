using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pixelsOffset;
    public LineRenderer lr;
    public float speed;

    private Path path;
    [SerializeField]
    private bool move;
    private Movement movement;
    private IEnumerator makeMove;
    private Tile nextTile;

    void Start()
    {
        if (lr == null)
            lr = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            path = TileField.Instance.GetPath(IndexOfPosition(transform.position), IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            if (nextTile != null)
                move = true;
            nextTile = path.fullPath[0];
            DrawLine(path);
        }

        if (Input.GetMouseButtonDown(1))
        {
            path = null;
            nextTile = null;
            move = false;
            ClearLine();
        }

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

        Move(nextTile);
    }

    private void DrawLine(Path path)
    {
        if (path == null)
            return;

        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
        lr.positionCount = path.fullPath.Count;
        lr.startColor = Color.cyan;
        lr.endColor = Color.cyan;
        Tile[] vec = path.fullPath.ToArray();
        List<Vector3> v2 = new List<Vector3>();
        for(int i = 0; i < vec.Length; i++)
            v2.Add((Vector2)vec[i].Index);

        lr.SetPositions(v2.ToArray());

        foreach (var pp in path.fullPath)
        {
            Debug.Log(pp);
        }

    }

    private void ClearLine()
    {
        lr.positionCount = 0;
    }

    private Vector2Int IndexOfPosition(Vector3 mp)
    {
        return new Vector2Int(Mathf.FloorToInt(mp.x + pixelsOffset), Mathf.FloorToInt(mp.y + pixelsOffset));
    }

    private void Move(Tile nextTile)
    {
        if (nextTile == null)
            return;

        MoveToTile(nextTile);

        
    }

    private void MoveToTile(Tile tile)
    {
        Debug.Log("Pos: " + transform.position);
        Debug.Log("Index: " + tile.Index);
        transform.position = Vector2.MoveTowards(transform.position, tile.Index, speed);
        if (Vector2.Distance(transform.position, nextTile.Index) == 0)
        {
            if (move)
            {
                if (path.NextPosition())
                    move = false;
                nextTile = path.fullPath[path.currentPos];
            }
        }
    }

}

public delegate bool Movement(Tile start, Tile end);