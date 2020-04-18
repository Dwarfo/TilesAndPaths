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
        pc.ClearPath();
    }

    public override void ProcessInput(PlayerMovementFSM pc, PlayerMovementFSM.Inputs input)
    {
        if (input == PlayerMovementFSM.Inputs.RightMouseClick) 
        {
            pc.ClearPath();
        }
        /*if (pc.currentPath != null)
            pc.TransitionToState(pc.moving);*/
    }

    public override void SetPath(PlayerMovementFSM pc, Vector2 startIndex, Vector2 endIndex)
    {
        if (pc.currentPath != null)
        {
            if (pc.currentPath.Destination.Index == endIndex)
            {
                pc.TransitionToState(pc.moving);
            }
            else
            {
                pc.ChangePath(GameManager.Instance.fieldScript.GetPath(startIndex.ToInt2(), endIndex.ToInt2()));
            }
        }
        else
        {
            pc.ChangePath(GameManager.Instance.fieldScript.GetPath(startIndex.ToInt2(), endIndex.ToInt2()));
        }

    }

    public override void Tick(PlayerMovementFSM pc)
    {

    }
}
