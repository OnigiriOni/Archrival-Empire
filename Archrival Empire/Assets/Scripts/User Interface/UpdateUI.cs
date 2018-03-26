using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    //private Game game;

    // Game
    public Text maxPopulationValue;
    public Text startCitizenValue;

    public Text startFoodValue;
    public Text startWoodValue;
    public Text startStoneValue;
    public Text startGoldValue;


    // Player 1
    public Text civNamePlayer1;
    public Image colorValuePlayer1;

    public Text foodValuePlayer1;
    public Text woodValuePlayer1;
    public Text stoneValuePlayer1;
    public Text goldValuePlayer1;


    // Player 2
    public Text civNamePlayer2;
    public Image colorValuePlayer2;

    public Text foodValuePlayer2;
    public Text woodValuePlayer2;
    public Text stoneValuePlayer2;
    public Text goldValuePlayer2;

    void Start () {
        //game = GameObject.Find("Game").GetComponent<Game>();
    }

	void Update()
    {
        //UpdateGame();
        //UpdatePlayer1();
        //UpdatePlayer2();
	}

    /// <summary>
    /// Update the UI for the game options
    /// </summary>
    void UpdateGame()
    {
        //maxPopulationValue.text = game.maxPopulation.ToString();
        //startCitizenValue.text = game.startCitizen.ToString();

        //startFoodValue.text = game.startResources.food.ToString();
        //startWoodValue.text = game.startResources.wood.ToString();
        //startStoneValue.text = game.startResources.stone.ToString();
        //startGoldValue.text = game.startResources.gold.ToString();
    }

    /// <summary>
    /// Update the UI for Player 1
    /// </summary>
    void UpdatePlayer1()
    {
        //civNamePlayer1.text = "Artificial Intelligence - " + game.player1.playerTag + " (" + game.player1.civilization.name + ")";
        //colorValuePlayer1.color = game.player1.playerColour;

        //foodValuePlayer1.text = game.player1.resources.food.ToString();
        //woodValuePlayer1.text = game.player1.resources.wood.ToString();
        //stoneValuePlayer1.text = game.player1.resources.stone.ToString();
        //goldValuePlayer1.text = game.player1.resources.gold.ToString();
    }

    /// <summary>
    /// Update the UI for Player 2
    /// </summary>
    void UpdatePlayer2()
    {
        //civNamePlayer2.text = "Artificial Intelligence - " + game.player2.playerTag + " (" + game.player2.civilization.name + ")";
        //colorValuePlayer2.color = game.player2.playerColour;

        //foodValuePlayer2.text = game.player2.resources.food.ToString();
        //woodValuePlayer2.text = game.player2.resources.wood.ToString();
        //stoneValuePlayer2.text = game.player2.resources.stone.ToString();
        //goldValuePlayer2.text = game.player2.resources.gold.ToString();
    }
}