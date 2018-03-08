﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ResourceCost
{
    public int food;
    public int wood;
    public int stone;
    public int gold;

    public ResourceCost(int food, int wood, int stone, int gold)
    {
        this.food = food;
        this.wood = wood;
        this.stone = stone;
        this.gold = gold;
    }
}

public class Resources
{
    public int food;
    public int wood;
    public int stone;
    public int gold;

    public Resources(int maxResourceAmount)
    {
        food =  maxResourceAmount;
        wood =  maxResourceAmount;
        stone = maxResourceAmount;
        gold =  maxResourceAmount;
    }
    public Resources(int maxFood, int maxWood, int maxStone, int maxGold)
    {
        food =  maxFood;
        wood =  maxWood;
        stone = maxStone;
        gold =  maxGold;
    }

    public int GetFood(int amount)
    {
        if (food >= amount)
        {
            food -= amount;
            return amount;
        }

        if (food > 0)
        {
            int value = food;
            food -= food;
            return value;
        }

        return 0;
    }
    public int GetWood(int amount)
    {
        if (wood >= amount)
        {
            wood -= amount;
            return amount;
        }

        if (wood > 0)
        {
            int value = wood;
            wood -= wood;
            return value;
        }

        return 0;
    }
    public int GetStone(int amount)
    {
        if (stone >= amount)
        {
            stone -= amount;
            return amount;
        }

        if (stone > 0)
        {
            int value = stone;
            stone -= stone;
            return value;
        }

        return 0;
    }
    public int GetGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return amount;
        }

        if (gold > 0)
        {
            int value = gold;
            gold -= gold;
            return value;
        }

        return 0;
    }
}