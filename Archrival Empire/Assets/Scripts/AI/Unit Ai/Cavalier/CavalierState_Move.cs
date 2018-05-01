using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalierState_Move : State<Cavalier>
{
    static readonly CavalierState_Move instance = new CavalierState_Move();

    static CavalierState_Move() { }
    public CavalierState_Move() { }

    public static CavalierState_Move Instance
    {
        get { return instance; }
    }


    public override void Enter(Cavalier cavalier)
    {
        // This has no effect but keep code tidy.
        cavalier.targetObject = null;
    }

    public override void Execute(Cavalier cavalier)
    {
        // If the target location is reached, idle.
        if (!cavalier.navMeshAgent.pathPending && cavalier.navMeshAgent.remainingDistance < 1)
        {
            cavalier.ChangeState(CavalierState_Idle.Instance);
        }
    }

    public override void Exit(Cavalier cavalier)
    {

    }
}
