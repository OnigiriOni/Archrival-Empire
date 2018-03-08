using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public ResourceCost capitolCost;
    public ResourceCost millCost;
    public ResourceCost sawmillCost;
    public ResourceCost mineCost;
    public ResourceCost goldmineCost;
    public ResourceCost barrackCost;
    public ResourceCost stablelCost;
    public ResourceCost foundryCost;
    public ResourceCost towerCost;


    private void Start()
    {
        GetBuildCosts();
    }

    /// <summary>
    /// Get the build costs to compare them later
    /// </summary>
    public void GetBuildCosts()
    {
        capitolCost =   FindObjectOfType<Capitol>().buildCost;
        millCost =      FindObjectOfType<Mill>().BuildCost;
        sawmillCost =   FindObjectOfType<Sawmill>().buildCost;
        mineCost =      FindObjectOfType<Mine>().buildCost;
        goldmineCost =  FindObjectOfType<Goldmine>().buildCost;
        barrackCost =   FindObjectOfType<Barrack>().buildCost;
        foundryCost =   FindObjectOfType<Foundry>().buildCost;
        towerCost =     FindObjectOfType<Tower>().buildCost;
    }

    public bool Compare(Resources playerResources, ResourceCost cost)
    {
        if (playerResources.food >= cost.food
            && playerResources.wood >= cost.wood
            && playerResources.stone >= cost.stone
            && playerResources.gold >= cost.gold)
        {
            return true;
        }
        return false;
    }


    public void BuildCapitol(Player player, Cell cell)
    {
        
    }

    public void BuildMill()
    {
        Game game = FindObjectOfType<Game>();
        //wait as long as FindObjectOfType<Mill>().buildTime   (6 seconds)
        new Mill(game.player1, game.map.blue);
    }

    //public struct Capitol
    //{
    //    public static readonly int Food = 1000;
    //    public static readonly int Wood = 1000;
    //    public static readonly int Stone = 1000;
    //    public static readonly int Gold = 1000;

    //    public static bool CanBuild(Resources playerResources)
    //    {
    //        if(
    //            playerResources.food >= Food &&
    //            playerResources.wood >= Wood &&
    //            playerResources.stone >= Stone &&
    //            playerResources.gold >= Gold)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //}
    //public struct BuildCostMill
    //{
    //    public static readonly int Food = 0;
    //    public static readonly int Wood = 100;
    //    public static readonly int Stone = 0;
    //    public static readonly int Gold = 0;
    //}
    //public struct BuildCostSawmill
    //{
    //    public static readonly int Food = 0;
    //    public static readonly int Wood = 100;
    //    public static readonly int Stone = 0;
    //    public static readonly int Gold = 0;
    //}
    //public struct BuildCostMine
    //{
    //    public static readonly int Food = 0;
    //    public static readonly int Wood = 150;
    //    public static readonly int Stone = 0;
    //    public static readonly int Gold = 0;
    //}
    //public struct BuildCostGoldmine
    //{
    //    public static readonly int Food = 0;
    //    public static readonly int Wood = 150;
    //    public static readonly int Stone = 0;
    //    public static readonly int Gold = 0;
    //}
}
