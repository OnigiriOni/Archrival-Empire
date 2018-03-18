using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct CombatOffense
{
    public float normalDamage;
    public float pierceDamage;
    public float siegeDamage;

    [Space(10)]
    public float attackRange;

    // Cooldown in seconds
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
public struct CombatDefense
{
    public float health;

    [Space(10)]
    // Armor in percent
    public float mormalArmor;
    public float pierceArmor;
    public float siegeArmor;
}

public struct DamageStruct
{
    public float normalDamage;
    public float pierceDamage;
    public float siegeDamage;
}

public class Unit : MonoBehaviour
{
    [Header("Unit Options")]
    public new string name;
    public PlayerTag playerTag;
    public Player player;

    [System.NonSerialized]
    public NavMeshAgent navMeshAgent;

    [Header("Build Options")]
    public BuildCost buildCost;
    
    // Build time in seconds
    public float buildTime;
    public float buildTimeLeft;

    [Header("Combat Options")]
    public CombatDefense combatDefense;
    public CombatOffense combatOffense;

    public void SetPlayerStats()
    {
        // Set the color of the building to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;
    }

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
        float damageValue = (Mathf.Clamp(damage.normalDamage - combatDefense.mormalArmor, 0, damage.normalDamage)) +
                            (Mathf.Clamp(damage.pierceDamage - combatDefense.pierceArmor, 0, damage.pierceDamage)) +
                            (Mathf.Clamp(damage.siegeDamage - combatDefense.siegeArmor, 0, damage.siegeDamage));

        // Deal the damage and destory the object if health is zero.
        combatDefense.health -= damageValue;

        if (combatDefense.health <= 0)
        {
            Destruct();
        }
    }
}
