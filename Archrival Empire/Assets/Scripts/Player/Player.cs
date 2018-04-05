using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The player tag to distinguish between players.
public enum PlayerTag
{
    Gaia,       // Gaia is the ancestral mother of all life: the primal Mother Earth goddess. Used for non player entities.
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6,
    Player7,
    Player8
}

[System.Serializable]
public struct PlayerResource
{
    public int food;
    public int wood;
    public int stone;
    public int gold;

    /// <summary>
    /// Add a type of resource to the player resources.
    /// </summary>
    /// <param name="amount">The amount that gets stored.</param>
    /// <param name="resourceType">The type of the resource.</param>
    public void AddResource(ResourceTransferStruct resourceTransferStruct)
    {
        switch(resourceTransferStruct.resourceType)
        {
            case ResourceType.Food:
                food += resourceTransferStruct.resourceAmount;
                break;

            case ResourceType.Wood:
                wood += resourceTransferStruct.resourceAmount;
                break;

            case ResourceType.Stone:
                stone += resourceTransferStruct.resourceAmount;
                break;

            case ResourceType.Gold:
                gold += resourceTransferStruct.resourceAmount;
                break;
        }
    }

    /// <summary>
    /// Removes the amount of the building/unit costs from the player resources.
    /// </summary>
    /// <param name="buildCost">The build costs of the building/unit</param>
    public void RemoveResources(BuildCost buildCost)
    {
        food -= buildCost.food;
        wood -= buildCost.wood;
        stone -= buildCost.stone;
        gold -= buildCost.gold;
    }
}

public class Player : MonoBehaviour
{
    // The player tag is used to indentify a player.
    public PlayerTag playerTag;
    
    // The Color of the player is used to colour buildings and units.
    public Color playerColor;

    // The resources of a player.
    public PlayerResource resources;
}
