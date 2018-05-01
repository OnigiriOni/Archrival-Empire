using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public abstract class Task
{
    // The strategy manager holds all information.
    public StrategyManager strategyManager;

    public bool isPrimitiveTask;
    public Task parentTask;
    
    // Holds all preconditions of this task. Abstract only.
    public List<Precondition> preconditions;

    // Holds all subtasks of this task. Abstract only.
    public List<Task> subtasks;

    // Holds the action of this task. Primitive only.
    public Action action;

    public abstract bool CheckPreconditions();
    public abstract void AddPrecondition(Precondition precondition);
    public abstract void AddSubtask(Task subtask);
}