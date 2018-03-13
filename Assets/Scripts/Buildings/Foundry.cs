using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : MonoBehaviour
{
    private new string name = "Foundry";
    private float health = 150;
    private BuildCost buildCost;// = new BuildCost(0, 200, 50, 50);
    private float buildTime = 10; // 10 seconds
    private Player player;

    public string Name
    {
        get { return name; }
    }
    public float Health
    {
        get { return health; }
    }
    public BuildCost BuildCost
    {
        get { return buildCost; }
    }
    public float BuildTime
    {
        get { return buildTime; }
    }
    public Player Player
    {
        get { return player; }
    }

    /// <summary>
    /// Spawn a Artillery Prefab
    /// </summary>
    public void TrainArtillery()
    {
        // Set the spawn postion (in front of the entrance)
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += 8F;

        // Loads the Prefab from the "Resources" folder
        GameObject artillery = (GameObject)Instantiate(Resources.Load("Artillery"), spawnPosition, transform.rotation);

        // Takes only the first Children and its first Material
       artillery.GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //new Color(0, 116, 255, 255); //TODO: MAKE THIS THE PLAYER COLOR
    }

    /// <summary>
    /// Receive damage. The object health goes down
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this);
        }
    }
}
