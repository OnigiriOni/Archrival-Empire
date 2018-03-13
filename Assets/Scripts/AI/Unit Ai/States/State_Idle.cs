using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : State<Citizen>
{
    static readonly State_Idle instance = new State_Idle();

    static State_Idle() { }
    public State_Idle() { }

    public static State_Idle Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        Debug.Log("Do nothing now");
    }

    public override void Execute(Citizen citizen)
    {
        //Debug.Log("I lost tick-tack-toe against a chicken ...");
    }

    public override void Exit(Citizen citizen)
    {
        Debug.Log("Time to work");
    }
}
