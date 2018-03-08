using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    public Barrack(Player player)
    {
        name = "Barrack";
        health = 150;
        this.player = player;

        buildCost.food = 0;
        buildCost.wood = 100;
        buildCost.stone = 50;
        buildCost.gold = 0;
        buildTime = 10;
    }

    public bool TrainSoldier()
    {
        //if (player.resources.food >= Object.FindObjectOfType<Soldier>().resourceCost.food)
        //{
        //    return true;
        //}
        return false;
    }
}
