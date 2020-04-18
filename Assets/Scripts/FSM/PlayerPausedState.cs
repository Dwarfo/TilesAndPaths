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

    public override void ProcessInput(PlayerMovementFSM pc, PlayerMovementFSM.Inputs input)
    {
        Debug.Log("Paused");
        if (input == PlayerMovementFSM.Inputs.SpaceBar)
        {
            pc.TransitionToState(pc.moving);

            return;
        }
        if (input == PlayerMovementFSM.Inputs.RightMouseClick)
        {
            pc.TransitionToState(pc.idle);

            return;
        }
        if (input == PlayerMovementFSM.Inputs.LeftMouseClick)
        {
            
            return;
        }
    }

    public override void SetPath(PlayerMovementFSM pc, Vector2 startIndex, Vector2 endIndex)
    {

        if (pc.currentPath.Destination.Index == endIndex)
        {
            pc.TransitionToState(pc.moving);
        }
        else
        {
            Path newPausedPath = GameManager.Instance.fieldScript.GetPath(startIndex.ToInt2(), endIndex.ToInt2());
            if (newPausedPath == null)
                return;
            pc.ChangePath(newPausedPath);
        }
    }


    public override void Tick(PlayerMovementFSM pc)
    {

    }
}
