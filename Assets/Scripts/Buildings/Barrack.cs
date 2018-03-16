using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    [Header("Barrack Options")]
    public Unit soldier;

    private ProductionPipe productionPipe;

    private void Start()
    {
        productionPipe = new ProductionPipe(this);
    }

    private void Update()
    {
        productionPipe.UpdatePipe();
    }

    /// <summary>
    /// Trains a Soldier and spawns it in front of the building (using ProductionPipe class).
    /// </summary>
    public void TrainSoldier()
    {
        // Check if the player has enough resources.
        if (productionPipe.EnoughResources(soldier))
        {
            // Remove the resource cost from the player resources.
            player.resources.RemoveResources(soldier.buildCost);

            // Add the soldier to the production pipeline.
            productionPipe.AddUnit(soldier);
        }
    }
}
