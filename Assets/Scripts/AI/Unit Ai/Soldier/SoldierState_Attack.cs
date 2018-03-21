using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierState_Attack : State<Soldier>
{
    static readonly SoldierState_Attack instance = new SoldierState_Attack();

    static SoldierState_Attack() { }
    public SoldierState_Attack() { }

    public static SoldierState_Attack Instance
    {
        get { return instance; }
    }


    public override void Enter(Soldier soldier)
    {

    }

    public override void Execute(Soldier soldier)
    {
        // Check if the target died, for whatever reason.
        if (soldier.targetObject != null)
        {
            // If the target is in range attack it, otherwise chase it
            if (soldier.perceivedObjectsInRange.Contains(soldier.targetObject))
            {
                // Attack the enemy if possible.
                if (soldier.combatOffense.damageCooldownLeft <= 0)
                {
                    soldier.navMeshAgent.ResetPath();
                    soldier.DealDamage(soldier.targetObject);
                }
            }
            else
            {
                soldier.navMeshAgent.SetDestination(soldier.targetObject.transform.position);
                soldier.ChangeState(SoldierState_Chase.Instance);
            }
        }

        // If enemies are around shoot them.
        if (soldier.perceivedObjectsInRange.Count >= 1)
        {
            SelectNearestTarget(soldier);
        }

        // If no enemies are around and the soldier has no target, idle.
        if (soldier.targetObject == null)
        {
            soldier.ChangeState(SoldierState_Idle.Instance);
        }
    }

    public override void Exit(Soldier soldier)
    {

    }

    private void SelectNearestTarget(Soldier soldier)
    {
        // Clear the perceived objects list from all null objects.
        soldier.perceivedObjectsInRange.ForEach(x => { if (x == null) soldier.perceivedObjectsInRange.Remove(x); });

        if (soldier.perceivedObjectsInRange.Count > 0)
        {
            // Take the first object as reference for the shortest distance.
            soldier.targetObject = soldier.perceivedObjectsInRange[0];
            float shortestDistance = Vector3.Distance(soldier.transform.position, soldier.targetObject.transform.position);

            // Start at 1 because we already got the object at 0.
            for (int i = 1; i < soldier.perceivedObjectsInRange.Count; i++)
            {
                // If the object is closer, make it the target.
                float distance = Vector3.Distance(soldier.transform.position, soldier.perceivedObjectsInRange[i].transform.position);
                if (distance < shortestDistance)
                {
                    soldier.targetObject = soldier.perceivedObjectsInRange[i];
                    shortestDistance = distance;
                }
            }
        }
    }
}
