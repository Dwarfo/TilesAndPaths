using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pixelsOffset;
    public float speed;

    public Path path;
    [SerializeField]
    private bool move;
    [SerializeField]
    private bool paused;
    private Tile nextTile;
    [SerializeField]
    private LineDrawer lineDrawer;
    private PlayerMovementFSM stateMachine;

    void Start()
    {
        stateMachine = new PlayerMovementFSM();
        stateMachine.playerTransform = transform;
    }

    void Update()
    {
        stateMachine.Tick();
        if (Input.GetMouseButtonDown(0))
        {
            if (path != null)
            {
                if (path.Destination.Index == IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    stateMachine.currentPath = path;
                }
                else
                {
                    path = TileField.Instance.GetPath(IndexOfPosition(transform.position),
                        IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                    lineDrawer.DrawLine(path);
                    move = false;
                }
            }
            else
            {
                path = TileField.Instance.GetPath(IndexOfPosition(transform.position),
                    IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
                lineDrawer.DrawLine(path);
                move = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //lineDrawer.ClearLine();
        }

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private Vector2Int IndexOfPosition(Vector3 mp)
    {
        return new Vector2Int(Mathf.FloorToInt(mp.x + pixelsOffset), Mathf.FloorToInt(mp.y + pixelsOffset));
    }

    private void Move(Path path, bool move)
    {
        if(path == null)
            return;
        
        if(MoveToTile(path.CurrentTile))
        {
            if(path.CurrentTile == path.Start && !move)
                return;

            if(!move || path.CurrentTile == path.Destination)
            {
                ClearPath();
                lineDrawer.ClearLine();
                paused = false;
            }
            else if(paused)
            {
                
            }
            else
            {
                path.NextPosition();
                lineDrawer.DrawLine(path);
            }
        }

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