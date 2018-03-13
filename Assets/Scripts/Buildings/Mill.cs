using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : MonoBehaviour
{
    public new string name = "Mill";
    public int health = 100;
    public int buildTime = 6;
    public BuildCost buildCost;// = new BuildCost(0, 100, 0, 0);
    public Player player;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }

}
