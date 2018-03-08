using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
    private HashSet<KeyValuePair<string, object>> preconditions;
    private HashSet<KeyValuePair<string, object>> effects;

    public float cost;
    public object target;

    #region Properties

    public HashSet<KeyValuePair<string, object>> Preconditions
    {
        get { return preconditions; }
    }

    public HashSet<KeyValuePair<string, object>> Effects
    {
        get { return effects; }
    }

    #endregion

    public Action()
    {
        preconditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public void AddPrecondition(string key, object value)
    {
        preconditions.Add(new KeyValuePair<string, object>(key, value));
    }

    public void AddEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }

    public void RemovePrecondition(string key)
    {
        foreach (KeyValuePair<string, object> keyValuePair in preconditions)
        {
            if (keyValuePair.Key.Equals(key))
            {
                preconditions.Remove(keyValuePair);
            }
        }
    }

    public void RemoveEffect(string key)
    {
        foreach (KeyValuePair<string, object> keyValuePair in effects)
        {
            if (keyValuePair.Key.Equals(key))
            {
                effects.Remove(keyValuePair);
            }
        }
    }

    public abstract bool Execute();
}
