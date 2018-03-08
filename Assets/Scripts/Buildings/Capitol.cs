using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capitol : MonoBehaviour//: Building
{
    public new string name = "Capitol";
    public int health = 500;
    public int buildTime = 15;

    public ResourceCost buildCost = new ResourceCost(1000, 1000, 1000, 1000);

    public Player player;
    public Cell currentCell;


    public Capitol(Player player, Cell cell)
    {
        this.player = player;
        currentCell = cell;
    }
}
