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
        // Are suitable stores in range?
        GameObject building = FindBuilding(citizen);

        if (building == null)
        {
            citizen.navMeshAgent.ResetPath();
            citizen.ChangeState(CitizenState_Idle.Instance);
        }
        else if (!citizen.perceivedObjects.Contains(building))
        {
            citizen.navMeshAgent.SetDestination(building.transform.position);
            citizen.targetGameObject = building;
        }
    }

    public override void Execute(Citizen citizen)
    {
        ///////////////////////////////////////
        // Preconditions
        ///////////////////////////////////////

        // Return to gathering the resource if the backpack is empty
        if (citizen.backpack.currentAmount == 0)
        {
            citizen.ChangeState(CitizenState_GatherResource.Instance);
        }

        // Check if the building disappeared because the enemy shot it down!
        if (citizen.targetGameObject == null)
        {
            // Are suitable stores in range?
            GameObject building = FindBuilding(citizen);

            if (building == null)
            {
                citizen.navMeshAgent.ResetPath();
                citizen.ChangeState(CitizenState_Idle.Instance);
            }
            else if (!citizen.perceivedObjects.Contains(building))
            {
                citizen.navMeshAgent.SetDestination(building.transform.position);
                citizen.targetGameObject = building;
            }
        }

        ///////////////////////////////////////
        // Action
        ///////////////////////////////////////

        // Deliver the resources if the building is in range.
        if (citizen.perceivedObjects.Contains(citizen.targetGameObject))
        {
            citizen.navMeshAgent.ResetPath();
            citizen.player.resources.AddResource(citizen.backpack.RemoveResourceWithDefinition());
        }
        else
        {
            // Move to the target building.
            if (citizen.targetGameObject != null && citizen.navMeshAgent.destination != citizen.targetGameObject.transform.position)
            {
                citizen.navMeshAgent.SetDestination(citizen.targetGameObject.transform.position);
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
        if (buildings.Count >= 1)
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
