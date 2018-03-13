using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_HarvestFood : State<Citizen>
{
    static readonly State_HarvestFood instance = new State_HarvestFood();

    static State_HarvestFood() { }
    public State_HarvestFood() { }

    public static State_HarvestFood Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        // Checks if the Citizen is at a resource field
        if (!citizen.isFoodResourceAvailable)
        {
            // Walk to the target resource field
            citizen.navMeshAgent.destination = citizen.targetResource.gameObject.transform.position;
        }
    }

    public override void Execute(Citizen citizen)
    {
        
        // Checks if the Backpack is full
        int freeSpace = citizen.backpack.GetFreeSpace();
        if (freeSpace <= 0)
        {
            
            //citizen.navMeshAgent.destination = //nearest mill
        }

        // Harvest the food if the cooldown is up
        citizen.resourceCollectionCooldownLeft -= Time.deltaTime;
        if (citizen.resourceCollectionCooldownLeft <= 0)
        {
            citizen.resourceCollectionCooldownLeft = citizen.resourceCollectionCooldown;

            // Takes as much food as the citizen can or there is space in the backpack.
            int collectionAmount = citizen.resourceCollectionPerTick;
            if (freeSpace < collectionAmount)
            {
                collectionAmount = freeSpace;
            }
            //citizen.backpack.AddFood(citizen.targetResource.GetAmount(collectionAmount));
        }
    }

    public override void Exit(Citizen citizen)
    {
        
    }

}
