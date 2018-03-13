using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //Pre-Game Options
    public int startCitizen = 5;
    //public Resources startResources = new Resources(200, 200, 0, 0);
    public int maxPopulation = 500;

    //In-Game Information
    //public Map map;
    public Player player1;
    public Player player2;

	void Start ()
    {
        //Player Setup
        //player1 = new Player(this, PlayerTag.Player1, Color.yellow, new China(), startResources);
        //player2 = new Player(this, PlayerTag.Player2, Color.green, new Germany(), startResources);
        //Game Setup
        //map = new Map();
        //AI Setup

        //UI Setup
        
    }
	
	void Update () {
		//Game Loop
	}
}