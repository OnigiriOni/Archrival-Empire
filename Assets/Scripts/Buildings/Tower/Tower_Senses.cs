using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Senses : MonoBehaviour
{
    Tower parent;

    private void Start()
    {
        parent = GetComponentInParent<Tower>();
        GetComponent<SphereCollider>().radius = parent.combatOffense.attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();

        // If the object is a enemy unit, add it.
        if (unit != null && unit.playerTag != parent.playerTag)
        {
            parent.perceivedUnits.Add(unit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Unit unit = other.gameObject.GetComponent<Unit>();

        // If the object is a perceived unit, remove it.
        if (unit != null && parent.perceivedUnits.Contains(unit))
        {
            parent.perceivedUnits.Remove(unit);
        }
    }
}
