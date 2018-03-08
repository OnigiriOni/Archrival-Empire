using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{
    public int damageToCitizen;
    public int damageToSoldiers;
    public int damageToCavalier;
    public int damageToArtillery;
    public int damageToBuildings;
}

public class Unit
{
    public string name;
    public float health;
    public float buildTime;

    public PlayerTag playerTag;
    public Color playerColour;
    
    public ResourceCost resourceCost;
    public Damage damage;

    public Cell currentCell;

    public void MoveTo(Cell cell)
    {
        if (cell == currentCell) return;

        currentCell.units2.Remove(this);
        //PATHFINDING IS NECESSARY FOR THIS!!!!!!!!!!!
        //timer ->
         float time;
        currentCell.connectedCells.TryGetValue(cell, out time);
        //timer <-
        cell.units2.Add(this);
        currentCell = cell;
    }

    public void Attack() //Soldiers or whatever
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
