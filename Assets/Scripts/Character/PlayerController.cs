using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public InventoryController ic;
    public PlayerMovementFSM stateMachine;
    public string entityName = "Player";

    private float pixelsOffset = 0;

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
            stateMachine.SetPath(TileField.IndexOfPosition(transform.position, pixelsOffset), TileField.IndexOfPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition), pixelsOffset));
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

    public PathEvent getPathChangedEvent()
    {
        return stateMachine.pathChanged;
    }

    public PathEvent getPathEndedEvent()
    {
        return stateMachine.pathEnded;
    }

    public void SetSettings(SO_GameSettings settings)
    {
        this.pixelsOffset = settings.pixelOffset;
    }
}

