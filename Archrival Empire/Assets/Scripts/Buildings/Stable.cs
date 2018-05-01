using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stable : Building
{
    [Header("Barrack Options")]
    public Unit cavalier;

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
    /// Add the stable to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.stableList.Add(this);
    }


    /// <summary>
    /// Trains a Cavalier and spawns it in front of the building (using ProductionPipe class).
    /// </summary>
    public void TrainCavalier()
    {
        // Check if the player has enough resources.
        if (productionPipe.EnoughResources(cavalier))
        {
            // Remove the resource cost from the player resources.
            player.resources.RemoveResources(cavalier.buildCost);

            // Add the soldier to the production pipeline.
            productionPipe.AddUnit(cavalier);
        }
    }
}
