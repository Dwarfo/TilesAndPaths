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
            if (path != null)
            {
                if (path.Destination.Index == IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    nextTile = path.CurrentTile;
                    move = true;
                    return;
                }
                else
                {
                    path = TileField.Instance.GetPath(IndexOfPosition(transform.position),
                    IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                    DrawLine(path);
                }
            }
            else
            {
                path = TileField.Instance.GetPath(IndexOfPosition(transform.position),
                IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                DrawLine(path);
                move = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            move = false;
            ClearLine();
        }

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = !move;
            //stop moving but not clear a path
        }

        Move(path, move);
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

    private void Move(Path path, bool move)
    {
        if(path == null)
            return;
            

        if (MoveToTile(path.CurrentTile) && move)
        {
            if (path.CurrentTile == path.Destination)
            {
                this.path = null;
                move = false;
                return;
            }
            path.NextPosition();
        }/*
        else if (MoveToTile(path.CurrentTile) && !move)
        {
            if (path.CurrentTile != path.Start)
            {
                Debug.Log("Nakasikasukasena");
                this.path = null;
            }
        }*/

    }

    private bool MoveToTile(Tile tile)
    {
        transform.position = Vector2.MoveTowards(transform.position, tile.Index, speed);
        return (Vector2.Distance(transform.position, tile.Index) == 0);
    }

    public void ClearPath()
    {
        this.path = null;
    }
}

public delegate bool Movement(Tile end);