using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    [Header("Tower Options")]
    public CombatOffense combatOffense;

    [System.NonSerialized]
    // The perceived units are used to identify enemies.
    public List<Unit> perceivedUnits;

    [System.NonSerialized]
    // The target object is the target the tower is attacking.
    public Unit targetUnit;


    private void Start()
    {
        // Set the player color and player tag.
        SetPlayerStats();
        AddToPlayerList();

        perceivedUnits = new List<Unit>();
    }

    private void Update()
    {
        // Calculate the cooldown for the next attack.
        combatOffense.CalculateDamageCooldown();

        // Select the nearest enemy unit as target.
        SelectNearestTarget();

        // Attack the nearest unit if possible.
        if (targetUnit != null && combatOffense.damageCooldownLeft <= 0)
        {
            Attack(targetUnit);
        }
    }

    /// <summary>
    /// Add the tower to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.towerList.Add(this);
    }


    /// <summary>
    /// Select the nearest perceived unit as a target.
    /// </summary>
    private void SelectNearestTarget()
    {
        // Clear the list from all null objects.
        perceivedUnits.ForEach(x => { if (x == null) perceivedUnits.Remove(x); });

        // Are objects in range?
        if (perceivedUnits.Count >= 1)
        {
            // Take the first object as reference for the shortest distance.
            targetUnit = perceivedUnits[0];
            float shortestDistance = Vector3.Distance(transform.position, targetUnit.transform.position);

            // Start at 1 because we already got the object at 0.
            for (int i = 1; i < perceivedUnits.Count; i++)
            {
                // If the object is closer, make it the target.
                float distance = Vector3.Distance(transform.position, perceivedUnits[i].transform.position);
                if (distance < shortestDistance)
                {
                    targetUnit = perceivedUnits[i];
                    shortestDistance = distance;
                }
            }
        }
    }

    /// <summary>
    /// Deal damage to a unit.
    /// </summary>
    public void Attack(Unit unit)
    {
        // Fill the damage struct.
        DamageStruct damageStruct;
        damageStruct.normalDamage = combatOffense.normalDamage;
        damageStruct.pierceDamage = combatOffense.pierceDamage;
        damageStruct.siegeDamage = combatOffense.siegeDamage;
        
        // Deal damage to the unit.
        unit.TakeDamage(damageStruct);

        // Reset the damage cooldown.
        combatOffense.damageCooldownLeft = combatOffense.damageCooldown;

        // Visualize attack.
        VisualizeAttack(unit);
    }

    /// <summary>
    /// Draws a debug ray to the target (Symbolysing shooting).
    /// </summary>
    /// <param name="unit">The unit that gets shot.</param>
    private void VisualizeAttack(Unit unit)
    {
        // The start point of the ray.
        Vector3 rayStartPoint = transform.position;
        rayStartPoint.y += 8.15F;

        // The end point of the ray.
        Vector3 unitPosition = unit.transform.position;
        unitPosition.y += 1.5F;

        //Draw the ray. Cool stuff.
        Debug.DrawRay(rayStartPoint, unitPosition - rayStartPoint, Color.red, 0.15F);
    }
}
