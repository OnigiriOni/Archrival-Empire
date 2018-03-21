using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_Idle : State<Citizen>
{
    static readonly CitizenState_Idle instance = new CitizenState_Idle();

    static CitizenState_Idle() { }
    public CitizenState_Idle() { }

    public static CitizenState_Idle Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        citizen.navMeshAgent.ResetPath();

        citizen.targetObject = null;
        citizen.targetResource = null;

        citizen.group = WorkerGroup.Idle;
    }

    public override void Execute(Citizen citizen)
    {

    }

    public override void Exit(Citizen citizen)
    {

    }
}
