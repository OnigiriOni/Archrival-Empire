using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germany : Civilization
{
    //The construction of military units cost 10% less resources.

    public Germany()
    {
        name = "Germany";
        militaryCostReductionPercent = 10;
    }
}
