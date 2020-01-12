using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : AbstractState
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
        Debug.Log("Idle");
        if (pc.currentPath != null)
            StartMoving(pc);
    }

    public override void StartMoving(PlayerMovementFSM pc)
    {
        pc.TransitionToState(pc.moving);
    }
}
