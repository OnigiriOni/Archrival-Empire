using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    Capitol,        // 0
    Warehouse,      // 1
    Marked,         // ...
    Factory
}

public class Building : MonoBehaviour
{
    public BuildingType type;

}
