using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class China : Civilization
{
    //The construction of Citizen cost 20% less food.
    public China()
    {
        name = "China";
        citizenCostReductionPercent = 20;
    }
}
