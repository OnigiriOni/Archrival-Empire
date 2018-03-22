using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryState_Attack : State<Artillery>
{
    static readonly ArtilleryState_Attack instance = new ArtilleryState_Attack();

    static ArtilleryState_Attack() { }
    public ArtilleryState_Attack() { }

    public static ArtilleryState_Attack Instance
    {
        get { return instance; }
    }


    public override void Enter(Artillery artillery)
    {
        
    }

    public override void Execute(Artillery artillery)
    {
        // Check if the target died, for whatever reason.
        if (artillery.targetObject != null)
        {
            // If the target is in range attack it, otherwise chase it
            if (artillery.perceivedObjectsInRange.Contains(artillery.targetObject))
            {
                // Attack the enemy if possible.
                if (artillery.combatOffense.damageCooldownLeft <= 0)
                {
                    artillery.navMeshAgent.ResetPath();
                    artillery.DealDamage(artillery.targetObject);
                }
            }
            else
            {
                artillery.navMeshAgent.SetDestination(artillery.targetObject.transform.position);
                artillery.ChangeState(ArtilleryState_Chase.Instance);
            }
        }

        // If enemies are around shoot them.
        if (artillery.perceivedObjectsInRange.Count >= 1)
        {
            SelectNearestTarget(artillery);
        }

        // If no enemies are around and the soldier has no target, idle.
        if (artillery.targetObject == null)
        {
            artillery.ChangeState(ArtilleryState_Idle.Instance);
        }
    }

    public override void Exit(Artillery artillery)
    {

    }

    private void SelectNearestTarget(Artillery artillery)
    {
        // Clear the perceived objects list from all null objects.
        artillery.perceivedObjectsInRange.ForEach(x => { if (x == null) artillery.perceivedObjectsInRange.Remove(x); });

        if (artillery.perceivedObjectsInRange.Count > 0)
        {
            // Take the first object as reference for the shortest distance.
            artillery.targetObject = artillery.perceivedObjectsInRange[0];
            float shortestDistance = Vector3.Distance(artillery.transform.position, artillery.targetObject.transform.position);

            // Start at 1 because we already got the object at 0.
            for (int i = 1; i < artillery.perceivedObjectsInRange.Count; i++)
            {
                // If the object is closer, make it the target.
                float distance = Vector3.Distance(artillery.transform.position, artillery.perceivedObjectsInRange[i].transform.position);
                if (distance < shortestDistance)
                {
                    artillery.targetObject = artillery.perceivedObjectsInRange[i];
                    shortestDistance = distance;
                }
            }
        }
    }
}