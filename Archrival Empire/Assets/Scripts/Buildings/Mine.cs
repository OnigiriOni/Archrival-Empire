using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    private void Start()
    {
        SetPlayerStats();
        AddToPlayerList();
    }

    /// <summary>
    /// Add the mine to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.mineList.Add(this);
    }
}
