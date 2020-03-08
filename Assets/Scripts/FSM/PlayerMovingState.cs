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

    private void GoToNextTile(PlayerMovementFSM pc)
    {
        if (MoveToTile(currentPath.CurrentTile, pc.playerTransform))
        {
            if (currentPath.CurrentTile == currentPath.Destination)
            {
                pc.EndPath(currentPath);
                pc.TransitionToState(pc.idle);
            }
            else if (movementPaused)
            {
                pc.TransitionToState(pc.paused);
                pc.ChangePath(null);
            }
            else
            {
                currentPath.NextPosition();
                pc.ChangePath(null);
            }
        }
    }

    private void PauseMovement()
    {
        movementPaused = !movementPaused;
    }

    public override void ProcessInput(PlayerMovementFSM pc, PlayerMovementFSM.Inputs input)
    {
        if (input == PlayerMovementFSM.Inputs.SpaceBar || input == PlayerMovementFSM.Inputs.RightMouseClick)
        {
            PauseMovement();
        }
    }

    public override void Tick(PlayerMovementFSM pc)
    {
        GoToNextTile(pc);
    }

    private bool MoveToTile(Tile tile, Transform transform)
    {   //TODO change static value for Speed
        transform.position = Vector2.MoveTowards(transform.position, tile.Index, GameManager.Instance.playerSpeed);
        return (Vector2.Distance(transform.position, tile.Index) == 0);
    }

    public override void SetPath(PlayerMovementFSM pc, Vector2 startIndex, Vector2 endIndex)
    {

    }
}
