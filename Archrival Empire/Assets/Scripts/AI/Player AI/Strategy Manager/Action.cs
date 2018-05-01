using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public delegate object[] ActionDelegate(StrategyManager strategyManager, object[] data);

public class Action
{
    // The duration of this task until the world state is changed.
    public float duration;

    // The method that is called in the plan.
    public ActionDelegate actionDelegate;

    public Action(float duration, ActionDelegate actionDelegate)
    {
        this.duration = duration;
        this.actionDelegate = actionDelegate;
    }

    /// <summary>
    /// Calls the store action method.
    /// </summary>
    /// <param name="strategyManager">The strategy manager of the player.</param>
    /// <param name="data">The necessary information to call this method.</param>
    /// <returns>Returns information that can be essential to other methods.</returns>
    public object[] Execute(StrategyManager strategyManager, object[] data)
    {
        return actionDelegate.Invoke(strategyManager, data);
    }
}