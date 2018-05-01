using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalierState_Attack : State<Cavalier>
{
    static readonly CavalierState_Attack instance = new CavalierState_Attack();

    static CavalierState_Attack() { }
    public CavalierState_Attack() { }

    public static CavalierState_Attack Instance
    {
        get { return instance; }
    }


    public override void Enter(Cavalier cavalier)
    {

    }

    public override void Execute(Cavalier cavalier)
    {
        // Check if the target died, for whatever reason.
        if (cavalier.targetObject != null)
        {
            // If the target is in range attack it, otherwise chase it
            if (cavalier.perceivedObjectsInRange.Contains(cavalier.targetObject))
            {
                // Attack the enemy if possible.
                if (cavalier.combatOffense.damageCooldownLeft <= 0)
                {
                    cavalier.navMeshAgent.ResetPath();
                    cavalier.DealDamage(cavalier.targetObject);
                }
            }
            else
            {
                cavalier.navMeshAgent.SetDestination(cavalier.targetObject.transform.position);
                cavalier.ChangeState(CavalierState_Chase.Instance);
            }
        }

        // If enemies are around shoot them.
        if (cavalier.perceivedObjectsInRange.Count >= 1)
        {
            SelectNearestTarget(cavalier);
        }

        // If no enemies are around and the cavalier has no target, idle.
        if (cavalier.targetObject == null)
        {
            cavalier.ChangeState(CavalierState_Idle.Instance);
        }
    }

    public override void Exit(Cavalier cavalier)
    {

    }

    private void SelectNearestTarget(Cavalier cavalier)
    {
        // Clear the perceived objects list from all null objects.
        cavalier.perceivedObjectsInRange.ForEach(x => { if (x == null) cavalier.perceivedObjectsInRange.Remove(x); });

        if (cavalier.perceivedObjectsInRange.Count > 0)
        {
            // Take the first object as reference for the shortest distance.
            cavalier.targetObject = cavalier.perceivedObjectsInRange[0];
            float shortestDistance = Vector3.Distance(cavalier.transform.position, cavalier.targetObject.transform.position);

            // Start at 1 because we already got the object at 0.
            for (int i = 1; i < cavalier.perceivedObjectsInRange.Count; i++)
            {
                // If the object is closer, make it the target.
                float distance = Vector3.Distance(cavalier.transform.position, cavalier.perceivedObjectsInRange[i].transform.position);
                if (distance < shortestDistance)
                {
                    cavalier.targetObject = cavalier.perceivedObjectsInRange[i];
                    shortestDistance = distance;
                }
            }
        }
    }
}
