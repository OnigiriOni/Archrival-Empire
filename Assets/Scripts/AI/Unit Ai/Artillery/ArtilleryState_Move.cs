using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryState_Move : State<Artillery>
{
    static readonly ArtilleryState_Move instance = new ArtilleryState_Move();

    static ArtilleryState_Move() { }
    public ArtilleryState_Move() { }

    public static ArtilleryState_Move Instance
    {
        get { return instance; }
    }


    public override void Enter(Artillery artillery)
    {
        // This has no effect but keep code tidy.
        artillery.targetObject = null;
    }

    public override void Execute(Artillery artillery)
    {
        // If the target location is reached, idle.
        if (!artillery.navMeshAgent.pathPending && artillery.navMeshAgent.remainingDistance < 1)
        {
            artillery.ChangeState(ArtilleryState_Idle.Instance);
        }
    }

    public override void Exit(Artillery artillery)
    {

    }
}