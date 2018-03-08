using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Building
{
    public Tower(Player player)
    {
        name = "Tower";
        health = 150;
        this.player = player;

        buildCost.food = 0;
        buildCost.wood = 0;
        buildCost.stone = 150;
        buildCost.gold = 0;
        buildTime = 10;
    }
}
