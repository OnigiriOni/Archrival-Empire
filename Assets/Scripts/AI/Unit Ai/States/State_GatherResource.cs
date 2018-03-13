using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GatherResource : State<Citizen>
{
    static readonly State_GatherResource instance = new State_GatherResource();

    static State_GatherResource() { }
    public State_GatherResource() { }

    public static State_GatherResource Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        Debug.Log("Attempt to get the resource");
    }

    public override void Execute(Citizen citizen)
    {
        //nehmen - gehen - ablegen  // wenn eines nicht klappt = idle

    }

    public override void Exit(Citizen citizen)
    {
        Debug.Log("Time to work elsewhere");
    }
}
