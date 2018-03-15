using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : Building
{
    /// <summary>
    /// Spawn a Artillery Prefab
    /// </summary>
    public void TrainArtillery()
    {
        //TODO: Artillery cost resources, but it is not considered
        // Set the spawn postion (in front of the entrance)
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += 8F;

        // Loads the Prefab from the "Resources" folder
        GameObject artillery = (GameObject)Instantiate(Resources.Load("Artillery"), spawnPosition, transform.rotation);

        // Takes only the first Children and its first Material
       artillery.GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //new Color(0, 116, 255, 255); //TODO: MAKE THIS THE PLAYER COLOR
    }
}
