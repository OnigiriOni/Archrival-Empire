using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkGroup
{
    Idle,
    Food,
    Wood,
    Stone,
    Gold
}

public class Citizen : Unit
{
    public WorkGroup workGroup;

    public Citizen(PlayerTag playerTag, Color playerColour)
    {
        health = 100;
        this.playerTag = playerTag;
        this.playerColour = playerColour;
        name = "Citizen";
        buildTime = 3;
        
        resourceCost.food = 50;
        resourceCost.wood = 0;
        resourceCost.stone = 0;
        resourceCost.gold = 0;

        damage.damageToCitizen = 10;
        damage.damageToSoldiers = 5;
        damage.damageToCavalier = 4;
        damage.damageToArtillery = 6;
        damage.damageToBuildings = 1;
    }
}
