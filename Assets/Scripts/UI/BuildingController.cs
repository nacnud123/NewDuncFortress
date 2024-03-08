using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public enum Buildings
    {
        Stockpile = 0,
        Wall = 1,
        Bed = 2,
        Door = 3
    }

    public static BuildingController init;

    private void Awake()
    {
        init = this;
    }

    [Header("Temp Stuff")]
    public GameObject stockPile;
    public GameObject wall;
    public GameObject bed;
    public GameObject door;

    public Buildings currBuildings;

    public GameObject currentBuildingObj;


    public void changeBuilding(int inB)
    {
        switch (inB)
        {
            case (int)Buildings.Stockpile:
                currBuildings = Buildings.Stockpile;
                currentBuildingObj = stockPile;
                break;
            case (int)Buildings.Wall:
                currBuildings = Buildings.Wall;
                currentBuildingObj = wall;
                break;
            case (int)Buildings.Bed:
                currBuildings = Buildings.Bed;
                currentBuildingObj = bed;
                break;
            case (int)Buildings.Door:
                currBuildings = Buildings.Door;
                currentBuildingObj = door;
                break;
        }
    }

}
