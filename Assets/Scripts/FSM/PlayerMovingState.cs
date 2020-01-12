using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : AbstractState
{
    private Path currentPath;
    private bool movementPaused;
    public override void CancelMovement(PlayerMovementFSM pc)
    {

    }

    public override void EnterState(PlayerMovementFSM pc)
    {
        currentPath = pc.currentPath;
        currentPath.NextPosition();
        movementPaused = false;
    }

    public override void GoToNextTile(PlayerMovementFSM pc)
    {
        if (MoveToTile(currentPath.CurrentTile, pc.playerTransform))
        {
            if (currentPath.CurrentTile == currentPath.Destination)
            {
                pc.TransitionToState(pc.idle);
            }
            else if (movementPaused)
            {
                pc.TransitionToState(pc.paused);
                //lineDrawer.DrawLine(path); TODO fire event for linedrawer
            }
            else
            {
                currentPath.NextPosition();
            }
        }
    }

    public override void PauseMovement(PlayerMovementFSM pc)
    {

    }

    public override void ProcessInput(PlayerMovementFSM pc)
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            movementPaused = !movementPaused;
        }

        GoToNextTile(pc);
    }

    public override void StartMoving(PlayerMovementFSM pc)
    {

    }

    private bool MoveToTile(Tile tile, Transform transform)
    {   //TODO change static value to Speed
        transform.position = Vector2.MoveTowards(transform.position, tile.Index, 0.05f);
        return (Vector2.Distance(transform.position, tile.Index) == 0);
    }
}
