using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldmine : Building
{
    public Goldmine(Player player)
    {
        name = "Goldmine";
        health = 100;
        this.player = player;

        buildCost.food = 0;
        buildCost.wood = 150;
        buildCost.stone = 0;
        buildCost.gold = 0;
        buildTime = 6;
    }
}
