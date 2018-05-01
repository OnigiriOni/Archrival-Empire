using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalierState_Chase : State<Cavalier>
{
    static readonly CavalierState_Chase instance = new CavalierState_Chase();

    static CavalierState_Chase() { }
    public CavalierState_Chase() { }

    public static CavalierState_Chase Instance
    {
        get { return instance; }
    }


    public override void Enter(Cavalier cavalier)
    {

    }

    public override void Execute(Cavalier cavalier)
    {
        // Check if the target died during the chase.
        if (cavalier.targetObject != null)
        {
            // The target is in range and can be shot.
            if (cavalier.perceivedObjectsInRange.Contains(cavalier.targetObject))
            {
                cavalier.ChangeState(CavalierState_Attack.Instance);
            }

            // Set the new target position.
            if (cavalier.targetObject.transform.position != cavalier.navMeshAgent.destination)
            {
                cavalier.navMeshAgent.SetDestination(cavalier.targetObject.transform.position);
            }
        }

        // The target is null, resume idle.
        if (cavalier.targetObject == null)
        {
            cavalier.ChangeState(CavalierState_Idle.Instance);
        }
    }

    public override void Exit(Cavalier cavalier)
    {

    }
}
