using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_Build : State<Citizen>
{
    static readonly CitizenState_Build instance = new CitizenState_Build();

    static CitizenState_Build() { }
    public CitizenState_Build() { }

    public static CitizenState_Build Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        // Set the destination for the citizen.
        citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
    }

    public override void Execute(Citizen citizen)
    {
        // Check if the building disappeared because the enemy shot it down!
        if (citizen.targetObject == null)
        {
            citizen.ChangeState(CitizenState_Idle.Instance);
        }

        // Build the building if it is in range.
        if (citizen.targetObject != null && citizen.perceivedObjectsInRange.Contains(citizen.targetObject))
        {
            citizen.navMeshAgent.ResetPath();

            ConstructionSite constructionSite = citizen.targetObject.GetComponent<ConstructionSite>();
            constructionSite.buildTimeLeft -= Time.deltaTime;
        }
        else
        {
            // Move to the target building.
            if (citizen.targetObject != null && citizen.navMeshAgent.destination != citizen.targetObject.transform.position)
            {
                citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
            }
        }
    }

    public override void Exit(Citizen citizen)
    {

    }
}
