using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_Chase : State<Citizen>
{
    static readonly CitizenState_Chase instance = new CitizenState_Chase();

    static CitizenState_Chase() { }
    public CitizenState_Chase() { }

    public static CitizenState_Chase Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {

    }

    public override void Execute(Citizen citizen)
    {
        // Check if the target died during the chase.
        if (citizen.targetObject != null)
        {
            // The target is in range and can be stabed.
            if (citizen.perceivedObjectsInRange.Contains(citizen.targetObject))
            {
                citizen.ChangeState(CitizenState_Attack.Instance);
            }

            //// Set the new target position.
            //if (citizen.targetObject.transform.position != citizen.navMeshAgent.destination)
            //{
            //    citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
            //}
            // Move to the target resource
            if (citizen.targetObject != null && citizen.navMeshAgent.destination != citizen.targetObject.transform.position)
            {
                citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
            }
        }

        Debug.Log(citizen.navMeshAgent.pathEndPosition);

        // The target is null, resume idle.
        if (citizen.targetObject == null)
        {
            citizen.ChangeState(CitizenState_Idle.Instance);
        }
    }

    public override void Exit(Citizen citizen)
    {

    }
}
