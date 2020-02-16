using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState 
{
    public abstract void EnterState(PlayerMovementFSM pc);
    public abstract void SetPath(PlayerMovementFSM pc, Vector2 startIndex, Vector2 endIndex);
    public abstract void CancelMovement(PlayerMovementFSM pc);
    public abstract void ProcessInput(PlayerMovementFSM pc, PlayerMovementFSM.Inputs input);
    public abstract void Tick(PlayerMovementFSM pc);


}
