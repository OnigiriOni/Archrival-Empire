using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class StrategyManager : MonoBehaviour
{
    // The player that is in charge of the htn.
    public Player player;

    // The enemy of the player.
    public Player enemy;

    HierarchicalTaskNetwork htn;
    Plan plan;

    // The precondition library.
    Preconditions preconditions;

    // The action library.
    Actions actions;

    // The amount of updates per second.
    private float updateFrequency = 0.5F;
    private float updateFrequencyLeft = 0;


    private void Start()
    {
        plan = new Plan();
        preconditions = new Preconditions();
        actions = new Actions();

        CreateTree();
    }

    private void Update()
    {
        updateFrequencyLeft -= Time.deltaTime;
        if (updateFrequencyLeft <= 0)
        {
            CreatePlan();

            ExecutePlan();

            updateFrequencyLeft = updateFrequency;
        }
    }


    /// <summary>
    /// Traverses the tree and adds actions to the plan.
    /// </summary>
    private void CreatePlan()
    {
        plan = htn.Execute();
        
        PrintPlan();
    }

    /// <summary>
    /// Prints the plan in the console. Debug.
    /// </summary>
    public void PrintPlan()
    {
        List<Action> actions = new List<Action>();

        actions = plan.GetPlan();

        for (int i = 0; i < actions.Count; i++)
        {
            Debug.Log(actions[i].actionDelegate.Method.Name);
        }
    }

    /// <summary>
    /// Calls all methods that are in the plan.
    /// </summary>
    private void ExecutePlan()
    {
        List<Action> actions = new List<Action>();

        actions = plan.GetPlan();

        for (int i = 0; i < actions.Count; i++)
        {
            // The necessary object from the previous task.
            //actions[i].Execute(this);
        }
    }

    /// <summary>
    /// Creates the tree that is used to represent the htn.
    /// </summary>
    private void CreateTree()
    {
        // Preconditions
        Precondition enoughFood = new Precondition(preconditions.IsFoodLessThan);

        // Actions
        ActionDelegate buildCitizenAction = new ActionDelegate(actions.BuildCitizen);

        // Abstract Tasks
        Task createCitizen = new AbstractTask(this);
        createCitizen.AddPrecondition(enoughFood);

        // Primitive Tasks
        Task buildCitizenT = new PrimitiveTask(this)
        {
            action = new Action(5F, buildCitizenAction)
        };

        // Create tree structure
        createCitizen.AddSubtask(buildCitizenT);

        htn = new HierarchicalTaskNetwork(createCitizen);
    }
}
