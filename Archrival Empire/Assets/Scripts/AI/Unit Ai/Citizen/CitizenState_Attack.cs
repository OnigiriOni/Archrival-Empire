using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_Attack : State<Citizen>
{
    static readonly CitizenState_Attack instance = new CitizenState_Attack();

    static CitizenState_Attack() { }
    public CitizenState_Attack() { }

    public static CitizenState_Attack Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {

    }

    public override void Execute(Citizen citizen)
    {
        // Check if the target died, for whatever reason.
        if (citizen.targetObject != null)
        {
            // If the target is in range attack it, otherwise chase it
            if (citizen.perceivedObjectsInRange.Contains(citizen.targetObject))
            {
                // Attack the enemy if possible.
                if (citizen.combatOffense.damageCooldownLeft <= 0)
                {
                    citizen.navMeshAgent.ResetPath();
                    citizen.DealDamage(citizen.targetObject);
                }
            }
            else
            {
                citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
                citizen.ChangeState(CitizenState_Chase.Instance);
            }
        }

        // If the citizen has no target, idle.
        if (citizen.targetObject == null)
        {
            citizen.ChangeState(CitizenState_Idle.Instance);
        }
    }

    public override void Exit(Citizen citizen)
    {

    }
}
