using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct Damage
{
    public float toCitizen;
    public float toSoldiers;
    public float toCavalier;
    public float toArtillery;
    public float toBuildings;
}

public class Unit : MonoBehaviour
{
    [Header("Unit Options")]
    public new string name;
    public float health;
    public PlayerTag playerTag;
    public Player player;
    public NavMeshAgent navMeshAgent;

    [Header("Build Options")]
    public BuildCost buildCost;

    // Build time in seconds
    public float buildTime;

    [Header("Combat Options")]
    public Damage damage;

    // Cooldown in seconds
    public float damageCooldown;
    public float damageCooldownLeft;



    //    //public void MoveTo(Cell cell)
    //    //{
    //    //    if (cell == currentCell) return;

    //    //    currentCell.units2.Remove(this);
    //    //    //PATHFINDING IS NECESSARY FOR THIS!!!!!!!!!!!
    //    //    //timer ->
    //    //     float time;
    //    //    currentCell.connectedCells.TryGetValue(cell, out time);
    //    //    //timer <-
    //    //    cell.units2.Add(this);
    //    //    currentCell = cell;
    //    //}

    //    public void Attack() //Soldiers or whatever
    //    {

    //    }

    //    public void TakeDamage(int damage)
    //    {
    //        health -= damage;
    //        if (health <= 0)
    //        {
    //            Die();
    //        }
    //    }

}
