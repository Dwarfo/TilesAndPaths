using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState 
{

    public abstract void EnterState(PlayerMovementFSM pc);
    public abstract void StartMoving(PlayerMovementFSM pc);
    public abstract void GoToNextTile(PlayerMovementFSM pc);
    public abstract void PauseMovement(PlayerMovementFSM pc);
    public abstract void CancelMovement(PlayerMovementFSM pc);
    public abstract void ProcessInput(PlayerMovementFSM pc);


}
