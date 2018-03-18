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
        // The player has selected a target for the soldier.
        if (soldier.targetObject != null)
        {
            // If the target is in range attack it, otherwise chase it
            if (soldier.perceivedObjects.Contains(soldier.targetObject))
            {
                if (soldier.combatOffense.damageCooldownLeft <= 0)
                {
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
        if (soldier.perceivedObjects.Count >= 1)
        {
            SelectNearestTarget(soldier);
        }

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
        soldier.perceivedObjects.ForEach(x => { if (x == null) soldier.perceivedObjects.Remove(x); });

        if (soldier.perceivedObjects.Count > 0)
        {
            // Take the first object as reference for the shortest distance.
            soldier.targetObject = soldier.perceivedObjects[0];
            float shortestDistance = Vector3.Distance(soldier.transform.position, soldier.targetObject.transform.position);

            // Start at 1 because we already got the object at 0.
            for (int i = 1; i < soldier.perceivedObjects.Count; i++)
            {
                // If the object is closer, make it the target.
                float distance = Vector3.Distance(soldier.transform.position, soldier.perceivedObjects[i].transform.position);
                if (distance < shortestDistance)
                {
                    soldier.targetObject = soldier.perceivedObjects[i];
                    shortestDistance = distance;
                }
            }
        }
    }
}
