using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private new string name = "Soldier";
    private float health = 100F;
    private float fireDelay = 0.5F;
    private Damage damage;// = new Damage(50F, 40F, 40F, 40F, 10F);
    private BuildCost buildCost;// = new BuildCost(50, 0, 0, 25);
    private float buildTime = 5F; // 5 seconds
    private Player player;

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
