using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFSM 
{
    private AbstractState currentState;
    public Path currentPath;
    public Transform playerTransform;

    public PlayerMovingState moving = new PlayerMovingState();
    public PlayerPausedState paused = new PlayerPausedState();
    public PlayerIdleState idle = new PlayerIdleState();

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
        currentState.ProcessInput(this);
    }

}
