using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
// Combat Offense is used by all entities that are able to attack.
public struct CombatOffense
{
    public float normalDamage;
    public float pierceDamage;
    public float siegeDamage;

    [Space(10)]
    public float attackRange;

    // Cooldown in seconds.
    public float damageCooldown;
    [System.NonSerialized]
    public float damageCooldownLeft;

    /// <summary>
    /// Calculate the cooldwon time for the next attack.
    /// </summary>
    public void CalculateDamageCooldown()
    {
        damageCooldownLeft -= Time.deltaTime;

        if (damageCooldownLeft < 0)
        {
            damageCooldownLeft = 0;
        }
    }
}

[System.Serializable]
// Combat Defense is used by all entities.
public struct CombatDefense
{
    public float health;

    [Space(10)]
    // Armor in percent.
    public float mormalArmor;
    public float pierceArmor;
    public float siegeArmor;
}

// The DamageStruct is passed to units for damage calculations.
public struct DamageStruct
{
    public float normalDamage;
    public float pierceDamage;
    public float siegeDamage;
}

public class Unit : PlayerObject
{
    [Header("Unit Options")]
    public new string name;

    [Header("Build Options")]
    public BuildCost buildCost;
    
    // Build time in seconds
    public float buildTime;

    [Header("Combat Options")]
    public CombatDefense combatDefense;
    public CombatOffense combatOffense;

    [System.NonSerialized]
    public NavMeshAgent navMeshAgent;

    [System.NonSerialized]
    // The perceived units list holds enemy units and buildings.
    public List<GameObject> perceivedObjectsInRange;

    [System.NonSerialized]
    // The target object is the object the unit is interacting with.
    public GameObject targetObject;

    private void Destruct()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Receive damage. The object health goes down
    /// </summary>
    /// <param name="damage">The amount and type of damage dealt</param>
    public void TakeDamage(DamageStruct damage)
    {
        // The damage that is inflicted to the object.
        float damageValue = (damage.normalDamage - (damage.normalDamage * combatDefense.mormalArmor / 100))
                            + (damage.pierceDamage - (damage.pierceDamage * combatDefense.pierceArmor / 100))
                            + (damage.siegeDamage - (damage.siegeDamage * combatDefense.siegeArmor / 100));

        // Deal the damage and destory the object if health is zero.
        combatDefense.health -= damageValue;

        if (combatDefense.health <= 0)
        {
            Destruct();
        }
    }
}
