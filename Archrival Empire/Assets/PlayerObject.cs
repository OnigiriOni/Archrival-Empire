using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public NewInfluenceMap influenceMap;

    // The strength of the influence. 0 - 1.
    public float influence = 1F;

    // The range of the influence.
    public float influenceRange;

    /////////////////////////////// The type of the influence.
    public PlayerTag playerTag;
    public Player player;

    [System.NonSerialized]
    public int gridPosition_x;
    [System.NonSerialized]
    public int gridPosition_z;

    /// <summary>
    /// Sets the playerColor of the object and the playerTag.
    /// </summary>
    public void SetPlayerStats()
    {
        // Set the color of the building to the player color (Takes only the first Children and its first Material).
        GetComponentInChildren<MeshRenderer>().material.color = player.playerColor;
        // Set the PlayerTag to the players PlayerTag.
        playerTag = player.playerTag;

        //influenceMap.AddPlayerObject(this);
    }
}
