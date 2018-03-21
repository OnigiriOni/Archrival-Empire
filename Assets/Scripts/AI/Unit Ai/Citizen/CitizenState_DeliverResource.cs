using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState_DeliverResource : State<Citizen>
{
    static readonly CitizenState_DeliverResource instance = new CitizenState_DeliverResource();

    static CitizenState_DeliverResource() { }
    public CitizenState_DeliverResource() { }

    public static CitizenState_DeliverResource Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {

    }

    public override void Execute(Citizen citizen)
    {
        // If the target object is not null the citizen was send to the object by the player
        if (citizen.targetObject != null)
        {
            // Check if the target object is in range.
            if (citizen.perceivedObjectsInRange.Contains(citizen.targetObject))
            {
                // Store the resource.
                citizen.navMeshAgent.ResetPath();
                citizen.player.resources.AddResource(citizen.backpack.RemoveResourceWithDefinition());

                // Check if the citizen has previously collected at a specific resource field.
                if (citizen.targetResource != null && citizen.backpack.currentAmount == 0)
                {
                    // Return to the resource.
                    citizen.ChangeState(CitizenState_GatherResource.Instance);
                }
                else
                {
                    // Check if there is a new resource field around.
                    if (citizen.group != WorkerGroup.Other && citizen.SelectNewResource())
                    {
                        // Collect at the new resource field.
                        citizen.ChangeState(CitizenState_GatherResource.Instance);
                    }
                    else
                    {
                        // Idle if the citizen has not collected a resource previously.
                        citizen.ChangeState(CitizenState_Idle.Instance);
                    }
                }
            }
            else
            {
                // Set the target object as destination.
                citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
            }
        }
        else
        {
            // The target building is unknown or destroyed.

            // Are suitable stores in range?
            citizen.targetObject = FindBuilding(citizen);

            if (citizen.targetObject != null)
            {
                // Set the target object as destination.
                citizen.navMeshAgent.SetDestination(citizen.targetObject.transform.position);
            }
            else
            {
                // There are no suitable buildings.
                citizen.ChangeState(CitizenState_Idle.Instance);
            }
        }
    }

    public override void Exit(Citizen citizen)
    {
        
    }

    private GameObject FindBuilding(Citizen citizen)
    {
        List<Building> buildings = new List<Building>();
        buildings.AddRange(Object.FindObjectsOfType<Building>());

        GameObject targetBuilding = null;
        float shortestDistance = 999F;

        // Are there buildings on the map?
        if (buildings.Count > 0)
        {
            foreach (Building building in buildings)
            {
                // Only friendly buildings.
                if (building.playerTag == citizen.playerTag)
                {
                    // Only buildings that can store the resource.
                    if (building.resourceCapacity.CanStore(citizen.backpack.resourceType))
                    {
                        // Only the closest building.
                        float distance = Vector3.Distance(citizen.transform.position, building.transform.position);
                        if (distance < shortestDistance)
                        {
                            targetBuilding = building.gameObject;
                            shortestDistance = distance;
                        }
                    }
                }
            }
        }

        // Return the target building, null if there is no suitable building.
        return targetBuilding;
    }
}
