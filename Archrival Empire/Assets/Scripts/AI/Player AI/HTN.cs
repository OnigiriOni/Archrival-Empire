using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class HierarchicalTaskNetwork
{
    Task initialTask;
    Task currentTask;
    Plan plan;

    public HierarchicalTaskNetwork(Task initialTask)
    {
        this.initialTask = initialTask;
        currentTask = initialTask;
        plan = new Plan();
    }

    /// <summary>
    /// Traverses the tree and creates a plan.
    /// </summary>
    /// <returns>The plan.</returns>
    public Plan Execute()
    {
        Perform(initialTask);
        return plan;
    }

    /// <summary>
    /// Traverse the tree and checks all preconditions and/or adds actions to the plan.
    /// </summary>
    /// <param name="currentTask">The tasks that is currently checked.</param>
    private void Perform(Task currentTask)
    {
        // Check if the task is primitive.
        if (!currentTask.isPrimitiveTask)
        {
            // Check the preconditions of the abstract task
            if (currentTask.CheckPreconditions())
            {
                // Update the predicted world state.

                // Traverse all subtasks.
                for (int i = 0; i < currentTask.subtasks.Count; i++)
                {
                    Perform(currentTask.subtasks[i]);
                }
            }
        }
        else
        {
            // Add the action of the primitive task to the plan.
            plan.AddActionToPlan(currentTask.action);
        }
    }
}