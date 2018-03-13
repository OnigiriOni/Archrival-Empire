using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitol : MonoBehaviour
{
    private new string name = "Capitol";
    private float health = 500;
    private BuildCost buildCost;// = new BuildCost(300, 300, 300, 300);
    private float buildTime = 15; // 15 seconds
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
    /// Spawn a Citizen Prefab
    /// </summary>
    public void TrainCitizen()
    {
        // Set the spawn postion (in front of the entrance)
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += 5.5F;

        // Loads the Prefab from the "Resources" folderw
        GameObject citizen = (GameObject) Instantiate(Resources.Load("Citizen"), spawnPosition, transform.rotation);

        // Takes only the first Children and its first Material
        citizen.GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //new Color(0, 116, 255, 255); //TODO: MAKE THIS THE PLAYER COLOR
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
