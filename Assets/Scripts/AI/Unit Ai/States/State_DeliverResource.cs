using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_DeliverResource : State<Citizen>
{
    static readonly State_DeliverResource instance = new State_DeliverResource();

    static State_DeliverResource() { }
    public State_DeliverResource() { }

    public static State_DeliverResource Instance
    {
        get { return instance; }
    }


    public override void Enter(Citizen citizen)
    {
        // Checks if the Citizen is at a Mill
        if (!citizen.isMillAvailable)
        {
            Mill mill = FindClosestMill(citizen);

            if (mill)
            {
                // Walk to the nearest mill
                citizen.navMeshAgent.destination = mill.transform.position;
            }
            else
            {
                // Idle because there is no mill
                citizen.ChangeState(State_Idle.Instance);
            }
        }
    }

    public override void Execute(Citizen citizen)
    {
        // Return to harvesting if the backpack is empty
        //if (citizen.backpack.Food <= 0)
        //{
            
        //}

        // give the player the resource and remove it from the citizen
        //citizen.player.resources.food += citizen.backpack.RemoveFood();
    }

    public override void Exit(Citizen citizen)
    {
        Debug.Log("Time to work");
    }

    private Mill FindClosestMill(Citizen citizen)
    {
        List<Mill> mills = new List<Mill>();
        mills.AddRange(Object.FindObjectsOfType<Mill>());

        if (mills.Count >= 1)
        {
            // Just the mills for the same player
            foreach (Mill mill in mills)
            {
                if (mill.player.playerTag != citizen.player.playerTag)
                {
                    mills.Remove(mill);
                }
            }

            // Does any friendly mill exist?
            if (mills.Count >= 1)
            {
                // Find the closest mill
                Mill targetMill = null;
                float shortestDistance = 999F; // As long as the map is not larger than 999
                float distance;

                foreach (Mill mill in mills)
                {
                    distance = Vector3.Distance(citizen.transform.position, mill.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        targetMill = mill;
                    }
                }

                // The nearest and friendliest mill in the whole world
                return targetMill;
            }

            // There is no mill for the player in the world
            return null;
        }

        // There are no mills for both players
        return null;
    }

}
