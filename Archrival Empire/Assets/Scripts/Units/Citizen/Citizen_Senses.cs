using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen_Senses : MonoBehaviour
{
    Citizen parent;

    private void Start()
    {
        parent = GetComponentInParent<Citizen>();
        GetComponent<SphereCollider>().radius = parent.perceptionRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        Resource resource = other.gameObject.GetComponent<Resource>();

        //Add resources to the perceived resources list.
        if (resource != null && !parent.perceivedResources.Contains(resource))
        {
            parent.perceivedResources.Add(resource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Resource resource = other.gameObject.GetComponent<Resource>();

        // Removes resources from the perceived resources list.
        if (resource != null && parent.perceivedResources.Contains(resource))
        {
            parent.perceivedResources.Remove(resource);
        }
    }
}
