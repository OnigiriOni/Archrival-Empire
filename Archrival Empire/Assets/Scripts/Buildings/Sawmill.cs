using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : Building
{
    private void Start()
    {
        SetPlayerStats();
        AddToPlayerList();
    }

    /// <summary>
    /// Add the sawmill to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.sawmillList.Add(this);
    }
}
