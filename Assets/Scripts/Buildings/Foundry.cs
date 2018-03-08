using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundry : Building
{
    public Foundry(Player player)
    {
        name = "Foundry";
        health = 150;
        this.player = player;

        buildCost.food = 0;
        buildCost.wood = 200;
        buildCost.stone = 50;
        buildCost.gold = 50;
        buildTime = 10;
    }
}
