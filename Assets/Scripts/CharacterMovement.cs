using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private State currentState;
    private Transition tranToMake;
    private Dictionary<Transition,State> tranToState = new Dictionary<Transition, State>();
    private bool move;
    private bool pause;


    /*public void Act()
    {
        if(tranToState.TryGetValue(currentState.Action(), out currentState))
        {
            Debug.Log("Transition to State " + currentState.name + " occured");
        }
        
    }*/

}

public class State
{
    private string name;
    public string Name{ get { return name;}}

    //public abstract Transition Action();
    
}

public class Transition
{
    State start;
    State end;

}