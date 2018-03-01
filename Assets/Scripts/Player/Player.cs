using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTag
{
    Player1 = 1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6,
    Player7,
    Player8
}

public enum PlayerColour
{
    Blue,
    Red,
    Yellow,
    Pink,
    Green,
    Black,
    White,
    Orange
}

public enum Civilization
{
    Chinese,    //Citizen cost less food.
    German,     //Military cost less gold.
    Russian     //Buildings cost less wood.
}

public class Player : MonoBehaviour
{
    public PlayerTag playerTag;
    public PlayerColour playerColour;

    public Resources resources;
    

	void Start () {
        resources = new Resources(200,200,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
