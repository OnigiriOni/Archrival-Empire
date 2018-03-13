using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum WorkerGroup
{
    Idle,
    GatherFood,
    GatherWood,
    GatherStone,
    GatherGold,
    Builder,
    Other
}

public class Backpack
{
    public int maxCarryCapacity;
    public int currentAmount;
    public ResourceType resourceType;

    /// <summary>
    /// Create a new backpack to store resources.
    /// </summary>
    /// <param name="maxCarryCapacity">The capacity of the backpack.</param>
    public Backpack(int maxCarryCapacity)
    {
        this.maxCarryCapacity = maxCarryCapacity;
        currentAmount = 0;
        resourceType = ResourceType.Food;
    }

    /// <summary>
    /// Create a new backpack to store resources.
    /// </summary>
    /// <param name="maxCarryCapacity">The capacity of the backpack.</param>
    /// <param name="currentAmount">The amount of resources in the backpack.</param>
    /// <param name="resourceType">The type of the current resource in the backpack.</param>
    public Backpack(int maxCarryCapacity, int currentAmount, ResourceType resourceType)
    {
        this.maxCarryCapacity = maxCarryCapacity;
        this.currentAmount = currentAmount;
        this.resourceType = resourceType;
    }

    /// <summary>
    /// Returns if the backpack is full.
    /// </summary>
    /// <returns>true if the backpack is full.</returns>
    public bool IsFull()
    {
        if (currentAmount >= maxCarryCapacity)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns the available space in the backpack.
    /// </summary>
    /// <returns>the available space as int.</returns>
    public int GetFreeSpace()
    {
        return maxCarryCapacity - currentAmount;
    }

    /// <summary>
    /// Add resources to the backpack. If the new ResourceType is different, the current resources are lost.
    /// </summary>
    /// <param name="amount">The amount that gets stored. Normaly the citizen collection capacity.</param>
    /// <param name="resourceType">The type of the resource, decides if the backpack gets cleared.</param>
    public void AddResource(int amount, ResourceType resourceType)
    {
        if (this.resourceType == resourceType)
        {
            // Fill the backpack with the amount of resource, if there is space left.
            int freeSpace = GetFreeSpace();

            if (freeSpace < amount)
            {
                currentAmount += freeSpace;
            }
            else
            {
                currentAmount += amount;
            }
        }
        else
        {
            // Discard the backpack for the new resource. Note that current resources are lost.
            currentAmount = 0;
            this.resourceType = resourceType;

            int freeSpace = GetFreeSpace();

            if (freeSpace < amount)
            {
                currentAmount += freeSpace;
            }
            else
            {
                currentAmount += amount;
            }
        }
    }

    /// <summary>
    /// Returns the amount of resources that are removed from the backpack.
    /// </summary>
    /// <returns>the amount of removed resources as int.</returns>
    public int RemoveResource()
    {
        int value = currentAmount;
        currentAmount = 0;
        return value;
    }

    /// <summary>
    /// Returns the amount of resources that are removed from the backpack with the coresponding ResourceType.
    /// </summary>
    /// <returns>the amount of removed resources and the ResourceType as struct.</returns>
    public ResourceTransferStruct RemoveResourceWithDefinition()
    {
        ResourceTransferStruct res;
        res.resourceAmount = currentAmount;
        res.resourceType = resourceType;

        currentAmount = 0;
        return res;
    }
}

public class Citizen : Unit
{
    [Header("Citizen Options")]
    public WorkerGroup group = WorkerGroup.Idle;
    public Backpack backpack = new Backpack(15);
    public int resourceCollectionPerTick = 1;

    // Resource collection in seconds
    public float resourceCollectionCooldown = 1F;
    public float resourceCollectionCooldownLeft = 0F;



    public StateMaschine<Citizen> stateMaschine;

    public Resource targetResource;

    public bool isFoodResourceAvailable;
    public bool isWoodResourceAvailable;
    public bool isStoneResourceAvailable;
    public bool isGoldResourceAvailable;

    public bool isMillAvailable;
    public bool isSawmillAvailable;
    public bool isMineAvailable;


    private void Start()
    {
        // Set the color of the unit to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;

        stateMaschine.Initialize(this, State_Idle.Instance);


    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Resource_Food"))
        {
            isFoodResourceAvailable = true;
        }
        if (other.tag.Equals("Resource_Wood"))
        {
            isWoodResourceAvailable = true;
        }
        if (other.tag.Equals("Resource_Stone"))
        {
            isStoneResourceAvailable = true;
        }
        if (other.tag.Equals("Resource_Gold"))
        {
            isGoldResourceAvailable = true;
        }

        if (other.name.Equals("Mill"))
        {
            isMillAvailable = true;
        }
        if (other.name.Equals("Sawmill"))
        {
            isSawmillAvailable = true;
        }
        if (other.name.Equals("Mine"))
        {
            isMineAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Resource_Food"))
        {
            isFoodResourceAvailable = false;
        }
        if (other.tag.Equals("Resource_Wood"))
        {
            isWoodResourceAvailable = false;
        }
        if (other.tag.Equals("Resource_Stone"))
        {
            isStoneResourceAvailable = false;
        }
        if (other.tag.Equals("Resource_Gold"))
        {
            isGoldResourceAvailable = false;
        }

        if (other.name.Equals("Mill"))
        {
            isMillAvailable = false;
        }
        if (other.name.Equals("Sawmill"))
        {
            isSawmillAvailable = false;
        }
        if (other.name.Equals("Mine"))
        {
            isMineAvailable = false;
        }
    }

    /// <summary>
    /// Receive damage. The object health goes down
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this);
        }
    }

    public void ChangeState(State<Citizen> state)
    {
        stateMaschine.SetState(state);
    }

    public void GatherResource(Resource resource)
    {
        stateMaschine.SetState(State_GatherResource.Instance);
    }

    public void MoveTo(Vector3 targetLocation)
    {
        group = WorkerGroup.Other;
        navMeshAgent.destination = targetLocation;
        stateMaschine.SetState(State_Move.Instance);
    }
}
