using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Unit
{
    // The state machine for the soldier.
    private StateMachine<Soldier> stateMachine;


    private void Start()
    {
        // Set the player color and player tag.
        SetPlayerStats();

        // Set stuff up before the state machine, because it uses this.
        perceivedObjectsInRange = new List<GameObject>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initialize the state machine. Do this after everything else.
        stateMachine = new StateMachine<Soldier>();
        stateMachine.Initialize(this, SoldierState_Idle.Instance);

        //Attack(GameObject.Find("Tower"));
    }

    private void Update()
    {
        // Calculate the damage cooldown.
        combatOffense.CalculateDamageCooldown();

        // Clear the perceived objects list from all null objects.
        perceivedObjectsInRange.ForEach(x => { if (x == null) perceivedObjectsInRange.Remove(x); });

        // Update the state maschine.
        stateMachine.Update();
    }


    /// <summary>
    /// Changes the state the soldier is in. Only use this in the state maschine itself.
    /// </summary>
    /// <param name="state">The new state.</param>
    public void ChangeState(State<Soldier> state)
    {
        stateMachine.SetState(state);
    }

    /// <summary>
    /// The soldier tries to attack an enemy object (Unit/Building).
    /// </summary>
    /// <param name="target">The enemy object (Unit/Building).</param>
    public void Attack(GameObject target)
    {
        targetObject = target;
        stateMachine.SetState(SoldierState_Attack.Instance);
    }

    /// <summary>
    /// The soldier moves to a location, ignoring enemies.
    /// </summary>
    /// <param name="targetLocation">The target location.</param>
    public void MoveTo(Vector3 targetLocation)
    {
        stateMachine.SetState(SoldierState_Move.Instance);
        navMeshAgent.SetDestination(targetLocation);
    }

    /// <summary>
    /// Deal damage to an object (Unit/Building). Only use this in the state maschine itself.
    /// </summary>
    /// <param name="target">The target object (Unit/Building).</param>
    public void DealDamage(GameObject target)
    {
        // Fill the damage struct.
        DamageStruct damageStruct;
        damageStruct.normalDamage = combatOffense.normalDamage;
        damageStruct.pierceDamage = combatOffense.pierceDamage;
        damageStruct.siegeDamage = combatOffense.siegeDamage;

        Unit unit = null;
        Building building = null;

        if ((unit = targetObject.GetComponent<Unit>()) != null)
        {
            unit.TakeDamage(damageStruct);
        }
        else if ((building = targetObject.GetComponent<Building>()) != null)
        {
            building.TakeDamage(damageStruct);
        }

        // Reset the damage cooldown.
        combatOffense.damageCooldownLeft = combatOffense.damageCooldown;

        // Visualize attack.
        VisualizeAttack(target);
    }

    /// <summary>
    /// Draws a debug ray to the target (Symbolysing shooting).
    /// </summary>
    /// <param name="target">The gameObject that gets shot.</param>
    private void VisualizeAttack(GameObject target)
    {
        // The start point of the ray.
        Vector3 rayStartPoint = transform.position;
        rayStartPoint.y += 1.5F;

        // The end point of the ray.
        Vector3 targetPosition = target.transform.position;
        targetPosition.y += 1.5F;

        //Draw the ray. Cool stuff.
        Debug.DrawRay(rayStartPoint, targetPosition - rayStartPoint, Color.red, 0.15F);
    }
}
