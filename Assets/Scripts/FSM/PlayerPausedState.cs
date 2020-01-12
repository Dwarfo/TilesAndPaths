using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPausedState : AbstractState
{
    public override void CancelMovement(PlayerMovementFSM pc)
    {
        
    }

    public override void EnterState(PlayerMovementFSM pc)
    {
        
    }

    public override void GoToNextTile(PlayerMovementFSM pc)
    {
        
    }

    public override void PauseMovement(PlayerMovementFSM pc)
    {
        
    }

    public override void ProcessInput(PlayerMovementFSM pc)
    {
        Debug.Log("Paused");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pc.TransitionToState(pc.moving);
        }
        if (Input.GetMouseButtonDown(1))
        {
            pc.TransitionToState(pc.idle);
        }
    }
    public override void StartMoving(PlayerMovementFSM pc)
    {
        
    }
}
