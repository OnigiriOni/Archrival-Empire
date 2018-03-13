using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Move : State<Citizen>
{
    static readonly State_Move instance = new State_Move();

    static State_Move() { }
    public State_Move() { }

    public static State_Move Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        Debug.Log("Moving to something");
    }

    public override void Execute(Citizen citizen)
    {
        if (citizen.transform.position == citizen.navMeshAgent.destination && citizen.group == WorkerGroup.GatherFood)
        {
            //citizen.ChangeState(State);
        }
        if (citizen.transform.position == citizen.navMeshAgent.destination && citizen.group == WorkerGroup.Builder)
        {
            //State Build
            //citizen.ChangeState(State_Idle.Instance);
        }
        if (citizen.transform.position == citizen.navMeshAgent.destination && citizen.group == WorkerGroup.Other)
        {
            citizen.ChangeState(State_Idle.Instance);
        }

    }

    public override void Exit(Citizen citizen)
    {
        Debug.Log("I am there");
    }
}
