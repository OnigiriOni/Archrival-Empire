using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerObject : MonoBehaviour
{
    // A reference to the influence map to add this object to it on creation.
    public NewInfluenceMap influenceMap;

    // The position on the influence map.
    [System.NonSerialized]
    public int gridPosition_x;
    [System.NonSerialized]
    public int gridPosition_z;

    // The strength of the influence. 0 - 1.
    public float influence;
    public float influenceRange;

    // The player reference.
    public PlayerTag playerTag;
    public Player player;

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

    /// <summary>
    /// Adds the object to the influence map.
    /// </summary>
    protected abstract void AddToPlayerList();
}
