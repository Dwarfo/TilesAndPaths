using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFSM 
{
    private AbstractState currentState;
    public Path currentPath;
    public Transform playerTransform;

    public PlayerMovingState    moving  = new PlayerMovingState();
    public PlayerPausedState    paused  = new PlayerPausedState();
    public PlayerIdleState      idle    = new PlayerIdleState();

    public PathEvent pathChanged    = new PathEvent();
    public PathEvent pathEnded      = new PathEvent();

    public PlayerMovementFSM()
    {
        currentState = idle;
        currentState.EnterState(this);
    }

    public void TransitionToState(AbstractState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void Tick()
    {
        currentState.Tick(this);
    }

    public void SetPath(Vector2Int startIndex, Vector2Int endIndex) 
    {
        currentState.SetPath(this, startIndex, endIndex);
    }

    public void ProcessInput(Inputs input) {
        currentState.ProcessInput(this, input);
    }

    public void ChangePath(Path path) 
    {
        if(path != null)
            currentPath = path;

        pathChanged.Invoke(currentPath);
    }

    public void EndPath(Path path) 
    {
        pathEnded.Invoke(currentPath);
    }

    public void ClearPath() 
    {
        currentPath = null;
        pathChanged.Invoke(currentPath);
    }
    public enum Inputs 
    {
        LeftMouseClick,
        RightMouseClick,
        MiddleMouseClick,
        SpaceBar,
        Escape
    }
}


