using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GatherResource : State<Citizen>
{
    static readonly State_GatherResource instance = new State_GatherResource();

    static State_GatherResource() { }
    public State_GatherResource() { }

    public static State_GatherResource Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        // Is the backpack full of the target resource?
        if (citizen.backpack.resourceType == citizen.targetResource.resourceType)
        {
            if (citizen.backpack.IsFull())
            {
                // Bring the resource to the nearest store.
                citizen.ChangeState(State_DeliverResource.Instance);
            }
        }

        // Is the target resource not in range?
        if (!citizen.perceivedObjects.Contains(citizen.targetResource.gameObject))
        {
            // Move to the target resource
            citizen.navMeshAgent.SetDestination(citizen.targetResource.transform.position);
        }
    }

    public override void Execute(Citizen citizen)
    {
        ///////////////////////////////////////
        // Preconditions
        ///////////////////////////////////////

        // Is the backpack full?
        if (citizen.backpack.IsFull())
        {
            // Bring the resource to the nearest store.
            citizen.ChangeState(State_DeliverResource.Instance);
        }

        // Check if the resource disappeared because there are no resources in the object anymore!
        // May cause failures if the destroyed object is not null.
        if (citizen.targetResource == null)
        {
            citizen.ChangeState(State_Idle.Instance);
        }

        ///////////////////////////////////////
        // Action
        ///////////////////////////////////////

        // Gather the resource if the target resource is in range
        if (citizen.targetResource != null && citizen.perceivedObjects.Contains(citizen.targetResource.gameObject))
        {
            citizen.navMeshAgent.ResetPath();
            GatherResource(citizen);
            Debug.Log(citizen.name + " :: " + citizen.backpack.currentAmount);
        }
        else
        {
            // Move to the target resource
            if (citizen.targetResource != null && citizen.navMeshAgent.destination != citizen.targetResource.transform.position)
            {
                citizen.navMeshAgent.SetDestination(citizen.targetResource.transform.position);
            }
        }
    }

    public override void Exit(Citizen citizen)
    {
        
    }

    private void GatherResource(Citizen citizen)
    {
        // Check if the collection cooldown is up.
        if (citizen.resourceCollectionCooldownLeft <= 0)
        {
            citizen.resourceCollectionCooldownLeft = citizen.resourceCollectionCooldown;

            // Calculate the amount that can be gathered.
            int collectionAmount = citizen.resourceCollectionPerTick;
            int freeSpace = citizen.backpack.GetFreeSpace();

            if (freeSpace < collectionAmount)
            {
                collectionAmount = freeSpace;
            }

            // Gather the resource.
            citizen.backpack.AddResource(citizen.targetResource.GatherResource(collectionAmount), citizen.targetResource.resourceType);
        }
    }
}
