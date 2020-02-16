using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pixelsOffset;
    public float speed;

    public Path path;
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

        GameManager.Instance.PlayerReady(this);
    }

    void Update()
    {
        stateMachine.Tick();
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.SetPath(IndexOfPosition(transform.position), IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            stateMachine.ProcessInput(PlayerMovementFSM.Inputs.LeftMouseClick);

            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.ProcessInput(PlayerMovementFSM.Inputs.RightMouseClick);

            return;
        }

        if (Input.GetMouseButtonDown(2))
        {
            stateMachine.ProcessInput(PlayerMovementFSM.Inputs.MiddleMouseClick);

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ProcessInput(PlayerMovementFSM.Inputs.SpaceBar);

            return;
        }
    }

    private Vector2Int IndexOfPosition(Vector3 mp)
    {
        return new Vector2Int(Mathf.FloorToInt(mp.x + pixelsOffset), Mathf.FloorToInt(mp.y + pixelsOffset));
    }


    public void ClearPath()
    {
        this.path = null;
    }

    public PathEvent getPathEvent() 
    {
        return stateMachine.pathChanged;
    }
}

