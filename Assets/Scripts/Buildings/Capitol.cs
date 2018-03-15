using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitol : Building
{
    /// <summary>
    /// Spawn a Citizen Prefab
    /// </summary>
    public void TrainCitizen()
    {
        //TODO: Citizen cost resources, but it is not considered
        // Set the spawn postion (in front of the entrance)
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += 5.5F;

        // Loads the Prefab from the "Resources" folderw
        GameObject citizen = (GameObject) Instantiate(Resources.Load("Citizen"), spawnPosition, transform.rotation);

        // Takes only the first Children and its first Material
        citizen.GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //new Color(0, 116, 255, 255); //TODO: MAKE THIS THE PLAYER COLOR
    }
}
