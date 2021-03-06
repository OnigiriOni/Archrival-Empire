﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Holding references of all available building prefabs.
    public Building capitol;
    public Building mill;
    public Building sawmill;
    public Building mine;
    public Building barrack;
    public Building stable;
    public Building foundry;
    public Building tower;

    /// <summary>
    /// Spawn a construction site on the map and all assigned citizen begin to work.
    /// </summary>
    /// <param name="building">The building to be build.</param>
    /// <param name="position">The world position of the build spot.</param>
    /// <param name="player">The player that owns the building</param>
    /// <param name="citizen">The assigned citizen.</param>
    /// <returns>true if the player has enough resources and the construction has started.</returns>
    public bool Build(Building building, Vector3 position, Player player, Citizen citizen)
    {
        // Check if the player has enough resources.
        if (EnoughResources(player, building))
        {
            // Remove the resource cost from the player resources.
            player.resources.RemoveResources(building.buildCost);

            GameObject construction;

            // Spawen a construction site according to the building size.
            if (building.constructionSiteSize == ConstructionSiteSize.Size4x4)
            {
                construction = (GameObject)Instantiate(Resources.Load("ConstructionSite4x4"), position, Quaternion.identity);
            }
            else
            {
                construction = (GameObject)Instantiate(Resources.Load("ConstructionSite8x8"), position, Quaternion.identity);
            }

            // Set the stats of the construction site.
            ConstructionSite constructionSite = construction.GetComponent<ConstructionSite>();

            constructionSite.buildTimeLeft      = building.buildTime;
            constructionSite.combatDefense      = building.combatDefense;
            constructionSite.name               = building.name + " Construction";
            constructionSite.building           = building.name;
            constructionSite.player             = player;
            constructionSite.SetPlayerStats();

            // Delegate the responsible citizen to the construction site.
            citizen.Build(construction);
            
            return true;
        }
        return false;
    }

    /// <summary>
    /// Spawn a construction site on the map and all assigned citizen begin to work.
    /// </summary>
    /// <param name="building">The building to be build.</param>
    /// <param name="position">The world position of the build spot.</param>
    /// <param name="player">The player that owns the building</param>
    /// <param name="citizens">The assigned citizen.</param>
    /// <returns>true if the player has enough resources and the construction has started.</returns>
    public bool Build(Building building, Vector3 position, Player player, Citizen[] citizens)
    {
        // Check if the player has enough resources.
        if (EnoughResources(player, building))
        {
            // Remove the resource cost from the player resources.
            player.resources.RemoveResources(building.buildCost);

            GameObject construction;

            // Spawen a construction site according to the building size.
            if (building.constructionSiteSize == ConstructionSiteSize.Size4x4)
            {
                construction = (GameObject) Instantiate(Resources.Load("ConstructionSite4x4"), position, Quaternion.identity);
            }
            else
            {
                construction = (GameObject) Instantiate(Resources.Load("ConstructionSite8x8"), position, Quaternion.identity);
            }

            // Set the stats of the construction site.
            ConstructionSite constructionSite = construction.GetComponent<ConstructionSite>();

            constructionSite.buildTimeLeft      = building.buildTime;
            constructionSite.combatDefense      = building.combatDefense;
            constructionSite.name               = building.name + " Construction";
            constructionSite.building           = building.name;
            constructionSite.player             = player;
            constructionSite.SetPlayerStats();

            // Delegate all responsible citizen to the construction site.
            foreach (Citizen citizen in citizens)
            {
                citizen.Build(construction);
            }

            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns if the player has enough resources to build a building.
    /// </summary>
    /// <param name="player">The player who wants to build.</param>
    /// <param name="building">The building that is about to be build.</param>
    /// <returns>true if the player has enough resources.</returns>
    private bool EnoughResources(Player player, Building building)
    {
        if (
            player.resources.food >= building.buildCost.food
            && player.resources.wood >= building.buildCost.wood
            && player.resources.stone >= building.buildCost.stone
            && player.resources.gold >= building.buildCost.gold
            )
        {
            return true;
        }
        return false;
    }
}
