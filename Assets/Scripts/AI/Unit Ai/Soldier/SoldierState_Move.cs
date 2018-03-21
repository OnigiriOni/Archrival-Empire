using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierState_Move : State<Soldier>
{
    static readonly SoldierState_Move instance = new SoldierState_Move();

    static SoldierState_Move() { }
    public SoldierState_Move() { }

    public static SoldierState_Move Instance
    {
        get { return instance; }
    }


    public override void Enter(Soldier soldier)
    {
        // This has no effect but keep code tidy.
        soldier.targetObject = null;
    }

    public override void Execute(Soldier soldier)
    {
        // If the target location is reached, idle.
        if (!soldier.navMeshAgent.pathPending && soldier.navMeshAgent.remainingDistance < 1)
        {
            soldier.ChangeState(SoldierState_Idle.Instance);
        }
    }

    public override void Exit(Soldier soldier)
    {

    }
}
