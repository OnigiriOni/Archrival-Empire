using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuildCost
{
    public int food;
    public int wood;
    public int stone;
    public int gold;
}

[System.Serializable]
public struct ResourceCapacity
{
    public bool canStoreFood;
    public bool canStoreWood;
    public bool canStoreStone;
    public bool canStoreGold;

    public bool CanStore(ResourceType resourceType)
    {
        switch(resourceType)
        {
            case ResourceType.Food: return canStoreFood;
            case ResourceType.Wood: return canStoreWood;
            case ResourceType.Stone: return canStoreStone;
            case ResourceType.Gold: return canStoreGold;
        }
        return false;
    }
}

public class Building : MonoBehaviour
{
    [Header("Building Options")]
    public new string name;
    public float health;
    public PlayerTag playerTag;
    public Player player;

    [Header("Build Options")]
    public BuildCost buildCost;

    // Build time in seconds
    public float buildTime;

    [Header("Resource Options")]
    public ResourceCapacity resourceCapacity;

    private void Start()
    {
        // Set the color of the building to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Receive damage. The object health goes down.
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this);
        }
    }

}
