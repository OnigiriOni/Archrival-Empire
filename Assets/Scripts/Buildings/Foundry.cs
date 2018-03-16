using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : Building
{
    [Header("Foudry Options")]
    public Unit artillery;
    
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


    ///// <summary>
    ///// Spawn a Artillery Prefab
    ///// </summary>
    //public void TrainArtillery()
    //{
    //    //TODO: Artillery cost resources, but it is not considered
    //    // Set the spawn postion (in front of the entrance)
    //    Vector3 spawnPosition = transform.position;
    //    spawnPosition.x += 8F;

    //    // Loads the Prefab from the "Resources" folder
    //    GameObject artillery = (GameObject)Instantiate(Resources.Load("Artillery"), spawnPosition, transform.rotation);

    //    // Takes only the first Children and its first Material
    //   artillery.GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //new Color(0, 116, 255, 255); //TODO: MAKE THIS THE PLAYER COLOR
    //}
}
