using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_GatherResource : State<Citizen>
{
    static readonly CitizenState_GatherResource instance = new CitizenState_GatherResource();

    static CitizenState_GatherResource() { }
    public CitizenState_GatherResource() { }

    public static CitizenState_GatherResource Instance
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
                citizen.ChangeState(CitizenState_DeliverResource.Instance);
            }
        }
    }

    public override void Execute(Citizen citizen)
    {
        // Is the backpack full?
        if (citizen.backpack.IsFull())
        {
            // Bring the resource to the nearest store.
            citizen.ChangeState(CitizenState_DeliverResource.Instance);
        }

        // Check if the resource disappeared because there are no resources in the object anymore.
        if (citizen.targetResource == null)
        {
            // Check for other resources of the same type in the area.
            if (!citizen.SelectNewResource())
            {
                // Deliver all current resources in the backpack if there are no other resources around.
                if (citizen.targetResource == null && citizen.backpack.GetFreeSpace() > 0)
                {
                    citizen.ChangeState(CitizenState_DeliverResource.Instance);
                }
                else
                {
                    citizen.ChangeState(CitizenState_Idle.Instance);
                }
                
            }
        }

        // Gather the resource if the target resource is in range
        if (citizen.targetResource != null && citizen.perceivedObjectsInRange.Contains(citizen.targetResource.gameObject))
        {
            citizen.navMeshAgent.ResetPath();
            GatherResource(citizen);
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
