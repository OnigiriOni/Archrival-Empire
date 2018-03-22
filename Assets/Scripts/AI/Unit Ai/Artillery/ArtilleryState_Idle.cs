using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryState_Idle : State<Artillery>
{
    static readonly ArtilleryState_Idle instance = new ArtilleryState_Idle();

    static ArtilleryState_Idle() { }
    public ArtilleryState_Idle() { }

    public static ArtilleryState_Idle Instance
    {
        get { return instance; }
    }


    public override void Enter(Artillery artillery)
    {
        artillery.navMeshAgent.ResetPath();
    }

    public override void Execute(Artillery artillery)
    {
        Debug.Log("Hi");
        // If enemies are around shoot them.
        if (artillery.perceivedObjectsInRange.Count > 0)
        {
            artillery.ChangeState(ArtilleryState_Attack.Instance);
        }
    }

    public override void Exit(Artillery artillery)
    {

    }
}