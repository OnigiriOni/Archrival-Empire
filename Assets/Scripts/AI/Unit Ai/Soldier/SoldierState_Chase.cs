using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierState_Chase : State<Soldier>
{
    static readonly SoldierState_Chase instance = new SoldierState_Chase();

    static SoldierState_Chase() { }
    public SoldierState_Chase() { }

    public static SoldierState_Chase Instance
    {
        get { return instance; }
    }


    public override void Enter(Soldier soldier)
    {

    }

    public override void Execute(Soldier soldier)
    {
        // Check if the target died during the chase.
        if (soldier.targetObject != null)
        {
            // The target is in range and can be shot.
            if (soldier.perceivedObjectsInRange.Contains(soldier.targetObject))
            {
                soldier.ChangeState(SoldierState_Attack.Instance);
            }

            // Set the new target position.
            if (soldier.targetObject.transform.position != soldier.navMeshAgent.destination)
            {
                soldier.navMeshAgent.SetDestination(soldier.targetObject.transform.position);
            }
        }
        
        // The target is null, resume idle.
        if (soldier.targetObject == null)
        {
            soldier.ChangeState(SoldierState_Idle.Instance);
        }
    }

    public override void Exit(Soldier soldier)
    {

    }
}
