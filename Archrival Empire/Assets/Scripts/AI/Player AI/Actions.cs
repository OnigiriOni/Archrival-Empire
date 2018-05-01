using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//////////////////////////////////////////////////////////////////////////////////
//////  C L A S S    N O T    I N    U S E
//////////////////////////////////////////////////////////////////////////////////

public class Actions : MonoBehaviour
{
    public object[] BuildCitizen(StrategyManager strategyManager, object[] data)
    {
        strategyManager.player.capitolList[0].TrainCitizen();

        return null;
    }

    public void BuildSoldier(StrategyManager strategyManager)
    {
        strategyManager.player.barrackList[0].TrainSoldier();
    }

    public void BuildCavalier(StrategyManager strategyManager)
    {
        strategyManager.player.stableList[0].TrainCavalier();
    }

    public void BuildArtillery(StrategyManager strategyManager)
    {
        strategyManager.player.foundryList[0].TrainArtillery();
    }



    //public void BuildCapitol(StrategyManager strategyManager)
    //{
    //    BuildManager bm = GameObject.Find("Game").GetComponent<BuildManager>();

    //    List<Citizen> citizen = new List<Citizen>();

    //    strategyManager.player.citizenList.ForEach(x =>
    //    {
    //        if (x.group == WorkerGroup.Idle)
    //        {
    //            citizen.Add(x);
    //            if (citizen.Count >= 2)
    //            {
    //                return;
    //            }
    //        }
    //    });

    //    bm.Build(bm.capitol, , strategyManager.player, citizen.ToArray());
    //}

    //public Vector3 GetBestPosition(Vector3 center, ConstructionSiteSize size)
    //{
    //    Vector3 bestPosition = center;

    //    float bSize = 0;
    //    if (size == ConstructionSiteSize.Size4x4) bSize = 4;
    //    if (size == ConstructionSiteSize.Size8x8) bSize = 8;

    //    for (int up = 0; up < 200; up++)
    //    {
    //        if (NavMesh.SamplePosition(bestPosition,) bestPosition)
    //        {

    //        }
    //    }
    //}
}
