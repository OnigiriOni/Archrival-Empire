using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryState_Chase : State<Artillery>
{
    static readonly ArtilleryState_Chase instance = new ArtilleryState_Chase();

    static ArtilleryState_Chase() { }
    public ArtilleryState_Chase() { }

    public static ArtilleryState_Chase Instance
    {
        get { return instance; }
    }


    public override void Enter(Artillery artillery)
    {

    }

    public override void Execute(Artillery artillery)
    {
        // Check if the target died during the chase.
        if (artillery.targetObject != null)
        {
            // The target is in range and can be shot.
            if (artillery.perceivedObjectsInRange.Contains(artillery.targetObject))
            {
                artillery.ChangeState(ArtilleryState_Attack.Instance);
            }

            // Set the new target position.
            if (artillery.targetObject.transform.position != artillery.navMeshAgent.destination)
            {
                artillery.navMeshAgent.SetDestination(artillery.targetObject.transform.position);
            }
        }

        // The target is null, resume idle.
        if (artillery.targetObject == null)
        {
            artillery.ChangeState(ArtilleryState_Idle.Instance);
        }
    }

    public override void Exit(Artillery artillery)
    {

    }
}