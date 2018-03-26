using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConstructionSiteSize
{
    Size4x4,
    Size8x8
}

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

public class ProductionPipe : MonoBehaviour
{
    // A object that hold the build time left for each unit in production.
    private class BuildTime
    {
        public Unit unit;
        public float buildTimeLeft;

        public BuildTime(Unit unit, float buildTime)
        {
            this.unit = unit;
            buildTimeLeft = buildTime;
        }
    }

    private Building building;
    private List<BuildTime> productionPipe = new List<BuildTime>();

    public ProductionPipe(Building building)
    {
        this.building = building;
    }

    /// <summary>
    /// Adds a unit to the production pipe using the BuildTime class.
    /// </summary>
    /// <param name="unit">The unit object that has to be cloned.</param>
    public void AddUnit(Unit unit)
    {
        productionPipe.Add(new BuildTime(unit, unit.buildTime));
    }

    /// <summary>
    /// Updates the build time left for each unit in the production pipe per frame.
    /// </summary>
    public void UpdatePipe()
    {
        if (productionPipe.Count >= 1)
        {
            //FIXME: An enum error occurs because of the remove call, but it has no negative impact. 
            foreach (BuildTime time in productionPipe)
            {
                time.buildTimeLeft -= Time.deltaTime;

                if (time.buildTimeLeft <= 0)
                {
                    SpawnUnit(time.unit);
                    productionPipe.Remove(time);
                }
            }
        }
    }

    /// <summary>
    /// Spawns an instance of the unit.
    /// </summary>
    /// <param name="unit">The unit object that has to be cloned.</param>
    private void SpawnUnit(Unit unit)
    {
        // Set the spawn postion (in front of the entrance)
        Vector3 spawnPosition = building.transform.position;
        spawnPosition.x += 8F;

        // Spawn the citizen.
        GameObject gameObject = (GameObject) Instantiate(Resources.Load(unit.name), spawnPosition, Quaternion.identity);

        // Set the stats of the citizen.
        Unit newUnit = gameObject.GetComponent<Unit>();

        newUnit.player = building.player;
        newUnit.playerTag = building.player.playerTag;
        newUnit.SetPlayerStats();
    }

    /// <summary>
    /// Returns if the player has enough resources to build a unit.
    /// </summary>
    /// <param name="player">The player who wants to build.</param>
    /// <param name="building">The unit that is about to be trained.</param>
    /// <returns>true if the player has enough resources.</returns>
    public bool EnoughResources(Unit unit)
    {
        if (
            building.player.resources.food >= unit.buildCost.food
            && building.player.resources.wood >= unit.buildCost.wood
            && building.player.resources.stone >= unit.buildCost.stone
            && building.player.resources.gold >= unit.buildCost.gold
            )
        {
            return true;
        }
        return false;
    }
}

public class Building : MonoBehaviour
{
    [Header("Building Options")]
    public new string name;
    public PlayerTag playerTag;
    public Player player;

    [Header("Build Options")]
    public BuildCost buildCost;

    // Build time in seconds
    public float buildTime;
    public ConstructionSiteSize constructionSiteSize;

    [Header("Resource Options")]
    public ResourceCapacity resourceCapacity;

    [Header("Combat Options")]
    public CombatDefense combatDefense;


    /// <summary>
    /// Sets the playerColor of the building and the playerTag.
    /// </summary>
    public void SetPlayerStats()
    {
        // Set the color of the building to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;
    }

    /// <summary>
    /// Destroys the GameObject.
    /// </summary>
    protected void Destruct()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Calculate the amount of damage the building receive.
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
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
