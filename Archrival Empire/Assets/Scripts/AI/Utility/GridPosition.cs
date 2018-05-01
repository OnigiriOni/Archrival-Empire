using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class GridPosition
{
    // The position on the influence map.
    public int mapPosition_x;
    public int mapPosition_z;

    // All neighbor grid positions.
    public List<KeyValuePair<GridPosition, float>> connections;

    // Influences from all Players.
    public List<KeyValuePair<Player, float>> influences;

    public GridPosition(int x, int z)
    {
        mapPosition_x = x;
        mapPosition_z = z;

        connections = new List<KeyValuePair<GridPosition, float>>();
        influences = new List<KeyValuePair<Player, float>>();
    }
}
