using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Path path;
    public InventoryController ic;
    public PlayerMovementFSM stateMachine;
    [SerializeField]
    private bool paused;
    private Tile nextTile;
    [SerializeField]
    private LineDrawer lineDrawer;
    public string entityName = "Player";

    void Start()
    {
        stateMachine = new PlayerMovementFSM();
        stateMachine.playerTransform = transform;
        ic = gameObject.GetComponent<InventoryController>();

        GameManager.Instance.PlayerReady(this);
    }

    void Update()
    {
        stateMachine.Tick();
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.SetPath(TileField.Instance.IndexOfPosition(transform.position), TileField.Instance.IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
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

    public void ClearPath()
    {
        this.path = null;
    }

    public PathEvent getPathChangedEvent()
    {
        return stateMachine.pathChanged;
    }

    public PathEvent getPathEndedEvent()
    {
        return stateMachine.pathEnded;
    }
}

