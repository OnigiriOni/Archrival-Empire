using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : Building
{
    [Header("Foudry Options")]
    public Unit artillery;
    
    // The production pipe builds units.
    private ProductionPipe productionPipe;


    private void Start()
    {
        productionPipe = new ProductionPipe(this);

        SetPlayerStats();
        AddToPlayerList();
    }

    private void Update()
    {
        productionPipe.UpdatePipe();
    }

    /// <summary>
    /// Add the foundry to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.foundryList.Add(this);
    }


    /// <summary>
    /// Trains an Artillery and spawns it in front of the building (using ProductionPipe class).
    /// </summary>
    public void TrainArtillery()
    {
        // Check if the player has enough resources.
        if (productionPipe.EnoughResources(artillery))
        {
            // Remove the resource cost from the player resources.
            player.resources.RemoveResources(artillery.buildCost);

            // Add the artillery to the production pipeline.
            productionPipe.AddUnit(artillery);
        }
    }
}
