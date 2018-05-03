using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The WorkerGroup is used to identify citizen actions.
public enum WorkerGroup
{
    Idle,           // The citizen does nothing.
    GatherFood,     // The citizen is collecting food.
    GatherWood,     // The citizen is collecting wood.
    GatherStone,    // The citizen is collecting stone.
    GatherGold,     // The citizen is collecting gold.
    Builder,        // The citizen is building.
    Fighter,        // The citizen is in combat.
    Other           // The citizen is just walking.
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
        if (this.resourceType != resourceType)
        {
            // Discard the backpack for the new resource. Note that current resources are lost.
            currentAmount = 0;
            this.resourceType = resourceType;
        }

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

    public float perceptionRange = 15F;

    // Resource collection in seconds.
    public float resourceCollectionCooldown = 1F;
    [System.NonSerialized]
    public float resourceCollectionCooldownLeft = 0F;

    // The state machine for the citizen.
    private StateMachine<Citizen> stateMachine;

    [System.NonSerialized]
    // The target resource is accessed in the State_GatherResource.
    public Resource targetResource;

    [System.NonSerialized]
    // The perceived resources are used for state checks.
    public List<Resource> perceivedResources;


    private void Start()
    {
        // Set the player color and player tag.
        SetPlayerStats();
        AddToPlayerList();

        // Set stuff up before the state machine, because it uses this.
        navMeshAgent = GetComponent<NavMeshAgent>();
        perceivedResources = new List<Resource>();
        perceivedObjectsInRange = new List<GameObject>();

        // Initialize the state machine. Do this after everything else.
        stateMachine = new StateMachine<Citizen>();
        stateMachine.Initialize(this, CitizenState_Idle.Instance);
    }

    private void Update()
    {
        // Calculate the damage cooldown.
        combatOffense.CalculateDamageCooldown();

        // Calculate the resource collection cooldown.
        CalculateResourceCollectionCooldown();

        // Update the state machine.
        stateMachine.Update();
    }

    /// <summary>
    /// Add the citizen to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.citizenList.Add(this);
    }


    /// <summary>
    /// Calculates the resource collection cooldown.
    /// </summary>
    private void CalculateResourceCollectionCooldown()
    {
        resourceCollectionCooldownLeft -= Time.deltaTime;

        if (resourceCollectionCooldownLeft < 0)
        {
            resourceCollectionCooldownLeft = 0;
        }
    }

    /// <summary>
    /// Changes the state the citizen is in. Only use this in the state maschine itself.
    /// </summary>
    /// <param name="state">The new state.</param>
    public void ChangeState(State<Citizen> state)
    {
        stateMachine.SetState(state);
    }

    /// <summary>
    /// The citizen starts collecting a resource.
    /// </summary>
    /// <param name="resource">The initial resource.</param>
    public void GatherResource(Resource resource)
    {
        targetResource = resource;
        stateMachine.SetState(CitizenState_GatherResource.Instance);
        group = SetWorkerGroup(resource);
    }

    /// <summary>
    /// The citizen deliveres the currently holding resources to the target building.
    /// </summary>
    /// <param name="building">The building that stores the resources of the citizen.</param>
    public void DeliverResources(Building building)
    {
        targetObject = building.gameObject;
        stateMachine.SetState(CitizenState_DeliverResource.Instance);
        group = WorkerGroup.Other;
    }

    /// <summary>
    /// Assign a WorkerGroup to the citizen depending on the resource.
    /// </summary>
    /// <param name="resource">The resource that is collected.</param>
    /// <returns></returns>
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

    /// <summary>
    /// The citizen tries to find a new resource fied of the same type to collect from. Only use this in the state maschine itself.
    /// </summary>
    /// <returns>true if the citizen found a suitable resource field.</returns>
    public bool SelectNewResource()
    {
        // Clear the perceived resources list from all null objects.
        perceivedResources.ForEach(x => { if (x == null) perceivedResources.Remove(x); });

        ResourceType rt = ResourceType.Food;
        switch (group)
        {
            case WorkerGroup.GatherFood: rt = ResourceType.Food; break;
            case WorkerGroup.GatherWood: rt = ResourceType.Wood; break;
            case WorkerGroup.GatherStone: rt = ResourceType.Stone; break;
            case WorkerGroup.GatherGold: rt = ResourceType.Gold; break;
        }

        List<Resource> resources = new List<Resource>();

        perceivedResources.ForEach(x => { if (x.resourceType == rt) resources.Add(x); });

        if (resources.Count > 0)
        {
            // Take the first object as reference for the shortest distance.
            targetResource = resources[0];
            float shortestDistance = Vector3.Distance(transform.position, targetResource.transform.position);

            // Start at 1 because we already got the object at 0.
            for (int i = 1; i < resources.Count; i++)
            {
                // If the object is closer, make it the target.
                float distance = Vector3.Distance(transform.position, resources[i].transform.position);
                if (distance < shortestDistance)
                {
                    targetResource = resources[i];
                    shortestDistance = distance;
                }
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// The citizen moves to a location, ignoring enemies.
    /// </summary>
    /// <param name="targetLocation">The target location.</param>
    public void MoveTo(Vector3 targetLocation)
    {
        navMeshAgent.SetDestination(targetLocation);
        stateMachine.SetState(CitizenState_Move.Instance);
        group = WorkerGroup.Other;
    }

    /// <summary>
    /// The citizen tries to attack an enemy object (Unit/Building).
    /// </summary>
    /// <param name="target">The enemy object (Unit/Building).</param>
    public void Attack(GameObject target)
    {
        targetObject = target;
        stateMachine.SetState(CitizenState_Attack.Instance);
        group = WorkerGroup.Fighter;
    }

    /// <summary>
    /// The citizen tries to build a building.
    /// </summary>
    /// <param name="building">The building that has to be build.</param>
    public void Build(GameObject building)
    {
        targetObject = building;
        stateMachine.SetState(CitizenState_Build.Instance);
        group = WorkerGroup.Builder;
    }

    /// <summary>
    /// Deal damage to an object (Unit/Building). Only use this in the state maschine itself.
    /// </summary>
    /// <param name="target">The target object (Unit/Building).</param>
    public void DealDamage(GameObject target)
    {
        // Fill the damage struct.
        DamageStruct damageStruct;
        damageStruct.normalDamage = combatOffense.normalDamage;
        damageStruct.pierceDamage = combatOffense.pierceDamage;
        damageStruct.siegeDamage = combatOffense.siegeDamage;

        Unit unit = null;
        Building building = null;

        if ((unit = targetObject.GetComponent<Unit>()) != null)
        {
            unit.TakeDamage(damageStruct);
        }
        else if ((building = targetObject.GetComponent<Building>()) != null)
        {
            building.TakeDamage(damageStruct);
        }

        // Reset the damage cooldown.
        combatOffense.damageCooldownLeft = combatOffense.damageCooldown;

        // Visualize attack.
        VisualizeAttack(target);
    }

    /// <summary>
    /// Draws a debug ray to the target (Symbolysing shooting).
    /// </summary>
    /// <param name="target">The gameObject that gets shot.</param>
    private void VisualizeAttack(GameObject target)
    {
        // The start point of the ray.
        Vector3 rayStartPoint = transform.position;
        rayStartPoint.y += 1.5F;

        // The end point of the ray.
        Vector3 targetPosition = target.transform.position;
        targetPosition.y += 1.5F;

        //Draw the ray. Cool stuff.
        Debug.DrawRay(rayStartPoint, targetPosition - rayStartPoint, Color.red, 0.15F);
    }
}
