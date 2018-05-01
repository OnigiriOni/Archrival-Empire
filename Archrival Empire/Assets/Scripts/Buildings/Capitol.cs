using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitol : Building
{
    [Header("Capitol Options")]
    public Unit citizen;

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
    /// Add the capitol to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.capitolList.Add(this);
    }


    /// <summary>
    /// Trains a citizen and spawns it in front of the building (using ProductionPipe class).
    /// </summary>
    public void TrainCitizen()
    {
        // Check if the player has enough resources.
        if (productionPipe.EnoughResources(citizen))
        {
            // Remove the resource cost from the player resources.
            player.resources.RemoveResources(citizen.buildCost);

            // Add the citizen to the production pipeline.
            productionPipe.AddUnit(citizen);
        }
    }
}
