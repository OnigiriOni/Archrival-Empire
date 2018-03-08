using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit
{
    public Soldier(PlayerTag playerTag, Color playerColour)
    {
        this.playerTag = playerTag;
        this.playerColour = playerColour;
        name = "Soldier";
        buildTime = 5;

        resourceCost.food = 50;
        resourceCost.wood = 0;
        resourceCost.stone = 0;
        resourceCost.gold = 25;

        health = 100;

        damage.damageToCitizen = 50;
        damage.damageToSoldiers = 40;
        damage.damageToCavalier = 40;
        damage.damageToArtillery = 40;
        damage.damageToBuildings = 10;
    }
}
