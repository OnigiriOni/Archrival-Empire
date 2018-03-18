using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SoldierState
{
    Idle,
    Attack,
    Chase
}

public class Soldier : Unit
{
    [System.NonSerialized]
    // The perceived units are used to identify enemies.
    public List<GameObject> perceivedObjects;

    [System.NonSerialized]
    // The target object is the target the soldier is attacking.
    public GameObject targetObject;

    private StateMaschine<Soldier> stateMaschine;

    [System.NonSerialized]
    public SoldierState state;

    private void Start()
    {
        // Set the player color and player tag.
        SetPlayerStats();

        perceivedObjects = new List<GameObject>();
        GetComponent<SphereCollider>().radius = combatOffense.attackRange;
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMaschine = new StateMaschine<Soldier>();
        stateMaschine.Initialize(this, SoldierState_Idle.Instance);

        

        
    }

    private void Update()
    {
        combatOffense.CalculateDamageCooldown();

        stateMaschine.Update();

        // Clear the perceived objects list from all null objects.
        perceivedObjects.ForEach(x => { if (x == null) perceivedObjects.Remove(x); });
    }

    public void ChangeState(State<Soldier> state)
    {
        stateMaschine.SetState(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add enemy units and buildings to the perceived objects list.
        Unit unit = other.gameObject.GetComponent<Unit>();
        Building building = other.gameObject.GetComponent<Building>();

        if (unit != null && unit.playerTag != playerTag)
        {
            perceivedObjects.Add(other.gameObject);
        }
        else if (building != null && building.playerTag != playerTag)
        {
            perceivedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Removes objects from the perceived objects list.
        if (perceivedObjects.Contains(other.gameObject))
        {
            perceivedObjects.Remove(other.gameObject);
        }
    }

    public void Attack(GameObject target)
    {
        targetObject = target;
        stateMaschine.SetState(SoldierState_Attack.Instance);
        state = SoldierState.Attack;
    }

    public void MoveTo(Vector3 targetLocation)
    {
        navMeshAgent.SetDestination(targetLocation);
    }

    /// <summary>
    /// Deal damage to an object.
    /// </summary>
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
        VisualizeAttack(unit.gameObject);
    }

    /// <summary>
    /// Draws a debug ray to the target (Symbolysing shooting).
    /// </summary>
    /// <param name="unit"></param>
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
