using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class Plan
{
    // The plan. Already ordered.
    List<Action> actionPlan;

    public Plan()
    {
        actionPlan = new List<Action>();
    }

    /// <summary>
    /// Adds a action to the plan.
    /// </summary>
    /// <param name="action">The action that will be added.</param>
    public void AddActionToPlan(Action action)
    {
        actionPlan.Add(action);
    }

    /// <summary>
    /// Returns the ordered plan.
    /// </summary>
    /// <returns>The ordered plan.</returns>
    public List<Action> GetPlan()
    {
        return actionPlan;
    }
}