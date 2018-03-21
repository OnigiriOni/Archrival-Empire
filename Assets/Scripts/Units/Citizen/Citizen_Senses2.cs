using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_Senses2 : MonoBehaviour
{
    Citizen parent;

    private void Start()
    {
        parent = GetComponentInParent<Citizen>();
        GetComponent<SphereCollider>().radius = parent.combatOffense.attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        Resource resource = other.gameObject.GetComponent<Resource>();
        Unit unit = other.gameObject.GetComponent<Unit>();
        Building building = other.gameObject.GetComponent<Building>();

        //Add resources to the perceived objects list.
        if (resource != null && !parent.perceivedObjectsInRange.Contains(resource.gameObject))
        {
            parent.perceivedObjectsInRange.Add(resource.gameObject);
        }

        //Add enemy units to the perceived objects list.
        if (unit != null && unit.playerTag != parent.playerTag && !parent.perceivedObjectsInRange.Contains(unit.gameObject))
        {
            parent.perceivedObjectsInRange.Add(unit.gameObject);
        }

        //Add enemy buildings to the perceived objects list.
        if (building != null && !parent.perceivedObjectsInRange.Contains(building.gameObject))
        {
            parent.perceivedObjectsInRange.Add(building.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Removes objects from the perceived objects list.
        if (parent.perceivedObjectsInRange.Contains(other.gameObject))
        {
            parent.perceivedObjectsInRange.Remove(other.gameObject);
        }
    }
}
