using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTag
{
    Gaia,
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6,
    Player7,
    Player8
}

public struct Buildings
{
    public struct CivilBuildings
    {
        public List<Capitol> capitols;
        public List<Mill> mills;
        public List<Sawmill> sawmills;
        public List<Mine> mines;
        public List<Goldmine> goldmines;
    }
    public CivilBuildings civilBuildings;

    public struct MilitaryBuildings
    {
        public List<Barrack> barracks;
        public List<Stable> stables;
        public List<Foundry> foundries;
        public List<Tower> tower;
    }
    public MilitaryBuildings militaryBuildings;
}

public struct Units
{
    public struct CivilUnits
    {
        public List<Citizen> citizens;
    }
    public CivilUnits civilBuildings;

    public struct MilitaryUnits
    {
        public List<Soldier> soldiers;
        public List<Cavalier> cavaliers;
        public List<Artillery> artilleries;
    }
    public MilitaryUnits militaryBuildings;
}

public class Player
{
    private Game game;
    public Civilization civilization;
    public Color playerColour;
    public PlayerTag playerTag;
    public Resources resources;

    public Buildings buildings;
    public Units units;

    public Player(Game game, PlayerTag playerTag, Color playerColour, Civilization civilization, Resources resources)
    {
        this.game = game;
        this.resources = resources;
        this.playerTag = playerTag;
        this.playerColour = playerColour;
        this.civilization = civilization;
    }
}
