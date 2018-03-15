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

    // Resource collection in seconds.
    public float resourceCollectionCooldown = 1F;
    public float resourceCollectionCooldownLeft = 0F;

    private StateMaschine<Citizen> stateMaschine;

    // The target resource is accessed in the State_GatherResource.
    public Resource targetResource;

    // The target gameobject is sccessed is the State_Move.
    public GameObject targetGameObject;

    // The perceived objects are used for state checks.
    public List<GameObject> perceivedObjects;


    private void Start()
    {
        // Set the color of the unit to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;

        stateMaschine = new StateMaschine<Citizen>();
        stateMaschine.Initialize(this, State_Idle.Instance);

        navMeshAgent = GetComponent<NavMeshAgent>();

        perceivedObjects = new List<GameObject>();


        // Test functions
        //GatherResource(GameObject.Find("Resource_Food").GetComponent<Resource>());
    }

    private void Update()
    {
        stateMaschine.Update();

        // Update the cooldowns
        resourceCollectionCooldownLeft -= Time.deltaTime;
        damageCooldownLeft -= Time.deltaTime;

        if (resourceCollectionCooldownLeft < 0)
        {
            resourceCollectionCooldownLeft = 0;
        }
        if (damageCooldownLeft < 0)
        {
            damageCooldownLeft = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!perceivedObjects.Contains(other.gameObject))
        {
            perceivedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (perceivedObjects.Contains(other.gameObject))
        {
            perceivedObjects.Remove(other.gameObject);
        }
    }

    public void ChangeState(State<Citizen> state)
    {
        stateMaschine.SetState(state);
    }

    public void GatherResource(Resource resource)
    {
        targetResource = resource;
        stateMaschine.SetState(State_GatherResource.Instance);
        group = SetWorkerGroup(resource);
    }

    private WorkerGroup SetWorkerGroup(Resource resource)
    {
        switch (resource.resourceType)
        {
            case ResourceType.Food: return WorkerGroup.GatherFood;
            case ResourceType.Wood: return WorkerGroup.GatherWood;
            case ResourceType.Stone: return WorkerGroup.GatherStone;
            case ResourceType.Gold: return WorkerGroup.GatherGold;
        }
        return WorkerGroup.Other;
    }

    public void MoveTo(Vector3 targetLocation)
    {
        navMeshAgent.SetDestination(targetLocation);
        group = WorkerGroup.Other;
    }

    public void Build(GameObject building)
    {
        targetGameObject = building;
        stateMaschine.SetState(State_Build.Instance);
        group = WorkerGroup.Builder;
    }
}
