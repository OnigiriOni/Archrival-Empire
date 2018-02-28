using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Resources resources;

    public Resources Resources
    {
        get { return resources; }
    }

	void Start ()
    {
        resources = new Resources(100000);
	}
	
	void Update () {
		
	}
}
