using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    // The object that uses the state machine.
    private T actor;

    // The state that is currently executed.
    private State<T> currentState;

    // The state that was previously executed.
    private State<T> previousState;


    public void Start()
    {
        currentState = null;
        previousState = null;
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Execute(actor);
        }
    }


    /// <summary>
    /// Sets the initial state.
    /// </summary>
    /// <param name="actor">The object that uses the state machine.</param>
    /// <param name="initialState">The initial state.</param>
    public void Initialize(T actor, State<T> initialState)
    {
        this.actor = actor;
        SetState(initialState);
    }

    /// <summary>
    /// Sets a new state to be active.
    /// </summary>
    /// <param name="state">The new state.</param>
    public void SetState(State<T> state)
    {
        previousState = currentState;
        currentState = state;

        if (previousState != null)
        {
            previousState.Exit(actor);
        }
        if (currentState != null)
        {
            currentState.Enter(actor);
        }
    }
}
