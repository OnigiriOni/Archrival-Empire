using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierState_Idle : State<Soldier>
{
    static readonly SoldierState_Idle instance = new SoldierState_Idle();

    static SoldierState_Idle() { }
    public SoldierState_Idle() { }

    public static SoldierState_Idle Instance
    {
        get { return instance; }
    }


    public override void Enter(Soldier soldier)
    {
        //TODO: may cause issues like in citizen.
        soldier.navMeshAgent.ResetPath();
        soldier.state = SoldierState.Idle;
    }

    public override void Execute(Soldier soldier)
    {
        // If enemies are around shoot them.
        if (soldier.perceivedObjects.Count >= 1)
        {
            soldier.ChangeState(SoldierState_Attack.Instance);
        }
    }

    public override void Exit(Soldier soldier)
    {

    }
}
