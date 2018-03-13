using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The resource type to distinguish between resources.
public enum ResourceType
{
    Food,
    Wood,
    Stone,
    Gold
}

// Use this struct to transfer an amount of resources with the coresponding ResourceType.
public struct ResourceTransferStruct
{
    public int resourceAmount;
    public ResourceType resourceType;
}

public class Resource : MonoBehaviour
{
    public ResourceType resourceTag;
    public int amount;

    /// <summary>
    /// Transfer an amount of resources from this object to another object by using this method.
    /// </summary>
    /// <param name="amount">The amount to be removed</param>
    /// <returns>the actual amount as int.</returns>
    public int GatherResource(int amount)
    {
        if (this.amount < amount)
        {
            int value = this.amount;
            this.amount = 0;
            return value;
        }
        this.amount -= amount;
        return amount;
    }

    private void Update()
    {
        // If the resource object is empty, destroy it.
        if (amount <= 0)
        {
            Destroy(this);
        }
    }
}
