using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    // Enter is called the time the state gets initialized
    public abstract void Enter(T actor);

    // Execute runs every frame
    public abstract void Execute(T actor);

    // Exit is called the time the state gets switched
    public abstract void Exit(T actor);
}
