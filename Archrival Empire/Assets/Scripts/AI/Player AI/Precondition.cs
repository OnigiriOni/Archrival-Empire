using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class Preconditions
{
    public bool IsFoodLessThan(StrategyManager strategyManager)
    {
        if (strategyManager.player.resources.food >= 50)
        {
            return true;
        }
        return false;
    }
}
