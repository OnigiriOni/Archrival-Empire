using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierState_Chase : State<Soldier>
{
    static readonly SoldierState_Chase instance = new SoldierState_Chase();

    static SoldierState_Chase() { }
    public SoldierState_Chase() { }

    public static SoldierState_Chase Instance
    {
        get { return instance; }
    }


    public override void Enter(Soldier soldier)
    {

    }

    public override void Execute(Soldier soldier)
    {
        
    }

    public override void Exit(Soldier soldier)
    {

    }
}
