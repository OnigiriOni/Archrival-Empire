using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : Building
{
    private void Start()
    {
        SetPlayerStats();
        AddToPlayerList();
    }

    /// <summary>
    /// Add the mill to the player list for better access.
    /// </summary>
    protected override void AddToPlayerList()
    {
        player.millList.Add(this);
    }
}
