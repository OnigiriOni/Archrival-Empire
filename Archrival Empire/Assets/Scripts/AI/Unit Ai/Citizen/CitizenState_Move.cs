using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_Move : State<Citizen>
{
    static readonly CitizenState_Move instance = new CitizenState_Move();

    static CitizenState_Move() { }
    public CitizenState_Move() { }

    public static CitizenState_Move Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        // This has no effect but keep code tidy.
        citizen.targetObject = null;
        citizen.targetResource = null;

        citizen.group = WorkerGroup.Other;
    }

    public override void Execute(Citizen citizen)
    {
        // If the target location is reached, idle.
        if (!citizen.navMeshAgent.pathPending && citizen.navMeshAgent.remainingDistance < 1)
        {
            citizen.ChangeState(CitizenState_Idle.Instance);
        }
    }

    public override void Exit(Citizen citizen)
    {

    }
}
