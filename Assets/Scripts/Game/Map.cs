using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public Cell blue;
    public Cell top;
    public Cell mid;
    public Cell bot;
    public Cell red;

    private int resourcesPerCell = 10000;

    public Map()
    {
        CreateCells();
        CreateConnections();
    }

    private void CreateCells()
    {
        blue = new Cell("Player 1", new Resources(resourcesPerCell));

        top = new Cell("Top", new Resources(resourcesPerCell));
        mid = new Cell("Mid", new Resources(resourcesPerCell));
        bot = new Cell("Bot", new Resources(resourcesPerCell));

        red = new Cell("Player 2", new Resources(resourcesPerCell));
    }

    private void CreateConnections()
    {
        blue.connectedCells.Add(top, 15.0F);
        blue.connectedCells.Add(mid, 10.0F);
        blue.connectedCells.Add(bot, 15.0F);

        top.connectedCells.Add(blue, 15.0F);
        top.connectedCells.Add(mid, 5.0F);
        top.connectedCells.Add(red, 15.0F);

        mid.connectedCells.Add(blue, 10.0F);
        mid.connectedCells.Add(red, 10.0F);
        mid.connectedCells.Add(top, 5.0F);
        mid.connectedCells.Add(bot, 5.0F);

        bot.connectedCells.Add(blue, 15.0F);
        bot.connectedCells.Add(mid, 5.0F);
        bot.connectedCells.Add(red, 15.0F);

        red.connectedCells.Add(top, 15.0F);
        red.connectedCells.Add(mid, 10.0F);
        red.connectedCells.Add(bot, 15.0F);
    }
}