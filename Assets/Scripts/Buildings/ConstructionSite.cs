using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : MonoBehaviour
{
    [Header("Building Options")]
    public new string name;
    public float health;
    public PlayerTag playerTag;
    public Player player;
    public string building;

    [Header("Build Options")]
    // Build time in seconds
    public float buildTimeLeft = 9999;

    public void SetPlayerStats()
    {
        // Set the color of the building to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;
    }

    private void Update()
    {
        if (buildTimeLeft <= 0)
        {
            FinishConstruction();
        }
    }

    private void FinishConstruction()
    {
        GameObject gameObject = (GameObject) Instantiate(Resources.Load(building), transform.position, transform.rotation);

        Building finishedBuilding = gameObject.GetComponent<Building>();
        finishedBuilding.player = player;
        finishedBuilding.playerTag = playerTag;

        Destruct();
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Receive damage. The object health goes down.
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destruct();
        }
    }
}
