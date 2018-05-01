using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class PrimitiveTask : Task
{
    public PrimitiveTask(StrategyManager strategyManager)
    {
        isPrimitiveTask = true;
        parentTask = null;
        this.strategyManager = strategyManager;

        preconditions = new List<Precondition>();
        action = null;
    }

    /// <summary>
    /// Does nothing because this task type has no preconditions.
    /// </summary>
    /// <returns>True</returns>
    public override bool CheckPreconditions()
    {
        return true;
    }

    /// <summary>
    /// Does nothing because this task type has no preconditions.
    /// </summary>
    /// <param name="precondition">The precondition that will not be added.</param>
    public override void AddPrecondition(Precondition precondition)
    {

    }

    /// <summary>
    /// Does nothing because this task type has no subtasks.
    /// </summary>
    /// <param name="subtask">The subtask that will not be added.</param>
    public override void AddSubtask(Task subtask)
    {

    }
}