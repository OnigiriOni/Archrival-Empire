using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition
{
    public int mapPosition_x;
    public int mapPosition_z;

    public List<KeyValuePair<GridPosition, float>> connections;
    public List<KeyValuePair<Player, float>> influences;

    public GridPosition(int x, int z)
    {
        mapPosition_x = x;
        mapPosition_z = z;

        connections = new List<KeyValuePair<GridPosition, float>>();
        influences = new List<KeyValuePair<Player, float>>();
    }
}
