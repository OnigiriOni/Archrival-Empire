using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Blue (metall / smooth mats): 0074FFFF
// Blue (normal mats):          0039FFFF

// Red  (all):                  00FF00FF

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

[System.Serializable]
public struct PlayerResource
{
    public int food;
    public int wood;
    public int stone;
    public int gold;
}

public class Player : MonoBehaviour
{
    public PlayerTag playerTag;
    public Color playerColor;
    public PlayerResource resources;

    //public Player(PlayerTag playerTag, Color playerColor, PlayerResource resources)
    //{
    //    this.playerTag = playerTag;
    //    this.playerColor = playerColor;
    //    this.resources = resources;
    //}
}
