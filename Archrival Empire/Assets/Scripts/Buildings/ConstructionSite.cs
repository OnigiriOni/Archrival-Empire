using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : Building
{
    [Header("Building Options")]
    // The name of the building it will become. Must be the same as the name of the prefab.
    public string building;

    [Header("Build Options")]
    // Build time in seconds
    public float buildTimeLeft;


    private void Start()
    {
        SetPlayerStats();
    }

    private void Update()
    {
        if (buildTimeLeft <= 0)
        {
            FinishConstruction();
        }
    }


    /// <summary>
    /// Builds the building.
    /// </summary>
    private void FinishConstruction()
    {
        // Create the new building.
        GameObject gameObject = (GameObject) Instantiate(Resources.Load(building), transform.position, transform.rotation);

        // Set the player of the new building.
        Building finishedBuilding = gameObject.GetComponent<Building>();
        finishedBuilding.player = player;
        finishedBuilding.playerTag = playerTag;

        // Destory this object.
        Destruct();
    }
}
