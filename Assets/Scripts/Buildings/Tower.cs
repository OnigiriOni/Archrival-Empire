using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    [Header("Tower Options")]
    public Damage damage;

    // Cooldown in seconds
    public float damageCooldown;
    public float damageCooldownLeft;

    /// <summary>
    /// Deal damage to an object
    /// </summary>
    public void Attack(GameObject gameObject) // TODO: I need something
    {
        //gameObject.TakeDamage(damage);
    }
}
