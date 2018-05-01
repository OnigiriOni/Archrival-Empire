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


    private void Update()
    {
        // Clears the lists from null pointer.
        citizenList.ForEach(x => { if (x == null) citizenList.Remove(x); });
        soldierList.ForEach(x => { if (x == null) soldierList.Remove(x); });
        cavalierList.ForEach(x => { if (x == null) cavalierList.Remove(x); });
        artilleryList.ForEach(x => { if (x == null) artilleryList.Remove(x); });

        capitolList.ForEach(x => { if (x == null) capitolList.Remove(x); });
        millList.ForEach(x => { if (x == null) millList.Remove(x); });
        sawmillList.ForEach(x => { if (x == null) sawmillList.Remove(x); });
        mineList.ForEach(x => { if (x == null) mineList.Remove(x); });
        barrackList.ForEach(x => { if (x == null) barrackList.Remove(x); });
        stableList.ForEach(x => { if (x == null) stableList.Remove(x); });
        foundryList.ForEach(x => { if (x == null) foundryList.Remove(x); });
        towerList.ForEach(x => { if (x == null) towerList.Remove(x); });
        constructionList.ForEach(x => { if (x == null) constructionList.Remove(x); });
    }

    public List<Citizen> citizenList;
    public List<Soldier> soldierList;
    public List<Cavalier> cavalierList;
    public List<Artillery> artilleryList;

    public List<Capitol> capitolList;
    public List<Mill> millList;
    public List<Sawmill> sawmillList;
    public List<Mine> mineList;
    public List<Barrack> barrackList;
    public List<Stable> stableList;
    public List<Foundry> foundryList;
    public List<Tower> towerList;
    public List<ConstructionSite> constructionList;
}
