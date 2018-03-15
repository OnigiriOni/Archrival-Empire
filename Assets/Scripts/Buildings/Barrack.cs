using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    /// <summary>
    /// Spawn a Soldier Prefab
    /// </summary>
    public void TrainSoldier()
    {
        //TODO: Soldiers cost resources, but it is not considered
        // Set the spawn postion (in front of the entrance)
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += 5.5F;

        // Loads the Prefab from the "Resources" folder
        GameObject soldier = (GameObject) Instantiate(Resources.Load("Soldier"), spawnPosition, transform.rotation);

        // Takes only the first Children and its first Material
        soldier.GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //new Color(0, 116, 255, 255); //TODO: MAKE THIS THE PLAYER COLOR
    }
}
