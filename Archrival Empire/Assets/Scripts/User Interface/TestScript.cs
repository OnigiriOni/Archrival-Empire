using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  T E S T    S C R I P T
//////////////////////////////////////////////////////////////////////////////////
/*
public class TestScript : MonoBehaviour
{
    public GameObject selectedObject;

    public Citizen citizen;
    public Soldier soldier;

    public Capitol capitol;
    public Barrack barrack;

    public KeyCode capitolBuild;
    public KeyCode barrackBuild;
    public KeyCode citizenBuildMill;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(capitolBuild))
        {
            capitol.TrainCitizen();
        }
        if (Input.GetKeyDown(barrackBuild))
        {
            barrack.TrainSoldier();
        }

        if (Input.GetMouseButtonDown(0))
        {
            citizen = null;
            soldier = null;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200f, LayerMask.GetMask("SelectableObject")))
            {
                if (citizen = hit.collider.gameObject.GetComponent<Citizen>()) return;

                if (soldier = hit.collider.gameObject.GetComponent<Soldier>()) return;
            }
        }

        if (Input.GetKeyDown(citizenBuildMill))
        {
            if (citizen)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 200f, LayerMask.GetMask("Terrain")))
                {
                    BuildManager bm = GameObject.Find("Game").GetComponent<BuildManager>();
                    bm.Build(bm.mill,hit.point,citizen.player, citizen);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (citizen)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject = hit.collider.gameObject;

                    if (hit.collider.gameObject.GetComponent<Terrain>())
                    {
                        citizen.MoveTo(hit.point);
                        return;
                    }
                    if (hit.collider.gameObject.GetComponent<Resource>())
                    {
                        citizen.GatherResource(hit.collider.gameObject.GetComponent<Resource>());
                        return;
                    }
                    if (hit.collider.gameObject.GetComponent<PlayerObject>().playerTag != citizen.playerTag)
                    {
                        citizen.Attack(hit.collider.gameObject);
                        return;
                    }
                }
            }

            if (soldier)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    selectedObject = hit.collider.gameObject;

                    if (hit.collider.gameObject.GetComponent<Terrain>())
                    {
                        soldier.MoveTo(hit.point);
                        return;
                    }
                    if (hit.collider.gameObject.GetComponent<PlayerObject>().playerTag != soldier.playerTag)
                    {
                        soldier.Attack(hit.collider.gameObject);
                        return;
                    }
                }
            }
        }
	}
}
*/