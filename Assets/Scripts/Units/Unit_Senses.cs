using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Senses : MonoBehaviour
{
    Unit parent;

    private void Start()
    {
        parent = GetComponentInParent<Unit>();
        GetComponent<SphereCollider>().radius = parent.combatOffense.attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Add enemy units and buildings to the perceived objects list.
        Unit unit = other.gameObject.GetComponent<Unit>();
        Building building = other.gameObject.GetComponent<Building>();

        if (unit != null && unit.playerTag != parent.playerTag)
        {
            parent.perceivedObjectsInRange.Add(other.gameObject);
        }

        if (building != null && building.playerTag != parent.playerTag)
        {
            parent.perceivedObjectsInRange.Add(other.gameObject);
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
