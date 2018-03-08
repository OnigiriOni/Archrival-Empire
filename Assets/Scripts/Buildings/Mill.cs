using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : Building
{
    private new string name = "Mill";
    private int health = 100;
    private int buildTime = 6;
    private ResourceCost buildCost = new ResourceCost(0, 100, 0, 0);
    private Player player;
    private Cell cell;

    private List<Citizen> worker = new List<Citizen>();
    private int maxWorker = 6;
    private int currentWorker = 0;

    #region Properties

    public string Name
    {
        get { return name; }
    }

    public int Health
    {
        get { return health; }
    }

    public int BuildTime
    {
        get { return buildTime; }
    }

    public ResourceCost BuildCost
    {
        get { return buildCost; }
    }

    public Player Player
    {
        get { return player; }
    }

    public Cell CurrentCell
    {
        get { return currentCell; }
    }

    public int MaxWorker
    {
        get { return maxWorker; }
    }

    public int CurrentWorker
    {
        get { return currentWorker; }
    }

    #endregion

    public Mill(Player player, Cell cell)
    {
        this.player = player;
        this.cell = cell;
        worker = new List<Citizen>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    public void Destruct()
    {
        player.resources.food += Mathf.FloorToInt(buildCost.food * 25 / 100);
        player.resources.wood += Mathf.FloorToInt(buildCost.wood * 25 / 100);
        player.resources.stone += Mathf.FloorToInt(buildCost.stone * 25 / 100);
        player.resources.gold += Mathf.FloorToInt(buildCost.gold * 25 / 100);
        Destroy(this);
    }

    //public bool Construct(Player player, Cell cell)
    //{
    //    if (player.resources.food >= buildCost.food && player.resources.wood >= buildCost.wood && player.resources.stone >= buildCost.stone && player.resources.gold >= buildCost.gold)
    //    {
    //        this.player = player;
    //        this.cell = cell;
    //        return true;
    //    }
    //    return false;
    //}

    public bool AddWorker(Citizen citizen)
    {
        if (worker.Count < maxWorker)
        {
            worker.Add(citizen);
            currentWorker++;
            return true;
        }
        return false;
    }

    public bool RemoveWorker()
    {
        if (worker.Count >= 1)
        {
            worker.RemoveAt(worker.Count - 1);
            currentWorker--;
            return true;
        }
        return false;
    }

}
