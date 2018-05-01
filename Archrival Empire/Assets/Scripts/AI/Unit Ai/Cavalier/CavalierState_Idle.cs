using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalierState_Idle : State<Cavalier>
{
    static readonly CavalierState_Idle instance = new CavalierState_Idle();

    static CavalierState_Idle() { }
    public CavalierState_Idle() { }

    public static CavalierState_Idle Instance
    {
        get { return instance; }
    }


    public override void Enter(Cavalier cavalier)
    {
        cavalier.navMeshAgent.ResetPath();
    }

    public override void Execute(Cavalier cavalier)
    {
        // If enemies are around shoot them.
        if (cavalier.perceivedObjectsInRange.Count >= 1)
        {
            cavalier.ChangeState(CavalierState_Attack.Instance);
        }
    }

    public override void Exit(Cavalier cavalier)
    {

    }
}
