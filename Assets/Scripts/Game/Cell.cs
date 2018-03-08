using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
	public string name;
    public Resources resources;

    public Dictionary<Cell, float> connectedCells;
    public Dictionary<PlayerTag, Building> buildings;
    public Dictionary<PlayerTag, Unit> units;
    public List<Unit> units2;

    public Cell(string name, Resources startResources)
    {
        this.name = name;
        resources = startResources;
        connectedCells = new Dictionary<Cell, float>();
        buildings = new Dictionary<PlayerTag, Building>();
        units = new Dictionary<PlayerTag, Unit>();
        units2 = new List<Unit>();
    }
}