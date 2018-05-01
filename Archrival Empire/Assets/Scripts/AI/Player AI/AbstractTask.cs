using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public delegate bool Precondition(StrategyManager strategyManager);

public class AbstractTask : Task
{
    public AbstractTask(StrategyManager strategyManager)
    {
        isPrimitiveTask = false;
        parentTask = null;
        this.strategyManager = strategyManager;

        preconditions = new List<Precondition>();
        subtasks = new List<Task>();
        action = null;
    }

    /// <summary>
    /// Checks all preconditions if they are true or not.
    /// </summary>
    /// <returns>True if all precondition are satisfied.</returns>
    public override bool CheckPreconditions()
    {
        for (int i = 0; i < preconditions.Count; i++)
        {
            if (preconditions[i].Invoke(strategyManager) == false)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Adds a precondition to the list.
    /// </summary>
    /// <param name="precondition">The precondition that has to be added.</param>
    public override void AddPrecondition(Precondition precondition)
    {
        preconditions.Add(precondition);
    }

    /// <summary>
    /// Adds a subtask to the list.
    /// </summary>
    /// <param name="subtask">The subtask that has to be added.</param>
    public override void AddSubtask(Task subtask)
    {
        subtask.parentTask = this;
        subtasks.Add(subtask);
    }
}