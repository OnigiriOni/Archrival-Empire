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
        citizen.group = WorkerGroup.Idle;
        //citizen.navMeshAgent.ResetPath();
    }

    public override void Execute(Citizen citizen)
    {

    }

    public override void Exit(Citizen citizen)
    {

    }
}
