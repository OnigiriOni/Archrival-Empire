using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Build : State<Citizen>
{
    static readonly State_Build instance = new State_Build();

    static State_Build() { }
    public State_Build() { }

    public static State_Build Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        // Set the destination for the citizen.
        citizen.navMeshAgent.SetDestination(citizen.targetGameObject.transform.position);
    }

    public override void Execute(Citizen citizen)
    {
        ///////////////////////////////////////
        // Preconditions
        ///////////////////////////////////////

        // Check if the building disappeared because the enemy shot it down!
        if (citizen.targetGameObject == null)
        {
            citizen.navMeshAgent.ResetPath();
            citizen.ChangeState(State_Idle.Instance);
        }

        ///////////////////////////////////////
        // Action
        ///////////////////////////////////////

        // Build the building if it is in range.
        if (citizen.targetGameObject != null && citizen.perceivedObjects.Contains(citizen.targetGameObject))
        {
            citizen.navMeshAgent.ResetPath();

            ConstructionSite constructionSite = citizen.targetGameObject.GetComponent<ConstructionSite>();
            constructionSite.buildTimeLeft -= Time.deltaTime;
        }
        else
        {
            // Move to the target building.
            if (citizen.targetGameObject != null && citizen.navMeshAgent.destination != citizen.targetGameObject.transform.position)
            {
                citizen.navMeshAgent.SetDestination(citizen.targetGameObject.transform.position);
            }
        }
    }

    public override void Exit(Citizen citizen)
    {

    }
}
