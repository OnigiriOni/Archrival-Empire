using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private new string name = "Tower";
    private float health = 150;
    private float fireDelay = 0.5F;
    private Damage damage;// = new Damage(80F, 49F, 30F, 60F, 10F);
    private BuildCost buildCost;// = new BuildCost(0, 0, 150, 0);
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
    /// Deal damage to an object
    /// </summary>
    public void Attack(GameObject gameObject) // TODO: I need something
    {
        //gameObject.TakeDamage(damage);
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
