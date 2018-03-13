using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMaschine<T>
{
    private T actor;

    private State<T> currentState;
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

    public void Initialize(T actor, State<T> initialState)
    {
        this.actor = actor;
        SetState(initialState);
    }

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
