using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct Damage
{
    public float toCitizen;
    public float toSoldiers;
    public float toCavalier;
    public float toArtillery;
    public float toBuildings;
}

public class Unit : MonoBehaviour
{
    [Header("Unit Options")]
    public new string name;
    public float health;
    public PlayerTag playerTag;
    public Player player;
    public NavMeshAgent navMeshAgent;

    [Header("Build Options")]
    public BuildCost buildCost;
    
    // Build time in seconds
    public float buildTime;
    public float buildTimeLeft;

    [Header("Combat Options")]
    public Damage damage;

    // Cooldown in seconds
    public float damageCooldown;
    public float damageCooldownLeft;

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
    /// <param name="damage">The amount of damage dealt</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destruct();
        }
    }
}
